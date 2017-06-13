namespace Presentation.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Destination")]
    public partial class Destination
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        
        public int IdTravel { get; set; }
        
        public int IdCity { get; set; }
        
        [ForeignKey("IdTravel")]
        public virtual Travel Travel { get; set; }
    }
}
