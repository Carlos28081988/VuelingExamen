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
    #region Constructors
    public class HTTPApiController {
        public static HttpClient client;
        public static ClientService clientService;
        public static PolicyService policyService;

        
        static HTTPApiController() {
            client = new HttpClient();
            clientService = new ClientService();
            policyService = new PolicyService();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["uriWebServiceClientsPolicies"].ToString());
        }

        public HTTPApiController() {
            client = new HttpClient();
            clientService = new ClientService();
            policyService = new PolicyService();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["uriWebServiceClientsPolicies"].ToString());
        }
        #endregion

        public static async Task InitBDAsync() {
            var clients = await GetAllClients();
            var policies = await GetAllPolicies();
        }
        
        public static async Task<List<ClientDto>> GetAllClients() {
            ContainerJsonClientDto ContainerJsonClients = new ContainerJsonClientDto();
            List<ClientDto> listClientDtos = new List<ClientDto>();
            try {
                HttpResponseMessage response = client.GetAsync(ConfigurationManager.AppSettings["pathToAllClients"].ToString()).Result;

                if (response.IsSuccessStatusCode) {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    ContainerJsonClients = JsonConvert.DeserializeObject<ContainerJsonClientDto>(jsonString);
                    
                    for(int i = 0; i<ContainerJsonClients.clientDto.Length; i++) {
                        listClientDtos.Add(ContainerJsonClients.clientDto[i]);
                    }

                    clientService.AddList(listClientDtos);
                }
                #region Exceptions and log
            } catch (ArgumentNullException e) {
                Log.Error(ResourceApi.ArgumentNullError
                    + e.Message + ResourceApi.ErrorLogSeparation
                    + e.Data + ResourceApi.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(ResourceApi.ArgumentNullError, e);

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
    
            return listClientDtos;
        }

        public static async Task<List<PolicyDto>> GetAllPolicies() {
            ContainerJsonPolicyDto ContainerJsonPolicies = new ContainerJsonPolicyDto();
            List<PolicyDto> listPolicyDtos = new List<PolicyDto>();

            try {
                HttpResponseMessage response = client.GetAsync(ConfigurationManager.AppSettings["pathToAllPolicies"].ToString()).Result;

                if (response.IsSuccessStatusCode) {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    ContainerJsonPolicies = JsonConvert.DeserializeObject<ContainerJsonPolicyDto>(jsonString);

                    for (int i = 0; i < ContainerJsonPolicies.policyDto.Length; i++) {
                        listPolicyDtos.Add(ContainerJsonPolicies.policyDto[i]);
                    }

                    policyService.AddList(listPolicyDtos);

                }
                #region Exceptions and log
            } catch (ArgumentNullException e) {
                Log.Error(ResourceApi.ArgumentNullError
                    + e.Message + ResourceApi.ErrorLogSeparation
                    + e.Data + ResourceApi.ErrorLogSeparation
                    + e.StackTrace);
                throw new VuelingException(ResourceApi.ArgumentNullError, e);

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
            return listPolicyDtos;
        }
        
    }
}
