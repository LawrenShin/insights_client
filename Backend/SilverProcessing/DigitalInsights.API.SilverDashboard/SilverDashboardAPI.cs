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

        SilverContext silverContext = new SilverContext();

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public SilverDashboardAPI()
        {
        }

        public APIGatewayProxyResponse GetRoleTypes(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");


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
    }
}
