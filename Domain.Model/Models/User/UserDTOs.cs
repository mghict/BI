using Moneyon.PowerBi.Domain.Modeling;
using System.ComponentModel.DataAnnotations;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class UserRegisterDto
{
    //[Required(ErrorMessage = "نام را وارد کنید")]
    //public string FirstName { get; set; }

    //[Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
    //public string LastName { get; set; }

    [Required(ErrorMessage = "کدملی را وارد کنید")]
    public string NationalCode { get; set; }

    [Required(ErrorMessage = "شماره همراه را وارد کنید")]
    public string MobileNumber { get; set; }
    
    //[Required(ErrorMessage = "شهر محل زندگی را وارد کنید")]
    //public int CityId { get; set; }
    
    //[Required(ErrorMessage = "آدرس را وارد کنید")]
    //public int AddressValue { get; set; }

    [Required(ErrorMessage = " کلمه عبور را وارد کنید")]
    public string Password { get; set; }

    [Required(ErrorMessage = " تکرار کلمه عبور را وارد کنید")]
    [Compare("Password", ErrorMessage = " کلمه عبور و تکرار آن با یکدیگر مطابقت ندارند")]
    public string RePassword { get; set; }

    public string CaptchaValue { get; set; }

}

public class UserRegisterActiveDto
{
    public string MobileNumber { get; set; }
    public string Otp { get; set; }
}

public class  UserRegisterResDto
{
    public int UserId { get; set; }
    public string[] RolesTitles { get; set; }
}

public class UserLogInByOTPDto
{
    [Required(ErrorMessage = "نام کاربری را وارد کنید")]
    public string UserName { get; set; }

    //[Required(ErrorMessage = "کد یکبار مصرف را وارد کنید")]
    //public string Otp { get; set; }

    public string CaptchaValue { get; set; }

    //[Required(ErrorMessage = " Otp Token is required")]
    //public Guid Token { get; set; }
}

public class UserLogInResDto
{
    public int UserId { get; set; }
    public string[] RolesTitles { get; set; }
}


public class UserLogInByPasswordDto
{
    [Required(ErrorMessage = "شماره مبایل را وارد کنید")]
    public string UserName { get; set; }

    [Required(ErrorMessage = " کلمه عبور را وارد کنید")]
    public string Password { get; set; }

    public string CaptchaValue { get; set; }

}

public class LogOutDto 
{
    public string Mobile { get; set; }
}

public class UserIdentityDto
{
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string?[] Roles { get; set; }
    public string?[] Permissions { get; set; }

    public bool IsActive { get; set; }
    public UserIdentityDto()
    {
        
    }
    public UserIdentityDto(string userName, string displayName,bool isActive, string?[] roles, string?[] permissions)
    {
        UserName = userName;
        DisplayName = displayName;
        Roles = roles;
        Permissions = permissions;
        IsActive = isActive;
    }
}

public class LoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string CaptchaValue { get; set; }
}

public class UserHeaderDto
{
    public User User { get; set; }
    //public string? Token { get; set; }
}

public class ForgetPassValidateDto
{
    [Required(ErrorMessage = "شماره همراه را وارد کنید")]
    public string MobileNumber { get; set; }

    [Required(ErrorMessage = " کلمه عبور را وارد کنید")]
    public string Password { get; set; }

    [Required(ErrorMessage = " تکرار کلمه عبور را وارد کنید")]
    [Compare("Password", ErrorMessage = " کلمه عبور و تکرار آن با یکدیگر مطابقت ندارند")]
    public string RePassword { get; set; }

    public string Otp { get; set; }

}

public class AdminResetPasswordDto
{
    public Guid PersonId { get; set; }

    [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
    public string Password { get; set; }

    [Required(ErrorMessage = " تکرار کلمه عبور را وارد کنید")]
    [Compare("Password", ErrorMessage = " کلمه عبور و تکرار آن با یکدیگر مطابقت ندارند")]
    public string RePassword { get; set; }

    [Required(ErrorMessage = "کد امنیتی را وارد کنید")]
    public string CaptchaValue { get; set; }
}

public class ResetPasswordDto
{
    [Required(ErrorMessage = "کلمه عبور جاری را وارد کنید")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "کلمه عبور جدید را وارد کنید")]
    public string Password { get; set; }

    [Required(ErrorMessage = " تکرار کلمه عبور جدید را وارد کنید")]
    [Compare("Password", ErrorMessage = " کلمه عبور جدید و تکرار آن با یکدیگر مطابقت ندارند")]
    public string RePassword { get; set; }

    [Required(ErrorMessage = "کد امنیتی را وارد کنید")]
    public string CaptchaValue { get; set; }
}