using Microsoft.EntityFrameworkCore.Diagnostics;
using Moneyon.Common.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;

namespace Domain.Model.Common.Data;

public interface IUnitOfWork  : IUnitOfWorkBase<string>
{
    public IAddressRepository AddressRepository { get; }
    public ICityRepository CityRepository { get; }
    public ICountryRepository CountryRepository { get; }
    public IProvinceRepository ProvinceRepository { get;  }
    public IPersonRepository PersonRepository { get;  }
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IPermissionRepository PermissionRepository { get; }
    public IOTPRepository OTPRepository { get; }
    public IUsersTokenRepository UsersTokenRepository { get; }
    public IPackageRepository PackageRepository { get; }
    public IReportRepository ReportRepository { get; }

    public IAccountGroupRepository AccountGroupRepository { get; }
    public IAccountRepository AccountRepository { get; }
    public ITransactionRepository TransactionRepository { get; }
    public ITransactionDetailRepository TransactionDetailRepository { get; }

    public IPaymentRepository PaymentRepository { get; }
    public IReceiptPaymentRepository ReceiptPaymentRepository { get; }
    public IIpgPaymentRepository IIpgPaymentRepository { get; }
    public IPurchesRepository PurchesRepository { get; }
    public ISubscriptionRepository SubscriptionRepository { get; }

    public IDocumentRepository DocumentRepository { get; }
    public IDocumentContentRepository DocumentContentRepository { get; }

    public ITicketRepository TicketRepository { get; }
    public ITicketCategoryRepository TicketCategoryRepository { get;}
    public ITicketDetailRepository TicketDetailRepository { get;}

    void SaveChanges();
    //public IPackagePermissionRepository PackagePermission { get;  }
    //public IPermissionRoleRepository PermissionRole { get; }
    //public ISubscriptionRepository SubscriptionRepository { get; }
    //public IPurchesRepository PurchesRepository { get; }
    //public IReportRepository ReportRepository { get; }

}
