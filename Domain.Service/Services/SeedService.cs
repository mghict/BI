using AutoMapper;
using Domain.Model.Common.Data;
using Moneyon.Common.Extensions;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common.Extensions;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;


namespace Moneyon.PowerBi.Domain.Service.Services;

[AutoRegister]
public class SeedService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public SeedService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task SeedPermission()
    {
        #region Permission
        List<Permission> permissions = new List<Permission>();
        List<Permission> lstNew = new List<Permission>();
        List<Permission> lstUpd = new List<Permission>();

        foreach (var permission in Enum.GetValues(typeof(PermissionEnum)))
        {
            permissions.Add(new Permission
            {
                Description = ((PermissionEnum)permission).GetDescription(),
                Name = permission.ToString(),
                PermisionId = (int)permission,
                Type = PermissionType.AccessForm
            });
        }


        foreach (var item in permissions)
        {
            var per = await _uw.PermissionRepository.FirstOrDefaultAsync(p => p.PermisionId == item.PermisionId);
            if (per is null)
            {
                await _uw.PermissionRepository.InsertAsync(item);
                await _uw.CommitAsync();
            }
            else
            {
                per.Description = item.Description;
                per.Name = item.Name;
                per.Type = item.Type;
                await _uw.CommitAsync();
            }

        }

        #endregion //Permission
    }

    public async Task SeedRoles()
    {
        var permissions = await _uw.PermissionRepository.ReadAsync();
        List<Role> roles = new List<Role>()
            {
                new Role()
                {
                    Title="SuperAdmin",
                    Permissions=permissions?.ToList(),
                    Description="کاربر ارشد"
                },
                new Role()
                {
                    Title="User",
                    Permissions=permissions?.Where(p=>p.Type==PermissionType.AccessForm)?.ToList(),
                    Description="کاربر سیستم"
                }
            };

        foreach (var item in roles)
        {
            var role=await _uw.RoleRepository.FirstOrDefaultAsync(p=>p.Title.ToLower().Trim()==item.Title.ToLower().Trim());
            if(role is null)
            {
                await _uw.RoleRepository.InsertAsync(item);
                await _uw.CommitAsync();
            }

            else
            {
                try
                {
                    role.Permissions=new List<Permission>();
                    await _uw.CommitAsync();
                    role.Permissions=item.Permissions;
                    await _uw.CommitAsync();
                }
                catch(Exception ex)
                {

                }
            }
        }
        
        
    }

    public async Task SeedPerson()
    {
        var roles = await _uw.RoleRepository.ReadAsync();

        List<Person> persons = SeedService.ConfigPerson().ToList();
        await _uw.PersonRepository.InsertManyAsync(persons);
        await _uw.CommitAsync();
    }



    public static IEnumerable<Permission> ConfigPermission()
    {
        #region Permission
        List<Permission> permissions = new List<Permission>();
        long id = 1L;
        foreach (var permission in Enum.GetValues(typeof(PermissionEnum)))
        {
            permissions.Add(new Permission
            {
                Id= id++,
                Description = ((PermissionEnum)permission).GetDescription(),
                Name = permission.ToString(),
                PermisionId = (int)permission,
                Type =PermissionType.AccessForm
            });
        }


        return permissions;

        #endregion //Permission
    }

    public static IEnumerable<Role> ConfigRoles()
    {
        var permissions = ConfigPermission();
        List<Role> roles = new List<Role>()
            {
                new Role()
                {
                    Id=1,
                    Title="SuperAdmin",
                    Permissions=permissions!.ToList(),
                    Description="کاربر ارشد"
                },
                new Role()
                {
                    Id=2,
                    Title="User",
                    Permissions=permissions!.Where(p=>p.Type==PermissionType.AccessForm)!.ToList(),
                    Description="کاربر سیستم"
                }
            };

        return roles;


    }

    public static IEnumerable<Person> ConfigPerson()
    {
        var roleList = ConfigRoles().ToList();
        return new List<Person>()
        {
            new Person() {
                FirstName="مهدی",
                LastName="گندمکار",
                Code=Guid.NewGuid(),
                NationalCode="0024788201",
                MobileNumber="09371616757",
                Accounts=new List<Account>()
                {
                    new Account(5, AccountKey.Wallet, $"کیف پول")
                },
                User=new User("09371616757","12345",roleList,true)
            },
            new Person() {
                FirstName="مصطفی",
                LastName="قرلی",
                Code=Guid.NewGuid(),
                NationalCode="0385245122",
                MobileNumber="09194878003",
                Accounts=new List<Account>()
                {
                    new Account(5, AccountKey.Wallet, $"کیف پول")
                },
                User=new User("09194878003","12345",roleList.Where(p=>p.Id==2)!.ToList(),true)
            },
        };
    }
}
