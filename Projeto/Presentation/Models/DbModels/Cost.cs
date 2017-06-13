namespace Presentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Cost")]
    public partial class Cost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cost()
        {
            TravelUserCost = new HashSet<TravelUserCost>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }
        
        public int IdTypeCost { get; set; }

        [ForeignKey("IdTypeCost")]
        public virtual TypeCost TypeCost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TravelUserCost> TravelUserCost { get; set; }
    }
}
