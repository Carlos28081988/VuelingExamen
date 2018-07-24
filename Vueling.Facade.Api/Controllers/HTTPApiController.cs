using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Vueling.Application.Dto;
using Vueling.Application.JsonModels;
using Vueling.Application.Services.Service;
using Vueling.Common.Layer;
using Vueling.Facade.Api.ViewModels;

namespace Vueling.Facade.Api.Controllers
{
    public class HTTPApiController {

        public static HTTPService httpService;

        #region Constructors
        static HTTPApiController() {
           httpService = new HTTPService();
        }

        public HTTPApiController() : this(new HTTPService()) {
        }

        public HTTPApiController(HTTPService hTTPService) {
            httpService = hTTPService;
        }
        #endregion

        public static async Task InitBDAsync() {
            await GetAllClients();
            await GetAllPolicies();
        }
        
        
        public static async Task<List<ClientDto>> GetAllClients() {
            
            try {
                var listClientDtos = await httpService.GetAllClients();
                return listClientDtos;

            #region Exceptions and log
            } catch (HttpRequestException e) {
                Log.Error(ResourceApi.ArgumentNullError
                    + e.Message + ResourceApi.ErrorLogSeparation
                    + e.Data + ResourceApi.ErrorLogSeparation
                    + e.StackTrace);

                var response = new HttpResponseMessage(HttpStatusCode.NotFound){
                    Content = new StringContent(string.Format(ResourceApi.HttpError)),  
                    ReasonPhrase = ResourceApi.HttpReasonError
                };

                throw new HttpResponseException(response);
            }
            #endregion
        }

        public static async Task<List<PolicyDto>> GetAllPolicies() {

            try {
                var listPolicytDtos = await httpService.GetAllPolicies();
                return listPolicytDtos;

            #region Exceptions and log
            } catch (HttpRequestException e) {
                Log.Error(ResourceApi.ArgumentNullError
                    + e.Message + ResourceApi.ErrorLogSeparation
                    + e.Data + ResourceApi.ErrorLogSeparation
                    + e.StackTrace);

                var response = new HttpResponseMessage(HttpStatusCode.NotFound) {
                    Content = new StringContent(string.Format(ResourceApi.HttpError)),
                    ReasonPhrase = ResourceApi.HttpReasonError
                };

                throw new HttpResponseException(response);
            }
            #endregion
        }

    }
}
