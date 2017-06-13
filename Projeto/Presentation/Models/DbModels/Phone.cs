namespace Presentation.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Phone")]
    public partial class Phone
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(5)]
        public string Prefix { get; set; }

        [Required]
        [StringLength(12)]
        public string Number { get; set; }
        
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }
}
