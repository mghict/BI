using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public enum SubscriptionType
{
    [Description("اشتراک")]
    PackageType,

    [Description("اشتراک شخصی سازی شده")]
    CustomType
}
