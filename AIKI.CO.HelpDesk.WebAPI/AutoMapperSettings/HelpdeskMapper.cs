using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AutoMapper;

namespace AIKI.CO.HelpDesk.WebAPI.AutoMapperSettings
{
    public class HelpdeskMapper : Profile
    {
        public HelpdeskMapper()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;
            CreateMap<string, string>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? null : s);
            CreateMap<byte[], byte[]>().ConvertUsing(b => b.Length == 0 ? null : b);
            CreateMap<Customer, CustomerResponse>().ReverseMap();
            CreateMap<Member, MemberResponse>().ReverseMap();
        }
    }
}
