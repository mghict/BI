using Domain.Model.Common.Data;
using Infrastructure.Data.Repositories;
using Moneyon.Common.Data;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using Moneyon.PowerBi.Infrastructure.Data.Repositories;

namespace Infrastructure.Data
{
    [AutoRegister(typeof(IUnitOfWork))]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PowerBiContext _db;

        private IUserRepository _UserRepository;
        private IRoleRepository _RoleRepository;
        private IAddressRepository _AddressRepository;
        private ICityRepository _CityRepository;
        private ICountryRepository _CountryRepository;
        private IProvinceRepository _ProvinceRepository;
        private IPersonRepository _PersonRepository;
        private IPermissionRepository _PermissionRepository;
        private IOTPRepository _OTPRepository;
        private IUsersTokenRepository _UsersTokenRepository;
        private IPackageRepository _PackageRepository;
        private IReportRepository _ReportRepository;
        private IAccountGroupRepository _AccountGroupRepository;
        private IAccountRepository _AccountRepository;
        private ITransactionRepository _TransactionRepository;
        private ITransactionDetailRepository _TransactionDetailRepository;
        private IPaymentRepository _PaymentRepository;
        private IPurchesRepository _PurchesRepository;
        private ISubscriptionRepository _SubscriptionRepository;
        private IDocumentRepository _DocumentRepository;
        private IDocumentContentRepository _DocumentContentRepository;
        private IReceiptPaymentRepository _ReceiptPaymentRepository;
        private IIpgPaymentRepository _IIpgPaymentRepository;
        private ITicketRepository _TicketRepository;
        private ITicketCategoryRepository _TicketCategoryRepository;
        private ITicketDetailRepository _TicketDetailRepository;



        public ITicketRepository TicketRepository => _TicketRepository=_TicketRepository ?? new TicketRepository(_db);
        public ITicketCategoryRepository TicketCategoryRepository => _TicketCategoryRepository=_TicketCategoryRepository ?? new TicketCategoryRepository(_db);
        public ITicketDetailRepository TicketDetailRepository=> _TicketDetailRepository=_TicketDetailRepository ?? new TicketDetailRepository(_db);

        public IReceiptPaymentRepository ReceiptPaymentRepository => _ReceiptPaymentRepository=_ReceiptPaymentRepository ?? new ReceiptPaymentRepository(_db);
        public IIpgPaymentRepository IIpgPaymentRepository => _IIpgPaymentRepository=_IIpgPaymentRepository ?? new IPGPaymentRepository(_db);
        public IDocumentRepository DocumentRepository => _DocumentRepository=_DocumentRepository?? new DocumentRepository(_db);
        public IDocumentContentRepository DocumentContentRepository =>_DocumentContentRepository=_DocumentContentRepository ?? new DocumentContentRepository(_db);
        public ISubscriptionRepository SubscriptionRepository=> _SubscriptionRepository=_SubscriptionRepository ?? new SubscriptionRepositorty(_db);
        public IPurchesRepository PurchesRepository=> _PurchesRepository=_PurchesRepository ?? new PurchesRepository(_db);
        public IAccountGroupRepository AccountGroupRepository => _AccountGroupRepository = _AccountGroupRepository ?? new AccountGroupRepository(_db);
        public IAccountRepository AccountRepository => _AccountRepository = _AccountRepository ?? new AccountRepository(_db);
        public ITransactionRepository TransactionRepository => _TransactionRepository = _TransactionRepository ?? new TransactionRepository(_db);
        public ITransactionDetailRepository TransactionDetailRepository => _TransactionDetailRepository = _TransactionDetailRepository ?? new TransactionDetailRepository(_db);
        public IPaymentRepository PaymentRepository => _PaymentRepository = _PaymentRepository ?? new PaymentRepository(_db);


        public IPackageRepository PackageRepository => _PackageRepository = _PackageRepository ?? new PackageRepository(_db);
        public IOTPRepository OTPRepository=> _OTPRepository=_OTPRepository ?? new OTPRepository(_db);
        public IUserRepository UserRepository => _UserRepository = _UserRepository ?? new UserRepository(_db);
        public IUsersTokenRepository UsersTokenRepository => _UsersTokenRepository = _UsersTokenRepository ?? new UserTokenRepository(_db);
        public IRoleRepository RoleRepository => _RoleRepository = _RoleRepository ?? new RoleRepository(_db);

        public IAddressRepository AddressRepository => _AddressRepository = _AddressRepository ?? new AddressRepository(_db);
        public ICityRepository CityRepository => _CityRepository = _CityRepository ?? new CityRepository(_db);
        public ICountryRepository CountryRepository => _CountryRepository = _CountryRepository ?? new CountryRepository(_db);
        public IProvinceRepository ProvinceRepository => _ProvinceRepository = _ProvinceRepository ?? new ProvinceRepository(_db);
        public IPersonRepository PersonRepository => _PersonRepository = _PersonRepository ?? new PersonRepository(_db);
        public IPermissionRepository PermissionRepository=> _PermissionRepository = _PermissionRepository ?? new PermissionRepository(_db);
        public IReportRepository ReportRepository => _ReportRepository = _ReportRepository ?? new ReportRepository(_db);

        public UnitOfWork(PowerBiContext db)
        {
            _db = db;
        }


        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public string GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }

        public async Task<IDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _db.CommitAsync();
        }
    }
}
