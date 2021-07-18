using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Blog;
using Blog.Data;
using Blog.Objects;
using Portfolio.Models;


namespace Portfolio.ViewComponents
{
    public class WidgetViewComponent : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;
        public WidgetViewComponent(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var widgetViewModel = new WidgetViewModel(_blogRepository);
            return View("Sidebar", widgetViewModel);
        }

    }
}
