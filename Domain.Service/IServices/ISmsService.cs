using Microsoft.EntityFrameworkCore;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;

namespace Moneyon.PowerBi.Domain.Service.IServices;

public interface ISmsService
{
    Task SendOTP(string mobile, string code);
}

public interface IPowerBiContext
{

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


    Task CommitAsync();

}