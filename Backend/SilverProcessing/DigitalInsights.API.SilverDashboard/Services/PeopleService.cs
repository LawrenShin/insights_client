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
    class PeopleService
    {
        SilverContext silverContext;

        public PeopleService(SilverContext context)
        {
            silverContext = context;
        }

        public Person[] GetPeople(int pageSize, int pageIndex, string searchPrefix)
        {
            var peopleQuery = silverContext.People.AsQueryable<Person>();

            if (!string.IsNullOrEmpty(searchPrefix))
            {
                peopleQuery = peopleQuery.Where(x => x.Name.StartsWith(searchPrefix));
            }

            return peopleQuery
                .Include(x => x.PersonCountries)
                .OrderBy(x => x.Name).Skip(pageIndex * pageSize).Take(pageSize).ToArray();
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

        internal void UpdateOrInsertPerson(Person source)
        {
            Person targetPerson;

            if (!source.Id.HasValue || source.Id == 0)
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
            targetPerson.BaseSalary = source.BaseSalary;
            targetPerson.BirthYear = source.BirthYear;
            targetPerson.Disability = source.Disability;
            targetPerson.EduInstitute = source.EduInstitute;
            targetPerson.EduSubject = source.EduSubject;
            targetPerson.Gender = source.Gender;
            targetPerson.HighEdu = source.HighEdu;
            targetPerson.Married = source.Married;
            targetPerson.Name = source.Name;
            targetPerson.OtherIncentive = source.OtherIncentive;
            targetPerson.Picture = source.Picture;
            targetPerson.Race = source.Race;
            targetPerson.Religion = source.Religion;
            targetPerson.Sexuality = source.Sexuality;
            targetPerson.Urban = source.Urban;

            // countries

            var srcIds = source.PersonCountries
                .Where(x => x.PersonCountryId.HasValue)
                .Select(x => x.PersonCountryId.Value)
                .ToHashSet();

            var toRemove = new List<PersonCountry>();
            foreach (var personCountry in targetPerson.PersonCountries)
            {
                if (!srcIds.Contains(personCountry.PersonCountryId.Value))
                {
                    toRemove.Add(personCountry);
                }
            }
            foreach (var item in toRemove)
            {
                targetPerson.PersonCountries.Remove(item);
                silverContext.Remove(item);
            }

            foreach (var personCountry in source.PersonCountries)
            {
                PersonCountry targetEntity;
                if (!personCountry.PersonCountryId.HasValue)
                {
                    targetEntity = new PersonCountry()
                    {
                        Person = targetPerson,
                    };

                    targetPerson.PersonCountries.Add(targetEntity);
                    silverContext.PersonCountries.Add(targetEntity);
                }
                else
                {
                    targetEntity = targetPerson.PersonCountries.First(x => x.PersonCountryId == personCountry.PersonCountryId);
                }
                targetEntity.CountryId = personCountry.CountryId;
            }

            silverContext.SaveChanges();
        }
    }
}
