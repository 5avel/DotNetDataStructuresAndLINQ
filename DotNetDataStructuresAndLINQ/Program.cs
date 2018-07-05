using System;

namespace DotNetDataStructuresAndLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var posts = new WebClient().GetAddressList();
            Console.WriteLine(posts.Count);

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
