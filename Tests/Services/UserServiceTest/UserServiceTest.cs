using Microsoft.EntityFrameworkCore;
using Moq;
using MT.Application.JwtService.Interfaces;
using MT.Application.UserService;
using MT.Domain.Entity;
using MT.Infrastructure.Data.Context;
using MT.Infrastructure.Data.Repository;

namespace Tests.Services.UserServiceTest;

public class UserServiceTest
{
    private readonly Mock<DbSet<User>> _mockSet;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly UserService _userService;
    public UserServiceTest()
    {
        _mockSet = new Mock<DbSet<User>>();
        Mock<TrackingContext> mockContext = new();


        // Настраиваем, чтобы DbContext возвращал замокированный DbSet
        mockContext.Setup(c => c.Set<User>()).Returns(_mockSet.Object);
        _jwtServiceMock = new Mock<IJwtService>();
        // Инициализируем репозиторий с замокированным контекстом
        var repository = new UserRepository(mockContext.Object);
        _userService = new UserService(repository, _jwtServiceMock.Object);
    }
    
    [Fact]
    public async Task GetGuidActiveUser_ShouldReturnGuid_WhenJwtIsValidAndUserExists()
    {
        // Arrange
        var jwt = "valid_jwt";
        var expectedGuid = Guid.NewGuid();
        var user = new User { Id = expectedGuid };

        _jwtServiceMock.Setup(j => j.GetGuidFromJwt(jwt)).Returns(expectedGuid);

        _mockSet.Setup(m => m.FindAsync(expectedGuid)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetActiveUserFromJwt(jwt);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user, result);
    }

    [Fact]
    public async Task GetGuidActiveUser_ShouldReturnNull_WhenJwtIsInvalid()
    {
        // Arrange
        var jwt = "invalid_jwt";
    
        _jwtServiceMock.Setup(j => j.GetGuidFromJwt(jwt)).Returns((Guid?)null);
    
        // Act
        var result = await _userService.GetActiveUserFromJwt(jwt);
    
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetGuidActiveUser_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        var jwt = "valid_jwt";
        var expectedGuid = Guid.NewGuid();
    
        _jwtServiceMock.Setup(j => j.GetGuidFromJwt(jwt)).Returns(expectedGuid);
   
    
        // Act
        var result = await _userService.GetActiveUserFromJwt(jwt);
    
        // Assert
        Assert.Null(result);
    }
}