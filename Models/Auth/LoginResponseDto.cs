namespace APPC.Models.Auth
{
    public class LoginResponseDto
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PlatformRole { get; set; }
        public string? Token { get; set; }

    }
}
