namespace TheProject.Core.DTOs
{
    public class BaseDto<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
