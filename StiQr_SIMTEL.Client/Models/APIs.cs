using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Client.Models
{
    internal class APIs
    {
        public const string AuthenticateUser = "/api/Users/AuthenticateUser";
        public const string RegisterUser = "/api/Users/RegisterUser";
        public const string GetLabelsQr = "/api/LabelsQr/GetLabelsQr";
        public const string GetLabelsQrById="/api/LabelsQr/GetLabelsQrById";
        public const string CheckHourLabelQr = "/api/LabelsQr/CheckHourLabelQr";
        public const string RechargeCash = "/api/LabelsQr/RechargeCash";
        public const string GetLabelsQrByPlate = "/api/LabelsQr/GetLabelsQrByPlate";
        public const string RegisterLabelQr = "/api/LabelsQr/RegisterLabelQr";
        public const string UpdateLabelQr = "/api/LabelsQr/UpdateLabelQr";
        public const string DeleteLabelQr = "/api/LabelsQr/DeleteLabelQr";
        public const string GetTransactions = "/api/Transactions/GetTransactions";

    }
}
