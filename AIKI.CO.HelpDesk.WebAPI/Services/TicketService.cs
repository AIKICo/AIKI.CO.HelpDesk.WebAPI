using System;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class TicketService:BaseService<Ticket, TicketResponse>
    {
        private readonly IService<TicketHistory, TicketHistoryResponse> _serviceHistory;

        public TicketService(IMapper map,
            IUnitOfWork unitofwork,
            IOptions<AppSettings> appSettings,
            IService<TicketHistory, 
                TicketHistoryResponse> serviceHistory) : base(map, unitofwork, appSettings)
        {
            _serviceHistory = serviceHistory;
        }
        public override async Task<int> AddRecord(TicketResponse request)
        {
            request.id = Guid.NewGuid();
            request.registerdate = DateTime.Now;
            request.enddate = null;
            await _unitofwork.GetRepository<Ticket>().InsertAsync(_map.Map<Ticket>(request));
            await _unitofwork.SaveChangesAsync();
            return await AddHistory(request, "درخواست رفع ایراد رایانه ای دریافت گردید",null);
        }

        public override async Task<int> PartialUpdateRecord(TicketResponse request)
        {
            var ticketInfo = await _repository.FindAsync(request.id);
            if (((ticketInfo.ticketrate ?? 0.00) != (request.ticketrate ?? 0.00)) && ticketInfo.ticketrate==null)
            {
                await AddHistory(request, $"ارزیابی ناظر {request.ticketrate} تعیین گردید",null);
            }
            return await base.PartialUpdateRecord(request);
        }

        private async Task<int> AddHistory(TicketResponse ticketInfo,string comment, string agentName = "سامانه ثبت درخواست ایراد رایانه ای")
        {
            return await _serviceHistory.AddRecord(new TicketHistoryResponse
            {
                id = Guid.NewGuid(),
                ticketid = ticketInfo.id,
                companyid =  ticketInfo.companyid,
                historycomment = comment,
                agentname = agentName
            });
        }
    }
}