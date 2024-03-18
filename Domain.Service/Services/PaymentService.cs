using AutoMapper;
using Domain.Model.Common.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Model.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;


namespace Moneyon.PowerBi.Domain.Service.Services;

[AutoRegister()]
public class PaymentService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;
    private readonly SubscriptionService _subscription;
    public PaymentService(IUnitOfWork uw, IMapper mp, SubscriptionService subscription)
    {
        _uw = uw;
        _mp = mp;
        _subscription = subscription;
    }

    public async Task CreatePaymentAsync(long userId,PaymentCreateDto dto)
    {
        var person=await _uw.PersonRepository.FirstOrDefaultAsync(p=>p.Id == userId);
        if (person is null) throw new BizException(BizExceptionCode.UserNotFound);

        await CreatePaymentAsync(person, dto);
    }

    public async Task CreatePaymentAsync(Guid userId, PaymentCreateDto dto)
    {
        var person = await _uw.PersonRepository.FirstOrDefaultAsync(p => p.Code == userId);
        if (person is null) throw new BizException(BizExceptionCode.UserNotFound);

        await CreatePaymentAsync(person, dto);
    }

    public async Task CreatePaymentAsync(Person person, PaymentCreateDto dto)
    {
        var payment = new Payment(person.Id,dto.AccountId, dto.Amount,dto.PackageId, $"شارژ کیف پول");

        await _uw.PaymentRepository.InsertAsync(payment);
        await _uw.CommitAsync();

        await PaymentSucceedAsync(payment.Id);
    }

    public async Task CreateIPGPaymentAsync(Person person, IpgPaymentCreateDto dto)
    {
        var payment = new IpgPayment(person.Id, dto.AccountId, dto.Amount, dto.PackageId, $"شارژ کیف پول-درگاه پرداخت");

        await _uw.PaymentRepository.InsertAsync(payment);
        await _uw.CommitAsync();

        await PaymentSucceedAsync(payment.Id);
    }
    
    public async Task CreateReceiptPaymentAsync(Person person, ReceiptPaymentCreateDto dto)
    {
        var doc = await _uw.DocumentRepository.FirstOrDefaultAsync(p => p.Guid == dto.ImageId);
        if (doc is null) throw new BizException(BizExceptionCode.DataNotFound);

        var payment = new ReceiptPayment(person.Id, dto.AccountId, dto.Amount,dto.PackageId,doc, $"شارژ کیف پول-رسید بانکی");

        await _uw.PaymentRepository.InsertAsync(payment);
        await _uw.CommitAsync();

        //await PaymentSucceed(payment.Id);
    }

    public async Task PaymentFailedAsync(long id)
    {
        var payment = await _uw.PaymentRepository.FirstOrDefaultAsync(_ => _.Id == id);
        if (payment is null) throw new BizException(BizExceptionCode.DataNotFound);

        payment.EnsureStatus(PaymentStatus.Pending);

        payment.Status = PaymentStatus.Failed;
        await _uw.CommitAsync();

    }

    public async Task PaymentFailedAsync(string trackingCode)
    {
        var payment = await _uw.PaymentRepository.FirstOrDefaultAsync(_ => _.TrackingCode == trackingCode);
        if (payment is null) throw new BizException(BizExceptionCode.DataNotFound);

        payment.EnsureStatus(PaymentStatus.Pending);

        payment.Status = PaymentStatus.Failed;
        await _uw.CommitAsync();

    }

    public async Task PaymentRejectedAsync(long id)
    {
        var payment = await _uw.PaymentRepository.FirstOrDefaultAsync(_ => _.Id == id);
        if (payment is null) throw new BizException(BizExceptionCode.DataNotFound);

        payment.EnsureStatus(PaymentStatus.Pending);

        payment.Status = PaymentStatus.Rejected;
        await _uw.CommitAsync();

    }

    public async Task PaymentRejectedAsync(string trackingCode)
    {
        var payment = await _uw.PaymentRepository.FirstOrDefaultAsync(_ => _.TrackingCode == trackingCode);
        if (payment is null) throw new BizException(BizExceptionCode.DataNotFound);

        payment.EnsureStatus(PaymentStatus.Pending);

        payment.Status = PaymentStatus.Rejected;
        await _uw.CommitAsync();

    }

    public async Task PaymentSucceedAsync(long id)
    {
        var payment=await _uw.PaymentRepository.FirstOrDefaultAsync(_ => _.Id == id);
        if (payment is null) throw new BizException(BizExceptionCode.DataNotFound);

        payment.EnsureStatus(PaymentStatus.Pending);

        payment.Status = PaymentStatus.Success;

        var platformBankAccount = await _uw.AccountRepository.GetAccountAsync(Person.PlatformCode, AccountKey.Bank);
        var userWalletAccount = await _uw.AccountRepository.GetAccountAsync(payment.AccountId);
        payment.Transaction = new Transaction(TransactionType.AccountCharge, platformBankAccount, userWalletAccount, payment.Amount, "شارژ کیف پول");

       
        await _uw.CommitAsync();
        
    }

    public async Task PaymentSucceedAsync(string trackingCode)
    {
        var payment = await _uw.PaymentRepository.FirstOrDefaultAsync(_ => _.TrackingCode == trackingCode);
        if (payment is null) throw new BizException(BizExceptionCode.DataNotFound);

        payment.EnsureStatus(PaymentStatus.Pending);

        payment.Status = PaymentStatus.Success;

        var platformBankAccount = await _uw.AccountRepository.GetAccountAsync(Person.PlatformCode, AccountKey.Bank);
        var userWalletAccount = await _uw.AccountRepository.GetAccountAsync(payment.AccountId);
        payment.Transaction = new Transaction(TransactionType.AccountCharge, platformBankAccount, userWalletAccount, payment.Amount, "شارژ کیف پول");


        await _uw.CommitAsync();

    }

    public async Task ReceiptPaymentSucceedAsync(Person person,string trackingCode)
    {
        var payment = await _uw.PaymentRepository.FirstOrDefaultAsync(_ => _.TrackingCode == trackingCode);
        if (payment is null) throw new BizException(BizExceptionCode.DataNotFound);

        payment.EnsureStatus(PaymentStatus.Pending);

        var package = await _uw.PackageRepository.FirstOrDefaultAsync(p => p.Id == payment.Id);
        if (package is null) throw new BizException(BizExceptionCode.DataNotFound);

        payment.Status = PaymentStatus.Success;

        var platformBankAccount = await _uw.AccountRepository.GetAccountAsync(Person.PlatformCode, AccountKey.Bank);
        var userWalletAccount = await _uw.AccountRepository.GetAccountAsync(payment.AccountId);
        payment.Transaction = new Transaction(TransactionType.AccountCharge, platformBankAccount, userWalletAccount, payment.Amount, "شارژ کیف پول-رسید بانکی");

        await PurchesAndTransactionAndSubscriptionAsync(person, package, userWalletAccount);

        await _uw.CommitAsync();

    }

    public async Task ReceiptPaymentChangeStatusAsync(Person person, ReceiptPaymentChangeStatus dto)
    {
        if(dto.Status==PaymentStatus.Pending) throw new BizException(BizExceptionCode.PaymentStatusInValid);

        var payment = await _uw.PaymentRepository.FirstOrDefaultAsync(_ => _.TrackingCode == dto.TrackingCode);
        if (payment is null) throw new BizException(BizExceptionCode.DataNotFound);

        payment.EnsureStatus(PaymentStatus.Pending);

        payment.Status = dto.Status;
        payment.Description= dto.Description;

        if (payment.Status == PaymentStatus.Success)
        {
            var package = await _uw.PackageRepository.FirstOrDefaultAsync(p => p.Id == payment.PackageId);
            if (package is null) throw new BizException(BizExceptionCode.DataNotFound);

            var platformBankAccount = await _uw.AccountRepository.GetAccountAsync(Person.PlatformCode, AccountKey.Bank);
            var userWalletAccount = await _uw.AccountRepository.GetAccountAsync(payment.AccountId);
            payment.Transaction = new Transaction(TransactionType.AccountCharge, platformBankAccount, userWalletAccount, payment.Amount, "شارژ کیف پول-رسید بانکی");

            await PurchesAndTransactionAndSubscriptionAsync(person, package, userWalletAccount);
        }
        await _uw.CommitAsync();

    }
    private async Task PurchesAndTransactionAndSubscriptionAsync(Person person,Package package, Account personWallet)
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

        await _subscription.CreateWithoutCommitAsync(subscription);

        Purches purches = new Purches(person.Id, package.Id, subscription);

        purches.Transaction = tx;

        await _uw.PurchesRepository.InsertAsync(purches);
    }
    public async Task<DataResult<PaymentDto>> PaymentReceiptPendingList(DataRequest request)
    {
        var payments=await _uw.ReceiptPaymentRepository.ReadPagableAsync(request,
                                                                         filter: p=>p.Status==PaymentStatus.Pending &&
                                                                                    p is ReceiptPayment);

        return _mp.MapDataResult<ReceiptPayment, PaymentDto>(payments);
    }

    public async Task<PaymentWithImageDto> ReceiptPaymentItemAsync(string trackingCode)
    {
        var payment = await _uw.ReceiptPaymentRepository.FirstOrDefaultAsync(filter:p=>p.TrackingCode==trackingCode,
                                                                              include: i=>i.Include(p=>p.Image!).ThenInclude(p=>p.Content!));

        return _mp.Map<ReceiptPayment, PaymentWithImageDto>(payment!);
    }

    //public async Task<DocumentDto> CreateReceiptPaymentAsync(ReceiptPaymentCreateDto receipt, long personId, CancellationToken cancellationToken = default)
    //{
    //    var person=await _uw.PersonRepository.FirstOrDefaultAsync(p=>p.Id== personId);
    //    if (person is null) throw new BizException(BizExceptionCode.UserNotFound);

    //    var image = await receipt.Image.ToDocumentAsync();
    //    var entity = _mp.Map(receipt, new ReceiptPayment(personId, image));
    //    await _uw.PaymentRepository.InsertAsync(entity, cancellationToken);
    //    await _uw.CommitAsync(cancellationToken);

    //    return _mp.Map<DocumentDto>(image);
    //}
}
