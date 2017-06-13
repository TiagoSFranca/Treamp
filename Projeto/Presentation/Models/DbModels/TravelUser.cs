namespace Presentation.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("TravelUser")]
    public partial class TravelUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TravelUser()
        {
            TravelUserCosts = new HashSet<TravelUserCost>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTravel { get; set; }

        public bool Pendent { get; set; }

        public bool IsOwner { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUser { get; set; }

        [ForeignKey("IdTravel")]
        public virtual Travel Travel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TravelUserCost> TravelUserCosts { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }
}
