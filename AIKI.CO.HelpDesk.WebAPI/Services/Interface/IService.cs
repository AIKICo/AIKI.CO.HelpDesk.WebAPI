using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IService<T, V, IDType> 
        where T : BaseObject 
        where V : BaseResponse
    {
        Task<V> GetById(IDType id);
        Task<IEnumerable<V>> GetAll();

        Task<int> AddRecord(V request);
        Task<int> UpdateRecord(V record);
        Task<int> DeleteRecord(IDType id);
    }
}
