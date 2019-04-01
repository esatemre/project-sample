namespace TheProject.Core.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Size")]
    public class Size : Entity<int>
    {
        public string Name { get; set; }
    }
}
