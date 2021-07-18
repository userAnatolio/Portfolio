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

        public ActionResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel(_blogRepository);

                ViewBag.Test = "huuuuuuuuuuuuuuuuuuuy";
                return PartialView("_Sidebars", widgetViewModel);

		}

        public ViewResult Category(string category, int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, category, "Category", p);
            if (viewModel.Category == null)
            {
                throw new HttpExteption(404, "Category Not Found");
            }

            ViewBag.Title = String.Format(@"Latest posts on category {0}", viewModel.Category.Name);
            return View("List", viewModel);
        }

        public ViewResult Tag(string tag, int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, tag, "Tag", p);
            if (viewModel.Tag == null)
            {
                throw new HttpExteption(404, "Tag Not Found");
            }

            ViewBag.Title = String.Format(@"Latest posts on tagged {0}", viewModel.Tag.Name);
            return View("List", viewModel);
        }

        public ViewResult Search(string s, int p = 1)
        {
            ViewBag.Title = String.Format(@"Lists of posts found for search text ""{0}""", s);
            var viewModel = new ListViewModel(_blogRepository, s, "Search", p);
            return View("List", viewModel);
        }

        public ViewResult Post(int year, int month, string title)
        {
            var post = _blogRepository.Post(year, month, title);

            if (post == null)
                throw new HttpExteption(404, "Post not found");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpExteption(401, "The post is not published");

            return View(post);
        }

        //*******************************************************************************************************************

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
