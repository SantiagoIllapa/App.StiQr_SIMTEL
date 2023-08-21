using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Shared.Transactions
{
    public class RegisterTransactionDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo IdAgent debe ser mayor que 0.")]
        public int IdAgent { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo IdLabelQr debe ser mayor que 0.")]
        public int IdLabelQr { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateMark { get; set; }
    }
}
