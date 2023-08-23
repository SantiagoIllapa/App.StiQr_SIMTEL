using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StiQr_SIMTEL.Server.Data
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IdAgent { get; set; } 
        [Required]
        public int IdLabelQr { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateMark { get; set; }
    }
}
