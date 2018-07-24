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

namespace Vueling.Facade.Api.Controllers
{
    [Authorize]
    public class PolicyApiController : ApiController
    {
        private readonly PolicyService policyService;

        public PolicyApiController() : this(new PolicyService()) {
            #region Init Log
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConfigurationManager.AppSettings["ErrorLog"].ToString(), fileSizeLimitBytes: 1000)
                .CreateLogger();
            #endregion
        }

        public PolicyApiController(PolicyService policyService) {
            this.policyService = policyService;
            #region Init Log
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConfigurationManager.AppSettings["ErrorLog"].ToString(), fileSizeLimitBytes: 1000)
                .CreateLogger();
            #endregion
        }


        // GET: api/PolicyApi
        public List<PolicyDto> Get() {
            return policyService.Get();
        }

        // GET: api/PolicyApi/5
        public PolicyDto Get(string idPolicy) {
            return policyService.GetById(idPolicy);
        }


        [Route("PolicyApi/policies/{name}")]
        [HttpGet]
        public List<PolicyDto> GetPoliciesByUserName(string name) {
            return policyService.GetPoliciesByUserName(name);
        }

        // POST: api/PolicyApi
        [ResponseType(typeof(PolicyDto))]
        public IHttpActionResult Post(PolicyDto policyDto) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            PolicyDto policyDtoAdded = new PolicyDto();

            try {
                policyDtoAdded = policyService.Add(policyDto);
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
                new { id = policyDtoAdded.Id }, policyDtoAdded);
        }



        // PUT: api/PolicyApi/5
        public string Put(string idPolicy, PolicyDto policyDto) {
            throw new NotImplementedException();
        }

        // DELETE: api/PolicyApi/5
        public string Delete(string idPolicy) {
            throw new NotImplementedException();
        }
    }
}
