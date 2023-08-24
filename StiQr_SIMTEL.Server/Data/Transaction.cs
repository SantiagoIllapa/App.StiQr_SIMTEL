using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StiQr_SIMTEL.Server.Data
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string IdUserTransmiter { get; set; } = null!;
        [Required]
        public int IdLabelQr { get; set; }
        [Range(1, 2)]
        public byte Type  { get; set; }
        [Column(TypeName = "decimal(18, 2)")] 
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)] 
        [DataType(DataType.Currency)] 
        public decimal Amount { get; set; }
        public string? Observations { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTransacction { get; set; }
    }
}
