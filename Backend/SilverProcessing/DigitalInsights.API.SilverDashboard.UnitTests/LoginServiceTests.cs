using DigitalInsights.API.SilverDashboard.DTO;
using DigitalInsights.API.SilverDashboard.Services;
using NUnit.Framework;

namespace DigitalInsights.API.SilverDashboard.UnitTests
{
    public class LoginServiceTests
    {
        private LoginService loginService;

        [SetUp]
        public void Setup()
        {
            loginService = new LoginService();
        }

        [Test]
        public void TestCreatAndValidateToken()
        {
            var login = new AuthInfoDTO()
            {
                UserName = "123",
                Password = "123"
            };

            var tokenData = loginService.CreateToken(login);

            Assert.IsNotNull(tokenData);
            Assert.IsNotNull(tokenData.Token);

            Assert.IsTrue(loginService.ValidateToken(tokenData.Token));
        }
    }
}