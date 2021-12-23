using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace MyIslamWebApp.Filter
{
    public class CustomResponseHandler : DelegatingHandler
    {
        public class ModelState
        {
            public string errormessage { get; set; }
        }
        public class ErrorObject
        {
            public string errormessage { get; set; }
            public ModelState modelState { get; set; }
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (!IsSwagger(request))
            {
                var response = await base.SendAsync(request, cancellationToken);

                try
                {
                    return GenerateResponse(request, response);
                }
                catch (Exception ex)
                {
                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                return await base.SendAsync(request, cancellationToken);
            }

        }

        private HttpResponseMessage GenerateResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            string errorMessage = null;
            HttpStatusCode statusCode = response.StatusCode;
            if (!IsResponseValid(response))
            {
                if (statusCode == HttpStatusCode.Unauthorized)
                    return request.CreateResponse(HttpStatusCode.Unauthorized, response.Content.ReadAsStringAsync());
                else if (statusCode == HttpStatusCode.BadRequest)
                {
                    ErrorObject errorObject = JsonConvert.DeserializeObject<ErrorObject>(response.Content.ReadAsStringAsync().Result);
                    errorObject.modelState = new ModelState();

                    var msg = response.Content.ReadAsStringAsync().Result;
                    if (msg.Contains("message"))
                    {
                        if (msg.Contains("modelState"))
                        {
                            errorObject.modelState.errormessage = response.Content.ReadAsStringAsync().Result.ToString().Split('[')[1].Replace("}", "").Replace("]", "");
                        }
                        else
                        {
                            errorObject.modelState.errormessage = response.Content.ReadAsStringAsync().Result.ToString().Split(':')[1].Replace("}", "");
                        }
                    }
                    else
                    {
                        errorObject.modelState.errormessage = response.Content.ReadAsStringAsync().Result.ToString().Split('[')[1].Replace("}", "").Replace("]", "");
                    }

                    string errormess = request.CreateResponse(HttpStatusCode.BadRequest, errorObject).ToString();

                    ResponseMetadata responseMetadatafalse = new ResponseMetadata();
                    responseMetadatafalse.Result = false;
                    responseMetadatafalse.Version = "1.0";
                    responseMetadatafalse.StatusCode = statusCode;
                    DateTime dt1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
                    responseMetadatafalse.Timestamp = dt1;
                    responseMetadatafalse.ErrorMessage = errorObject.modelState.errormessage;
                    var resultFalse = request.CreateResponse(response.StatusCode, responseMetadatafalse);
                    return resultFalse;
                }
            }
            object responseContent;
            if (response.TryGetContentValue(out responseContent))
            {
                HttpError httpError = responseContent as HttpError;
                if (httpError != null)
                {
                    errorMessage = httpError.Message;
                    statusCode = HttpStatusCode.InternalServerError;
                    responseContent = null;
                }
            }
            ResponseMetadata responseMetadatatrue = new ResponseMetadata();
            responseMetadatatrue.Result = true;
            responseMetadatatrue.Version = "1.0";
            responseMetadatatrue.StatusCode = statusCode;
            responseMetadatatrue.Content = responseContent;
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            responseMetadatatrue.Timestamp = dt;
            responseMetadatatrue.ErrorMessage = errorMessage;
            responseMetadatatrue.Size = responseContent.ToString().Length;
            var resultTrue = request.CreateResponse(response.StatusCode, responseMetadatatrue);
            return resultTrue;
        }
        private bool IsResponseValid(HttpResponseMessage response)
        {
            if ((response != null) && (response.StatusCode == HttpStatusCode.OK))
                return true;
            return false;
        }

        private bool IsSwagger(HttpRequestMessage request)
        {
            return request.RequestUri.PathAndQuery.StartsWith("/swagger");
        }
    }
}