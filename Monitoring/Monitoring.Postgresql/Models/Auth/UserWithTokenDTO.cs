namespace Monitoring.Postgresql.Models.Auth
{
    public class UserWithTokenDTO: TokenApiModel
    {
        public UserDTO User { get; set; }
    }
}
