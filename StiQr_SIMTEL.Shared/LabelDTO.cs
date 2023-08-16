namespace StiQr_SIMTEL.Shared
{
    using System.ComponentModel.DataAnnotations;
    public class LabelDTO
    {

        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue,ErrorMessage = "El campo {0} es requerido")]
        public int IdVehicle { get; set; }

        public DateTime CreationDate { get; set; }
        public VehicleDTO? Vehicle { get; set; }
    }
}
