using HTTP.Requests;
using HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunesWebApp.Controlers
{
    public class UsersController : BaseControler
    {
        public IHttpResponse Login(IHttpRequest request) => this.View();

        public IHttpResponse Register(IHttpRequest request) => this.View();
    }
}
