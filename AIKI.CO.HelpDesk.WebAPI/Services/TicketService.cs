using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.VisualBasic;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class TicketService : BaseService<Ticket, TicketResponse>
    {
        private readonly IService<TicketHistory, TicketHistoryResponse> _serviceHistory;

        public TicketService(IMapper map,
            IUnitOfWork unitofwork,
            IOptions<AppSettings> appSettings,
            IService<TicketHistory, TicketHistoryResponse> serviceHistory,
            IHttpContextAccessor context,
            IDataProtectionProvider provider) : base(map, unitofwork, appSettings, context, provider)
        {
            _serviceHistory = serviceHistory;
        }

        public override async Task<int> AddRecord(TicketResponse request, Guid? companyId = null)
        {
            request.id = Guid.NewGuid();
            request.registerdate = DateTime.Now;
            request.enddate = null;
            var record = _map.Map<Ticket>(request);
            record.companyid = _companyId;
            await _unitofwork.GetRepository<Ticket>().InsertAsync(record);
            await _unitofwork.SaveChangesAsync();
            return await AddHistory(request, "درخواست رفع ایراد رایانه ای دریافت گردید", null);
        }

        public override async Task<int> PartialUpdateRecord(TicketResponse request)
        {
            var ticketInfo = await _repository.FindAsync(request.id);
            if ((ticketInfo.ticketrate ?? 0.00) != (request.ticketrate ?? 0.00))
                await AddHistory(request,
                    $"ارزیابی ناظر {new string('\x2605', Convert.ToInt32(request.ticketrate))} تعیین گردید", null);

            if (request.tickettype != ticketInfo.tickettype && request.tickettype != null)
            {
                if (request.tickettype == new Guid("e746ba44-ccf0-4159-a60d-1f147656bdfc")) //درخواست بسته شود
                {
                    request.enddate = DateTime.Now;
                    await AddHistory(request, "درخواست کار بسته شد", null);
                }
                else if (request.tickettype == new Guid("9e2a917a-fd55-4483-9270-e2a7fa3d69c0")) //درخواست رد شود
                {
                    request.enddate = DateTime.Now;
                    await AddHistory(request, "درخواست رد گردید", null);
                }
                else if (request.tickettype == new Guid("e6c7460f-de37-4a0e-8790-bbfe5a5e8ac9")) //مجدد باز شده
                {
                    request.enddate = null;
                    await AddHistory(request, "درخواست مجدد باز گردید", null);
                }
            }

            return await base.PartialUpdateRecord(request);
        }

        public override async Task<int> UpdateRecord(TicketResponse request)
        {
            var ticketInfo = await _repository.FindAsync(request.id);
            if (request.tickettype != ticketInfo.tickettype && request.tickettype != null)
            {
                var ticketTypeinfo =
                    (await _serviceHistory.GetAnotherTableRecords<AppConstantItem, AppConstantItemResponse>(
                        q => q.id == request.tickettype)).Single();
                await AddHistory(request, $"وضعیت درخواست به {ticketTypeinfo.value1} تغییر داده شد ", null);
            }

            return await base.UpdateRecord(request);
        }

        private async Task<int> AddHistory(TicketResponse ticketInfo, string comment,
            string agentName = "سامانه ثبت درخواست")
        {
            return await _serviceHistory.AddRecord(new TicketHistoryResponse
            {
                id = Guid.NewGuid(),
                ticketid = ticketInfo.id,
                historycomment = comment,
                agentname = agentName,
            });
        }
    }
}