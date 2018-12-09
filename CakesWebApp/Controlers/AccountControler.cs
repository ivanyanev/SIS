using CakesWebApp.Models;
using CakesWebApp.Services;
using HTTP.Cookies;
using HTTP.Requests;
using HTTP.Responses;
using System;
using System.Linq;
using WebServer.Results;

namespace CakesWebApp.Controlers
{
    public class AccountControler : BaseControler
    {
        private IHashService hashService;

        public AccountControler()
        {
            this.hashService = new HashService();
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View("Register");
        }

        public IHttpResponse DoRegister(IHttpRequest request)
        {
            var userName = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmPassword"].ToString();

            // Validate credentials
            if (string.IsNullOrWhiteSpace(userName) || userName.Length < 4)
            {
                return this.BadRequestError("Please provide valid username with length of 4 or more characters.");
            }

            if (this.Db.Users.Any(x => x.Username == userName))
            {
                return this.BadRequestError("User with the same name already exists.");
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                return this.BadRequestError("Please provide password of length 6 or more.");
            }

            if (password != confirmPassword)
            {
                return this.BadRequestError("Passwords do not match.");
            }

            // Generate password hash
            var hashedPassword = this.hashService.Hash(password);

            // Create user
            var user = new User
            {
                Name = userName,
                Username = userName,
                Password = hashedPassword
            };
            this.Db.Users.Add(user);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: Log error
                return this.ServertError(e.Message);
            }

            // TOOD: Login

            // Redirect to home page
            return new RedirectResult("/"); 
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View("Login");
        }

        public IHttpResponse DoLogin(IHttpRequest request)
        {
            // Validate user
            var userName = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();

            var hashedPassword = this.hashService.Hash(password);

            var user = this.Db.Users.FirstOrDefault(x => 
                x.Username == userName &&
                x.Password == hashedPassword);

            if (user == null)
            {
                return this.BadRequestError("Invalid username or password");
            }
            
            // Save cookie/session with the user
            var cookieContent = this.UserCookieService.GetUserCookie(user.Username);

            var response = new RedirectResult("/hello");
            var cookie = new HttpCookie(".auth-cakes", cookieContent, 7) { HttpOnly = true };
            response.Cookies.Add(cookie);

            // Redirect
            return response;
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(".auth-cakes"))
            {
                return new RedirectResult("/");
            }
            var cookie = request.Cookies.GetCookie(".auth-cakes");
            cookie.Delete();

            var response = new RedirectResult("/");
            response.Cookies.Add(cookie);
            return response;    
        }
    }
}
