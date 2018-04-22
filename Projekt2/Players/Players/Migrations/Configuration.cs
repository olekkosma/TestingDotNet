namespace Players.Migrations
{
    using Players.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Players.DAL.PlayerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Players.DAL.PlayerContext context)
        {
            var players = new List<Player>
            {
                new Models.Player {FirstMidName = "Leo", LastName="Messi" },
                new Models.Player {FirstMidName = "Euzebiusz", LastName="Smolarek" },
                new Models.Player {FirstMidName = "Cristiano", LastName="Ronaldo" },
                new Models.Player {FirstMidName = "Luis", LastName="Suarez" },
                new Models.Player {FirstMidName = "Philiphe", LastName="Coutinho" },
            };
            players.ForEach(s => context.Players.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var matches = new List<Match>
            {
                 new Match {City = "Barcelona",  Date = DateTime.Parse("2017-12-11") ,Result="2:2"},
                 new Match {City = "Sevilla",  Date = DateTime.Parse("2017-07-15"),Result="4:2" },
                 new Match {City = "Madrit",  Date = DateTime.Parse("2017-07-22"),Result="1:0" },
            };
            matches.ForEach(s => context.Matches.AddOrUpdate(p => p.City, s));
            context.SaveChanges();

            var statistics = new List<Statistic>
            {
                new Statistic { PlayerID = players.Single(s => s.LastName=="Messi").ID,
                MatchID = matches.Single(c => c.City == "Barcelona").MatchID,
                goals = 2,
                assists = 1,
                minutes = 90},
                 new Statistic { PlayerID = players.Single(s => s.LastName=="Ronaldo").ID,
                MatchID = matches.Single(c => c.City == "Barcelona").MatchID,
                goals = 0,
                assists = 0,
                minutes = 78},
                  new Statistic { PlayerID = players.Single(s => s.LastName=="Suarez").ID,
                MatchID = matches.Single(c => c.City == "Barcelona").MatchID,
                goals = 0,
                assists = 3,
                minutes = 90},
                   new Statistic { PlayerID = players.Single(s => s.LastName=="Coutinho").ID,
                MatchID = matches.Single(c => c.City == "Barcelona").MatchID,
                goals = 1,
                assists = 0,
                minutes = 90},
                    new Statistic { PlayerID = players.Single(s => s.LastName=="Messi").ID,
                MatchID = matches.Single(c => c.City == "Madrit").MatchID,
                goals = 3,
                assists = 0,
                minutes = 90},
                     new Statistic { PlayerID = players.Single(s => s.LastName=="Messi").ID,
                MatchID = matches.Single(c => c.City == "Sevilla").MatchID,
                goals = 2,
                assists = 1,
                minutes = 90},
                     new Statistic { PlayerID = players.Single(s => s.LastName=="Ronaldo").ID,
                MatchID = matches.Single(c => c.City == "Sevilla").MatchID,
                goals = 0,
                assists = 1,
                minutes = 90},
                     new Statistic { PlayerID = players.Single(s => s.LastName=="Ronaldo").ID,
                MatchID = matches.Single(c => c.City == "Madrit").MatchID,
                goals = 0,
                assists = 0,
                minutes = 90}
            };

            foreach (Statistic e in statistics)
            {
                var statisticInDataBase = context.Statistics.Where(
                    s =>
                         s.Player.ID == e.PlayerID &&
                         s.Match.MatchID == e.MatchID).SingleOrDefault();
                if (statisticInDataBase == null)
                {
                    context.Statistics.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
