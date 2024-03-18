namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class OTP : AppEntity
{

    public OTPType Type { get; set; }

    public string MobileNumber { get; set; }
    public long UserId { get; set; }

    public string Value { get; set; }

    public DateTime SendDate { get; set; }

    public bool IsUsed { get; set; }
    public Guid Token { get; set; }

    public User User { get; set; }
    public OTP()
    {
            
    }


    public OTP(OTPType type, string mobileNumber, long userId)
    {
        Token = Guid.NewGuid();
        SendDate = DateTime.Now;
        Value = new Random().Next(1000, 99999).ToString("00000");
        Type = type;
        MobileNumber = mobileNumber;
        IsUsed = false;
        UserId = userId;
    }

    public OTP(OTPType type, string mobileNumber , string value, long userId)
    {
        Token = Guid.NewGuid();
        SendDate = DateTime.Now;
        Type = type;
        MobileNumber = mobileNumber;
        Value = value;
        IsUsed = false;
        UserId = userId;
    }

    


}

public enum OTPType
{
    SignIn = 1,
    ForgetPassword = 2
}
