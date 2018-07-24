using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vueling.Application.Dto;

namespace Vueling.Application.JsonModels {
    public class ContainerJsonPolicyDto {

        [JsonProperty("policies")]
        public PolicyDto []policyDto;
    }
}
