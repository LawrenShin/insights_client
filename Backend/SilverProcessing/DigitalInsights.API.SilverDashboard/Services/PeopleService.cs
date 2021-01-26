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

            var properties = PropertyMetadataStorage.CurrentPropertyMetadata["person"];
            foreach (var property in properties.Values)
            {
                switch(property.PropertyName.ToLowerInvariant())
                {
                    case "age":
                        ValidationHelper.ValidateAndSetProperty<int?>(property, () => source.Age, x => targetPerson.Age = (short?)x);
                        break;
                    case "educationinstitute":
                        ValidationHelper.ValidateAndSetProperty(property, () => source.EducationInstitute, x => targetPerson.EducationInstitute = x);
                        break;
                    case "educationsubject":
                        ValidationHelper.ValidateAndSetProperty(
                            property,
                            () => source.EducationSubject,
                            x => targetPerson.EducationSubject = (DB.Common.Enums.EducationSubject?)x);
                        break;
                    case "higheducation":
                        ValidationHelper.ValidateAndSetProperty(
                            property,
                            () => source.HighEducation,
                            x => targetPerson.HighEducation = (DB.Common.Enums.EducationLevel?)x);
                        break;
                    case "kids":
                        ValidationHelper.ValidateAndSetProperty(property, () => source.Kids, x => targetPerson.Kids = x);
                        break;
                    case "name":
                        ValidationHelper.ValidateAndSetProperty(property, () => source.Name, x => targetPerson.Name = x);
                        break;
                    case "married":
                        ValidationHelper.ValidateAndSetProperty(property, () => source.Married, x => targetPerson.Married = x);
                        break;
                    case "race":
                        ValidationHelper.ValidateAndSetProperty(property, () => source.Race, x => targetPerson.Race = (DB.Common.Enums.Race?)x);
                        break;
                    case "religion":
                        ValidationHelper.ValidateAndSetProperty(property, () => source.Religion, x => targetPerson.Religion = (DB.Common.Enums.Religion?)x);
                        break;
                    case "sexuality":
                        ValidationHelper.ValidateAndSetProperty(property, () => source.Sexuality, x => targetPerson.Sexuality = x);
                        break;
                    case "urban":
                        ValidationHelper.ValidateAndSetProperty(property, () => source.Urban, x => targetPerson.Urban = x);
                        break;
                    case "visibledisability":
                        ValidationHelper.ValidateAndSetProperty(property, () => source.VisibleDisability, x => targetPerson.VisibleDisability = x);
                        break;
                    case "nationalities":
                        if (!property.IsEditable) break;
                        if (source.Nationalities.Any(x => !countries.Contains(x.Country)))
                        {
                            throw new ArgumentException("Country not found");
                        }

                        var srcIds = source.Nationalities.Select(x => x.Country).ToHashSet();

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

                        foreach (var country in source.Nationalities)
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
                        break;
                    default:
                        throw new NotSupportedException($"{property.EntityName} {property.PropertyName}");
                }
            }

            silverContext.SaveChanges();
        }
    }
}
