namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class CreateTicketCategoryDto
{
    public string Name { get; set; }
    public Guid? ParentId { get; set; } = null;
}
