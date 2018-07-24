using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vueling.Application.Dto {
    public class ClientDto {
        public ClientDto() : this(new HashSet<PolicyDto>()) {
        }

        public ClientDto(HashSet<PolicyDto> hashSet) {
            this.Policy = hashSet;
        }

        public ClientDto(string id, string name, string email, string role) {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
            Policy = new HashSet<PolicyDto>();
        }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }

        public ICollection<PolicyDto> Policy { get; set; }

    
    }
}
