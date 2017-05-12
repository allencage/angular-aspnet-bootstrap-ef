namespace Repo.EF.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repo.EF.MessageBoardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Repo.EF.MessageBoardContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (context.Topics.Count() > 0) return;
            var topics = new List<Topic>
            {
                new Topic
                {
                    Created = DateTime.UtcNow,
                    Body = "First Topic",
                    Replies = new List<Reply>
                    {
                        new Reply
                        {
                            Body = "First reply",
                            Created = DateTime.UtcNow
                        },
                        new Reply
                        {
                            Body = "Second reply",
                            Created = DateTime.UtcNow
                        }
                    }
                },
                new Topic
                {
                    Created = DateTime.UtcNow,
                    Body = "Second Topic",
                    Replies = new List<Reply>
                    {
                        new Reply
                        {
                            Body = "First reply",
                            Created = DateTime.UtcNow
                        }
                    }
                }
            };

            context.Topics.AddRange(topics);
            context.SaveChanges();
        }
    }
}
