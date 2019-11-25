using LinqAndLamdaExpressions.Models;
using System.Collections.Generic;

namespace LinqAndLamdaExpressions
{
    public class UserPosts
    {
        public UserPosts()
        {
            Posts = new List<Post>();
        }
        public User User { get; set; }
        public List<Post> Posts { get; set; }
    }
}