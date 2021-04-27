using Xunit;
using Hippo.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Hippo.Models;
using Moq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Hippo.Tests
{
    public class AppControllerTest
    {
        private readonly AppController controller;

        private readonly Account admin;

        private readonly Account user;

        public AppControllerTest()
        {
            admin = new Account
            {
                UserName = "admin",
                Id = "1",
                IsSuperUser = true,
            };
            user = new Account
            {
                UserName = "user",
                Id = "2"
            };
            var store = new Mock<IUserStore<Account>>();
            store.Setup(x => x.FindByIdAsync("1", CancellationToken.None))
            .ReturnsAsync(admin);
            store.Setup(x => x.FindByIdAsync("2", CancellationToken.None))
            .ReturnsAsync(user);
            var environment = new Mock<IWebHostEnvironment>();
            environment.Setup(x => x.ContentRootPath).Returns("/etc");
            var repository = new FakeAppRepository();
            var userManager = new UserManager<Account>(store.Object, null, null, null, null, null, null, null, null);
            controller = new AppController(repository, userManager, environment.Object);
        }

        [Fact]
        public void GetApps()
        {
            var fakeIdentity = new GenericIdentity(admin.UserName);
            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(fakeIdentity)
                }
            };
            controller.ControllerContext = context;

            var viewResult = controller.Index();
            Assert.NotNull(viewResult);
        }
    }
}