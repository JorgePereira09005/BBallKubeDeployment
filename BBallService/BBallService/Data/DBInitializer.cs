using BBallService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBallService.Data
{
    public static class DBInitializer
    {
        public static void Initialize(BBallContext context)
        {
            context.Database.EnsureCreated();

            if (context.Players.Any())
            {
                return;  
            }

            var teams = new Team[]
            {
                new Team
                {
                    Name = "Minesota Timberwolves",
                    FoundingDate = DateTime.Parse("1956-09-01"),
                    City = "Minneappois",
                    State = "Minnesota"
                },
                new Team
                {
                    Name = "Denver Nuggets",
                    FoundingDate = DateTime.Parse("1946-09-01"),
                    City = "Denver",
                    State = "Colorado"
                },
                new Team
                {
                    Name = "Springfield Isotopes",
                    FoundingDate = DateTime.Parse("1989-09-01"),
                    City = "Springfield",
                    State = "Somewhere"
                },
                new Team
                {
                    Name = "New York Knicks",
                    FoundingDate = DateTime.Parse("1976-09-01"),
                    City = "New York City",
                    State = "New York"
                }
            };

            foreach (Team t in teams)
            {
                context.Teams.Add(t);
            }
            context.SaveChanges();

            var players = new Player[]
            {
                new Player {
                    Position = Position.SF,
                    FirstName = "Lebron",
                    LastName = "James",
                    Salary = 35321000,
                    SigningDate = DateTime.Parse("2010-09-01"),
                    Number = 69,
                    IsAllStar = true, 
                    TeamID = teams.Single(t => t.Name == "Denver Nuggets" ).ID
                },
                new Player {
                    Position = Position.PG,
                    FirstName = "Chris",
                    LastName = "Paul",
                    Salary = 88765647,
                    SigningDate = DateTime.Parse("2021-09-01"),
                    Number = 18,
                    IsAllStar = true,
                    TeamID = teams.Single(t => t.Name == "Springfield Isotopes" ).ID
                },
                new Player {
                    Position = Position.SG,
                    FirstName = "Scrub",
                    LastName = "McScrub",
                    Salary = 100,
                    SigningDate = DateTime.Parse("2015-09-01"),
                    Number = 80,
                    IsAllStar = false,
                    TeamID = teams.Single(t => t.Name == "New York Knicks" ).ID
                },
                new Player {
                    Position = Position.C,
                    FirstName = "Karl",
                    LastName = "Towns",
                    Salary = 67890977,
                    SigningDate = DateTime.Parse("2016-09-01"),
                    Number = 26,
                    IsAllStar = true,
                    TeamID = teams.Single(t => t.Name == "Minesota Timberwolves" ).ID
                },
                new Player {
                    Position = Position.PF,
                    FirstName = "McIntosh",
                    LastName = "Bubbly",
                    Salary = 67890977,
                    SigningDate = DateTime.Parse("2016-09-01"),
                    Number = 69,
                    IsAllStar = false,
                    TeamID = teams.Single(t => t.Name == "Minesota Timberwolves" ).ID
                },
                new Player {
                    Position = Position.PG,
                    FirstName = "LeGM",
                    LastName = "LeBaron",
                    Salary = 111222333,
                    SigningDate = DateTime.Parse("1995-09-01"),
                    Number = 42,
                    IsAllStar = false,
                    TeamID = teams.Single(t => t.Name == "New York Knicks" ).ID
                },

            };

            foreach (Player p in players)
            {
                context.Players.Add(p);
            }
            context.SaveChanges();

            var agents = new Agent[]
            {
                new Agent
                {
                    Name = "Rich Paul"
                },
                new Agent
                {
                    Name = "Picasso"
                },
                new Agent
                {
                    Name = "Babe Ruth"
                },
                new Agent
                {
                    Name = "Nessie"
                }
            };

            foreach (Agent a in agents)
            {
                context.Agents.Add(a);
            }
            context.SaveChanges();

            var representations = new Representation[]
            {
                new Representation {
                    PlayerID = players.Single(s => s.FirstName == "Lebron" && s.LastName == "James").ID,
                    AgentID = agents.Single(c => c.Name == "Rich Paul" ).ID,
                    Comission = 10
                },
                new Representation {
                    PlayerID = players.Single(s => s.FirstName == "Chris" && s.LastName == "Paul").ID,
                    AgentID = agents.Single(c => c.Name == "Picasso" ).ID,
                    Comission = 15
                },
                new Representation {
                    PlayerID = players.Single(s => s.FirstName == "Lebron" && s.LastName == "James").ID,
                    AgentID = agents.Single(c => c.Name == "Picasso" ).ID,
                    Comission = 0
                },
                new Representation {
                    PlayerID = players.Single(s => s.FirstName == "Scrub" && s.LastName == "McScrub").ID,
                    AgentID = agents.Single(c => c.Name == "Babe Ruth" ).ID,
                    Comission = 10
                },
                new Representation {
                    PlayerID = players.Single(s => s.FirstName == "Karl" && s.LastName == "Towns").ID,
                    AgentID = agents.Single(c => c.Name == "Nessie" ).ID,
                    Comission = 10
                },
                new Representation {
                    PlayerID = players.Single(s => s.FirstName == "McIntosh" && s.LastName == "Bubbly").ID,
                    AgentID = agents.Single(c => c.Name == "Nessie" ).ID,
                    Comission = 10
                },
                new Representation {
                    PlayerID = players.Single(s => s.FirstName == "LeGM" && s.LastName == "LeBaron").ID,
                    AgentID = agents.Single(c => c.Name == "Babe Ruth" ).ID,
                    Comission = 3
                },
                new Representation {
                    PlayerID = players.Single(s => s.FirstName == "LeGM" && s.LastName == "LeBaron").ID,
                    AgentID = agents.Single(c => c.Name == "Rich Paul" ).ID,
                    Comission = 3
                },

            };

            foreach (Representation r in representations)
            {
                context.Representations.Add(r);
            }
            context.SaveChanges();
        }
    }
}
