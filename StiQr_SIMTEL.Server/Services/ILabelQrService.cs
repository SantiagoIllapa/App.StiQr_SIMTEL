

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
        Task<ResponseAPI<GetLabelQrDTO>> GetLabelQrByPlate(string LabelPlate);
        Task<ResponseAPI<string>> RechargeCash(RechargeCashDTO rechargeDTO);
        Task<ResponseAPI<string>> CheckHourLabelQR(CheckHourDTO ChecklabelQrDTO);
        Task<ResponseAPI<string>> UpdateLabelQr(CreateLabelQrDTO labelDto, int id);
        Task<ResponseAPI<string>> DeleteLabelQr(int id);

    }
}
