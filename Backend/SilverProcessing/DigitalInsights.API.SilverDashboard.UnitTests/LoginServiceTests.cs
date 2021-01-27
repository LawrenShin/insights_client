using DigitalInsights.API.SilverDashboard.DTO;
using DigitalInsights.API.SilverDashboard.Security;
using DigitalInsights.API.SilverDashboard.Services;
using DigitalInsights.DB.Silver;
using NUnit.Framework;

namespace DigitalInsights.API.SilverDashboard.UnitTests
{
    public class LoginServiceTests
    {
        private LoginService loginService;

        [SetUp]
        public void Setup()
        {
            loginService = new LoginService(new SilverContext());
        }

        [Test]
        public void TestCreateAndValidateToken()
        {
            var login = new AuthInfoDTO()
            {
                UserName = "testUser",
                Password = "1qaz@@wsx"
            };

            var tokenData = loginService.CreateToken(login);

            Assert.IsNotNull(tokenData);
            Assert.IsNotNull(tokenData.Token);

            Assert.IsTrue(loginService.ValidateToken(tokenData.Token));
        }

        [Test]
        public void PasswordHelperTest()
        {
            var password = "mko)),lp-";
            var result = PasswordHelper.HashPassword(password);
            Assert.AreEqual(PasswordVerificationResult.Success, PasswordHelper.VerifyHashedPassword(result, password));
        }
    }
}