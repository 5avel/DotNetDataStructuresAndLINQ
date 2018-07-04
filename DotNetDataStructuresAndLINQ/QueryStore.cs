using DotNetDataStructuresAndLINQ.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DotNetDataStructuresAndLINQ
{
    public class QueryStore
    {
        private IWebClient _webClient;
        public QueryStore(IWebClient webClient)
        {
            _webClient = webClient;
            CreateAssociateEntities();
        }

        private void CreateAssociateEntities()
        {
            var users = _webClient.GetUsersList();
            var posts = _webClient.GetPostsList();
            var comments = _webClient.GetCommentsList();
            var todos = _webClient.GetTodosList();
            var address = _webClient.GetAddressList();

             var test = posts.GroupJoin(comments, p => p.Id, c => c.PostId,
                (p, c) =>  new Post()
                {
                    Id = p.Id,
                    CreatedAt = p.CreatedAt,
                    Title = p.Title,
                    Body = p.Body,
                    UserId = p.UserId,
                    Likes = p.Likes,
                    Comments = c
                });
            //users.GroupJoin<User>

            Console.WriteLine("edfdf");
            //var collection = users.GroupJoin(posts, u => u.Id, p => p.UserId,
            //    (u, ps) => new
            //    {
            //        Id = u.Id,
            //        Name = u.Name,
            //        CreatedAt = u.CreatedAt,
            //        Email = u.Email,
            //        Avatar = u.Avatar,
            //        Posts = ps
            //    });

            // работает но не совсем коректно))
            //var collection = (from u in users
            //                 join p in posts on u.Id equals p.UserId
            //                 join c in comments on p.Id equals c.PostId
            //                 join t in todos on u.Id equals t.UserId
            //                 join a in address on u.Id equals a.UserId
            //                 group new { u, p, c, t, a } by u into usr
            //                 select new
            //                 {
            //                     Id = usr.Key.Id,
            //                     Name = usr.Key.Name,
            //                     CreatedAt = usr.Key.CreatedAt,
            //                     Email = usr.Key.Email,
            //                     Avatar = usr.Key.Avatar,

            //                     Posts = usr.Select(x => new
            //                     {
            //                         id = x.p.Id,
            //                         CreatedAt = x.p.CreatedAt,
            //                         Title = x.p.Title,
            //                         Body = x.p.Body,
            //                         Likes = x.p.Likes,
            //                         Comments = usr.Select(y => new
            //                         {
            //                             Id = y.c.Id,
            //                             CreatedAt = y.c.CreatedAt,
            //                             Body = y.c.Body,
            //                             Likes = y.c.Likes

            //                         }).ToList()
            //                     }).ToList(),
            //                     Todos = usr.Select(x => new
            //                     {
            //                         id = x.t.Id,
            //                         CreatedAt = x.t.CreatedAt,
            //                         Name = x.t.name,
            //                         IsComplete = x.t.IsComplete,

            //                     }).ToList(),
            //                     Address = usr.Select(x => new
            //                     {
            //                         Id = x.a.Id,
            //                         Country = x.a.Country,
            //                         City = x.a.City,
            //                         Street = x.a.Street,
            //                         Zip = x.a.Zip
            //                     }).ToList()
            //                 }).ToList();

            var collectionUsersTodos =
               (from u in users
                join t in todos on u.Id equals t.UserId
                group new { u, t } by u into u
                select new
                {
                    Id = u.Key.Id,
                    Name = u.Key.Name,
                    CreatedAt = u.Key.CreatedAt,
                    Email = u.Key.Email,
                    Avatar = u.Key.Avatar,
                    Todos = u.Select(x => new
                    {
                        id = x.t.Id,
                        CreatedAt = x.t.CreatedAt,
                        Name = x.t.name,
                        IsComplete = x.t.IsComplete,
                    }).ToList(),
                }).ToList();

            var collectionPostsComments =
                (from p in posts
                 join c in comments on p.Id equals c.PostId
                 group new { p, c } by p into p
                 select new
                 {
                     Id = p.Key.Id,
                     CreatedAt = p.Key.CreatedAt,
                     Title = p.Key.Title,
                     Body = p.Key.Body,
                     UserId = p.Key.UserId,
                     Likes = p.Key.Likes,
                     Comments = p.Select(x => new
                     {
                         id = x.c.Id,
                         CreatedAt = x.c.CreatedAt,
                         Body = x.c.Body,
                         UserId = x.c.UserId,
                         Likes = x.c.Likes
                     }).ToList(),
                 }).ToList();

            var collectionUsersTodosPostsComments =
               (from ut in collectionUsersTodos
                join pc in collectionPostsComments on ut.Id equals pc.UserId
                group new { ut, pc } by ut into ut
                select new
                {
                    Id = ut.Key.Id,
                    Name = ut.Key.Name,
                    CreatedAt = ut.Key.CreatedAt,
                    Email = ut.Key.Email,
                    Avatar = ut.Key.Avatar,
                    Todos = ut.Key.Todos,
                    Posts = ut.Select(x => new
                    {
                        id = x.pc.Id,
                        CreatedAt = x.pc.CreatedAt,
                        Title = x.pc.Title,
                        Body = x.pc.Body,
                        Likes = x.pc.Likes,
                        Comments = x.pc.Comments
                    }).ToList(),
                }).ToList();


            var collectionUsersTodosPostsCommentsAddress =
   (from u in collectionUsersTodosPostsComments
    join a in address on u.Id equals a.UserId
    group new { u, a } by u into u
    select new
    {
        Id = u.Key.Id,
        Name = u.Key.Name,
        CreatedAt = u.Key.CreatedAt,
        Email = u.Key.Email,
        Avatar = u.Key.Avatar,
        Todos = u.Key.Todos,
        Posts = u.Key.Posts,
        Address = u.Select(x => new
        {
            id = x.a.Id,
            Country = x.a.Country,
            City = x.a.City,
            Street = x.a.Street,
            Zip = x.a.Zip
        }).ToList(),
    }).ToList();

            //var rrr = collection.Where(x => x.Posts.ToList().Count > 1).ToList();
        }

    }
}
