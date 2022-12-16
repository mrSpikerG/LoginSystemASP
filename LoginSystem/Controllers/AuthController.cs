using LoginSystem.Models;
using Microsoft.AspNetCore.Mvc;
using UAParser;

namespace LoginSystem.Controllers {

    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : Controller {


        public static List<AuthModel> Users { get; set; } = new List<AuthModel>();

        [HttpPost(Name = "Register")]
        public IActionResult Register(string login, string password, string mail) {

            int loginSize = login.Length;
            int passwordSize = password.Length;

            if (loginSize <= 4 || loginSize > 12) {
                return StatusCode(403);
            }

            if (passwordSize <= 4 || passwordSize > 16) {
                return StatusCode(403);
            }


            try {
                AuthModel user = Users.First(x => x.Login.Equals(login) && x.Mail.Equals(mail));
            } catch (Exception ex) {
                Users.Add(new AuthModel(login, password, mail));
                return Ok("Ok");
            }

            return StatusCode(403);

        }


        [HttpPost(Name = "LoginByName")]
        public IActionResult LoginbyName(string login, string password) {


            if (!Users.Any(x => x.Login.Equals(login))) {
                return StatusCode(403);
            }

            var user = Users.First(x => x.Login.Equals(login));
            string shaPassword = AuthUtilController.ComputeSha256Hash(password);

            if (user.Password.Equals(shaPassword)) {
                return Ok("Ok");
            }
            return StatusCode(403);
        }

        [HttpPost(Name = "LoginByMail")]
        public IActionResult LoginByMail(string mail, string password) {


            if (!Users.Any(x => x.Mail.Equals(mail))) {
                return StatusCode(403);
            }

            var user = Users.First(x => x.Mail.Equals(mail));
            string shaPassword = AuthUtilController.ComputeSha256Hash(password);

            if (user.Password.Equals(shaPassword)) {
                return Ok("Ok");
            }
            return StatusCode(403);
        }

    }
}
