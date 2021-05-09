using System;
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
            //var obj = _blogContext.Posts.Find();
            
            List<Post> posts = new List<Post>();

            return posts;
        }

        public int TotalPosts()
        {
            return 2;
        }
    }
}
