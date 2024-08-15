using Microsoft.EntityFrameworkCore;
using Moq;
using MT.Domain.Entity;
using MT.Infrastructure.Data.Context;
using MT.Infrastructure.Data.Repository;

namespace Tests.Repository.TagsRepositoryTest;

public class TagsRepositoryTest
{
    private readonly TagRepository _repository;
    private readonly Guid _guid = Guid.NewGuid();
    private readonly Guid _guid2 = Guid.NewGuid();
    private readonly Guid _userGuid = Guid.NewGuid();

    public TagsRepositoryTest()
    {
        IQueryable<Tag> entities = new List<Tag>
        {
            new Tag { TagId = _guid, Name = "Entity1", UserId = _userGuid },
            new Tag { TagId = _guid2, Name = "Entity1", UserId = Guid.NewGuid() },
            new Tag { TagId = Guid.NewGuid(), Name = "Entity3", UserId = _userGuid }
        }.AsQueryable();

        Mock<DbSet<Tag>> mockSet = new();
        mockSet = CreateMockDbSet(entities);

        Mock<TrackingContext> mockContext = new();
        mockContext.Setup(m => m.Tags).Returns(mockSet.Object);

        _repository = new TagRepository(mockContext.Object);
    }

    [Fact]
    public void GetExistingByGuidsAsync_ShouldReturnExistingTags()
    {
        // Arrange
        var guids = new List<Guid> { _guid, _guid2 };

        // Act
        var result = _repository.GetExistingByGuids(guids);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetExistingByGuidsAsync_ShouldReturnEmptyCollectionWhenNoMatchingTags()
    {
        // Arrange
        var guids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        // Act
        var result = _repository.GetExistingByGuids(guids);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetExistingByGuidsAsync_ShouldReturnOneFromTwoTagsGuidsOneMatching()
    {
        // Arrange
        var guids = new List<Guid> { Guid.NewGuid(), _guid };

        // Act
        var result = _repository.GetExistingByGuids(guids);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}