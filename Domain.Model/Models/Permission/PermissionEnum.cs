using System.ComponentModel;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public enum PermissionEnum
{
    //Permission
    //[Description("گزارش شماره 1")]
    //Report1 = 10,
    //[Description("گزارش شماره 2")]
    //Report2 = 11,
    //[Description("گزارش شماره 3")]
    //Report3 = 12,

    //Access

    // Packages Managment
    [Description("نمایش بسته های اشتراک")]
    PackagesView = 200,

    [Description("ایجاد بسته های اشتراک")]
    PackagesCreate = 201,

    [Description("ویرایش بسته های اشتراک")]
    PackagesEdit = 202,

    // Role Managment
    [Description("نمایش نقشها")]
    RolesView =210,

    [Description("ایجاد نقش")]
    RolesCreate =211,

    [Description("ویرایش نقش")]
    RolesEdit =212,

    // User Managment
    [Description("نمایش کاربران")]
    UsersView = 220,

    [Description("تغییر رمزعبور کاربران")]
    UsersResetPassword =221,

    [Description("تغییر نقش کاربران")]
    UsersChangeRoles =222,

    // ReportManagment
    [Description("نمایش گزارشات")]
    ReportView = 230,

    [Description("ایجاد گزارشات")]
    ReportCreate = 231,

    [Description("ویرایش گزارشات")]
    ReportEdit = 232,

    // PaymentManagment
    [Description("مشاهده فیش واریز")]
    ReceiptPaymentView = 240,
    [Description("تایید فیش واریز")]
    ReceiptPaymentConfirm =241,

    // SubscriptionManagment
    [Description("مشاهده اشتراک کاربران")]
    SubscriptionView = 250,

    //------------------------------

}
