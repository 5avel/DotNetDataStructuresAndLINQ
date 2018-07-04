using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetDataStructuresAndLINQ.Model
{
    public class User
    {
        public int Id { set; get; }
        public DateTime CreatedAt { set; get; }
        public string Name { set; get; }
        public string Avatar { set; get; }
        public string Email { set; get; }

    }
}
