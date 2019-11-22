using System.Collections.Generic;

namespace LinqAndLamdaExpressions
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var allUsers = ReadUsers("users.json");
            var allPosts = ReadPosts("posts.json");

            // 1 - find all users having email ending with ".net".
            var users1 = from u in allUsers
                         where u.Email.EndsWith(".net")
                         select u;

            var users2 = allUsers.Where(x => x.Email.EndsWith(".net"));

            var emails = allUsers.Select(x => x.Email).ToList();

            // 2 - find all posts for users having email ending with ".net".


            // 3 - print number of posts for each user.

            Dictionary<int, int> userPostsAgg = new Dictionary<int, int>();
            foreach (var post in allPosts)
            {
                var userId = post.UserId;

                if (userPostsAgg.ContainsKey(userId))
                {
                    userPostsAgg[userId]++;
                }
                else
                {
                    userPostsAgg.Add(userId, 1);
                }
            }

            foreach (var value in userPostsAgg)
            {
                Console.WriteLine($"{value.Key} - {value.Value}");
            }

            var result = allPosts.GroupBy(p => p.UserId).Select(g => new
            {
                UserId = g.Key,
                NumberOfPosts = g.Count()
            });


            // 4 - find all users that have lat and long negative.


            // 5 - find the post with longest body.


            // 6 - print the name of the employee that have post with longest body.


            // 7 - select all addresses in a new List<Address>. print the list.


            // 8 - print the user with min lat
            var usersOrderedByLat = from u in allUsers
                                    orderby u.Address.Geo.Lat
                                    select u;

            var user = usersOrderedByLat.First();

            var minLat = allUsers.Min(x => x.Address.Geo.Lat);

            // 9 - print the user with max long


            // 10 - create a new class: public class UserPosts { public User User {get; set}; public List<Post> Posts {get; set} }
            //    - create a new list: List<UserPosts>
            //    - insert in this list each user with his posts only


            // 11 - order users by zip code
            var usersOrderedByZipCode = allUsers.OrderBy(x => x.Address.Zipcode).ToList();

            // 12 - order users by number of posts
        }

        private static List<Post> ReadPosts(string file)
        {
            return ReadData.ReadFrom<Post>(file);
        }

        private static List<User> ReadUsers(string file)
        {
            return ReadData.ReadFrom<User>(file);
        }
    }
}
