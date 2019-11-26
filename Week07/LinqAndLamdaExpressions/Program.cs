namespace LinqAndLamdaExpressions
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            List<User> allUsers = ReadUsers("users.json");
            List<Post> allPosts = ReadPosts("posts.json");

            #region Demo

            // 1 - find all users having email ending with ".net".
            var users1 = from user in allUsers
                         where user.Email.EndsWith(".net")
                         select user;

            var users11 = allUsers.Where(user => user.Email.EndsWith(".net"));

            IEnumerable<string> userNames = from user in allUsers
                                            select user.Name;

            var userNames2 = allUsers.Select(user => user.Name);

            //            foreach (var value in userNames2)
            //            {
            //                Console.WriteLine(value);
            //            }

            IEnumerable<Company> allCompanies = from user in allUsers
                                                select user.Company;

            var users2 = from user in allUsers
                         orderby user.Email
                         select user;

            var netUser = allUsers.First(user => user.Email.Contains(".net"));
            //            Console.WriteLine(netUser.Username);

            #endregion

            // 2 - find all posts for users having email ending with ".net".
            var usersIdsWithDotNetMails = from user in allUsers
                                          where user.Email.EndsWith(".net")
                                          select user.Id;

            var posts = allPosts.Where(post => usersIdsWithDotNetMails.Contains(post.UserId));

            //            foreach (var post in posts)
            //            {
            //                Console.WriteLine(post.Id + " " + "user: " + post.UserId);
            //            }

            // 3 - print number of posts for each user.

            var nrOfPosts = allUsers.Join(allPosts, u => u.Id, p => p.UserId, (u, p) => new { u, p })
                        .GroupBy(@t => new { @t.u.Username, @t.p.UserId }, @t => @t.u)
                        .Select(all => new { userName = all.Key.Username, Posts = all.Count(), });

            //            foreach (var nrOfPost in nrOfPosts)
            //            {
            //                Console.WriteLine($"{nrOfPost.userName} {nrOfPost.Posts}");
            //            }

            // 4 - find all users that have lat and long negative.

            var ugn = allUsers.Where(allUser => allUser.Address.Geo.Lat < 0 && allUser.Address.Geo.Lng < 0)
                .Select(allUser => allUser.Username);


            var z = from user in allUsers
                    where user.Address.Geo.Lat < 0 && user.Address.Geo.Lng < 0
                    select user;
            //            foreach (var u in ugn)
            //            {
            //                Console.WriteLine(u);
            //            }

            // 5 - find the post with longest body.
            var postLogestBody = (allPosts.Select(p => p.Body.Length)).Max();
            var postWithLogestBody = (allPosts.Where(p => p.Body.Length == postLogestBody));
            //            foreach (var post in postWithLogestBody)
            //            {
            //                Console.WriteLine(post.Body);
            //            }



            // 6 - print the name of the employee that have post with longest body.

            var nameEmpWithLogBody = allUsers
                .Join(allPosts, user => user.Id, post => post.UserId, (user, post) => new { user, post })
                .Where(@t => @t.post.Body.Length == postLogestBody)
                .Select(@t => @t.user.Username);
            //            foreach (var x in nameEmpWithLogBody)
            //            {
            //                Console.WriteLine(x);
            //            }




            // 7 - select all addresses in a new List<Address>. print the list.
            List<Address> allAddresses = (allUsers.Select(us => us.Address)).ToList();
            //                foreach (var allAddress in allAddresses)
            //                {
            //                    Console.WriteLine(allAddress.Geo.Lat);
            //                    Console.WriteLine(allAddress.Geo.Lng);
            //                    Console.WriteLine(allAddress.City);
            //                    Console.WriteLine(allAddress.Street);
            //                    Console.WriteLine(allAddress.Suite);
            //                    Console.WriteLine(allAddress.Zipcode);
            //                    Console.WriteLine();
            //            }

            // 8 - print the user with min lat
            var minLat = (allAddresses.Select(add => add.Geo.Lat)).Min();
            var userWithMinLat = allUsers.Where(user => user.Address.Geo.Lat.Equals(minLat));

            // 9 - print the user with max long
            var maxLong = (allAddresses.Select(add => add.Geo.Lat)).Max();
            var userWithMaxLongt = allUsers.Where(user => user.Address.Geo.Lat.Equals(maxLong));


            // 10 - create a new class: public class UserPosts { public User User {get; set}; public List<Post> Posts {get; set} }
            //    - create a new list: List<UserPosts>
            //    - insert in this list each user with his posts only

            List<UserPosts> userPost = new List<UserPosts>();
            var xxx = (from post in allPosts
                       join user in allUsers on post.UserId equals user.Id
                       group user by new
                       {
                           user,
                           post

                       }
                into allPostsForUser
                       select new
                       {
                           allPostsForUser.Key.user,
                           allPostsForUser.Key.post
                       }).ToList();

            foreach (var t in xxx)
            {
                var up = new UserPosts();
                up.User = t.user;
                up.Posts.Add(t.post);
                userPost.Add(up);
            }

            foreach (var up in userPost)
            {
                Console.WriteLine(up.User.Username);

                foreach (var p in up.Posts)
                {
                    Console.WriteLine(p.Body);
                }

                Console.WriteLine();
                Console.WriteLine();

            }




            // 11 - order users by zip code
            var usersOrderdByZip = allUsers.OrderBy(x => x.Address.Zipcode);


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
