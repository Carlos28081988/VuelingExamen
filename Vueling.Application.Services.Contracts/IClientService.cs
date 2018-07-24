using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vueling.Application.Services.Contracts {
    public interface IClientService<T> {
        T GetUserByPolicyId(string idPolicy);
        T GetByName(string name);
    }
}
