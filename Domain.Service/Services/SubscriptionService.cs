using AutoMapper;
using Domain.Model.Common.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Service.IServices;
using ZstdSharp.Unsafe;

namespace Moneyon.PowerBi.Domain.Service.Services
{
    [AutoRegister()]
    public class SubscriptionService 
    {
        private readonly IUnitOfWork _uw;
        private readonly IMapper _mp;

        public SubscriptionService(IUnitOfWork uw, IMapper mp)
        {
            _uw = uw;
            _mp = mp;
        }

        public async Task<DataResult<SubscriptionShortDto>> GetList(DataRequest request)
        {
            var result=await _uw.SubscriptionRepository.ReadPagableAsync(request, include: i => i.Include(p => p.Owner).Include(p => p.Package!));
            return _mp.MapDataResult<Subscription,SubscriptionShortDto>(result);
        }

        public async Task<DataResult<SubscriptionShortDto>> GetList(long personId, DataRequest request)
        {
            if(request.Sort is null)
            {
                request.Sort = new DataRequestSort
                {
                    Asc = false,
                    Field = "StartDate"
                };
            }
            var result = await _uw.SubscriptionRepository.ReadPagableAsync(request,filter: p=>p.OwnerId==personId,include: i=>i.Include(p=>p.Owner).Include(p=>p.Package!));
            return _mp.MapDataResult<Subscription, SubscriptionShortDto>(result);
        }

        public async Task<SubscriptionDto> GetItem(long subscriptionId)
        {
            var subscription = await _uw.SubscriptionRepository.FirstOrDefaultAsync(filter:p => p.Id == subscriptionId, 
                                                                                    include: i => i.Include(p => p.Owner)
                                                                                                   .Include(p => p.Package!)
                                                                                                   .ThenInclude(p=>p.Reports!)
                                                                                                   .ThenInclude(p=>p.Report)
                                                                                                   .Include(p=>p.Purches)
                                                                                                   );
            return _mp.Map<SubscriptionDto>(subscription);
        }

        public async Task<SubscriptionDto> GetItem(long personId, long subscriptionId)
        {
            var subscription = await _uw.SubscriptionRepository.FirstOrDefaultAsync(filter: p => p.Id == subscriptionId && p.OwnerId==personId,
                                                                                    include: i => i.Include(p => p.Owner)
                                                                                                   .Include(p => p.Package!)
                                                                                                   .ThenInclude(p => p.Reports!)
                                                                                                   .ThenInclude(p => p.Report)
                                                                                                   .Include(p => p.Purches)
                                                                                                   );
            return _mp.Map<SubscriptionDto>(subscription);
        }

        public async Task CreateWithoutCommitAsync(Subscription subscription)
        {
            await _uw.SubscriptionRepository.InsertAsync(subscription);
        }
    }
}
