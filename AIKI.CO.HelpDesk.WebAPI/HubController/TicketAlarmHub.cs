using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.BuilderExtensions;
using AIKI.CO.HelpDesk.WebAPI.Controllers;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;

namespace AIKI.CO.HelpDesk.WebAPI.HubController
{
    [Authorize]
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

        public void SendMessage()
        {
            this.ListenForAlarmNotifications();
        }
        
        public async void ListenForAlarmNotifications()
        {
            
            NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString());
            conn.StateChange += conn_StateChange;
            conn.Open();
            var listenCommand = conn.CreateCommand();
            listenCommand.CommandText = $"listen notifyalarmticket;";
            listenCommand.ExecuteNonQuery();
            conn.Notification += PostgresNotificationReceived;
            while (true)
            {
                await conn.WaitAsync();
            }
        }
        
        private async void PostgresNotificationReceived(object sender, NpgsqlNotificationEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Payload)) return;
            
            var actionName = e.Payload.ToString();
            /*var recordid = Guid.Parse(actionName.Split(" ")[1]);
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
            }*/

            await _hubContext.Clients.All.SendAsync("ReceiveMessage",e.Payload);
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
    }
}