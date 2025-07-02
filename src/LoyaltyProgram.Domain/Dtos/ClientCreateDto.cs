namespace LoyaltyProgram.Domain.Dtos
{
    public record ClientCreateDto(
        string FirstName,
        string LastName,
        string Address,
        string Email,
        string PhoneNumber
    );
}