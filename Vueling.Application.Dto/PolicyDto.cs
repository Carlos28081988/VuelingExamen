using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vueling.Application.Dto {
    public class PolicyDto {

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("amountInsured")]
        public decimal AmountInsured { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("inceptionDate")]
        public System.DateTime InceptionDate { get; set; }
        [JsonProperty("installmentPayment")]
        public bool InstallmentPayment { get; set; }
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        public ClientDto Client { get; set; }

    }
}
