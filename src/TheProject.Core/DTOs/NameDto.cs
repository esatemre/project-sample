namespace TheProject.Core.DTOs
{
    public class NameDto<TPrimaryKey> : BaseDto<TPrimaryKey>
    {
        public string Name { get; set; }
    }
}
