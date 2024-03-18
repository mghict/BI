using System.ComponentModel.DataAnnotations;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class ReportDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string LatinName { get; set; }
    public string URL { get; set; }
    public bool IsEnabled { get; set; }
}

public class ReportCreateDto
{
    [MaxLength(255,ErrorMessage ="حداکثر کاراکتر نام 250 کاراکتر می باشد")]
    public string Title { get; set; }

    [MaxLength(150,ErrorMessage = "حداکثر کاراکتر نام لاتین 150 کاراکتر می باشد")]
    public string LatinName { get; set; }
    public string URL { get; set; }
}

public class ReportEditDto
{
    public long Id { get; set; }

    [MaxLength(255, ErrorMessage = "حداکثر کاراکتر نام 250 کاراکتر می باشد")]
    public string Title { get; set; }

    [MaxLength(150, ErrorMessage = "حداکثر کاراکتر نام لاتین 150 کاراکتر می باشد")]
    public string LatinName { get; set; }
    public string URL { get; set; }
    public bool IsEnabled { get; set; }
}

public class ReportShowDto
{
    public string Title { get; set; }
    public string LatinName { get; set; }
}