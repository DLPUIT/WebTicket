

namespace WebTicket
{
    using SquirrelFramework.Domain.Model;
    [Collection("User")]
    public class UserModel : DomainModel
    {
        public string Name { get; set; }
        public string Telphone { get; set; }
        public string RuiJieId { get; set; }

        public string Gender { get; set; }
        public string Dormitory { get; set; }

        public string MacAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsRememberMe { get; set; }

    }
}
