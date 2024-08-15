using Microsoft.EntityFrameworkCore;
using Moq;
using MT.Domain.Entity;
using MT.Infrastructure.Data.Context;
using MT.Infrastructure.Data.Repository;
using Xunit.Abstractions;
using static Tests.Repository.TestModelBuilder;
namespace Tests.Repository.UserRepositoryTest;

public class UserRepositoryTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Mock<DbSet<User>> _mockSet;
    private readonly Mock<TrackingContext> _mockContext;
    private readonly UserRepository _repository;

    public UserRepositoryTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _mockSet = new Mock<DbSet<User>>();
        _mockContext = new Mock<TrackingContext>();

        // Настраиваем, чтобы DbContext возвращал замокированный DbSet
        _mockContext.Setup(c => c.Set<User>()).Returns(_mockSet.Object);

        // Инициализируем репозиторий с замокированным контекстом
        _repository = new UserRepository(_mockContext.Object);
    }
    
    [Fact]
    public async Task GetByGuidAsync_ShouldReturnEntity_WhenEntityExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new User { Id = id, UserName = "TestName", Email = "test@mail.com" };

        // Настраиваем, чтобы FindAsync возвращал сущность
        _mockSet.Setup(m => m.FindAsync(id)).ReturnsAsync(entity);

        // Act
        var result = await _repository.GetByGuidAsync(id);

        // Assert
        Assert.Equal(entity, result);
        _mockSet.Verify(m => m.FindAsync(id), Times.Once);
    }

    [Fact]
    public async Task TestGetUserByGuidShouldBeEqual()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = CreateTestUser(id);
        
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == id))).ReturnsAsync(entity);

        await _repository.AddAsync(entity);
        // Act
        var result = await _repository.GetByGuidAsync(id);

        //Assert
        Assert.Equal(result, entity);
        _mockSet.Verify(m => m.FindAsync(id), Times.Once);
    }
    
    [Fact]
    public async Task TestGetUserByGuidShouldBeNotEqual()
    {
        // Arrange
        var correctId = Guid.NewGuid();

        var correctEntity = CreateTestUser(correctId);
        var incorrectEntity = CreateTestUser();
        
        _mockSet
            .Setup(m => m.FindAsync(It.IsAny<Guid>()))
            .ReturnsAsync(incorrectEntity);
        // Act
        var result = await _repository.GetByGuidAsync(correctId);


        //Assert
        Assert.NotEqual(result, correctEntity);
        _mockSet.Verify(m => m.FindAsync(correctId), Times.Once);
    }

    [Fact]
    public async Task TestAddUser()
    {
        //Arrange
        var id = Guid.NewGuid();
        var entity = CreateTestUser(id);
        var cloneEntity = CloneObject(entity);
       
        //Act
        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
         _mockSet.Verify(m => m.AddAsync(It.Is<User>(u => AreEqual(u, cloneEntity)), 
             It.IsAny<CancellationToken>()), Times.Once);
         _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    private bool AreEqual(User u1, User u2)
    {
        return u1.Id == u2.Id &&
               u1.UserName == u2.UserName &&
               u1.Email == u2.Email;
    }
    
    private User CloneObject(User source)
    {
        return new User
        {
            Id = source.Id,
            UserName = source.UserName,
            Email = source.Email,
        };
    }
}