using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vueling.Infrastructure.Repository.Contracts {
    public interface IPolicyRepository<T> {
        List<T> GetPoliciesByUserName(string username);
    }
}
