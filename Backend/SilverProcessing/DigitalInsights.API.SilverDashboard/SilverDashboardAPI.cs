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

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DigitalInsights.API.SilverDashboard
{
    public class SilverDashboardAPI
    {
        const string PAGE_SIZE = "pageSize";
        const string PAGE_INDEX = "pageIndex";

        SilverContext silverContext = new SilverContext();

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public SilverDashboardAPI()
        {
        }


        /// <summary>
        /// A Lambda function to respond to HTTP Get methods from API Gateway
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

                var companies = silverContext.Companies.OrderBy(x => x.LegalName).Skip(pageIndex * pageSize).Take(pageSize).ToArray();


                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(companies),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };

                context.Logger.LogLine(response.Body);

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
