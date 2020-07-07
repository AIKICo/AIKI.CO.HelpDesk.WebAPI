using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Controllers;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace AIKI.CO.HelpDesk.WebAPI.HubController
{
    public class TicketAlarmHub: Hub
    {
        private readonly IMemoryCache _cache; 
        private readonly IHubContext<TicketAlarmHub> _hubContext;
        private readonly IConfiguration _Configuration;
        private readonly IService<Ticket, TicketResponse> _serviceTicket;
        public TicketAlarmHub(
            IMemoryCache cache, 
            IHubContext<TicketAlarmHub> hubContext,
            IConfiguration Configuration,
            IService<Ticket, TicketResponse> serviceTicket)
        {
            _cache = cache;
            _hubContext = hubContext;
            _Configuration = Configuration;
            _serviceTicket = serviceTicket;
        }

        public async Task SendMessage()
        {
            if (!_cache.TryGetValue("TicketAlarm", out string response))
            {
                var ticketList = new TicketListenerController (_hubContext,_cache, _Configuration, _serviceTicket);
                ticketList.ListenForAlarmNotifications();
                var jsonticketalarm = ticketList.GetAlarmList();
                _cache.Set("TicketAlarm", jsonticketalarm);
                await Clients.All.SendAsync("ReceiveMessage", _cache.Get("TicketAlarm").ToString());
            }
            else
            {
                await Clients.All.SendAsync("ReceiveMessage", _cache.Get("TicketAlarm").ToString());
            }
        }
    }
}