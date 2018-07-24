using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vueling.Application.Dto;

namespace Vueling.Application.Services.Contracts {
    public interface IHTTPService {
        Task<List<ClientDto>> GetAllClients();
        Task<List<PolicyDto>> GetAllPolicies();
    }
}
