using LoginSystem.Controllers;

namespace LoginSystem.Models {
    public class AuthModel {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public DateTime TimeCreation { get; set; }

        public AuthModel(string login, string password, string mail) {
            this.Login = login;
            this.Password = AuthUtilController.ComputeSha256Hash(password);
            this.Mail = mail;
            this.TimeCreation = DateTime.Now;
        }
    }
}
