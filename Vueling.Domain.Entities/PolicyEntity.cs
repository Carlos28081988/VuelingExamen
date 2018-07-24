using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vueling.Domain.Entities {
    public class PolicyEntity {
        public string Id { get; set; }
        public decimal AmountInsured { get; set; }
        public string Email { get; set; }
        public System.DateTime InceptionDate { get; set; }
        public bool InstallmentPayment { get; set; }
        public string ClientId { get; set; }

        public ClientEntity Client { get; set; }
        
    }
}
