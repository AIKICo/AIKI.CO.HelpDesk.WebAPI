using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AIKI.CO.HelpDesk.WebAPI.IntegrationTests
{
    [TestClass]
    public class UserContollerTests
    {
        private readonly HttpClient Client;
        private string Token;
        private string encryptedCompanyID;

        public TestContext TestContext { get; set; }

        public UserContollerTests()
        {
            Client = TestsHttpClient.Instane;
        }

        [TestMethod]
        public async Task Authenticate()
        {
            var loginInfo = new
            {
                Username = Environment.GetEnvironmentVariable("testUserName"),
                Password = Environment.GetEnvironmentVariable("testPassword")
            };
            StringContent content =
                new StringContent(JsonConvert.SerializeObject(loginInfo), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/en-US/users/authenticate", content);
            var member = JsonConvert.DeserializeObject<MemberResponse>(await response.Content.ReadAsStringAsync());
            Token = member.token;
            encryptedCompanyID = member.encryptedCompnayId;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}