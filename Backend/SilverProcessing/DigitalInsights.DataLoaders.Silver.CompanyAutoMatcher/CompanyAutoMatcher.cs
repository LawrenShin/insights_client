using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using CsvHelper;
using DigitalInsights.Common.Logging;
using DigitalInsights.DataLoaders.Silver.CompanyAutoMatcher.Helpers;
using DigitalInsights.DB.Silver;
using DigitalInsights.DB.Silver.Entities;
using Microsoft.EntityFrameworkCore;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DigitalInsights.DataLoaders.Silver.CompanyAutoMatcher
{
    public class CompanyAutoMatcher
    {
        const string TRANSFORMER_NAME = "CompanyAutoMatcher";

        private List<Tuple<string, string>> legalNames = new List<Tuple<string, string>>();
        private Dictionary<string, int> legalNameToId = new Dictionary<string, int>();

        private List<Tuple<string, string>> akaNames = new List<Tuple<string, string>>();
        private Dictionary<string, int> akaNameToId = new Dictionary<string, int>();

        private List<Tuple<string, string>> transliteratedNames = new List<Tuple<string, string>>();
        private Dictionary<string, int> transliteratedNameToId = new Dictionary<string, int>();

        private List<Country> countries;

        /// <summary>
        /// Automatcher between given company names and companies
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(ILambdaContext context)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Logger.Init(TRANSFORMER_NAME, "{2}");
                Logger.Log("Started");
                // todo: switch to S3
                var filename = "C:\\temp\\Companies.csv";

                Logger.Log("Configuring DB context.");

                /*var factory = LoggerFactory.Create(builder =>
                {
                    builder.AddProvider(new TraceLoggerProvider());
                });*/
                var dbContext = new SilverContext(/*factory*/);

                dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                dbContext.ChangeTracker.LazyLoadingEnabled = true;

                dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE company_match");

                countries = dbContext.Countries.AsNoTracking().ToList();

                var names = dbContext.CompanyNames.AsNoTracking()
                    .Include(x => x.Company)
                    .Select(x => new { x.Name, x.Type, x.Id, x.Company.LegalJurisdiction }).ToList(); // note that ID is internal to our DB, it's different from LEI

                Action<string, string, int, List<Tuple<string, string>>, Dictionary<string, int>> add = (name, code, id, namesList, namesDictionary) =>
                {
                    var standard = Standardize(name);
                    if (!string.IsNullOrEmpty(standard) && standard.Length > 1)
                    {
                        if (!namesDictionary.ContainsKey(standard))
                        {
                            namesList.Add(Tuple.Create(standard, code.Split('-')[0])); // the split is to handle codes like "US-WI", "CA-ON".
                            namesDictionary[standard] = id;
                        }
                        else
                        {
                            Logger.Log("Found duplicate entity name: " + name);
                        }

                    }
                };

                foreach (var n in names)
                {
                    if (n.Type == "TRADING_OR_OPERATING_NAME")
                    {
                        add(n.Name, n.LegalJurisdiction, n.Id, akaNames, akaNameToId);
                    }
                    else if (n.Type == "AUTO_ASCII_TRANSLITERATED_LEGAL_NAME" ||
                        n.Type == "PREFERRED_ASCII_TRANSLITERATED_LEGAL_NAME" ||
                        n.Type == "ALTERNATIVE_LANGUAGE_LEGAL_NAME")
                    {
                        add(n.Name, n.LegalJurisdiction, n.Id, transliteratedNames, transliteratedNameToId);
                    }
                }

                var companies = dbContext.Companies.AsNoTracking().Select(x => new { x.LegalName, x.LegalJurisdiction, x.Id }).ToList();

                foreach (var n in companies)
                {
                    add(n.LegalName, n.LegalJurisdiction, n.Id, legalNames, legalNameToId);
                }

                // The following code is used to dump all relevant DB data except countries - it saves time to reload ready data structures from hard drive

                //BinaryFormatter bf = new BinaryFormatter();

                //using (var fs = new FileStream("C:\\temp\\legal_names.txt", FileMode.Create, FileAccess.Write))
                //{
                //    bf.Serialize(fs, legalNames);
                //}

                //using (var fs = new FileStream("C:\\temp\\legal_names.txt", FileMode.Open, FileAccess.Read))
                //{
                //    legalNames = (List<Tuple<string, string>>)bf.Deserialize(fs);
                //}

                //using (var fs = new FileStream("C:\\temp\\legal_dict.txt", FileMode.Create, FileAccess.Write))
                //{
                //    bf.Serialize(fs, legalNameToId);
                //}

                //using (var fs = new FileStream("C:\\temp\\legal_dict.txt", FileMode.Open, FileAccess.Read))
                //{
                //    legalNameToId = (Dictionary<string, int>)bf.Deserialize(fs);
                //}

                //using (var fs = new FileStream("C:\\temp\\transliterated_names.txt", FileMode.Create, FileAccess.Write))
                //{
                //    bf.Serialize(fs, transliteratedNames);
                //}

                //using (var fs = new FileStream("C:\\temp\\transliterated_names.txt", FileMode.Open, FileAccess.Read))
                //{
                //    transliteratedNames = (List<Tuple<string, string>>)bf.Deserialize(fs);
                //}

                //using (var fs = new FileStream("C:\\temp\\transliterated_dict.txt", FileMode.Create, FileAccess.Write))
                //{
                //    bf.Serialize(fs, transliteratedNameToId);
                //}

                //using (var fs = new FileStream("C:\\temp\\transliterated_dict.txt", FileMode.Open, FileAccess.Read))
                //{
                //    transliteratedNameToId = (Dictionary<string, int>)bf.Deserialize(fs);
                //}

                //using (var fs = new FileStream("C:\\temp\\other_names.txt", FileMode.Create, FileAccess.Write))
                //{
                //    bf.Serialize(fs, akaNames);
                //}

                //using (var fs = new FileStream("C:\\temp\\other_names.txt", FileMode.Open, FileAccess.Read))
                //{
                //    akaNames = (List<Tuple<string, string>>)bf.Deserialize(fs);
                //}

                //using (var fs = new FileStream("C:\\temp\\other_dict.txt", FileMode.Create, FileAccess.Write))
                //{
                //    bf.Serialize(fs, akaNameToId);
                //}

                //using (var fs = new FileStream("C:\\temp\\other_dict.txt", FileMode.Open, FileAccess.Read))
                //{
                //    akaNameToId = (Dictionary<string, int>)bf.Deserialize(fs);
                //}


                using (var fileReader = new StreamReader(filename))
                {
                    Logger.Log("Configuring CSV reader");
                    var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.MissingFieldFound = null;

                    string prevCompanyName = "";
                    Company targetCompany = null;
                    Person previousPerson = null;
                    csvReader.Read();
                    while (csvReader.Read())
                    {
                        var companyName = csvReader.GetField(0); // company name

                        if (companyName != prevCompanyName)
                        {
                            prevCompanyName = companyName;

                            var standName = Standardize(companyName);

                            var countryName = csvReader.GetField(4); // company HQ
                            if (countryName == "Russia") countryName = "Russian Federation";
                            var countryCode = countries.Where(x => x.Name == countryName).Select(x => x.Code).FirstOrDefault();

                            if (countryCode == null)
                            {
                                throw new InvalidOperationException($"No country is found for name \"{countryName}\"");
                            }

                            var companyIds = MatchCompanyName(companyName, countryCode);

                            if (companyIds.Count > 0)
                            {
                                foreach (var id in companyIds)
                                {
                                    dbContext.CompanyMatches.Add(
                                    new CompanyMatch()
                                    {
                                        CompanyId = id,
                                        Name = companyName
                                    });
                                }
                            }
                            else
                            {
                                dbContext.CompanyMatches.Add(
                                    new CompanyMatch()
                                    {
                                        CompanyId = null,
                                        Name = companyName
                                    });
                            }
                        }

                    }
                }

                dbContext.SaveChanges();

                dbContext.Dispose();

                sw.Stop();
                Logger.Log($"Finished successfully in {sw.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Logger.Log($"ERROR: Unhandled exception: {ex.ToString()}");
            }
        }

        private static string Standardize(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            string result = name.ToUpperInvariant()
                .Replace(".", ". ")
                .Replace(".", "")
                .Replace("  ", " ")
                .Replace(",", "")
                .Replace("\"", "")
                .Replace(" & ", " AND ")
                .Replace(" PLC", " PUBLIC LIMITED COMPANY")
                .Replace(" COMPANY", " CO")
                .Replace(" CORPORATION", " CORP")
                .Replace(" INCORPORATED", " INC")
                .Replace(" LIMITED", " LTD")
                .Trim();

            if (result.Length >= 4 && result.Substring(0, 3) == "THE")
                return result.Substring(4).Trim();

            return result.Trim();
        }

        private List<int> MatchCompanyName(string companyName, string countryCode)
        {
            var target = Standardize(companyName);

            int maxLegal = 0;
            var minLegalNames = FindSimilarNames(target, countryCode, legalNames, out maxLegal);

            int maxAka = 0;
            var minAkaNames = FindSimilarNames(target, countryCode, akaNames, out maxAka);

            int maxTrans = 0;
            var minTransliteratedNames = FindSimilarNames(target, countryCode, transliteratedNames, out maxTrans);

            int max = Math.Max(Math.Max(maxLegal, maxAka), maxTrans);

            if (max == maxLegal)
            {
                if (minLegalNames.Count == 1)
                {
                    Logger.Log($"Matched {companyName} ({countryCode}) to legal name: {minLegalNames[0]}");
                    return new List<int>() { legalNameToId[minLegalNames[0]] };
                }
                else if (minLegalNames.Count > 1)
                {
                    Logger.Log($"Too many matches found for {companyName} ({countryCode}): {string.Join(", ", minLegalNames)}");
                    return minLegalNames.Select(x => legalNameToId[x]).ToList();
                }
            }

            if (max == maxAka)
            {
                if (minAkaNames.Count == 1)
                {
                    Logger.Log($"Matched {companyName} ({countryCode}) to AKA name: {minAkaNames[0]}");
                    return new List<int>() { akaNameToId[minAkaNames[0]] };
                }
                else if (minAkaNames.Count > 1)
                {
                    Logger.Log($"Too many matches found for {companyName} ({countryCode}): {string.Join(", ", minAkaNames)}");
                    return minAkaNames.Select(x => akaNameToId[x]).ToList();
                }
            }

            if (max == maxTrans)
            {
                if (minTransliteratedNames.Count == 1)
                {
                    Logger.Log($"Matched {companyName} ({countryCode}) to transliterated name: {minTransliteratedNames[0]}");
                    return new List<int>() { transliteratedNameToId[minTransliteratedNames[0]] };
                }
                else if (minTransliteratedNames.Count > 1)
                {
                    Logger.Log($"Too many matches found for {companyName} ({countryCode}): {string.Join(", ", minTransliteratedNames)}");
                    return minTransliteratedNames.Select(x => transliteratedNameToId[x]).ToList();
                }
            }

            Logger.Log($"No match found for {companyName} ({countryCode})");
            return new List<int>();
        }

        private List<string> FindSimilarNames(string target, string code, List<Tuple<string, string>> sources, out int similarWordsCount)
        {
            List<Tuple<string, int>> minNames = new List<Tuple<string, int>>();
            var maxWordCount = 1;

            var targetWords = target.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var tuple in sources)
            {
                var source = tuple.Item1;

                if (source[0] != target[0] || !string.IsNullOrEmpty(tuple.Item2) && tuple.Item2.ToUpperInvariant() != code.ToUpperInvariant()) continue;
                if (source == target)
                {
                    minNames.Clear();
                    minNames.Add(Tuple.Create(source, 0));
                    break;
                }
                var prefix = StringAlgorithmHelper.CommonPrefix(source, target);
                var prefixWords = prefix.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var prefixWordCount = prefixWords.Length;

                if (prefix.EndsWith(' '))
                {
                    //partial match, but words are fully matched
                }
                else
                {
                    // partial match with the end at the middle of the word
                    var sourceWords = source.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (sourceWords[prefixWordCount - 1] != targetWords[prefixWordCount - 1])
                    {
                        prefixWordCount--;
                    } // else full match
                }

                if (prefixWordCount > maxWordCount)
                {
                    minNames.Clear();
                    maxWordCount = prefixWordCount;
                }

                if (prefixWordCount == maxWordCount)
                {
                    var distance = StringAlgorithmHelper.DamerauLevenshteinDistance(source, target, 10);
                    if (distance <= 10)
                    {
                        minNames.Add(Tuple.Create(source, distance));
                    }
                }
            }

            similarWordsCount = maxWordCount;

            return minNames.OrderBy(x => x.Item2).Select(x => x.Item1).ToList();
        }
    }
}
