using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        [TestInitialize]
        public void CreateWebFactory()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
        }

        [TestMethod]
        public void AddUser_UserIsAdded()
        {
            var testService = new TestableUserService();

            testService.AddUser_Return = new User
            {
                FirstName = "Raymond",
                LastName = "Shiner"
            };

            var mapper = Mapper.Instance;
            var controller = new UserController(testService, mapper);

            var viewModel = new UserInputViewModel
            {
                FirstName = "Raymond",
                LastName = "Shiner"
            };

            var result = controller.Post(viewModel);

            Assert.IsTrue(result is CreatedAtActionResult);
            Assert.AreEqual(viewModel.FirstName, testService.AddUser_User.FirstName);
            Assert.AreEqual(viewModel.LastName, testService.AddUser_User.LastName);

        }

        [TestMethod]
        public async Task AddUserViaApi_UserIsAdded()
        {
            var client = Factory.CreateClient();

            var userViewModel = new UserInputViewModel
            {
                FirstName = "Raymond",
                LastName = "Shiner"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(userViewModel), Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/user", stringContent);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();

            var resultViewModel = JsonConvert.DeserializeObject<UserViewModel>(result);

            Assert.AreEqual(userViewModel.FirstName, resultViewModel.FirstName);
        }

        [TestMethod]
        public async Task AddUserViaApi_FailsDueToMissingFirstName()
        {
            var client = Factory.CreateClient();
            var viewModel = new UserInputViewModel
            {
                FirstName = "",
                LastName = "Shiner"
            };

            var stringContent =
                new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/user", stringContent);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();

            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(result);

            var errors = problemDetails.Extensions["errors"] as JObject;

            var firstError = (JProperty) errors.First;

            var dontKnowWhatThisIs = firstError.Value[0];

            Assert.AreEqual("The FirstName field is required.", ((JValue)dontKnowWhatThisIs).Value);
        }
    }
}
