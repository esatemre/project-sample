namespace TheProject.Core.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Color")]
    public class Color : Entity<int>
    {
        public string Name { get; set; }
    }
}
