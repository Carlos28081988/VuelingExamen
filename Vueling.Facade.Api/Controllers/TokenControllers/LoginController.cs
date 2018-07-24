using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vueling.Application.Dto;
using Vueling.Application.Services.Service;
using Vueling.Facade.Api.Models;

namespace Vueling.Facade.Api.Controllers.TokenControllers{

    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

       
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            ClientService clientService = new ClientService();
            ClientDto client = clientService.GetByName(login.Username);

            bool isCredentialValid = login.Id.Equals(client.Id) && login.Username.Equals(client.Name);

            if (isCredentialValid) {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
