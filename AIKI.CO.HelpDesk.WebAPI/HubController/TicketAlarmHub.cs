using System;
using System.Data;
using AIKI.CO.HelpDesk.WebAPI.BuilderExtensions;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SslMode = AIKI.CO.HelpDesk.WebAPI.BuilderExtensions.SslMode;

namespace AIKI.CO.HelpDesk.WebAPI.HubController
{
    [Authorize]
    public class TicketAlarmHub : Hub
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _Configuration;
        private readonly IHubContext<TicketAlarmHub> _hubContext;
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

        public async void SendMessage()
        {
            var conn = new NpgsqlConnection(GetConnectionString());
            conn.StateChange += conn_StateChange;
            conn.Open();
            var listenCommand = conn.CreateCommand();
            listenCommand.CommandText = "listen notifyalarmticket;";
            listenCommand.ExecuteNonQuery();
            conn.Notification += PostgresNotificationReceived;
            while (true) await conn.WaitAsync();
        }

        private async void PostgresNotificationReceived(object sender, NpgsqlNotificationEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Payload)) return;
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", e.Payload);
        }

        private void conn_StateChange(object sender, StateChangeEventArgs e)
        {
            _hubContext.Clients.All.SendAsync(
                "Current State: " + e.CurrentState + " Original State: " + e.OriginalState,
                "connection state changed");
        }

        private string GetConnectionString()
        {
            var builder = new PostgreSqlConnectionStringBuilder(Environment.GetEnvironmentVariable("DATABASE_URL"))
            {
                Pooling = true,
                TrustServerCertificate = true,
                SslMode = SslMode.Require
            };
            return builder.ConnectionString;
        }
    }
}