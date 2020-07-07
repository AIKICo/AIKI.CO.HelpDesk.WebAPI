using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AutoMapper;
using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Mvc;

namespace AIKI.CO.HelpDesk.WebAPI.AutoMapperSettings
{
    public sealed class HelpdeskMapper : Profile
    {
        public HelpdeskMapper()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;
            CreateMap<string, string>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? null : s);
            CreateMap<byte[], byte[]>().ConvertUsing(b => b.Length == 0 ? null : b);
            CreateMap<bool?, bool?>().ConvertUsing(b => b ?? true);
            CreateMap<Customer, CustomerResponse>().ReverseMap();
            CreateMap<Member, MemberResponse>().ReverseMap();
            CreateMap<OperatingHour, OperatingHoureResponse>().ReverseMap();
            CreateMap<SLASetting, SLASettingResponse>().ReverseMap();
            CreateMap<Group, GroupResponse>().ReverseMap();
            CreateMap<AppConstant, AppConstantResponse>().ReverseMap();
            CreateMap<AppConstantItem, AppConstantItemResponse>().ReverseMap();
            CreateMap<OrganizeChart, OrganizeChartResponse>().ReverseMap();
            CreateMap<OrganizeCharts_JsonView, OrganizeCharts_JsonViewResponse>().ReverseMap();
            CreateMap<Asset, AssetResponse>().ReverseMap();
            CreateMap<AssetsView, AssetsViewResponse>().ReverseMap();
            CreateMap<Ticket, TicketResponse>().ReverseMap();
            CreateMap<TicketsView, TicketsViewResponse>()
                .ForMember(d => d.registerdate,
                    opt =>
                        opt.MapFrom(s =>
                            $"{new PersianDateTime(s.registerdate).ToShortDateString()} {new PersianDateTime(s.registerdate).ToShortTimeString()}"))
                .ForMember(d => d.enddate,
                    opt =>
                        opt.MapFrom(s => s.enddate == null ? string.Empty : ConvertToJalili(s.enddate))).ReverseMap();
            CreateMap<TicketHistory, TicketHistoryResponse>()
                .ForMember(d => d.historydate, opt =>
                    opt.MapFrom(s => ConvertToJalili(s.historydate)))
                .ReverseMap();
            CreateMap<Last30Ticket, Last30TicketResponse>().ReverseMap();
            CreateMap<Company, CompanyResponse>().ReverseMap();
            CreateMap<TicketCountInfo, TicketCountInfoResponse>().ReverseMap();
        }

        private string ConvertToJalili(DateTime? date)
        {
            if (date == null) return string.Empty;
            return $"{new PersianDateTime(date).ToShortDateString()} {new PersianDateTime(date).ToLongTimeString()}";
        }
    }
}