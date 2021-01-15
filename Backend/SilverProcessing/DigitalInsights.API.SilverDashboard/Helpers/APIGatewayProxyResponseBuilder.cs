using Amazon.Lambda.APIGatewayEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.Helpers
{
    internal class APIGatewayProxyResponseBuilder
    {
        string body = null;
        HttpStatusCode code = HttpStatusCode.OK;
        Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "ContentType", "text/plain" },
            { "AccessControlAllowOrigin", "*" },
        };


        public APIGatewayProxyResponseBuilder WithBody(string body)
        {
            this.body = body;
            return this;
        }

        public APIGatewayProxyResponseBuilder WithStatusCode(HttpStatusCode code)
        {
            this.code = code;
            return this;
        }

        public APIGatewayProxyResponseBuilder WithBadRequestCode()
        {
            return WithStatusCode(HttpStatusCode.BadRequest);
        }

        public APIGatewayProxyResponseBuilder WithOkCode()
        {
            return WithStatusCode(HttpStatusCode.OK);
        }

        public APIGatewayProxyResponseBuilder WithForbiddenCode()
        {
            return WithStatusCode(HttpStatusCode.Forbidden);
        }

        public APIGatewayProxyResponseBuilder WithHeader(string key, string value)
        {
            headers[key] = value;
            return this;
        }

        public APIGatewayProxyResponseBuilder WithPlainTextContent()
        {
            return WithHeader("ContentType", "text/plain");
        }

        public APIGatewayProxyResponseBuilder WithJsonContent()
        {
            return WithHeader("ContentType", "application/json");
        }
        public APIGatewayProxyResponseBuilder WithSimpleError(string body)
        {
            return WithBadRequestCode().WithPlainTextContent().WithBody(body);
        }

        public APIGatewayProxyResponse Build()
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)code,
                Body = body,
                Headers = headers
            };
        }
    }
}
