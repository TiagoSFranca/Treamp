namespace Presentation.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Address")]
    public partial class Address
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Street { get; set; }

        [Required]
        [StringLength(45)]
        public string District { get; set; }

        [Required]
        [StringLength(10)]
        public string Number { get; set; }
        
        public int IdCity { get; set; }

        public int IdUser { get; set; }
        
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }
}
