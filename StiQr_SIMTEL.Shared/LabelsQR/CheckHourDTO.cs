using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Shared.LabelsQR
{
    public class CheckHourDTO
    {

        [Required]
        public string IdUserRecharger { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo IdLabelQr debe ser mayor que 0.")]
        public int IdLabelQr { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastMark { get; set; }
    }
}
