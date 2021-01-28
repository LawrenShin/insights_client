using System;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using DigitalInsights.DB.Silver;
using Newtonsoft.Json;
using DigitalInsights.DB.Silver.Entities;
using DigitalInsights.API.SilverDashboard.Services;
using DigitalInsights.API.SilverDashboard.Helpers;
using DigitalInsights.API.SilverDashboard.DTO;
using Microsoft.IdentityModel.Tokens;

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

        private bool ValidateRequest(APIGatewayProxyRequest request)
        {
            if (request.Headers != null && request.Headers.ContainsKey(AUTH_HEADER))
            {
                var token = request.Headers[AUTH_HEADER];

                return new LoginService(new SilverContext()).ValidateToken(token);
            }
            return false;
        }

        public APIGatewayProxyResponse Login(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                AuthInfoDTO authInfo = null;

                if (request.Body == null)
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithSimpleError($"Request body is empty.\n Consider {JsonConvert.SerializeObject(new AuthInfoDTO() { UserName = "Sample", Password = "Sample" })}")
                        .Build();
                }

                authInfo = JsonConvert.DeserializeObject<AuthInfoDTO>(request.Body);

                if (authInfo == null)
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithSimpleError($"Bad formatting of the request body: {request.Body}.\n Consider {JsonConvert.SerializeObject(new AuthInfoDTO() { UserName = "Sample", Password = "Sample" })}")
                        .Build();
                }

                return new APIGatewayProxyResponseBuilder()
                    .WithOkCode()
                    .WithJsonContent()
                    .WithBody(JsonConvert.SerializeObject(new LoginService(new SilverContext()).CreateToken(authInfo)))
                    .Build();
            }
            catch(SecurityTokenValidationException securityException)
            {
                return new APIGatewayProxyResponseBuilder()
                    .WithSimpleError(securityException.Message)
                    .Build();
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponseBuilder()
                    .WithSimpleError($"Bad query: {ex}.\n Consider {JsonConvert.SerializeObject(new AuthInfoDTO() { UserName = "Sample", Password = "Sample" })}")
                    .Build();
            }
        }

        public APIGatewayProxyResponse GetDictionaries(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithForbiddenCode()
                        .WithPlainTextContent()
                        .Build();
                }

                var service = new DictionaryService(new SilverContext());

                return new APIGatewayProxyResponseBuilder()
                    .WithOkCode()
                    .WithJsonContent()
                    .WithBody(JsonConvert.SerializeObject(
                        new DictionariesDTO()
                        {
                            AddressTypes = service.GetAddressTypes(),
                            Countries = service.GetCountries(),
                            EducationLevels = service.GetEducationLevels(),
                            EducationSubjects = service.GetEducationSubjects(),
                            Genders = service.GetGenders(),
                            Industries = service.GetIndustries(),
                            IndustryCodes = service.GetIndustryCodes(),
                            Races = service.GetRaces(),
                            Religions = service.GetReligions(),
                            RoleTypes = service.GetRoleTypes(),
                        }))
                    .Build();
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponseBuilder()
                    .WithSimpleError($"Bad query: {ex}")
                    .Build();
            }
        }

        public APIGatewayProxyResponse GetUIMetadata(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithForbiddenCode()
                        .WithPlainTextContent()
                        .Build();
                }

                var service = new MetadataService(new SilverContext());

                return new APIGatewayProxyResponseBuilder()
                    .WithOkCode()
                    .WithJsonContent()
                    .WithBody(JsonConvert.SerializeObject(service.GetUIMetadata("company", "person")))
                    .Build();
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponseBuilder()
                    .WithSimpleError($"Bad query: {ex}")
                    .Build();
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
                    return new APIGatewayProxyResponseBuilder()
                        .WithForbiddenCode()
                        .WithPlainTextContent()
                        .Build();
                }

                if (request.QueryStringParameters == null)
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithSimpleError("Expected query string parameters")
                        .Build();
                }

                int pageSize;
                int pageIndex;

                if (!int.TryParse(request.QueryStringParameters[PAGE_SIZE], out pageSize) ||
                    !int.TryParse(request.QueryStringParameters[PAGE_INDEX], out pageIndex))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithSimpleError("Wrong number format")
                        .Build();
                }

                string prefix = null;
                if (request.QueryStringParameters.ContainsKey(SEARCH_PREFIX))
                {
                    prefix = request.QueryStringParameters[SEARCH_PREFIX];
                }

                var service = new CompanyService(new SilverContext());

                return new APIGatewayProxyResponseBuilder()
                    .WithOkCode()
                    .WithJsonContent()
                    .WithBody(JsonConvert.SerializeObject(
                        service.GetCompanies(pageSize, pageIndex, prefix), 
                        new JsonSerializerSettings() { ContractResolver = new MetadataBasedContractResolver() }))
                    .Build();
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponseBuilder()
                    .WithSimpleError($"Bad query: {ex}")
                    .Build();
            }
        }

        public APIGatewayProxyResponse DeleteCompany(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithForbiddenCode()
                        .WithPlainTextContent()
                        .Build();
                }

                if (request.QueryStringParameters == null ||
                    !request.QueryStringParameters.ContainsKey(ID))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithBadRequestCode()
                        .WithPlainTextContent()
                        .WithBody("Expected query string parameters")
                        .Build();
                }

                int id;
                if (!int.TryParse(request.QueryStringParameters[ID], out id))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithBadRequestCode()
                        .WithPlainTextContent()
                        .WithBody("Wrong number format")
                        .Build();
                }

                var service = new CompanyService(new SilverContext());
                service.DeleteCompany(id);

                return new APIGatewayProxyResponseBuilder()
                        .WithOkCode()
                        .WithPlainTextContent()
                        .Build();
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponseBuilder()
                    .WithBadRequestCode()
                    .WithPlainTextContent()
                    .WithBody($"Bad query: {ex}")
                    .Build();
            }
        }

        public APIGatewayProxyResponse SaveCompany(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithForbiddenCode()
                        .WithPlainTextContent()
                        .Build();
                }

                if (string.IsNullOrEmpty(request.Body))
                {
                    throw new ArgumentNullException("request.Body");
                }

                var service = new CompanyService(new SilverContext());
                var dto = JsonConvert.DeserializeObject<CompanyDTO>(
                    request.Body, 
                    new JsonSerializerSettings() { ContractResolver = new MetadataBasedContractResolver() });
                service.UpdateOrInsertCompany(dto);

                return new APIGatewayProxyResponseBuilder()
                    .WithOkCode()
                    .WithPlainTextContent()
                    .Build();
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponseBuilder()
                    .WithSimpleError($"Bad query: {ex}")
                    .Build();
            }
        }

        /// <summary>
        /// A Lambda function to get a list of people to HTTP Get methods from API Gateway
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The API Gateway response.</returns>
        public APIGatewayProxyResponse GetPeople(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithForbiddenCode()
                        .WithPlainTextContent()
                        .Build();
                }

                if (request.QueryStringParameters == null)
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithSimpleError("Expected query string parameters")
                        .Build();
                }

                int pageSize;
                int pageIndex;
                if (!int.TryParse(request.QueryStringParameters[PAGE_SIZE], out pageSize) ||
                    !int.TryParse(request.QueryStringParameters[PAGE_INDEX], out pageIndex))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithSimpleError("Wrong number format")
                        .Build();
                }

                string prefix = null;
                if (request.QueryStringParameters.ContainsKey(SEARCH_PREFIX))
                {
                    prefix = request.QueryStringParameters[SEARCH_PREFIX];
                }

                var service = new PeopleService(new SilverContext());

                return new APIGatewayProxyResponseBuilder()
                    .WithOkCode()
                    .WithJsonContent()
                    .WithBody(JsonConvert.SerializeObject(
                        service.GetPeople(pageSize, pageIndex, prefix),
                        new JsonSerializerSettings() { ContractResolver = new MetadataBasedContractResolver() }))
                    .Build();

            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponseBuilder()
                    .WithSimpleError($"Bad query: {ex}")
                    .Build();
            }
        }

        public APIGatewayProxyResponse DeletePerson(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithForbiddenCode()
                        .WithPlainTextContent()
                        .Build();
                }

                if (request.QueryStringParameters == null ||
                    !request.QueryStringParameters.ContainsKey(ID))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithBadRequestCode()
                        .WithPlainTextContent()
                        .WithBody("Expected query string parameters")
                        .Build();
                }

                int id;
                if (!int.TryParse(request.QueryStringParameters[ID], out id))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithBadRequestCode()
                        .WithPlainTextContent()
                        .WithBody("Wrong number format")
                        .Build();
                }

                var service = new PeopleService(new SilverContext());
                service.DeletePerson(id);

                return new APIGatewayProxyResponseBuilder()
                        .WithOkCode()
                        .WithPlainTextContent()
                        .Build();
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponseBuilder()
                    .WithBadRequestCode()
                    .WithPlainTextContent()
                    .WithBody($"Bad query: {ex}")
                    .Build();
            }
        }

        public APIGatewayProxyResponse SavePerson(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Get Request\n");

                if (!ValidateRequest(request))
                {
                    return new APIGatewayProxyResponseBuilder()
                        .WithForbiddenCode()
                        .WithPlainTextContent()
                        .Build();
                }

                if (string.IsNullOrEmpty(request.Body))
                {
                    throw new ArgumentNullException("request.Body");
                }

                var service = new PeopleService(new SilverContext());
                service.UpdateOrInsertPerson(
                    JsonConvert.DeserializeObject<PersonDTO>(
                        request.Body,
                        new JsonSerializerSettings() { ContractResolver = new MetadataBasedContractResolver() })
                    );

                return new APIGatewayProxyResponseBuilder()
                    .WithOkCode()
                    .WithPlainTextContent()
                    .Build();
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponseBuilder()
                    .WithSimpleError($"Bad query: {ex}")
                    .Build();
            }
        }
    }
}
