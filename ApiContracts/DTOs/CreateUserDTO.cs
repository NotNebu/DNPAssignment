namespace ApiContracts.DTOs
{
    // Data Transfer Object til at håndtere oprettelse af brugere
    public class CreateUserDTO
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}