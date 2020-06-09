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
    public class TicketService:BaseService<Ticket, TicketResponse>
    {
        public TicketService(IMapper map,
            IUnitOfWork unitofwork,
            IOptions<AppSettings> appSettings):base(map, unitofwork, appSettings)
        {
                
        }

        public override async Task<int> AddRecord(TicketResponse request)
        {
            request.id = Guid.NewGuid();
            request.registerdate = DateTime.Now;
            request.enddate = null;
            await _unitofwork.GetRepository<Ticket>().InsertAsync(_map.Map<Ticket>(request));
            return await _unitofwork.SaveChangesAsync();
        }
    }
}