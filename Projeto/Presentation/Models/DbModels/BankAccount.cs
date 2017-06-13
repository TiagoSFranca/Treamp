namespace Presentation.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("BankAccount")]
    public partial class BankAccount
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Agency { get; set; }

        [Required]
        [StringLength(20)]
        public string Account { get; set; }
        
        public int IdUser { get; set; }
        
        public int IdTypeAccount { get; set; }

        [ForeignKey("IdTypeAccount")]
        public virtual TypeAccount TypeAccount { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }
}
