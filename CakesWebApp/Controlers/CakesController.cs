using CakesWebApp.Extensions;
using CakesWebApp.Models;
using HTTP.Enums;
using HTTP.Requests;
using HTTP.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebServer.Results;

namespace CakesWebApp.Controlers
{
    public class CakesController : BaseControler
    {
        public IHttpResponse AddCakePage(IHttpRequest request)
        {
            return this.View("AddCakes");
        }

        public IHttpResponse AddCake(IHttpRequest request)
        {
            var productName = request.FormData["productName"].ToString().UrlDecode();
            var productPrice = decimal.Parse(request.FormData["productPrice"].ToString().UrlDecode());
            var productPictureUrl = request.FormData["productPictureUrl"].ToString().UrlDecode();

            var product = new Product
            {
                Name = productName,
                Price = productPrice,
                ImageUrl = productPictureUrl.UrlDecode()
            };

            this.Db.Products.Add(product);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: Log error
                return this.ServertError(e.Message);
            }

            return this.View("AddCakes");
        }

        public IHttpResponse SearchPage(IHttpRequest request)
        {
            var products = this.GetProducts(request);

            var sb = new StringBuilder();

            sb.Append(File.ReadAllText("Views/" + "Search" + ".html"))
              .Append(this.ListProducts(products));

            return new HtmlResult(sb.ToString(), HttpResponseStatusCode.Ok);
        }

        public IHttpResponse DoSearch(IHttpRequest request)
        {
            var products = this.GetProducts(request);

            if (products is null)
            {
                return new TextResult("Cake Not Found", HttpResponseStatusCode.NotFound);
            }

            var sb = new StringBuilder();

            sb.Append(File.ReadAllText("Views/" + "Search" + ".html"))
              .Append(this.ListProducts(products));

            return new HtmlResult(sb.ToString(), HttpResponseStatusCode.Ok);
        }

        public IHttpResponse GetCakeDetails(IHttpRequest request)
        {
            var id = int.Parse(request.QueryData["id"].ToString());
            var product = this.Db.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return this.BadRequestError("Cake not found.");
            }

            var viewBag = new Dictionary<string, string>();
            viewBag.Add("Name", product.Name);
            viewBag.Add("Price", product.Price.ToString());
            viewBag.Add("ImageUrl", product.ImageUrl);

            var allContent = this.GetViewContent("CakeDetails", viewBag);

            return new HtmlResult(allContent, HttpResponseStatusCode.Ok);
        }

        private IEnumerable<Product> GetProducts (IHttpRequest request)
        {
            var keyword = string.Empty;

            try
            {
                keyword = request.FormData["keyword"].ToString();
            }
            catch (Exception) { }

            if (keyword == string.Empty)
            {
                return this.Db.Products.ToList();
            }

            return this.Db.Products.Where(x => x.Name.Contains(keyword));
        }

        private string ListProducts(IEnumerable<Product> products)
        {
            var sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.Append($"<form method=\"post\" action=\"/order\"><a href=\"cakeDetails?id={product.Id}\">{product.Name}</a> ${product.Price:F2} <input type=\"submit\" value=\"Order\"/></form>");
            }

            return sb.ToString();
        }
    }
}
