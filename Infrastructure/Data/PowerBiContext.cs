using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moneyon.PowerBi.Domain.Model.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using Moneyon.PowerBi.Domain.Service.IServices;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Infrastructure.Data
{
    public class PowerBiContext : DbContext, IPowerBiContext
    {
        public PowerBiContext(DbContextOptions<PowerBiContext> options) : base (options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Purches> Purches { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ReceiptPayment> ReceiptPayments { get; set; }
        public DbSet<IpgPayment> IpgPayments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentContent> DocumentContent { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketDetail> TicketDetails { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=BI_DB2;User Id=CrowdUser;Password=Mgh@99450;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            }
            

            optionsBuilder.EnableSensitiveDataLogging();
        }

        #region Fluent Api

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(p => p.DisplayName)
                .HasComputedColumnSql("Case  when [FirstName] is null and [LastName] is null then [MobileNumber] else [FirstName]+' '+[LastName] end");

            modelBuilder.Entity<Person>()
                .HasOne(p => p.User)
                .WithOne(p=>p.Person)
                .HasForeignKey<User>(p=>p.PersonId)
                .IsRequired();

            modelBuilder.Entity<Report>()
                .HasOne(p => p.Permission)
                .WithOne(p => p.Report)
                .HasForeignKey<Permission>(p => p.ReportId);
                //.IsRequired();

            modelBuilder.Entity<Package>()
                .HasMany(p=>p.Reports)
                .WithMany(p => p.Packages)
                .UsingEntity("PackagesPermissions");

            modelBuilder.Entity<Role>()
                .HasMany(p => p.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity("RolesPermissions");

            modelBuilder.Entity<Role>()
                .HasMany(p => p.Users)
                .WithMany(p => p.Roles)
                .UsingEntity("UserRoles");

            modelBuilder.Entity<Purches>()
                .HasOne(p => p.Subscription)
                .WithOne(p => p.Purches)
                .HasForeignKey<Subscription>(p => p.PurchesId);

            modelBuilder.Entity<Document>()
                .HasOne(p => p.Content)
                .WithOne(p => p.Document)
                .HasPrincipalKey<Document>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReceiptPayment>()
                .HasOne(x => x.Image)
                .WithOne()
                .HasForeignKey<ReceiptPayment>(x => x.ImageId)
                .OnDelete(DeleteBehavior.NoAction);

            #region People
            var platform = new Person
            {
                Id=1,
                Code=Person.PlatformCode,
                MobileNumber = "0912000000",
                FirstName="پلتفرم",
                LastName=" ",
                Email = "MEHRTA@LIVE.COM",
                NationalCode="1234567890"
            };

            modelBuilder.Entity<Person>().HasData(platform);

            #endregion

            #region AccountGroups
            var accountGroups = new List<AccountGroup>()
            {
                new AccountGroup()
                {
                    Id = 1,
                    Name = "دارایی‌ها",
                    HasDebitNature = true,
                    Key = AccountGroupKey.Assets,
                },
                new  AccountGroup()
                 {
                    Id = 2,
                    ParentId = 1,
                    Key = AccountGroupKey.CurrentAssets,
                    Name = "‌دارایی‌های جاری",
                    HasDebitNature = true,
                 },
                new AccountGroup()
                {
                    Id = 3,
                    Name = "‌بدهی‌ها",
                    HasDebitNature = false,
                    Key = AccountGroupKey.Liabilities,
                },
                new AccountGroup()
                {
                    Id = 4,
                    ParentId = 3,
                    Key = AccountGroupKey.CurrentLiabilities,
                    Name = "‌بدهی‌های جاری",
                    HasDebitNature = false,
                },
                new AccountGroup()
                {
                    Id = 5,
                    ParentId = 3,
                    Key = AccountGroupKey.PeopleWallets,
                    HasDebitNature = false,
                    Name = "‌‌کیف پول اشخاص",
                },
                new AccountGroup()
                {
                    Id = 6,
                    ParentId = 3,
                    Key = AccountGroupKey.PlatformFundraising,
                    HasDebitNature = false,
                    Name = "تجمیع ‌‌سرمایه پروژه‌ها",
                },
                new AccountGroup()
                {
                    Id = 7,
                    Name = "‌حقوق صاحبان سهام",
                    Key = AccountGroupKey.Equity,
                    HasDebitNature = false,
                },
                new AccountGroup()
                {
                    Id = 8,
                    Name = "‌درآمدها",
                    Key = AccountGroupKey.Incomes,
                    HasDebitNature = false,
                },
            };

            modelBuilder.Entity<AccountGroup>().HasData(accountGroups);
            #endregion

            #region Accounts
            var accounts = new List<Account>()
            {
                new Account()
                {
                    Id = 1,
                    AccountGroupId = 5,
                    Key = AccountKey.Wallet,
                    OwnerId = platform.Id,
                    Name = "کیف پول",
                },
                new Account()
                {
                    Id = 2,
                    AccountGroupId = 2,
                    Key = AccountKey.Bank,
                    OwnerId = platform.Id,
                    Name = "حساب بانک",
                },
                new Account()
                {
                    Id = 3,
                    AccountGroupId = 8,
                    Key = AccountKey.PlatformIncomes,
                    OwnerId = platform.Id,
                    Name = "درآمدها",
                }

            };

            modelBuilder.Entity<Account>().HasData(accounts);
            #endregion

            var permissions = SeedService.ConfigPermission();
            modelBuilder.Entity<Permission>().HasData(permissions);

            //List<Role> roles = new List<Role>()
            //{
            //    new Role()
            //    {
            //        Id=1,
            //        Title="SuperAdmin",
            //        Permissions=permissions!.ToList(),
            //        Description="کاربر ارشد"
            //    },
            //    new Role()
            //    {
            //        Id=2,
            //        Title="User",
            //        Permissions=permissions!.Where(p=>p.Type==PermissionType.AccessForm)!.ToList(),
            //        Description="کاربر سیستم"
            //    }
            //};
            //modelBuilder.Entity<Role>().HasData(roles);

            //var users = SeedService.ConfigPerson();
            //modelBuilder.Entity<Person>().HasData(users);

            //modelBuilder.Entity<Permission>()
            //    .HasOne<>
            //#region User
            ////modelBuilder.Entity<User>().Property(x => x.FirstName).IsRequired();
            ////modelBuilder.Entity<User>().Property(x => x.LastName).IsRequired();
            ////modelBuilder.Entity<User>().Property(x => x.Mobile).IsRequired();
            ////modelBuilder.Entity<User>().Property(x => x.CreateDate).IsRequired();
            ////modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();

            //#endregion

            //#region Role

            //#endregion

            //#region OTP
            //modelBuilder.Entity<OTP>().Property(x => x.SendDate).IsRequired();
            //modelBuilder.Entity<OTP>().Property(x => x.Type).IsRequired();
            //modelBuilder.Entity<OTP>().Property(x => x.IsUsed).IsRequired();
            //modelBuilder.Entity<OTP>().Property(x => x.MobileNumber).IsRequired();
            //modelBuilder.Entity<OTP>().Property(x => x.Token).IsRequired();
            //modelBuilder.Entity<OTP>().Property(x => x.Value).IsRequired();


            //#endregion

            //#region Purches

            //modelBuilder.Entity<Purches>().HasOne(x => x.Subscription).WithOne(x => x.Purches).HasForeignKey<Purches>(x => x.SubscriptionId).OnDelete(DeleteBehavior.NoAction);
            //#endregion

            //#region Subscription

            //modelBuilder.Entity<Subscription>().HasOne(x => x.Purches).WithOne(x => x.Subscription).HasForeignKey<Subscription>(x => x.PurchesId).OnDelete(DeleteBehavior.NoAction);


            //#endregion

            //#region Package
            //modelBuilder.Entity<Package>().HasMany(x => x.Reports).WithMany(x => x.Packages);

            //#endregion

            //#region Report

            //modelBuilder.Entity<Report>().HasMany(x => x.Packages).WithMany(x => x.Reports);


            //#endregion

            //#region SeedData

            //#region Role

            //modelBuilder.Entity<Role>().HasData(new Role
            //{
            //    Id = 1,
            //    Title = "Admin",

            //});


            //#endregion

            //#endregion

            base.OnModelCreating(modelBuilder);
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            AuditHandel();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditHandel();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AuditHandel()
        {
            var now = DateTime.UtcNow;

            var entities = ChangeTracker.Entries().Where(e => e.Entity is IAudit && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entityEntry in entities)
            {
                var entity = (IAudit)entityEntry.Entity;
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entity.ModifiedOn = now;
                        entity.CreateOn = now;
                        break;
                    case EntityState.Modified:
                        entity.ModifiedOn = now;
                        break;
                }
            }
        }

        

        #endregion
    }
}
