namespace Presentation.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Contact")]
    public partial class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public bool Pendent { get; set; }
        
        public int IdRequester { get; set; }
        
        public int IdRemittee { get; set; }

        [ForeignKey("IdRequester")]
        public virtual User Requester { get; set; }

        [ForeignKey("IdRemittee")]
        public virtual User Remittee { get; set; }
    }
}
