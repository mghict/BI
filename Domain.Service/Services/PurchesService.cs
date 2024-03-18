using AutoMapper;
using Domain.Model.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using System.IO.Packaging;
using System.Threading;

namespace Moneyon.PowerBi.Domain.Service.Services
{
    [AutoRegister()]
    public class PurchesService //: IPucheService
    {
        private readonly IUnitOfWork _uw;
        private readonly IMapper _mp;
        private readonly PaymentService paymentService;
        private readonly SubscriptionService subscriptionService;
        public PurchesService(IUnitOfWork uw, IMapper mp, PaymentService paymentService, SubscriptionService subscriptionService)
        {
            _uw = uw;
            _mp = mp;
            this.paymentService = paymentService;
            this.subscriptionService = subscriptionService;
        }

        public async Task CreatePurches(long ownerId,PurchesCreateDto dto)
        {
            var person = await _uw.PersonRepository.FirstOrDefaultAsync(p => p.Id == ownerId);
            if (person is null) throw new BizException(BizExceptionCode.DataNotFound);

            await CreatePurches(person, dto);
        }
        public async Task CreatePurches(Guid ownerId, PurchesCreateDto dto)
        {
            var person = await _uw.PersonRepository.FirstOrDefaultAsync(p => p.Code == ownerId);
            if (person is null) throw new BizException(BizExceptionCode.DataNotFound);

            await CreatePurches(person, dto);
        }
        public async Task CreatePurches(Person person, PurchesCreateDto dto)
        {
            var package = await _uw.PackageRepository.FirstOrDefaultAsync(p => p.Id == dto.PackageId);
            if (package is null) throw new BizException(BizExceptionCode.DataNotFound);

            var personWallet = await _uw.AccountRepository.GetAccountAsync(person.Id, AccountKey.Wallet);
            if (personWallet is null) throw new BizException(BizExceptionCode.PersonWalletNotFound);

            await paymentService.CreatePaymentAsync(person, new PaymentCreateDto(dto.Amount,dto.PackageId,personWallet.Id));

            personWallet.EnsureBalance(package.PackageAmount);

            var platformBankAccount = await _uw.AccountRepository.GetAccountAsync(Person.PlatformCode, AccountKey.PlatformIncomes);
            var tx = new Transaction(TransactionType.AccountCharge, personWallet, platformBankAccount, package.PackageAmount, $"خرید اشتراک - {package.Name}");


            Subscription subscription = new Subscription()
            {
                OwnerId = person.Id,
                Active = true,
                StartDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMonths(package.TimePeriod),
                SubscriptionType = SubscriptionType.PackageType,
                PackageId = package.Id,
            };

            Purches purches = new Purches(person.Id, dto.PackageId, subscription);

            purches.Transaction = tx;

            await _uw.PurchesRepository.InsertAsync(purches);
            await _uw.CommitAsync();
        }
        public async Task CreateIpgPurches(Person person, PurchesIpgCreateDto dto)
        {
            var package = await _uw.PackageRepository.FirstOrDefaultAsync(p => p.Id == dto.PackageId);
            if (package is null) throw new BizException(BizExceptionCode.DataNotFound);

            var personWallet = await _uw.AccountRepository.GetAccountAsync(person.Id, AccountKey.Wallet);
            if (personWallet is null) throw new BizException(BizExceptionCode.PersonWalletNotFound);

            await paymentService.CreateIPGPaymentAsync(person, new IpgPaymentCreateDto(dto.Amount,dto.PackageId, personWallet.Id));

            personWallet.EnsureBalance(package.PackageAmount);

            await PurchesAndTransactionAndSubscriptionAsync(person, package, personWallet);

            await _uw.CommitAsync();
        }
        public async Task CreateReceiptPurches(Person person, PurchesReceiptCreateDto dto)
        {
            var package = await _uw.PackageRepository.FirstOrDefaultAsync(p => p.Id == dto.PackageId);
            if (package is null) throw new BizException(BizExceptionCode.DataNotFound);

            

            var personWallet = await _uw.AccountRepository.GetAccountAsync(person.Id, AccountKey.Wallet);
            if (personWallet is null) throw new BizException(BizExceptionCode.PersonWalletNotFound);

            await paymentService.CreateReceiptPaymentAsync(person,new ReceiptPaymentCreateDto(dto.ImageId,dto.Amount, personWallet.Id,dto.PackageId));

            
        }

        private async Task PurchesAndTransactionAndSubscriptionAsync(Person person, Model.Modeling.Package package,Account personWallet)
        {
            var platformBankAccount = await _uw.AccountRepository.GetAccountAsync(Person.PlatformCode, AccountKey.PlatformIncomes);
            var tx = new Transaction(TransactionType.PurchesSubscription, personWallet, platformBankAccount, package.PackageAmount, $"خرید اشتراک - {package.Name}");


            Subscription subscription = new Subscription()
            {
                OwnerId = person.Id,
                Active = true,
                StartDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMonths(package.TimePeriod),
                SubscriptionType = SubscriptionType.PackageType,
                PackageId = package.Id,
            };

            await subscriptionService.CreateWithoutCommitAsync(subscription);

            Purches purches = new Purches(person.Id, package.Id, subscription);

            purches.Transaction = tx;

            await _uw.PurchesRepository.InsertAsync(purches);
        }

    }
}
