using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Dependency;
using Newtonsoft.Json;

namespace AIKI.CO.HelpDesk.WebAPI.IntegrationTests
{
    [TestClass]
    public class UserControllerTests
    {
        private readonly HttpClient _client;

        public UserControllerTests()
        {
            _client = TestsHttpClient.Instance;
        }

        [TestMethod]
        public async Task AuthenticateTest()
        {
            var loginInfo = new
            {
                username = "moh.mehrnia.lavan@gmail.com",
                password = "Mveyma6303$"
            };
            string json = await Task.Run(() => JsonConvert.SerializeObject(loginInfo));
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/en-US/users/Authenticate", httpContent);
            Assert.Equals(response.StatusCode, HttpStatusCode.OK);
        }
    }
}