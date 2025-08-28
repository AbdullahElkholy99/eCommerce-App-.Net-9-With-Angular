
using Microsoft.AspNetCore.Http;

namespace Ecom.Core.DTO;

public record ProductForCreationDTO
{
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal NewPrice { get; set; }
    public decimal OldPrice { get; set; }
    public int CategoryId { get; set; }
    public IFormFileCollection Photos { get; set; }

}

public record ProductForUpdateDTO : ProductForCreationDTO
{
    public int Id { get; set; }
}
public record ProductDTO 
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal NewPrice { get; set; }
    public decimal OldPrice { get; set; }
    public virtual List<PhotoDTO> Photos { get; set; }
    public string CategoryName { get; set; }
}
