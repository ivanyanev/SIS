using CakesWebApp.Services;
using HTTP.Cookies;
using HTTP.Enums;
using HTTP.Requests;
using HTTP.Responses;
using IRunesWebApp.Models;
using System;
using System.Linq;
using WebServer.Results;

namespace IRunesWebApp.Controlers
{
    public class UsersController : BaseControler
    {
        private readonly HashService hashService;
        
        public UsersController()
        {
            this.hashService = new HashService();
        }
        
        public IHttpResponse Login(IHttpRequest request) => this.View();

        public IHttpResponse PostLogin(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString();
            var password = request.FormData["password"].ToString();
            var hashedPassword = hashService.Hash(password);

            var user = this.Context.Users
                .Where(x => x.Username == username &&
                x.HashedPassword == hashedPassword);

            if (user == null)
            {
                return new RedirectResult("/login");
            }

            var response = new RedirectResult("/home/index");

            this.SignInUser(username, response, request);

            return response;
        }

        public IHttpResponse Register(IHttpRequest request) => this.View();

        public IHttpResponse PostRegister(IHttpRequest request)
        {
            var userName = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmPassword"].ToString();

            // Validate credentials
            //if (string.IsNullOrWhiteSpace(userName) || userName.Length < 4)
            //{
            //    return new BadRequestResult("Please provide valid username with length of 4 or more characters.");
            //}

            //if (this.Context.Users.Any(x => x.Username == userName))
            //{
            //    return this.BadRequestError("User with the same name already exists.");
            //}

            //if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            //{
            //    return this.BadRequestError("Please provide password of length 6 or more.");
            //}

            if (password != confirmPassword)
            {
                return new BadRequestResult("Passwords do not match.", HttpResponseStatusCode.SeeOther);
            }

            // Generate password hash
            var hashedPassword = this.hashService.Hash(password);

            // Create user
            var user = new User
            {
                Username = userName,
                HashedPassword = hashedPassword
            };
            this.Context.Users.Add(user);

            try
            {
                this.Context.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: Log error
                return new BadRequestResult(e.Message, HttpResponseStatusCode.InternalServerError);
            }

            // TOOD: Login
            var response = new RedirectResult("/");

            this.SignInUser(userName, response, request);

            // Redirect to home page
            return response;
        }
    }
}
