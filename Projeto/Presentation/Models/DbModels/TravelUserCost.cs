namespace Presentation.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("TravelUserCost")]
    public partial class TravelUserCost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("TravelUser"), Column(Order = 0)]
        public int IdTravelUserTravel { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("TravelUser"), Column(Order = 1)]
        public int IdTravelUserUser { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Cost")]
        public int IdCost { get; set; }

        public virtual Cost Cost { get; set; }

        public virtual TravelUser TravelUser { get; set; }
    }
}
