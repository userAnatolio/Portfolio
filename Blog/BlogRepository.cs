using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Blog.Objects;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog
{
    public class BlogRepository : IBlogRepository
    {
        private BlogContext _blogContext = new BlogContext();

        public IList<Post> Posts(int pageNo, int pageSize)
        {
            var posts = _blogContext.Posts
                            .Where(p => p.Published)
                            .OrderByDescending(p => p.PostedOn)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Include(p=>p.Category)
                            .ToList();

      var postIds = posts.Select(p => p.Id).ToList();

      return _blogContext.Posts
            .Where(p => postIds.Contains(p.Id))
            .OrderByDescending(p => p.PostedOn)
            .Include(p=>p.Tags)
            .ToList();
        }

        public int TotalPosts(bool checkIsPublished = true)
        {
            return _blogContext.Posts.Where(p => !checkIsPublished || p.Published == true).Count();
        }


    }
}
