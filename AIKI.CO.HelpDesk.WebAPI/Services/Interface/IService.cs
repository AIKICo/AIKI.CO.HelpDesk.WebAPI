﻿using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IService<T, V> 
        where T : BaseObject 
        where V : BaseResponse
    {
        Task<IEnumerable<V>> GetAll();
    }
}
