using Microsoft.EntityFrameworkCore;
using Moq;
using MT.Infrastructure.Data.Context;
using MT.Infrastructure.Data.Repository;

namespace Tests.Repository.Base;

public class BaseRepositoryTest
{ 
    private readonly Mock<DbSet<TestEntity>> _mockSet;
    private readonly Mock<DbContext> _mockContext;
    private readonly BaseRepository<TestEntity> _repository;
    
    private readonly IQueryable<TestEntity> _entities;
    
    private readonly Guid _guid = Guid.NewGuid();
    private readonly Guid _fakeGuid = Guid.NewGuid();
    public BaseRepositoryTest()
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
    }

    #region GetFilteredEntitiesTest

    [Fact]
    public void TestGetFilteredWithoutParams_ShouldReturnAll()
    {
        var result = _repository.GetFilteredEntities<TestEntity, TestEntity>();
        
        Assert.NotNull(result.First());
        Assert.Equal(3, result.Count()); // Добавьте это утверждение, если хотите проверить количество
    }

    [Fact]
    public void TestGetFilteredWithSpecification_WithoutMapper_ShouldReturnOneByName()
    {
        var spec = new NameSpecification("Entity1");

        var result = _repository.GetFilteredEntities<TestEntity, TestEntity>( specification: spec);
        
        Assert.NotNull(result.First());
        Assert.Equal(2, result.Count());
        Assert.Equal("Entity1", result.First().Name);
    }

    [Fact]
    public void TestGetFilteredWithMapper_WithoutSpecification_ShouldReturnAllWithOneCharDescription()
    {
        var mapper = TestEntityDto.Mapper();
        var result = _repository.GetFilteredEntities(mapper);
        
        Assert.NotNull(result.First());
        Assert.Equal(3, result.Count());
        Assert.IsType<TestEntityDto>(result.First());
        Assert.Single(result.First().OneCharDiscription);
    }

    [Fact]
    void TestGetFilteredWithMapper_WithSpecification_WithMapper_ShouldReturnOneByNameWithOneCharDescription()
    {
        var mapper = TestEntityDto.Mapper();
        
        var spec = new NameSpecification("Entity1");

        var result = _repository.GetFilteredEntities(mapper, spec);
        
        Assert.NotNull(result.First());
        Assert.Equal(2, result.Count());
        Assert.IsType<TestEntityDto>(result.First());
        Assert.Equal("Entity1", result.First().Name);
        Assert.Single(result.First().OneCharDiscription);
    }

    #endregion
    
    #region GetByGuidAsyncTests

    [Fact]
    public async Task GetByGuidAsyncTest_ShouldEqual()
    {
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == _guid))).ReturnsAsync(_entities.First());
        
        var result = await _repository.GetByGuidAsync(_guid);

        //Assert.NotNull(result);
        Assert.Equal(_entities.First(), result);
    }
    
    [Fact]
    public async Task GetByGuidAsyncTest_ShouldNotEqual()
    {
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == _fakeGuid))).ReturnsAsync(_entities.Last());
        
        var result = await _repository.GetByGuidAsync(_fakeGuid);

        //Assert.NotNull(result);
        Assert.NotEqual(_entities.First(), result);
    }
    
    [Fact]
    public async Task GetByGuidAsyncTest_ShouldNull()
    {
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == _guid))).ReturnsAsync(_entities.First());
        
        var result = await _repository.GetByGuidAsync(_fakeGuid);

        //Assert.NotNull(result);
        Assert.Null(result);
    }

    #endregion

    #region AddAsyncTests

    [Fact]
    public async Task AddAsyncTest()
    {
        var entity1 = new TestEntity();
        
        await _repository.AddAsync(entity1);
        
        _mockSet.Verify(m => m.AddAsync(entity1, default), Times.Once);
    }
    
    [Fact]
    public async Task AddAsync_ShouldAddMultipleEntitiesWithDifferentEntities()
    {
        // Arrange
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        // Act
        await _repository.AddAsync(entity1);
        await _repository.AddAsync(entity2);

        // Assert
        _mockSet.Verify(m => m.AddAsync(entity1, default), Times.Once);
        _mockSet.Verify(m => m.AddAsync(entity2, default), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ShouldAddMultipleEntitiesWithSameEntities()
    {
        var entity = new TestEntity();

        await _repository.AddAsync(entity);
        await _repository.AddAsync(entity);
        
        _mockSet.Verify(m => m.AddAsync(entity, default), Times.Exactly(2));
        
    }

    [Fact]
    public async Task AddAsync_ShouldNeverAdd()
    {
        // Arrange
        var entity = new TestEntity();
        var entity2 = new TestEntity();

        //Act
        await _repository.AddAsync(entity);
        
        //Assert
        _mockSet.Verify(m => m.AddAsync(entity2, default), Times.Never);
    }

    #endregion

    #region DeleteTests
    
    [Fact]
    public void DeleteShouldBeEqual()
    {
        var entity = _entities.First();
        
        _repository.Delete(entity);
        
        _mockSet.Verify(m => m.Remove(entity), Times.Once);
    }

    [Fact]
    public async Task DeleteAsyncShouldBeEqual()
    {
        var entity = _entities.First();
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == entity.Guid))).ReturnsAsync(entity);
        
        
        await _repository.Delete(entity.Guid);
        
        _mockSet.Verify(m => m.Remove(entity), Times.Once);
        _mockSet.Verify(m => m.FindAsync(entity.Guid), Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsyncShouldNotEqual()
    {
        var entity = _entities.Last();
        var fakeEntity = _entities.First();
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == entity.Guid))).ReturnsAsync(entity);
        
        
        await _repository.Delete(entity.Guid);
        
        _mockSet.Verify(m => m.Remove(fakeEntity), Times.Never);
        _mockSet.Verify(m => m.FindAsync(fakeEntity.Guid), Times.Never);
    }
    

    #endregion
    
    [Fact]
    public void UpdateAsyncShouldBeEqual()
    {
        var entity = _entities.First();
        _mockSet.Setup(m => m.FindAsync(It.Is<Guid>(g => g == entity.Guid))).ReturnsAsync(entity);
        
        _repository.Update(entity);
        
        _mockSet.Verify(m => m.Update(entity), Times.Once);
    }

    [Fact]
    public async Task SaveAsyncShouldBeEqual()
    {
        await _repository.SaveAsync();
        
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}