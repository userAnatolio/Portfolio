using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Portfolio.AccountModels;
using Portfolio.ManageModels;
using Blog;
using Blog.Objects;
using Newtonsoft.Json;
using Portfolio;

namespace Portfolio.Controllers
{
    public class ManageController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IBlogRepository _blogRepository;
        public ManageController(IBlogRepository blogRepository, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult ShowGrid()
        {
            return View();
        }

        public ContentResult Posts(JqInViewModel jqParams)
        {
            var posts = _blogRepository.Posts(jqParams.page - 1, jqParams.rows, jqParams.sidx, jqParams.sord == "asc");
            var totalPosts = _blogRepository.TotalPosts(false);

            return Content(JsonConvert.SerializeObject(new
            {
                page = jqParams.page,
                records = totalPosts,
                rows = posts,
                total = Math.Ceiling(Convert.ToDouble(totalPosts) / jqParams.rows)
            }, new CustomDateTimeConverter()), "application/json");
        }

        public IActionResult TestPosts(JqInViewModel jqParams)
        {
            IList<Post> posts = _blogRepository.Posts(2 - 1, 1, "Title", jqParams.sord == "asc");
            var totalPosts = _blogRepository.TotalPosts(false);

            return View(posts);
        }
    }
}
