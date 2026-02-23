namespace MyTaskAPI.Model.Auth
{
    public class SignInDTO
    {
        public string email { set; get; }
        public string password { set; get; }
        public bool rememberMe { set; get; }

    }
}
