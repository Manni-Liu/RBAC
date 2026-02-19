using Xunit;
using RBAC.Application.Users;
using RBAC.Domain.Entities;
using RBAC.Application.Common;
using RBAC.Tests.Common;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RBAC.Tests.Application.Users
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUser_ShouldAddUser()
        {
            // Arrange
            var db = TestDbContextFactory.Create();
            var service = new UserService(db);
            
            var dto = new CreateUserDto("testuser", "123456", "test@test.com", "123456789");

            // Act
            var userId = await service.CreateAsync(1, dto);

            // Assert
            var user = await db.Users.FindAsync(userId);
            Assert.NotNull(user);
            Assert.Equal("testuser", user.Username);
            Assert.Equal("test@test.com", user.Email);
        }

        [Fact]
        public async Task CreateUser_ShouldThrow_WhenUsernameExists()
        {
            // Arrange
            var db = TestDbContextFactory.Create();
            db.Users.Add(new User(1, "admin", "hash"));
            await db.SaveChangesAsync();
            var service = new UserService(db);

            var dto = new CreateUserDto("admin", "123456", "test@test.com", null);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => service.CreateAsync(1, dto));
        }

        [Fact]
        public async Task GetList_ShouldReturnAllUsers()
        {
            // Arrange
            var db = TestDbContextFactory.Create();
            db.Users.Add(new User(1, "user1", "hash1"));
            db.Users.Add(new User(1, "user2", "hash2"));
            await db.SaveChangesAsync();

            var service = new UserService(db);

            // Act
            var list = await service.GetListAsync();

            // Assert
            Assert.Equal(2, list.Count);
            Assert.Contains(list, u => u.Username == "user1");
            Assert.Contains(list, u => u.Username == "user2");
        }
    }
}
