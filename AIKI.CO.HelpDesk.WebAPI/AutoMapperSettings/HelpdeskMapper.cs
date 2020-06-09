﻿using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
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
                .ForMember(d=>d.registerdate, 
                    opt=> 
                        opt.MapFrom(s=> $"{new PersianDateTime(s.registerdate).ToShortDateString()}  {new PersianDateTime(s.registerdate).ToShortTimeString()}"))
                .ForMember(d=>d.enddate, 
                    opt=> 
                        opt.MapFrom(s=> s.enddate==null?string.Empty:$"{new PersianDateTime(s.enddate).ToShortDateString()}  {new PersianDateTime(s.enddate).ToShortTimeString()}")).ReverseMap();
        }
    }
}