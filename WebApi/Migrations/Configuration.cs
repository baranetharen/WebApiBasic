namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApi.Models.CampContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WebApi.Models.CampContext";
        }

        protected override void Seed(WebApi.Models.CampContext context)
        {
            if (!context.Camps.Any())
            {
                context.Camps.AddOrUpdate(x => x.CampId,
                  new Camp()
                  {
                      CampId = 1,
                      Moniker = "ATL2018",
                      Name = "Atlanta Code Camp",
                      EventDate = new DateTime(2018, 10, 18),
                      Location = new Location()
                      {
                          VenueName = "Atlanta Convention Center",
                          Address1 = "123 Main Street",
                          CityTown = "Atlanta",
                          StateProvince = "GA",
                          PostalCode = "12345",
                          Country = "USA"
                      },
                      Length = 1,
                      Talks = new Talk[]
                    {
            new Talk
            {
              TalkId = 1,
              Title = "Entity Framework From Scratch",
              Abstract = "Entity Framework from scratch in an hour. Probably cover it all",
              Level = 100,
              Speaker = new Speaker
              {
                SpeakerId = 1,
                FirstName = "Shawn",
                LastName = "Wildermuth",
                BlogUrl = "http://wildermuth.com",
                Company = "Wilder Minds LLC",
                CompanyUrl = "http://wilderminds.com",
                GitHub = "shawnwildermuth",
                Twitter = "shawnwildermuth"
              }
            },
            new Talk
            {
              TalkId = 2,
              Title = "Writing Sample Data Made Easy",
              Abstract = "Thinking of good sample data examples is tiring.",
              Level = 200,
              Speaker = new Speaker
              {
                SpeakerId = 2,
                FirstName = "Resa",
                LastName = "Wildermuth",
                BlogUrl = "http://shawnandresa.com",
                Company = "Wilder Minds LLC",
                CompanyUrl = "http://wilderminds.com",
                GitHub = "resawildermuth",
                Twitter = "resawildermuth"
              }
            }
                    }
                  });
            }
        }
    }
}
