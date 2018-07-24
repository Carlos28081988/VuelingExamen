using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vueling.Facade.Api.ViewModels {
    public class ClientViewModel {
        public ClientViewModel() : this(new HashSet<PolicyViewModel>()) {
        }

        public ClientViewModel(HashSet<PolicyViewModel> hashSet) {
            this.Policy = hashSet;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public ICollection<PolicyViewModel> Policy { get; set; }

    }
}
