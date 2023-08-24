using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Shared.Transactions
{
    public class GetTransactionDTO
    {
        public int Id { get; set; }
        public string IdUserTransmiter { get; set; } = null!;
        public int IdLabelQr { get; set; }
        public byte Type { get; set; }

        public decimal Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTransacction { get; set; }
        public string? Observations { get; set; }
    }
}
