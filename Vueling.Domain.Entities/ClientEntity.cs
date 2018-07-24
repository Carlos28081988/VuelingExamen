using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Vueling.Domain.Entities {
    public class ClientEntity  {
        
        public ClientEntity() : this(new HashSet<PolicyEntity>()) {
        }

        public ClientEntity(HashSet<PolicyEntity> hashSet) {
            this.Policy = hashSet;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public ICollection<PolicyEntity> Policy { get; set; }

    }
}
