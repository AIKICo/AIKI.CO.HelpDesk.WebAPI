using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AIKI.CO.HelpDesk.WebAPI.BuilderExtensions;
using AIKI.CO.HelpDesk.WebAPI.HubController;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize]
    public class TicketListenerController
    {
        private readonly IHubContext<TicketAlarmHub> _hubContext;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _Configuration;
        private readonly IService<Ticket, TicketResponse> _serviceTicket;

        public TicketListenerController(
            IHubContext<TicketAlarmHub> hubContext, 
            IMemoryCache cache,
            IConfiguration Configuration,
            IService<Ticket, TicketResponse> serviceTicket)
        {
            _hubContext = hubContext;
            _cache = cache;
            _Configuration = Configuration;
            _serviceTicket = serviceTicket;
        }

        private string GetConnectionString()
        {
            var builder = new PostgreSqlConnectionStringBuilder(_Configuration["DATABASE_URL"])
            {
                Pooling = true,
                TrustServerCertificate = true,
                SslMode = BuilderExtensions.SslMode.Require
            };
            return builder.ConnectionString;
        }
        public void ListenForAlarmNotifications()
        {
            
            NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString());
            conn.StateChange += conn_StateChange;
            conn.Open();
            var listenCommand = conn.CreateCommand();
            listenCommand.CommandText = $"listen notifyalarmticket;";
            listenCommand.ExecuteNonQuery();
            conn.Notification += PostgresNotificationReceived;
            _hubContext.Clients.All.SendAsync(this.GetAlarmList());
            while (true)
            {
                conn.Wait();
            }
        }

        private void PostgresNotificationReceived(object sender, NpgsqlNotificationEventArgs e)
        {
            string actionName = e.Payload.ToString();
            //string actionType = string.Empty;
            if (actionName.Contains("DELETE"))
            {
                //actionType = "Delete";
            }

            if (actionName.Contains("UPDATE"))
            {
                //actionType = "Update";
            }

            if (actionName.Contains("INSERT"))
            {
                //actionType = "Insert";
            }

            _hubContext.Clients.All.SendAsync("ReceiveMessage", e.Payload);
        }

        public string GetAlarmList()
        {
            var TicketInfo = _serviceTicket.GetAll(q => q.enddate == null).Result;
            var AlarmList = TicketInfo.ToList().Select(q => new TicketAlarmResponse
            {
                id = q.id,
                registerdate = q.registerdate.ToString()
            }).ToList();
            _cache.Set("TicketAlarm", SerializeObjectToJson(AlarmList));
            return _cache.Get("TicketAlarm").ToString();
        }

        public static string SerializeObjectToJson(Object alarmticket)
        {
            try
            {
                return JsonConvert.SerializeObject(alarmticket);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void conn_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            _hubContext.Clients.All.SendAsync(
                "Current State: " + e.CurrentState.ToString() + " Original State: " + e.OriginalState.ToString(),
                "connection state changed");
        }
    }
}