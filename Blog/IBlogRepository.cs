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
		
        //method Categories that returns all the categories from database
        IList<Category> Categories();

        //возвращает посты относящиеся к категории основанных на slug(UrlSlug)
        IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);
        //возвращает общее кол-во записей основанных на данной категории
        int TotalPostsForCategory(string categorySlug);
        //возвращает экземпляр категории
        Category Category(string categorySlug);

        //возвращает посты относящиеся к тегу основанных на slug(TagSlug)
        IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);
        //возвращает общее кол-во записей основанных на данном теге
        int TotalPostsForTag(string tagSlug);
        //возвращает экземпляр Tag
        Tag Tag(string tagSlug);

        //Возврат всех опубликованных сообщений по параметру строки тега.
        IList<Post> PostsForSearch(string search, int pageNo, int pageSize);
        // Вернет общее количество сообщений, принадлежащих определенному тегу
        int TotalPostsForSearch(string Search);

        //Возвращает пост основанный на трех параметрах(год, месяц, titleSlug)
        Post Post(int year, int month, string titleSlug);
    }

   
    
}
