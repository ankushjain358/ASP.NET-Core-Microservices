using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;

namespace eShop.AccountService.API
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //TODO:Apply logging & make it more better to handle expected & unexpected errors
            string errorMessage = string.Empty;
            int statusCode = 500;

            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

                // TODO: Throw specific types of exceptions for specific types of error codes.
                statusCode = (int)HttpStatusCode.InternalServerError;
            }

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var response = new BaseResponse()
                {
                    Success = false,
                    Message = errorMessage
                };

                var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                await context.Response.WriteAsync(json);
            }
        }
    }
}