namespace WebService.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("City")]
    public partial class City
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }        

        public int IdState { get; set; }
        
        [ForeignKey("IdState")]
        public virtual State State { get; set; }
    }
}
