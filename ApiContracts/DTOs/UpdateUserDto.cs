namespace ApiContracts.DTOs
{
    /// <summary>
    /// Data Transfer Object to handle the update of user information.
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}