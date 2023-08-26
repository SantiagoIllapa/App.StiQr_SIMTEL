using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Shared.LabelsQR
{
    public class GetLabelQrDTO
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public string Plate { get; set; } = null!;
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastMark { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpiredDateMark { get; set; }
        public decimal Amount { get; set; }
    }
}
