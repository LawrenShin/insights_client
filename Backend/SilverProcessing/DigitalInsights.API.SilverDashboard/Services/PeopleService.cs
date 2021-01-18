using DigitalInsights.API.SilverDashboard.DTO;
using DigitalInsights.API.SilverDashboard.Helpers;
using DigitalInsights.DB.Silver;
using DigitalInsights.DB.Silver.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.Services
{
    public class PeopleService
    {
        SilverContext silverContext;

        public PeopleService(SilverContext context)
        {
            silverContext = context;
        }

        public PeopleDTO GetPeople(int pageSize, int pageIndex, string searchPrefix)
        {
            var peopleQuery = silverContext.People.AsQueryable<Person>();

            if (!string.IsNullOrEmpty(searchPrefix))
            {
                peopleQuery = peopleQuery.Where(x => x.Name.StartsWith(searchPrefix));
            }

            return new PeopleDTO(
                peopleQuery
                    .Include(x => x.PersonCountries)
                    .OrderBy(x => x.Name)
                    .Skip(pageIndex * pageSize).Take(pageSize)
                    .ToArray(),
                pageSize,
                pageIndex,
                (int)Math.Ceiling((double)silverContext.People.Count() / pageSize));
        }

        public void DeletePerson(int id)
        {
            var person = silverContext.People.Where(x => x.Id == id).FirstOrDefault();

            if (person == null)
            {
                throw new ArgumentException("id");
            }

            silverContext.People.Remove(person);
            silverContext.SaveChanges();
        }

        public void UpdateOrInsertPerson(PersonDTO source)
        {
            var countries = silverContext.Countries.Select(x=>x.Id).ToHashSet();

            Person targetPerson;

            if (!source.Id.HasValue)
            {
                targetPerson = new Person();
                silverContext.People.Add(targetPerson);
            }
            else
            {
                targetPerson = silverContext.People.Where(x => x.Id == source.Id)
                    .Include(x=>x.PersonCountries)
                    .FirstOrDefault();

                if (targetPerson == null)
                {
                    throw new ArgumentOutOfRangeException("person");
                }
            }

            targetPerson.Age = source.Age;
            targetPerson.BirthYear = source.BirthYear;
            targetPerson.EducationInstitute = source.EducationInstitute;
            targetPerson.EducationLevel = EnumHelper.ParseInputEnumValue<DB.Common.Enums.EducationLevel>(source.EducationLevel);
            targetPerson.EducationSubject = EnumHelper.ParseInputEnumValue<DB.Common.Enums.EducationSubject>(source.EducationSubject);
            targetPerson.Gender = EnumHelper.ParseInputEnumValue<DB.Common.Enums.Gender>(source.Gender);
            targetPerson.HasKids = source.HasKids;
            targetPerson.Married = EnumHelper.ParseInputEnumValue<DB.Common.Enums.MaritalStatus>(source.MaritalStatus);
            targetPerson.Name = source.Name;
            targetPerson.Picture = source.Picture;
            targetPerson.Religion = EnumHelper.ParseInputEnumValue<DB.Common.Enums.Religion>(source.Religion);
            targetPerson.Sexuality = source.Sexuality;
            targetPerson.Urban = source.Urban;
            targetPerson.VisibleDisability = source.VisibleDisability;
            targetPerson.Website = source.Website;

            // countries

            if (source.Countries.Any(x => !countries.Contains(x)))
            {
                throw new ArgumentException("Country not found");
            }

            var srcIds = source.Countries.ToHashSet();

            var toRemove = new List<PersonCountry>();
            foreach (var personCountry in targetPerson.PersonCountries)
            {
                if (!srcIds.Contains(personCountry.PersonCountryId))
                {
                    toRemove.Add(personCountry);
                }
            }

            foreach (var item in toRemove)
            {
                targetPerson.PersonCountries.Remove(item);
                silverContext.Remove(item);
            }

            foreach (var countryId in source.Countries)
            {
                if(!targetPerson.PersonCountries.Any(x=>x.CountryId == countryId))
                {
                    PersonCountry targetEntity = new PersonCountry()
                    {
                        Person = targetPerson,
                        CountryId = countryId
                    };

                    targetPerson.PersonCountries.Add(targetEntity);
                    silverContext.PersonCountries.Add(targetEntity);
                }
            }

            silverContext.SaveChanges();
        }
    }
}
