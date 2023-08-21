

using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelQR;

namespace StiQr_SIMTEL.Server.Services
{
    public interface ILabelQrService
    {

        Task<ResponseAPI<string>> RegisterLabelQR(CreateLabelQrDTO LabelQrDTO);
    }
}
