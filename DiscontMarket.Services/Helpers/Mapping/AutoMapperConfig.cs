using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.Services.Helpers.Mapping
{
    public class AutoMapperConfig<T, Tmodel>
    {
        public static IMapper Initialize()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, Tmodel>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
