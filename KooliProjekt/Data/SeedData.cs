using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            GenerateUsers(context);
            GenerateMatch(context);
            GenerateTournaments(context);
            GenerateTournaments(context);
            GenerateTeams(context);
            GeneratePredictions(context);
        }
        // Method to generate and seed match data
        public static void GenerateMatch(ApplicationDbContext context)
        {
            // Ensure that tournaments are seeded first
            var tournaments = context.Tournaments.ToList(); // Get all tournaments
            var teams = context.Teams.Take(2).ToList(); // Get at least two teams

            // Ensure we have at least two teams and two tournaments
            if (teams.Count >= 2 && tournaments.Count >= 2)
            {
                // Select the first two tournaments (or select based on your logic)
                var tournament1 = tournaments[0]; // First tournament
                var tournament2 = tournaments[1]; // Second tournament

                // Create matches
                var matches = new List<Match>
        {
            new Match
            {
                Team1Id = teams[0].Id,
                Team2Id = teams[1].Id,
                TournamentId = tournament1.Id,  // Use the valid tournament ID (first tournament)
                Team1_goals = 5,
                Team2_goals = 3,
                Match_time = DateTime.Now,
                Name = "UEFA Europe League"
            },
            new Match
            {
                Team1Id = teams[1].Id,
                Team2Id = teams[0].Id,
                TournamentId = tournament2.Id,  // Use the valid tournament ID (second tournament)
                Team1_goals = 4,
                Team2_goals = 2,
                Match_time = DateTime.Now,
                Name = "UEFA Europe League"
            }
        };

                context.Matchs.AddRange(matches);  // Add the matches to the context
                context.SaveChanges();  // Save matches to the database
            }
            else
            {
                Console.WriteLine("Unable to create matches due to missing teams or tournaments.");
            }
        }
        

        public static void GenerateTeams(ApplicationDbContext context)
        {
            // Check if teams already exist to avoid duplicate data
            if (context.Teams.Any())
            {
                return;
            }

            // Add sample teams
            var teams = new List<Team>
            {
                new Team { TeamName = "Barcelona", TeamDescription = "cool" },
                new Team { TeamName = "FCflora", TeamDescription = "cool" },
                new Team { TeamName = "Manchester City", TeamDescription = "cool" },
                new Team { TeamName = "Manchester United", TeamDescription = "cool" },
                new Team { TeamName = "Legion", TeamDescription = "cool" },
                new Team { TeamName = "PSG", TeamDescription = "cool" },
                new Team { TeamName = "Chelsea", TeamDescription = "cool" }
            };

            context.Teams.AddRange(teams);
            context.SaveChanges();
        }

        // Method to generate and seed user data
        public static void GenerateUsers(ApplicationDbContext context)
        {
            // Check if users already exist
            if (context.Users.Any())
            {
                return;
            }

            // Add sample users
            var users = new List<User>
            {
                new User { Username = "john_doe", IsAdmin = true, UserEmail="" },
                new User { Username = "jane_doe", IsAdmin = false, UserEmail="" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();  // Save users to the database
        }

        // Method to generate and seed predictions
        public static void GeneratePredictions(ApplicationDbContext context)
        {
            // Check if predictions already exist to avoid duplicate data
            if (context.Predictions.Any())
            {
                return;
            }

            // Ensure that there are matches available before adding predictions
            var matches = context.Matchs.ToList(); // Get all the matches

            if (matches.Count > 0) // Ensure there are matches in the database
            {
                // Create sample predictions for existing matches
                var predictions = new List<Prediction>
        {
            new Prediction
            {
                Team1_predicted_goals = 5,
                Team2_predicted_goals = 2,
                PointsEarned = 7,
                MatchId = matches[0].Id,  // Assign a valid MatchId from the database
                Points = 5,
                Name = "public prediction",
                User = context.Users.First()
            },
            new Prediction
            {
                Team1_predicted_goals = 6,
                Team2_predicted_goals = 4,
                PointsEarned = 10,
                MatchId = matches[1].Id,  // Assign a valid MatchId from the database
                Points = 5,
                Name = "public prediction",
                User = context.Users.Skip(1).First()
            }
        };

                context.Predictions.AddRange(predictions);  // Add the predictions to the context
                context.SaveChanges();  // Save predictions to the database
            }
            else
            {
                Console.WriteLine("Unable to create predictions due to missing matches.");
            }
        }

        public static void GenerateTournaments(ApplicationDbContext context)
        {
            // Check if tournaments already exist to avoid duplicate data
            if (context.Tournaments.Any())
            {
                return;
            }

            // Add sample tournaments
            var tournaments = new List<Tournament>
    {
        new Tournament { TournamentName = "Eesti Jalgpalli Turniir"},
        new Tournament { TournamentName = "Worldwide Football Tournament"}
    };

            context.Tournaments.AddRange(tournaments);
            context.SaveChanges();  // Save tournaments to the database
        }
    } 
}

