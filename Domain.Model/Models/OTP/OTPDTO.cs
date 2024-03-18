using DocumentFormat.OpenXml.Presentation;
using System.ComponentModel.DataAnnotations;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class OTPDto
{
    public OTPType Type { get; set; }

    public string MobileNumber { get; set; }

    public string Value { get; set; }

    public DateTime SendDate { get; set; }

    public bool IsUsed { get; set; }
    public Guid Token { get; set; }


}

public class SendOTPRequest
{
    [Required(ErrorMessage = " شماره موبایل را وارد کنید")]
    [StringLength(11 , MinimumLength =11 , ErrorMessage = "شماره موبایل باید 11 رقم باشد")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = " کد کپچا را وارد کنید")]
    public string CaptchaValue { get; set; }
}

public class SendOTPResponse
{
    public bool IsNewUser { get; set; } = false;
    public Guid Token { get; set; }
}
