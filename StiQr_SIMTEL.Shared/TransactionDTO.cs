namespace StiQr_SIMTEL.Shared
{
    using System.ComponentModel.DataAnnotations;
    public class TransactionDTO
    {
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int IdWallet { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int IdUserTransmitter { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Type { get; set; } = null!;

        public DateTime DateTime { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int Amount { get; set; }

        public string? Observations { get; set; }
        public WalletDTO? Wallet { get; set; }
        public UserDTO? User { get; set; }
    }
}
