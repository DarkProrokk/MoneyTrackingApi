using System.Linq.Expressions;

namespace Tests.Repository.Base;

public class TestEntityDto
{
    public string Name { get; set; }
    
    public string OneCharDiscription { get; set; }
    
    public static Expression<Func<TestEntity, TestEntityDto>> Mapper()
    {
        return item => new TestEntityDto
        {
            Name = item.Name,
            OneCharDiscription = item.Description.Substring(0, 1)
        };
    }
}