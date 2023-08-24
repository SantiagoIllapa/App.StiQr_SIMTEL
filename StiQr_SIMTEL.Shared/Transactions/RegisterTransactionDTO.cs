using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Shared.Transactions
{
    public class RegisterTransactionDTO
    {
        [Required]
        public string IdUserTransmiter { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo IdLabelQr debe ser mayor que 0.")]
        public int IdLabelQr { get; set; }
        [Range(1, 2, ErrorMessage = "El campo Type debe ser 1 o 2.")]
        public byte Type { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency, ErrorMessage ="El campo Amount debe ser un valor ")]
        public decimal Amount { get; set; }
        public string? Observations { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTransacction { get; set; }
    }
}
