
namespace Ecom.Core.DTO;

public record CategoryForCreationDTO
{
    public string Name { get; init; }
    public string Description { get; init; }
}

public record CategoryForUpdateDTO : CategoryForCreationDTO
{
    public int Id { get; set; }
}
