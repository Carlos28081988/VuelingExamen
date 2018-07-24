using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vueling.Facade.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vueling.Application.Services.Service;
using Vueling.Application.Dto;

namespace Vueling.Facade.Api.Controllers.UnitTest.Tests {
    [TestClass()]
    public class ClientApiControllerTests {

        ClientService clientService = new ClientService();

        [TestMethod()]
        public void GetTest() {
            List<ClientDto> listClientDto = clientService.Get();
            Assert.IsTrue(listClientDto.Count() == 194);
        }

        [DataRow("a0ece5db-cd14-4f21-812f-966633e7be86", "Britney")]
        [DataRow("e8fd159b - 57c4 - 4d36 - 9bd7 - a59ca13057bb", "Manning")]
        [DataRow("a3b8d425-2b60-4ad7-becc-bedf2ef860bd", "Barnett")]
        [DataTestMethod]
        public void GetTest1(string id, string name) {
            ClientDto clientDto;
            clientDto = clientService.GetById(id);
            Assert.Equals(clientDto.Id, id);
            Assert.Equals(clientDto.Name, name);
        }
        
    }
}