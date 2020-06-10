using System;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class TicketHistoryService:BaseService<TicketHistory, TicketHistoryResponse>
    {
       public TicketHistoryService(IMapper map,
                   IUnitOfWork unitofwork,
                   IOptions<AppSettings> appSettings) : base(map, unitofwork, appSettings){}

       public override async Task<int> AddRecord(TicketHistoryResponse request)
       {
           request.id = Guid.NewGuid();
           var newRecord = _map.Map<TicketHistory>(request);
           newRecord.historydate = DateTime.Now;
           await _unitofwork.GetRepository<TicketHistory>().InsertAsync(newRecord);
           return await _unitofwork.SaveChangesAsync();
       }
    }
}