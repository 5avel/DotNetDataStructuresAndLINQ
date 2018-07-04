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
        private IEnumerable<User> _collection;
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

             var postsComments = posts.GroupJoin(comments, p => p.Id, c => c.PostId,
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
            
            var usersPostComments = users.GroupJoin(postsComments, u => u.Id, p => p.UserId,
                (u, ps) => new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    CreatedAt = u.CreatedAt,
                    Email = u.Email,
                    Avatar = u.Avatar,
                    Posts = ps
                });

            var usersPostCommentsTodos = usersPostComments.GroupJoin(todos, u => u.Id, t => t.UserId,
                (u, t) => new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    CreatedAt = u.CreatedAt,
                    Email = u.Email,
                    Avatar = u.Avatar,
                    Posts = u.Posts,
                    Todos = t
                });

            _collection = usersPostCommentsTodos.GroupJoin(address, u => u.Id, a => a.UserId,
                (u, a) => new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    CreatedAt = u.CreatedAt,
                    Email = u.Email,
                    Avatar = u.Avatar,
                    Posts = u.Posts,
                    Todos = u.Todos,
                    Address = a
                });
        }

    }
}
