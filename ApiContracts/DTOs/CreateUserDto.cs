namespace ApiContracts.DTOs
{
    /// <summary>
    /// Data Transfer Object to handle the creation of users.
    /// </summary>
    public class CreateUserDto
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public required string Password { get; set; }
    }
}