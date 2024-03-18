using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Service.IServices;
using SmsIrRestfulNetCore;

namespace Moneyon.PowerBi.Infrastructure.Services
{

    //[AutoRegister(typeof(ISmsService) , new string[] { "Production" })]
    [AutoRegister(typeof(ISmsService))]
    public class SmsService : ISmsService
    {
        private string token;
        static DateTime? lastTokenCreationTime;


        public async Task SendOTP(string mobile , string code)
        {
            var restVerificationCode = new RestVerificationCode()
            {
                Code = code,
                MobileNumber = mobile
            };

            var token = GetToken();

            var restVerificationCodeRespone = new VerificationCode().Send(token, restVerificationCode);

            if (restVerificationCodeRespone.IsSuccessful)
            {
                return;
            }
            else
            {
                throw new BizException(BizExceptionCode.SmsSendFailed);
            }
        }



        private string GetToken()
        {
            if (token == null || (DateTime.Now - lastTokenCreationTime > TimeSpan.FromMinutes(20)))
            {
                SmsIrRestfulNetCore.Token tk = new SmsIrRestfulNetCore.Token();
                token = tk.GetToken("b25aff9fa21b7f78a156e364", "lending-project-sec-code");
                lastTokenCreationTime = DateTime.Now;
            }

            return token;
        }

    }
}
