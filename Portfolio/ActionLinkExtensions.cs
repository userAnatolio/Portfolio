using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Portfolio.Models;
using Blog.Data;
using Blog.Objects;
using Microsoft.Extensions.DependencyInjection;
using System.Web;



namespace Portfolio
{
    public static class ActionLinkExtensions
    {
        public static HtmlString PostLink(this IHtmlHelper helper, Post post)
        {
            return (HtmlString)helper.ActionLink(post.Title, "Post", "Blog",
                new
                {           // route parameters
                    year = post.PostedOn.Year,
                    month = post.PostedOn.Month,
                    title = post.UrlSlug
                },
                new
                {           // html attributes
                    title = post.Title
                });

        }

        public static HtmlString CategoryLink(this IHtmlHelper helper,
           Category category)
        {
            return (HtmlString)helper.ActionLink(category.Name, "Category", "Blog",
                new
                {
                    category = category.UrlSlug
                },
                new
                {
                    title = String.Format("See all posts in {0}", category.Name)
                });
        }

        public static HtmlString TagLink(this IHtmlHelper helper, Tag tag)
        {
            return (HtmlString)helper.ActionLink(tag.Name, "Tag", "Blog", new { tag = tag.UrlSlug },
                new
                {
                    title = String.Format("See all posts in {0}", tag.Name)
                });
        }


    }
}
