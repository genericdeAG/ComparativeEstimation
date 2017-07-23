using System;
using System.Collections.Generic;
using CLAP;

namespace CeConsole
{
    class CLIPortal
    {
        [Verb(IsDefault = true)]
        public static void Connect(
            [Required, Aliases("address,endpoint,a,e"),DefaultValue("http://localhost:8080")] string endpointAddress) {
            new RESTProvider().Configure(endpointAddress);
            Console.WriteLine("Connected to {0}", endpointAddress);
        }


        [Verb(Aliases = "c,new,n")]
        public static void Create()
        {
            var userStories = new List<string>();
            while(true) {
                Console.Write("User story: ");
                var text = Console.ReadLine();
                if (string.IsNullOrEmpty(text)) break;
                userStories.Add(text);
            }

            if (userStories.Count > 0) {
                var server = new RESTProvider();
                var sprintId = server.Create_Sprint(userStories.ToArray());
                Console.WriteLine("sprint created with id '{0}'", sprintId);
            }
        }


        [Verb(Aliases = "d,del")]
        public static void Delete(
            [Required, Aliases("id")] string sprintId)
        {
            var server = new RESTProvider();
            server.Delete_Sprint(sprintId);
            Console.WriteLine("sprint deleted: {0}", sprintId);
        }


        [Verb(Aliases = "w")]
        public static void Watch(
            [Required, Aliases("id")] string sprintId)
        {
            var server = new RESTProvider();
            var totalWeighting = server.Get_total_weighting_for_sprint(sprintId);

            Console.WriteLine("Total weighting of sprint '{0}':", totalWeighting.SprintId);
            for (var i = 0; i < totalWeighting.Stories.Length; i++)
                Console.WriteLine($"  {i + 1}. {totalWeighting.Stories[i]}");
            Console.WriteLine("Number of votings: {0} at {1}", totalWeighting.NumberOfVotings, DateTime.Now);
        }


        [Verb(Aliases = "v")]
        public static void Vote(
            [Required, Aliases("id")] string sprintId)
        {
            var server = new RESTProvider();
            var comparisonpairs = server.ComparisonPairs(sprintId);
            Console.WriteLine("Compare the following user story pairs of sprint {0}:", comparisonpairs.SprintId);
            foreach(var cp in comparisonpairs.Pairs) {
                Console.WriteLine($"{cp.Id}.a) {cp.A}");
                Console.WriteLine($"{cp.Id}.b) {cp.B}");
                Console.WriteLine();
            }
        }
    }
}
