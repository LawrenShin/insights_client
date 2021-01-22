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
                    .Include(x => x.PersonNationalities).ThenInclude(x=>x.Country)
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

            var groups = silverContext.PropertyMetadata.GroupBy(x => x.EntityName).ToDictionary(x => x.Key, x => x);

            Person targetPerson;

            if (!source.Id.HasValue)
            {
                targetPerson = new Person();
                silverContext.People.Add(targetPerson);
            }
            else
            {
                targetPerson = silverContext.People.Where(x => x.Id == source.Id)
                    .Include(x=>x.PersonNationalities)
                    .FirstOrDefault();

                if (targetPerson == null)
                {
                    throw new ArgumentOutOfRangeException("person");
                }
            }

            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("Age") 
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["Age"].IsEditable)
                targetPerson.Age = source.Age;
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("EducationInstitute")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["EducationInstitute"].IsEditable)
                targetPerson.EducationInstitute = source.EducationInstitute;
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("EducationSubject")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["EducationSubject"].IsEditable)
                targetPerson.EducationSubject = EnumHelper.ParseInputEnumValue<DB.Common.Enums.EducationSubject>(source.EducationSubject);
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("Gender") 
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["Gender"].IsEditable)
                targetPerson.Gender = EnumHelper.ParseInputEnumValue<DB.Common.Enums.Gender>(source.Gender);
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("HighEducation")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["HighEducation"].IsEditable)
                targetPerson.HighEducation = EnumHelper.ParseInputEnumValue<DB.Common.Enums.EducationLevel>(source.HighEducation);
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("Kids")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["Kids"].IsEditable)
                targetPerson.Kids = source.Kids;
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("Married")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["Married"].IsEditable)
                targetPerson.Married = source.Married;
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("Name")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["Name"].IsEditable)
                targetPerson.Name = source.Name;
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("Race")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["Race"].IsEditable)
                targetPerson.Race = EnumHelper.ParseInputEnumValue<DB.Common.Enums.Race>(source.Race);
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("Religion")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["Religion"].IsEditable)
                targetPerson.Religion = EnumHelper.ParseInputEnumValue<DB.Common.Enums.Religion>(source.Religion);
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("Sexuality")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["Sexuality"].IsEditable)
                targetPerson.Sexuality = source.Sexuality;
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("Urban")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["Urban"].IsEditable)
                targetPerson.Urban = source.Urban;
            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("VisibleDisability")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["VisibleDisability"].IsEditable)
                targetPerson.VisibleDisability = source.VisibleDisability;

            // countries

            if (PropertyMetadataStorage.CurrentPropertyMetadata["person"].ContainsKey("PersonNationalities")
                && PropertyMetadataStorage.CurrentPropertyMetadata["person"]["PersonNationalities"].IsEditable)
            {

                if (source.PersonNationalities.Any(x => !countries.Contains(x.Country)))
                {
                    throw new ArgumentException("Country not found");
                }

                var srcIds = source.PersonNationalities.Select(x => x.Country).ToHashSet();

                var toRemove = new List<PersonNationality>();
                foreach (var personCountry in targetPerson.PersonNationalities)
                {
                    if (!srcIds.Contains(personCountry.PersonNationalityId))
                    {
                        toRemove.Add(personCountry);
                    }
                }

                foreach (var item in toRemove)
                {
                    targetPerson.PersonNationalities.Remove(item);
                    silverContext.Remove(item);
                }

                foreach (var country in source.PersonNationalities)
                {
                    if (!targetPerson.PersonNationalities.Any(x => x.CountryId == country.Country))
                    {
                        PersonNationality targetEntity = new PersonNationality()
                        {
                            Person = targetPerson,
                            CountryId = country.Country
                        };

                        targetPerson.PersonNationalities.Add(targetEntity);
                        silverContext.PersonNationalities.Add(targetEntity);
                    }
                }
            }

            silverContext.SaveChanges();
        }
    }
}
