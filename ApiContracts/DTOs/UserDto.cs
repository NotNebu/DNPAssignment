namespace ApiContracts.DTOs
{
    /// <summary>
    /// Data Transfer Object to handle user information.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the Id of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }
    }
}