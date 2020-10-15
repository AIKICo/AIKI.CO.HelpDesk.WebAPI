using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AIKI.CO.HelpDesk.WebAPI.IntegrationTests
{
    public class BaseControllerTest
    {
        protected readonly HttpClient Client;
        protected string Token;
        protected string encryptedCompanyID;

        public TestContext TestContext { get; set; }

        protected BaseControllerTest()
        {
            Client = TestsHttpClient.Instane;
        }
    }
}