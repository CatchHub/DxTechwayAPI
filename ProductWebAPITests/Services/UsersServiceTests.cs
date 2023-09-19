using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductAppAPI.Entities;
using ProductAppAPI.Helpers;
using ProductAppAPI.Services;
using ProductListApp.Persistence.Contexts;
using ProductWebAPI.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAppAPI.Services.Tests
{
    [TestClass()]
    public class UsersServiceTests
    {
        

        [TestMethod()]
        public async Task Password_Contains_Special_Character_RegisterTest()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ProductListAppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ProductListAppDbContext(dbContextOptions))
            {
                var user = new User
                {
                    UserName = "newuser1",
                    Password = PasswordHasher.HashPassword("Password123"),
                    Email = "newuser1@example.com",
                    FirstName = "Ali1",
                    LastName = "Dogan1",
                    Role = "User1",
                    Token = ""
                };
                var userService = new UsersService(dbContext);
                // Act
                var result = await userService.RegisterUser(user);
                // Assert
                Assert.AreEqual(typeof(OkObjectResult), result.GetType());
            }
        }

        [TestMethod()]
        public async Task AuthenticateTest()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ProductListAppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ProductListAppDbContext(dbContextOptions))
            {

                var testUser = new User
                {
                    Id = new Guid(),
                    UserName = "testuser",
                    Email = "newuser@example.com",
                    FirstName = "Test",
                    LastName = "User",
                    Role = "User",
                    Password = PasswordHasher.HashPassword("Password123!"),
                    Token = string.Empty
                };
                await dbContext.Users.AddAsync(testUser);
                await dbContext.SaveChangesAsync();


                //var dbContextMock = new Mock<ProductListAppDbContext>(dbContextOptions);
                var userService = new UsersService(dbContext);

                var userObject = new UserValidateDTO
                {
                    UserName = "testuser",
                    Password = "Password123!"
                };

                // Act
                var result = await userService.Authenticate(userObject);
                // Assert
                Assert.AreEqual(typeof(OkObjectResult), result.GetType());
                //Assert.Fail();
            }


        }

    }
}