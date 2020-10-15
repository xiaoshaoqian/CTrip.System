using System;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;

namespace CTrip.System.Interfaces
{
    public class BaseService<T> : DBcontext, IBaseService<T> where T : class, new()
    {
    }
}
