using AutoMapper;
using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Common.ObjectMapper
{

    public static class ObjectMapperExtensions
    {
        
        public static IEnumerable<TDestination> MapCollection<TSource, TDestination>(this IMapper mapper, IEnumerable<TSource>? enumerable)
        {
            if (enumerable is null)
                return Enumerable.Empty<TDestination>();

            return enumerable?.Select(item => mapper.Map<TDestination>(item));
        }

        public static DataResult<TDestination> MapDataResult<TSource, TDestination>(this IMapper mapper, DataResult<TSource> dataResult)
        {
            var items = dataResult.Data.Select(x => mapper.Map<TDestination>(x)).ToList();
            return new DataResult<TDestination>(dataResult.Request, items, dataResult.Total);
        }

        public static DataResult<TDestination> MapDataResult<TSource, TDestination>(this IMapper mapper, IEnumerable<TSource>? enumerable, DataRequest dataRequest)
        {
            return new DataResult<TDestination>
            {
                Data = enumerable?.Select(item => mapper.Map<TDestination>(item))?.ToList() ?? new List<TDestination>(),
                Request = dataRequest,
                Total = enumerable?.Count() ?? 0
            };
        }

        public static DataResult<TSource> MapDataResult<TSource>(this IMapper mapper, IEnumerable<TSource>? enumerable, DataRequest dataRequest)
        {
            return new DataResult<TSource>
            {
                Data = enumerable?.ToList() ?? new List<TSource>(),
                Request = dataRequest,
                Total = enumerable?.Count() ?? 0
            };
        }
    }
}
