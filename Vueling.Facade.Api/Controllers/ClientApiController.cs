using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Vueling.Application.Dto;
using Vueling.Application.Services.Contracts;
using Vueling.Application.Services.Service;
using Vueling.Common.Layer;

namespace Vueling.Facade.Api.Controllers{

    [Authorize]
    public class ClientApiController : ApiController {

        private readonly ClientService clientService;

        /// <summary>
        /// Void Constructor
        /// </summary>
        public ClientApiController() : this(new ClientService()) {
            #region Init Log
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConfigurationManager.AppSettings["ErrorLog"].ToString(), fileSizeLimitBytes: 1000)
                .CreateLogger();
            #endregion
        }

        /// <summary>
        /// Void constructor
        /// </summary>
        /// <param name="clientService"></param>
        public ClientApiController(ClientService clientService) {
            this.clientService = clientService;
            #region Init Log
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConfigurationManager.AppSettings["ErrorLog"].ToString(), fileSizeLimitBytes: 1000)
                .CreateLogger();
            #endregion
        }

        /// <summary>
        /// Get all clients
        /// </summary>
        /// <returns>List</returns>
        [HttpGet]
        [Authorize(Users = "admin, user")]
        // GET: api/ClientApi
        public List<ClientDto> Get() {
            return clientService.Get();
        }

        /// <summary>
        /// Get client by his id
        /// </summary>
        /// <param name="idClient"></param>
        /// <returns></returns>
        // GET: api/ClientApi/5
        [Route("ClientApi/{id}")]
        [HttpGet]
        public ClientDto Get(string idClient) {
            return clientService.GetById(idClient);
        }

        ///
        [Route("ClientApi/name/{name}")]
        [HttpGet]
        public ClientDto GetUserByName(string name) {
            return clientService.GetByName(name);
        }


        [Route("ClientApi/policy/{idPolicy}")]
        [HttpGet]
        public ClientDto GetUserByPolicyId(string idPolicy) {
            return clientService.GetUserByPolicyId(idPolicy);
        }

        /// <summary>
        /// Add a client
        /// </summary>
        /// <param name="clientDto"></param>
        /// <returns></returns>
        // POST: api/ClientApi
        [ResponseType(typeof(ClientDto))]
        public IHttpActionResult Post(ClientDto clientDto) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            ClientDto clientDtoAdded = new ClientDto();

            try {
                clientDtoAdded = clientService.Add(clientDto);
            }
            #region Exceptions & Log
            catch (VuelingException e) {
                Log.Error(ResourceApi.AddError
                    + e.InnerException + ResourceApi.ErrorLogSeparation
                    + e.Message + ResourceApi.ErrorLogSeparation
                    + e.Data + ResourceApi.ErrorLogSeparation
                    + e.StackTrace);

                var response = new HttpResponseMessage(HttpStatusCode.NotFound);

                throw new HttpResponseException(response);
                #endregion
            }
            return CreatedAtRoute(ResourceApi.HttpRoute,
                new { id = clientDtoAdded.Id }, clientDtoAdded);
        }
    }

}
