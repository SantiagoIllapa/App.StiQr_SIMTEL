

using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelQR;
using StiQr_SIMTEL.Shared.LabelsQR;

namespace StiQr_SIMTEL.Server.Services
{
    public interface ILabelQrService
    {

        Task<ResponseAPI<string>> RegisterLabelQR(CreateLabelQrDTO LabelQrDTO);
        Task<ResponseAPI<List<GetLabelQrDTO>>> GetLabelQrAll();
        Task<ResponseAPI<GetLabelQrDTO>> GetLabelQrById(int LabelId);
        Task<ResponseAPI<string>> CheckHourLabelQR(CheckHourLabelQrDTO ChecklabelQrDTO, int id);
    }
}
