namespace BugTrackingSystem.Data.Model
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public byte UserRoleID { get; set; }
    }
}