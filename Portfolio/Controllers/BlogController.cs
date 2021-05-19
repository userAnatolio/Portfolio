using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog;
using Blog.Objects;
using Portfolio.Models;
using System.Web;
using System.Runtime.Serialization;

namespace Portfolio.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        //Данный метод возвращает страницу с определенным номером
        //p=1 значение первой страницы указано по умолчанию или последний пост...
        public ViewResult Posts(int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, p);

            ViewBag.Title = "Latest Posts";
            return View("List", viewModel);
        }

        public ViewResult Category(string category, int p)
        {
            var viewModel = new ListViewModel(_blogRepository, category, p);
            if(viewModel.Category == null)
            {
                throw new HttpExteption(404, "Category Not Found");


            }

            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""",
                        viewModel.Category.Name);
            return View("List", viewModel);
        }
    }

    [Serializable]
    internal class HttpExteption : Exception
    {
        private int v1;
        private string v2;

        public HttpExteption()
        {
        }

        public HttpExteption(string message) : base(message)
        {
        }

        public HttpExteption(int v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public HttpExteption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpExteption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
