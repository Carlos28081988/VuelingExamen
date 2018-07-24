using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vueling.Facade.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vueling.Application.Services.Contracts;
using Vueling.Application.Dto;
using Moq;
using Vueling.Facade.ApiTests1;

namespace Vueling.Facade.Api.Controllers.UnitTest.Tests {
    [TestClass()]
    public class ClientApiControllerTests {
        private IClientService<ClientDto> mockObject;
        private ClientDto clientDto;

        [TestInitialize]
        public void Setup() {
            var mock = new Mock<IClientService<ClientDto>>();

            clientDto = new ClientDto("a0ece5db-cd14-4f21-812f-966633e7be86",
                                       "Britney",
                                       "britneyblankenship@quotezart.com",
                                       "admin");

            mock.Setup(x => x.GetUserByPolicyId(ResourceUnitTests.idPolicyOfTest1))
                .Returns(clientDto);

            mockObject = mock.Object;
        }


        [TestCategory("Get")]
        [TestMethod]
        public void GetTest() {
            var result = mockObject.GetUserByPolicyId(ResourceUnitTests.idPolicyOfTest1);
            Assert.AreEqual(result.Id, clientDto.Id);
        }

    }
}