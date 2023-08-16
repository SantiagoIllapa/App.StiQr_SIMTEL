namespace StiQr_SIMTEL.Shared
{
    using System.ComponentModel.DataAnnotations;
    public class WalletDTO
    {
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int IdUserDriver { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int Amount { get; set; }

        public bool State { get; set; }

        public UserDTO? User { get; set; }
    }
}
