using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using DigitalInsights.DB.Silver;
using Amazon.Lambda.Serialization.SystemTextJson;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using DigitalInsights.DB.Silver.Entities;
using Microsoft.EntityFrameworkCore;
using DigitalInsights.DB.Silver.Enums;
using CompanyQuestion = DigitalInsights.DB.Silver.Entities.CompanyQuestion;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DigitalInsights.API.SilverDashboard
{
    public class SilverDashboardAPI
    {
        // Get companies query string parameters
        const string PAGE_SIZE = "page_size";
        const string PAGE_INDEX = "page_index";
        const string SEARCH_PREFIX = "search_prefix";

        // Delete company query string parameters
        const string ID = "id";

        const string AUTH_HEADER = "x-api-token";

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public SilverDashboardAPI()
        {
        }

        //public APIGatewayCustomAuthorizerResponse Authorize(APIGatewayCustomAuthorizerRequest request, ILambdaContext context)
        //{
        //    var defaultAnswer =
        //        new APIGatewayCustomAuthorizerResponse()
        //        {
        //            PolicyDocument = new APIGatewayCustomAuthorizerPolicy
        //            {
        //                Version = "2012-10-17",
        //                Statement = new List<APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>() {
        //                    new APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement()
        //                    {
        //                        Action = new HashSet<string>(){"execute-api:Invoke"},
        //                        Effect = "Deny",
        //                        Resource = new HashSet<string>(){  request.MethodArn }
        //                    }
        //                }
        //            }
        //        };

        //    try
        //    {
        //        context.Logger.LogLine("Get Request\n");

        //        if (request.Headers != null && request.Headers.ContainsKey(AUTH_HEADER))
        //        {
        //            var result = JWTHelper.ValidateToken(request.Headers[AUTH_HEADER]);

        //            defaultAnswer.PolicyDocument.Statement.First().Effect = result ? "Allow" : "Deny";
        //        }
        //    }
        //    catch
        //    {

        //    }
        //    return defaultAnswer;
        //}

        private bool ValidateRequest(APIGatewayProxyRequest request)
        {
            if (request.Headers != null && request.Headers.ContainsKey(AUTH_HEADER))
            {
                return JWTHelper.ValidateToken(request.Headers[AUTH_HEADER]);
            }
            return false;
        }

        public APIGatewayProxyResponse Login(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                var authInfo = JsonConvert.DeserializeObject<AuthInfo>(request.Body);

                if(authInfo == null)
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,

                        Body = $"Bad formatting of the request body: {request.Body}.\n Consider {JsonConvert.SerializeObject(new AuthInfo() { UserName = "Sample", Password = "Sample" })}",
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }

                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,

                    Body = JWTHelper.CreateToken(authInfo),
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };

                return response;

            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = $"Bad query: {ex}.\n Consider {JsonConvert.SerializeObject(new AuthInfo() { UserName = "Sample", Password = "Sample" })}",
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
        }

        public APIGatewayProxyResponse GetRoleTypes(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden,
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }

                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,

                    Body = JsonConvert.SerializeObject(Enum.GetValues<RoleType>().Select(x => new { Id = (int)x, Name = x.ToString() }).ToArray()),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };

                return response;

            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = $"Bad query: {ex}",
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
        }

        public APIGatewayProxyResponse GetCountries(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden,
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }
                SilverContext silverContext = new SilverContext();
                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(silverContext.Countries.ToList()),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };

                return response;

            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = $"Bad query: {ex}",
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
        }

        public APIGatewayProxyResponse GetIndustries(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden,
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }
                SilverContext silverContext = new SilverContext();
                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(silverContext.Industries.ToList()),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };

                return response;

            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = $"Bad query: {ex}",
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
        }


        /// <summary>
        /// A Lambda function to get a list of companies to HTTP Get methods from API Gateway
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The API Gateway response.</returns>
        public APIGatewayProxyResponse GetCompanies(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden,
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }

                int pageSize;
                int pageIndex;

                if (request.QueryStringParameters == null)
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Body = "Expected query string parameters",
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }

                if (!int.TryParse(request.QueryStringParameters[PAGE_SIZE], out pageSize) ||
                    !int.TryParse(request.QueryStringParameters[PAGE_INDEX], out pageIndex))
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Body = "Wrong number format",
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }
                SilverContext silverContext = new SilverContext();
                var companiesQuery = silverContext.Companies.AsQueryable<Company>();

                string prefix = null;
                if (request.QueryStringParameters.ContainsKey(SEARCH_PREFIX) &&
                    !string.IsNullOrEmpty(prefix = request.QueryStringParameters[SEARCH_PREFIX]))
                {
                    companiesQuery = companiesQuery.Where(x => x.LegalName.StartsWith(prefix));
                }

                var companies = companiesQuery
                    .Include(x => x.Roles).ThenInclude(x => x.Person)
                    .Include(x => x.CompanyNames)
                    .Include(x => x.CompanyExtendedData)
                    .Include(x => x.CompanyQuestionnaires)
                    .OrderBy(x => x.LegalName).Skip(pageIndex * pageSize).Take(pageSize).ToArray();

                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(companies),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };

                return response;

            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = $"Bad query: {ex}",
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
        }

        public APIGatewayProxyResponse DeleteCompany(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden,
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }

                int id;

                if (request.QueryStringParameters == null ||
                    !request.QueryStringParameters.ContainsKey(ID))
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Body = "Expected query string parameters",
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }

                if (!int.TryParse(request.QueryStringParameters[ID], out id))
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Body = "Wrong number format",
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }
                SilverContext silverContext = new SilverContext();
                var company = silverContext.Companies.Where(x => x.Id == id).FirstOrDefault();

                if(company == null)
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Body = "Item is not found",
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }

                silverContext.Companies.Remove(company);
                silverContext.SaveChanges();

                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };

                return response;

            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = $"Bad query: {ex}",
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
        }

        public APIGatewayProxyResponse SaveCompany(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden,
                        Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                    };
                }

                if (string.IsNullOrEmpty(request.Body))
                {
                    throw new ArgumentNullException("request.Body");
                }

                Company source = JsonConvert.DeserializeObject<Company>(request.Body);

                Company targetCompany;
                SilverContext silverContext = new SilverContext();
                if (!source.Id.HasValue || source.Id == 0)
                {
                    targetCompany = new Company();
                    silverContext.Companies.Add(targetCompany);
                }
                else
                {
                    targetCompany = silverContext.Companies.Where(x => x.Id == source.Id)
                        .Include(x => x.CompanyCountries)
                        .Include(x=>x.CompanyExtendedData)
                        .Include(x=>x.CompanyQuestionnaires)
                        .Include(x=>x.CompanyIndustries)
                        .Include(x=>x.CompanyNames)
                        .FirstOrDefault();

                    if (targetCompany == null)
                    {
                        throw new ArgumentOutOfRangeException("company");
                    }
                }

                targetCompany.Lei = source.Lei;
                targetCompany.Status = source.Status;
                targetCompany.LegalName = source.LegalName;
                targetCompany.NumEmployees = source.NumEmployees;

                // countries

                var srcIds = source.CompanyCountries
                    .Where(x=>x.CompanyCountryId.HasValue)
                    .Select(x => x.CompanyCountryId.Value)
                    .ToHashSet();

                var toRemove = new List<CompanyCountry>();
                foreach (var companyCountry in targetCompany.CompanyCountries)
                {
                    if(!srcIds.Contains(companyCountry.CompanyCountryId.Value))
                    {
                        toRemove.Add(companyCountry);
                    }
                }
                foreach(var item in toRemove)
                {
                    targetCompany.CompanyCountries.Remove(item);
                    silverContext.Remove(item);
                }
                
                foreach (var companyCountry in source.CompanyCountries)
                {
                    CompanyCountry targetEntity;
                    if (!companyCountry.CompanyCountryId.HasValue)
                    {
                        targetEntity = new CompanyCountry()
                        {
                            Company = targetCompany
                        };

                        targetCompany.CompanyCountries.Add(targetEntity);
                        silverContext.CompanyCountries.Add(targetEntity);        
                    }
                    else
                    {
                        targetEntity = targetCompany.CompanyCountries.First(x => x.CompanyCountryId == companyCountry.CompanyCountryId);
                    }
                    targetEntity.CountryId = companyCountry.CountryId;
                    targetEntity.IsPrimary = companyCountry.IsPrimary;
                    targetEntity.LegalJurisdiction = companyCountry.LegalJurisdiction;
                    targetEntity.Ticker = companyCountry.Ticker;
                }

                // extended data
                if(source.CompanyExtendedData.Count>0)
                {
                    var sourceExtendedData = source.CompanyExtendedData.First();
                    CompanyExtendedData targetExtendedData; 
                    if(targetCompany.CompanyExtendedData.Any())
                    {
                        targetExtendedData = targetCompany.CompanyExtendedData.First();
                    }
                    else
                    {
                        targetExtendedData = new CompanyExtendedData();
                        targetCompany.CompanyExtendedData.Add(targetExtendedData);
                        silverContext.Add(targetExtendedData);
                    }
                    targetExtendedData.BelowNationalAvgIncome = sourceExtendedData.BelowNationalAvgIncome;
                    targetExtendedData.Company = targetCompany;
                    targetExtendedData.DisabledEmployees = sourceExtendedData.DisabledEmployees;
                    targetExtendedData.HierarchyLevel = sourceExtendedData.HierarchyLevel;
                    targetExtendedData.RetentionRate = sourceExtendedData.RetentionRate;
                    targetExtendedData.MedianSalary = sourceExtendedData.MedianSalary;
                }
                else
                {
                    targetCompany.CompanyExtendedData.Clear();
                }

                // questions
                if (source.CompanyQuestionnaires.Count > 0)
                {
                    srcIds = source.CompanyQuestionnaires
                    .Where(x => x.Id.HasValue)
                    .Select(x => x.Id.Value)
                    .ToHashSet();

                    var questionsToRemove = new List<CompanyQuestion>();
                    foreach (var companyQuestion in targetCompany.CompanyQuestionnaires)
                    {
                        if (!srcIds.Contains(companyQuestion.Id.Value))
                        {
                            questionsToRemove.Add(companyQuestion);
                        }
                    }
                    foreach (var item in questionsToRemove)
                    {
                        targetCompany.CompanyQuestionnaires.Remove(item);
                        silverContext.Remove(item);
                    }

                    foreach (var companyQuestion in source.CompanyQuestionnaires)
                    {
                        CompanyQuestion targetEntity;
                        if (!companyQuestion.Id.HasValue)
                        {
                            targetEntity = new CompanyQuestion()
                            {
                                Company = targetCompany
                            };

                            targetCompany.CompanyQuestionnaires.Add(targetEntity);
                            silverContext.CompanyQuestionnaires.Add(targetEntity);
                        }
                        else
                        {
                            targetEntity = targetCompany.CompanyQuestionnaires.First(x => x.Id == companyQuestion.Id);
                        }
                        targetEntity.Question = companyQuestion.Question;
                        targetEntity.Answer = companyQuestion.Answer;
                    }
                }
                else
                {
                    targetCompany.CompanyQuestionnaires.Clear();
                }

                // names
                if (source.CompanyNames.Count > 0)
                {
                    srcIds = source.CompanyNames
                    .Where(x => x.Id.HasValue)
                    .Select(x => x.Id.Value)
                    .ToHashSet();

                    var namesToRemove = new List<CompanyName>();
                    foreach (var companyName in targetCompany.CompanyNames)
                    {
                        if (!srcIds.Contains(companyName.Id.Value))
                        {
                            namesToRemove.Add(companyName);
                        }
                    }
                    foreach (var item in namesToRemove)
                    {
                        targetCompany.CompanyNames.Remove(item);
                        silverContext.Remove(item);
                    }

                    foreach (var companyName in source.CompanyNames)
                    {
                        CompanyName targetEntity;
                        if (!companyName.Id.HasValue)
                        {
                            targetEntity = new CompanyName()
                            {
                                Company = targetCompany
                            };

                            targetCompany.CompanyNames.Add(targetEntity);
                            silverContext.CompanyNames.Add(targetEntity);
                        }
                        else
                        {
                            targetEntity = targetCompany.CompanyNames.First(x => x.Id == companyName.Id);
                        }
                        targetEntity.Name = companyName.Name;
                        targetEntity.Type = companyName.Type;
                    }
                }
                else
                {
                    targetCompany.CompanyNames.Clear();
                }

                // industries
                if (source.CompanyIndustries.Count > 0)
                {
                    srcIds = source.CompanyIndustries
                    .Where(x => x.Id.HasValue)
                    .Select(x => x.Id.Value)
                    .ToHashSet();

                    var industriesToRemove = new List<CompanyIndustry>();
                    foreach (var companyName in targetCompany.CompanyIndustries)
                    {
                        if (!srcIds.Contains(companyName.Id.Value))
                        {
                            industriesToRemove.Add(companyName);
                        }
                    }
                    foreach (var item in industriesToRemove)
                    {
                        targetCompany.CompanyIndustries.Remove(item);
                        silverContext.Remove(item);
                    }

                    foreach (var companyIndustry in source.CompanyIndustries)
                    {
                        CompanyIndustry targetEntity;
                        if (!companyIndustry.Id.HasValue)
                        {
                            targetEntity = new CompanyIndustry()
                            {
                                Company = targetCompany
                            };

                            targetCompany.CompanyIndustries.Add(targetEntity);
                            silverContext.CompanyIndustries.Add(targetEntity);
                        }
                        else
                        {
                            targetEntity = targetCompany.CompanyIndustries.First(x => x.Id == companyIndustry.Id);
                        }
                        targetEntity.IndustryId = companyIndustry.IndustryId;
                        targetEntity.PrimarySecondary = companyIndustry.PrimarySecondary;
                    }
                }
                else
                {
                    targetCompany.CompanyIndustries.Clear();
                }

                // TODO: roles and people

                silverContext.SaveChanges();

                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };

                return response;

            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = $"Bad query: {ex}",
                    Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
                };
            }
        }
    }
}
