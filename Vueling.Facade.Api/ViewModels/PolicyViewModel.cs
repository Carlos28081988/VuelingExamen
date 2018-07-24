using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vueling.Facade.Api.ViewModels {
    public class PolicyViewModel {
        public string Id { get; set; }
        public decimal AmountInsured { get; set; }
        public string Email { get; set; }
        public System.DateTime InceptionDate { get; set; }
        public bool InstallmentPayment { get; set; }
        public string ClientId { get; set; }
        public ClientViewModel Client { get; set; }

    }
}