using System;

namespace DotNetDataStructuresAndLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();

            QueryStore qs = new QueryStore(wc);

            Console.WriteLine("\n\n Query 1\n");
            var query1 = qs.GetPostCommentsCountByUserId(5);
            foreach(var t in query1)
            {
                Console.WriteLine($"PostId - {t.Item1.Id}, Comments count - {t.Item2}");
            }

            Console.WriteLine("\n\n Query 2\n");
            var query2 = qs.GetPostCommentsBodyLessThan50ByUserId(3);
            foreach (var p in query2)
            {
                Console.WriteLine(p.Body);
            }

            Console.WriteLine("\n\n Query 3\n");
            var query3 = qs.GetTodoIdNameByUserId(3);
            foreach (var p in query3)
            {
                Console.WriteLine($"id - {p.Item1}, name - {p.Item2}");
            }

            Console.WriteLine("\n\n Query 4\n");
            var query4 = qs.GetUsetsSortByNameAndTodosSortByNameDesc();
            foreach (var user in query4)
            {
                Console.WriteLine($"user name - {user.Name}");
                foreach(var todo in user.Todos)
                {
                    Console.WriteLine($"todo name {todo.name}");
                }
            }


            Console.WriteLine("\n\n Query 5\n");
            var query5 = qs.GetUserById(3);
            Console.WriteLine($"user id - {query5.Item1.Id},\n" +
                $" LastPostID - {query5.Item2.Id},\n" +
                $" LastPostCommentsCount - {query5.Item3},\n" +
                $" TodosCount - {query5.Item4},\n" +
                $" MostPopularPostId -  {query5.Item5.Id},\n" +
                $" MostPopularByLikesPostId - {query5.Item6.Id} ");

            Console.WriteLine("\n\n Query 6\n");
            var query6 = qs.GetPostById(3);
            Console.WriteLine(
                $" PostID - {query6.Item1.Id},\n" +
                $" LongComment - {query6.Item2.Body},\n" +
                $" MostPopularByLikesComment - {query6.Item3.Likes},\n" +
                $" CommentCount - {query6.Item4} ");




            Console.ReadKey();
        }
    }
}
