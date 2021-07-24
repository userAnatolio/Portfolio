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

        //******************Реализация методов интерфейса для извлечения записей на основе категории**********************
        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            var posts = _blogContext.Posts
                                  .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                                  .OrderByDescending(p => p.PostedOn)
                                  .Skip(pageNo * pageSize)
                                  .Take(pageSize)
                                  .Include(p => p.Category)
                                  .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _blogContext.Posts
                  .Where(p => postIds.Contains(p.Id))
                  .OrderByDescending(p => p.PostedOn)
                  .Include(p => p.Tags)
                  .ToList();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _blogContext.Posts
                          .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                          .Count();
        }

        public Category Category(string categorySlug)
        {
            return _blogContext.Categories.FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }
        //******************************************************************************************************************

        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            var posts = _blogContext.Posts
                              .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                              .OrderByDescending(p => p.PostedOn)
                              .Skip(pageNo * pageSize)
                              .Take(pageSize)
                              .Include(p => p.Category)
                              .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _blogContext.Posts
                          .Where(p => postIds.Contains(p.Id))
                          .OrderByDescending(p => p.PostedOn)
                          .Include(p => p.Tags)
                          .ToList();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return _blogContext.Posts
                        .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                        .Count();
        }

        public Tag Tag(string tagSlug)
        {
            return _blogContext.Tags.FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }
        //*******************************************************************************************************************

        public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            var posts = _blogContext.Posts
                                  .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                                  .OrderByDescending(p => p.PostedOn)
                                  .Skip(pageNo * pageSize)
                                  .Take(pageSize)
                                  .Include(p => p.Category)
                                  .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _blogContext.Posts
                  .Where(p => postIds.Contains(p.Id))
                  .OrderByDescending(p => p.PostedOn)
                  .Include(p => p.Tags)
                  .ToList();
        }

        public int TotalPostsForSearch(string search)
        {
            return _blogContext.Posts
                    .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                    .Count();
        }
        //*******************************************************************************************************************
        public Post Post(int year, int month, string titleSlug)
        {
            var query = _blogContext.Posts
                                .Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug))
                                .Include(p => p.Category);

            query.Include(p => p.Tags);

            return query.Single();
        }

        public IList<Category>Categories()
        {
            var query = _blogContext.Categories;
            return query.ToList();
        }
        //*******************************************************************************************************************

        public IList<Post>Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            IList<Post> posts;
            IList<int> postIds;
            switch(sortColumn)
            {
                case "Title":
                    if(sortByAscending)
                    {
                        posts = _blogContext.Posts
                        .OrderBy(p => p.Title)
                        .Skip(pageNo * pageSize)
                        .Take(pageSize)
                        .Include(p => p.Category)
                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _blogContext.Posts
                          .Where(p => postIds.Contains(p.Id))
                          .OrderBy(p => p.Title)
                          .Include(p => p.Tags)//FetchMany???
                          .ToList();
                    }
                    else
                    {
                        posts = _blogContext.Posts
                        .OrderByDescending(p => p.Title)
                        .Skip(pageNo * pageSize)
                        .Take(pageSize)
                        .Include(p => p.Category)
                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _blogContext.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderByDescending(p => p.Title)
                                          .Include(p => p.Tags)
                                          .ToList();
                    }

                    break;
                case "Published":
                    if (sortByAscending)
                    {
                        posts = _blogContext.Posts
                                        .OrderBy(p => p.Published)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .Include(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _blogContext.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderBy(p => p.Published)
                                          .Include(p => p.Tags)
                                          .ToList();
                    }
                    else
                    {
                        posts = _blogContext.Posts
                                        .OrderByDescending(p => p.Published)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .Include(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _blogContext.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderByDescending(p => p.Published)
                                          .Include(p => p.Tags)
                                          .ToList();
                    }
                    break;
                case "PostedOn":
                    if (sortByAscending)
                    {
                        posts = _blogContext.Posts
                                        .OrderBy(p => p.PostedOn)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .Include(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _blogContext.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderBy(p => p.PostedOn)
                                          .Include(p => p.Tags)
                                          .ToList();
                    }
                    else
                    {
                        posts = _blogContext.Posts
                                        .OrderByDescending(p => p.PostedOn)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .Include(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _blogContext.Posts
                                        .Where(p => postIds.Contains(p.Id))
                                        .OrderByDescending(p => p.PostedOn)
                                        .Include(p => p.Tags)
                                        .ToList();
                    }
                    break;
                case "Modified":
                    if (sortByAscending)
                    {
                        posts = _blogContext.Posts
                                        .OrderBy(p => p.Modified)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .Include(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _blogContext.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderBy(p => p.Modified)
                                          .Include(p => p.Tags)
                                          .ToList();
                    }
                    else
                    {
                        posts = _blogContext.Posts
                                        .OrderByDescending(p => p.Modified)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .Include(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _blogContext.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderByDescending(p => p.Modified)
                                          .Include(p => p.Tags)
                                          .ToList();
                    }
                    break;
                case "Category":
                    if (sortByAscending)
                    {
                        posts = _blogContext.Posts
                                        .OrderBy(p => p.Category.Name)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .Include(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _blogContext.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderBy(p => p.Category.Name)
                                          .Include(p => p.Tags)
                                          .ToList();
                    }
                    else
                    {
                        posts = _blogContext.Posts
                                        .OrderByDescending(p => p.Category.Name)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .Include(p => p.Category)
                                        .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _blogContext.Posts
                                          .Where(p => postIds.Contains(p.Id))
                                          .OrderByDescending(p => p.Category.Name)
                                          .Include(p => p.Tags)
                                          .ToList();
                    }
                    break;
                default:
                    posts = _blogContext.Posts.OrderByDescending(p => p.PostedOn)
                                    .Skip(pageNo * pageSize)
                                    .Take(pageSize)
                                    .Include(p => p.Category)
                                    .ToList();

                    postIds = posts.Select(p => p.Id).ToList();

                    posts = _blogContext.Posts.Where(p => postIds.Contains(p.Id))
                                      .OrderByDescending(p => p.PostedOn)
                                      .Include(p => p.Tags)
                                      .ToList();
                    break;
            }

            return posts;
        }
        //*******************************************************************************************************************
    }
}