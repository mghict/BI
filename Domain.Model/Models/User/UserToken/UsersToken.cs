using Moneyon.PowerBi.Domain.Model.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Modeling;

[Table("UsersTokens")]
public class UsersToken:AppEntity
{
    public string Token { get; set; }

    [Column(TypeName ="datetime")]
    public DateTime CreateDate { get; set; } = System.DateTime.UtcNow;
    public bool IsExpire { get; set; }=false;

    public long UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}
