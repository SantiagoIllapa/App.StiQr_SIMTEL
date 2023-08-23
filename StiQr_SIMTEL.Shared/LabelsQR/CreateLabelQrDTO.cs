using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Shared.LabelQR
{
    public class CreateLabelQrDTO
    {
        [Required]
        public string UserEmail { get; set; } = null!;
        [MaxLength(8)]
        [MinLength(7)]
        public string Plate { get; set; } = null!;
        
    }
}
