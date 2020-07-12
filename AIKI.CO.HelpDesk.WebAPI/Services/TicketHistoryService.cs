using System;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class TicketHistoryService : BaseService<TicketHistory, TicketHistoryResponse>
    {
        public TicketHistoryService(IMapper map,
            IUnitOfWork unitofwork,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor context,
            IDataProtectionProvider provider) : base(map, unitofwork, appSettings, context, provider)
        {
        }

        public override async Task<int> AddRecord(TicketHistoryResponse request, Guid? companyId = null)
        {
            request.id = Guid.NewGuid();
            var newRecord = _map.Map<TicketHistory>(request);
            newRecord.historydate = DateTime.Now;
            newRecord.companyid = _companyId;
            await _repository.InsertAsync(newRecord);
            return await _unitofwork.SaveChangesAsync();
        }
    }
}