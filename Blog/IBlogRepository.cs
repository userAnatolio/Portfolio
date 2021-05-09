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
        int TotalPosts();
    }
}
