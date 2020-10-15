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
    public class UserContollerTests:BaseControllerTest
    {
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

            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            Client.DefaultRequestHeaders.Add("CompanyID", encryptedCompanyID);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get()
        {
            var response = await Client.GetAsync("/en-US/users");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task IsEmailExists()
        {
            var response = await Client.GetAsync($"/en-US/users/IsEmailExists/{Guid.NewGuid()}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task IsUserNameExists()
        {
            var response = await Client.GetAsync("/en-US/users/IsUserNameExists/qermezkon@gmail.com");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}