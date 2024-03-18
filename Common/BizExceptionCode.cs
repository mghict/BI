using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Common
{
    public static class BizExceptionCode
    {
        public static readonly string SmsSendFailed = "ارسال پیامک با خطا مواجه شده است";
        public static readonly string SendOtpLimit = "برای ارسال مجدد رمز یکبار مصرف دو دقیقه صبر کنید";
        public static readonly string ReceiveOtpLimit = "کد یکبار مصرف منتضی شده است، لطفا مجددا آن را دریافت نمایید";
        public static readonly string UserExists = "کاربر با مشخصات وارد شده وجود دارد";
        public static readonly string UserNotFound = "کاربر با مشخصات وارد شده یافت نشد";
        public static readonly string OtpIsNotValid = "کد یکبار مصرف صحیح نمی باشد";
        public static readonly string NameIsExists = "نام وارد شده وجود دارد";
        public static readonly string DataNotFound = "اطلاعات یافت نشد";
        public static readonly string PasswordInvalid = "کلمه عبور صحیح نمی باشد";
        public static readonly string InsufficientAccountBalance = "موجودی حساب صحیح نمی باشد";
        public static readonly string InvalidTransaction = "تراکنش صحیح نمی باشد";
        public static readonly string InvalidStatus = "وضعیت صحیح نمی باشد";
        public static readonly string PermissionNotNull = "دسترسی به گزارشات انتخاب نشده است";
        public static readonly string PersonWalletNotFound = "کیف پول یافت نشد";
        public static readonly string PaymentStatusInValid = "وضعیت صحیح نیست";
        public static readonly string User_NotPermission = "کاربر به عملیات فوق دسترسی ندارد";
        public static readonly string TicketHasCloseStatus = "وضعیت تیکت بسته شده است";
        public static readonly string TicketNotFound = "تیکت یافت نشد";
        public static readonly string General_DeleteNotComplete = "حذف اطلاعات امکان پذیر نمی باشد";
    }
}
