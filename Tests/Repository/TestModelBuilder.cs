using MT.Domain.Entity;

namespace Tests.Repository;

public static class TestModelBuilder
{
    public static Item CreateTestItem(Guid? guid = null)
    {
        return new Item
        {
            ItemId = guid ?? Guid.NewGuid(),
            Name = "TestItem",
            Price = 100,
            PossiblePrice = 200,
            Useful = true,
            PossibleUseful = true,
            Description = "TestDescription",
        };
    }
    
    public static User CreateTestUser(Guid? guid = null)
    {
        return new User
        {
            Id = guid ?? Guid.NewGuid(),
            UserName = "TestName",
            Email = "test@mail.com",
        };
    }

    public static Tag CreateTestTag(Guid? guid = null)
    {
        return new Tag
        {
            TagId = guid ?? Guid.NewGuid(),
            Name = "TestTag",
        };
    }
}