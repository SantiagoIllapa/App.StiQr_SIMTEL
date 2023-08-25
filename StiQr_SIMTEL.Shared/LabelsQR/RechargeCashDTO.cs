using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Shared.LabelsQR
{
    public class RechargeCashDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo IdLabelQr debe ser mayor que 0.")]
        public int IdLabelQr { get; set; }
        [Required]
        public string IdUserRecharger{ get; set; } = null!;
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency, ErrorMessage = "El CashAmount debe ser un valor ")]
        [Range(1, 20, ErrorMessage = "El monto a recargar debe ser unicamente de $1 a $20.")]
        public decimal CashAmount { get; set; }
    }
}
