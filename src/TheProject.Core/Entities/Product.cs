namespace TheProject.Core.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("Product")]
    public class Product : Entity<int>
    {
        public string ArtikelCode { get; set; }
        public string ColorCode { get; set; }
        public string Description { get; set; }
    }
    
}
