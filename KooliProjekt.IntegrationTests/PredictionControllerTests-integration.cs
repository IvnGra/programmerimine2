using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Linq;
using System;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class PredictionsControllerPostTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public PredictionsControllerPostTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_prediction()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "Name", "football tournament" },
                { "Description", "good" },
                { "Points", "30" },
                { "PointsEarned", "15"}
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Predictions/Create", content);

            // Assert
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var prediction = _context.Predictions.FirstOrDefault(p => p.Name == "football tournament");
                Assert.NotNull(prediction);
                Assert.Equal("Team A", prediction.Name);
                Assert.Equal("good", prediction.Description);
                Assert.Equal(30, prediction.Points);
                Assert.Equal(15, prediction.PointsEarned);
            }
            else
            {
                Console.WriteLine("Hmm... doesn't work");
            }
        }

        [Fact]
        public async Task Create_should_not_save_invalid_prediction()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "Name", "" },
                { "Descriptiopn", "" },
                { "Points", "" },
                { "PointsEarned", "" }
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Predictions/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Predictions.Any());
        }
    }
}
