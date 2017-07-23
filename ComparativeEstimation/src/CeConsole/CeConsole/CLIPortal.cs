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
            Console.WriteLine("del sprint {0}", sprintId);
        }

        [Verb(Aliases = "w")]
        public static void Watch(
            [Required, Aliases("id")] string sprintId)
        {
            Console.WriteLine("watch sprint {0}", sprintId);
        }

        [Verb(Aliases = "v")]
        public static void Vote(
            [Required, Aliases("id")] string sprintId)
        {
            Console.WriteLine("vote for {0}", sprintId);
        }
    }
}
