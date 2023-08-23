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
    }
}
