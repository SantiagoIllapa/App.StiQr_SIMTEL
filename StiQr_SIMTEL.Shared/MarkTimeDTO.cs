namespace StiQr_SIMTEL.Shared
{
    using System.ComponentModel.DataAnnotations;
    public class MarkTimeDTO
    {
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int IdLabel { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int IdTransaction { get; set; }

        public DateTime TimeAdmission { get; set; }

        public DateTime TimeDeparture { get; set; }

        public LabelDTO? Label { get; set; }
        public TransactionDTO? Transaction { get; set; }
    }
}
