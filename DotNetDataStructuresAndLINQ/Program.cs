using System;

namespace DotNetDataStructuresAndLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();

            QueryStore qs = new QueryStore(wc);



           

            Console.ReadKey();
        }
    }
}
