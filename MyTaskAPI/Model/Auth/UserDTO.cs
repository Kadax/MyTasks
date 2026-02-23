namespace MyTaskAPI.Model.Auth
{
    public class UserDTO
    {
        public string name { set; get; }
        public DateTime? expirationDate { set; get; }
        public string email { set; get; }
        public List<string> roles { set; get; }

    }
}
