using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vueling.Facade.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vueling.Application.Dto;

namespace Vueling.Facade.Api.Controllers.UnitTest.Tests {
    [TestClass()]
    public class HTTPApiControllerTests {

        [TestMethod()]
        public void GetAllClientsTest() {
            List<ClientDto> listClientsDto = HTTPApiController.GetAllClients().Result;
            Assert.IsTrue(listClientsDto.Count() > 0);
        }

    }
}