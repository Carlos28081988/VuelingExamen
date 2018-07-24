using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vueling.Application.Services.Contracts {
    public interface IService<T> {
        T Add(T model);
        List<T> Get();
        T GetById(string id);
        List<T> AddList(List<T> listClientDto);
    }
}
