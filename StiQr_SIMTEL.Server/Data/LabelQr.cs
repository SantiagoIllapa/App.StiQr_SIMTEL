using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiQr_SIMTEL.Server.Data
{
    public class LabelQr
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserEmail { get; set; } = null!;
        [MaxLength (8)]
        [MinLength (7)]
        public string Plate { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastMark { get; set; }

        [Column(TypeName = "decimal(18, 2)")] // Define la precisión de dos decimales para dinero.
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)] // Formato de moneda
        [DataType(DataType.Currency)] // Indica que es un campo de dinero
        public decimal? Amount { get; set; }
    }
}
