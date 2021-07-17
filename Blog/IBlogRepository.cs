using System;
using System.Collections.Generic;
using System.Text;
using Blog.Objects;


namespace Blog
{
    public interface IBlogRepository
    {
        //Метод Posts используется для последних опубликованных записей
        IList<Post> Posts(int pageNo, int pageSize);
        //TotalPosts используется для возврата общего кол-ва всех опубликованных сообщений
        int TotalPosts(bool checkIsPublished = true);
        //возвращает посты относящиеся к категории основанных на slug(UrlSlug)
        IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);
        //возвращает общее кол-во записей основанных на данной категории
        int TotalPostsForCategory(string categorySlug);
        //возвращает экземпляр категории
        Category Category(string categorySlug);
    }

   
    
}
