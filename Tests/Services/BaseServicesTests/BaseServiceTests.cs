using Microsoft.EntityFrameworkCore;
using Moq;
using MT.Application.BaseService;
using MT.Infrastructure.Data.Repository;
using Tests.Repository.Base;

namespace Tests.Services.BaseServicesTests;

public class BaseServiceTests
{
    private readonly Mock<DbSet<TestEntity>> _mockSet;
    private readonly Mock<DbContext> _mockContext;
    private readonly BaseRepository<TestEntity> _repository;
    private readonly BaseService<TestEntity> _service;
    
    
    private readonly IQueryable<TestEntity> _entities;
    
    private readonly Guid _guid = Guid.NewGuid();
    private readonly Guid _fakeGuid = Guid.NewGuid();
    public BaseServiceTests()
    {
        // Инициализация тестовых данных
        _entities = new List<TestEntity>
        {
            new TestEntity { Guid = _guid, Name = "Entity1", Description = "TestDescription1", Price = 50},
            new TestEntity { Guid = Guid.NewGuid(), Name = "Entity1", Description = "TestDescription2", Price = 100},
            new TestEntity { Guid = _fakeGuid, Name = "Entity3", Description = "TestDescription3", Price = 200}
        }.AsQueryable();

        
        _mockSet = new Mock<DbSet<TestEntity>>();
        _mockSet = CreateMockDbSet(_entities);
        
        _mockContext = new Mock<DbContext>();
        _mockContext
            .Setup(m => m.Set<TestEntity>())
            .Returns(_mockSet.Object);
        
        _repository = new BaseRepository<TestEntity>(_mockContext.Object);
        _service = new BaseService<TestEntity>(_repository);
    }

    [Fact]
    public void GetAll_ShouldReturnAllEntities()
    {
        var result = _service.GetAll();
        
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task AddAsync_ShouldEqual()
    {
        var entity1 = new TestEntity();
        
        await _service.AddAsync(entity1);
        
        _mockSet.Verify(m => m.AddAsync(entity1, default), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ShouldNotEqual()
    {
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        await _service.AddAsync(entity1);
        
        _mockSet.Verify(m => m.AddAsync(entity2, default), Times.Never);
    }

    [Fact]
    public async Task GetByGuidAsync_ShouldReturnOneAndEqual()
    {
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == _guid))).ReturnsAsync(_entities.First());
        
        var result = await _service.GetByGuidAsync(_guid);

        //Assert.NotNull(result);
        Assert.Equal(_entities.First(), result);
        Assert.IsType<TestEntity>(result);
    }
    
    [Fact]
    public async Task GetByGuidAsync_ShouldReturnNotEqual()
    {
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == _fakeGuid))).ReturnsAsync(_entities.Last());
        
        var result = await _service.GetByGuidAsync(_fakeGuid);

        //Assert.NotNull(result);
        Assert.NotEqual(_entities.First(), result);
    }

    [Fact]
    public async Task IsExist_ShouldReturnTrue()
    {
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == _guid))).ReturnsAsync(_entities.First());
        
        var result = await _service.IsExistAsync(_guid);
        
        Assert.True(result);
        _mockSet.Verify(m => m.FindAsync(_guid), Times.Once());
    }
    
    [Fact]
    public async Task IsExist_ShouldReturnFalse()
    {
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == _guid))).ReturnsAsync(_entities.First());
        
        var result = await _service.IsExistAsync(_fakeGuid);
        
        Assert.False(result);
        _mockSet.Verify(m => m.FindAsync(_guid), Times.Never);
    }
}