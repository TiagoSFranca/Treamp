namespace Presentation.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Notification")]
    public partial class Notification
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public bool Active { get; set; }
        
        public int IdUser { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }
}
