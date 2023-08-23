using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Client.Models
{
    public class SearchPlate
    {
        [Required(ErrorMessage = "El código de placa es obligatorio.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El código de placa debe tener 3 letras.")]
        [RegularExpression("^[A-Z]+$", ErrorMessage = "El código de placa debe contener solo letras mayúsculas.")]
        public string PlateCode { get; set; }

        [Required(ErrorMessage = "El número de placa es obligatorio.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "El número de placa debe tener  3 ó 4 dígitos.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El número de placa debe contener solo dígitos.")]
        public string PlateNumber { get; set; }
    }
}
