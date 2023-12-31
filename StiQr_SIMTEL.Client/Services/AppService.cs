﻿using Newtonsoft.Json;
using StiQr_SIMTEL.Client.Models;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelQR;
using StiQr_SIMTEL.Shared.LabelsQR;
using StiQr_SIMTEL.Shared.Transactions;
using StiQr_SIMTEL.Shared.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Client.Services
{
    internal class AppService : IAppService
    {
        public string _baseUrl = "http://192.168.100.17:9095";


        //Users
        public async Task<string> AuthenticateUser(LogInUserDTO loginModel)
        {
            string returnStr = string.Empty;
            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}{APIs.AuthenticateUser}";
                var serializeStr = JsonConvert.SerializeObject(loginModel);
                var response = await client.PostAsync(url, new StringContent(serializeStr, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    returnStr = await response.Content.ReadAsStringAsync();
                }
            }
            return returnStr;
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegisterUserDTO registrationModel)
        {
            string errorMessage = string.Empty;
            bool isSuccess = false;
            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}{APIs.RegisterUser}";
                var serializeStr = JsonConvert.SerializeObject(registrationModel);
                var response = await client.PostAsync(url, new StringContent(serializeStr, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    isSuccess = true;
                }
                else
                {
                    errorMessage = await response.Content.ReadAsStringAsync();
                }
            }
            return (isSuccess, errorMessage);
        }
        //LabelsQr
        public async Task<(string ConfirmMessage, string ErrorMessage)>CreateLabelQr(CreateLabelQrDTO labelModel)
        {
            string errorMessage = string.Empty;
            string confirmMessage = string.Empty;
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}{APIs.RegisterLabelQr}";
                var serializeStr = JsonConvert.SerializeObject(labelModel);
                var apiResponse = await client.PostAsync(url, new StringContent(serializeStr, Encoding.UTF8, "application/json"));
                 
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response= await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<string>>(response);
                    if (deserializeResponse.IsSuccess)
                    {
                        confirmMessage = deserializeResponse.Content;
                    }
                    else
                    {
                        errorMessage = deserializeResponse.ErrorMessage;
                    }
                }
                else
                {
                    errorMessage = "No se ha podido conectar con el servidor";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return (confirmMessage, errorMessage);
        }
        public async Task<(string ConfirmMessage, string ErrorMessage)> UpdateLabelQr(CreateLabelQrDTO labelModel,int id)
        {
            string confirmMessage = string.Empty;
            string errorMessage = string.Empty;
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}{APIs.UpdateLabelQr}/{id}";
                var serializeStr = JsonConvert.SerializeObject(labelModel);
                Debug.WriteLine(serializeStr);
                var apiResponse = await client.PutAsync(url, new StringContent(serializeStr, Encoding.UTF8, "application/json"));
                if (apiResponse.IsSuccessStatusCode)
                {

                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<string>>(response);
                    if (deserializeResponse.IsSuccess)
                    {
                        confirmMessage = deserializeResponse.Content;
                    }
                    else
                    {
                        errorMessage = deserializeResponse.ErrorMessage;
                    }
                }
                else
                {
                    errorMessage = await apiResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (confirmMessage, errorMessage);
        }
        public async Task<(string ConfirmMessage, string ErrorMessage)> DeleteLabelQr(int id)
        {
            string confirmMessage = string.Empty;
            string errorMessage = string.Empty;
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}{APIs.DeleteLabelQr}/{id}";


                var apiResponse = await client.DeleteAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {

                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<string>>(response);
                    if (deserializeResponse.IsSuccess)
                    {
                        confirmMessage = deserializeResponse.Content;
                    }
                    else
                    {
                        errorMessage = deserializeResponse.ErrorMessage;
                    }
                }
                else
                {
                    errorMessage = await apiResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (confirmMessage, errorMessage);
        }
        public async Task<(List<GetLabelQrDTO> LabelsList, string ErrorMessage)> GetLabelsQr()
        {
            var labelsList = new List<GetLabelQrDTO>();
            string errorMessage = string.Empty;
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}{APIs.GetLabelsQr}";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {

                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<List<GetLabelQrDTO>>>(response);
                    if (deserializeResponse.IsSuccess)
                    {
                        labelsList = deserializeResponse.Content;
                    }
                    else
                    {
                        errorMessage = deserializeResponse.ErrorMessage;
                    }
                }
                else
                {
                    errorMessage = "No se ha podido conectar con el servidor";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (labelsList, errorMessage);

        }
        public async Task<(GetLabelQrDTO LabelQr, string ErrorMessage)> GetLabelQrById(int id)
        {
            var labelQr = new GetLabelQrDTO();
            string errorMessage = string.Empty;
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}{APIs.GetLabelsQrById}/{id}";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {

                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<GetLabelQrDTO>>(response);
                    if (deserializeResponse.IsSuccess)
                    {
                        labelQr = deserializeResponse.Content;
                    }
                    else
                    {
                        errorMessage = deserializeResponse.ErrorMessage;
                    }
                }
                else
                {
                    errorMessage = "No se ha podido conectar con el servidor";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (labelQr, errorMessage);
        }
        public async Task<(GetLabelQrDTO LabelQr, string ErrorMessage)> GetLabelQrByPlate(string plate)
        {
            var labelQr = new GetLabelQrDTO();
            string errorMessage = string.Empty;
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}{APIs.GetLabelsQrByPlate}/{plate}";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {


                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<GetLabelQrDTO>>(response);
                    if (deserializeResponse.IsSuccess)
                    {
                        labelQr = deserializeResponse.Content;
                    }
                    else
                    {
                        errorMessage = deserializeResponse.ErrorMessage;
                    }
                }
                else
                {
                    errorMessage = "No se ha podido conectar con el servidor";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (labelQr, errorMessage);
        }
        public async Task<(GetLabelQrDTO LabelQr, string ErrorMessage)> GetLabelQrByUrl(string url)
        {
            var labelQr = new GetLabelQrDTO();
            string errorMessage = string.Empty;
            try
            {
                using var client = new HttpClient();

                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {

                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<GetLabelQrDTO>>(response);
                    if (deserializeResponse.IsSuccess)
                    {
                        labelQr = deserializeResponse.Content;
                    }
                    else
                    {
                        errorMessage = deserializeResponse.ErrorMessage;
                    }
                }
                else
                {
                    errorMessage = "No se ha podido conectar con el servidor";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (labelQr, errorMessage);
        }
        public async Task<(string ConfirmMessage, string ErrorMessage)> CheckHourLabelQr(CheckHourDTO checkHourLabelQr)

        {
            string confirmMessage = string.Empty;
            string errorMessage = string.Empty;
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}{APIs.CheckHourLabelQr}";
                var serializeStr = JsonConvert.SerializeObject(checkHourLabelQr);
                Debug.WriteLine(serializeStr);
                var apiResponse = await client.PutAsync(url, new StringContent(serializeStr, Encoding.UTF8, "application/json"));
                if (apiResponse.IsSuccessStatusCode)
                {

                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<string>>(response);
                    if (deserializeResponse.IsSuccess)
                    {
                        confirmMessage = deserializeResponse.Content;
                    }
                    else
                    {
                        errorMessage = deserializeResponse.ErrorMessage;
                    }
                }
                else
                {
                    errorMessage = await apiResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (confirmMessage, errorMessage);
        }
        public async Task<(string ConfirmMessage, string ErrorMessage)> RechargeLabelQr(RechargeCashDTO rechargeCashDTO)
        {
            string confirmMessage = string.Empty;
            string errorMessage = string.Empty;
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}{APIs.RechargeCash}";
                var serializeStr = JsonConvert.SerializeObject(rechargeCashDTO);
                var apiResponse = await client.PutAsync(url, new StringContent(serializeStr, Encoding.UTF8, "application/json"));
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<string>>(response);
                    if (deserializeResponse.IsSuccess)
                    {
                        confirmMessage = deserializeResponse.Content;
                    }
                    else
                    {
                        errorMessage = deserializeResponse.ErrorMessage;
                    }
                }
                else
                {
                    errorMessage = "No se ha podido conectar con el servidor";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (confirmMessage, errorMessage);
        }
        public async Task<(List<GetTransactionDTO> TransactionList, string ErrorMessage)> GetTransactions()
        {
            var transactionList = new List<GetTransactionDTO>();
            string errorMessage = string.Empty;
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}{APIs.GetTransactions}";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {

                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<List<GetTransactionDTO>>>(response);
                    if (deserializeResponse.IsSuccess)
                    {
                        transactionList = deserializeResponse.Content;
                    }
                    else
                    {
                        errorMessage = deserializeResponse.ErrorMessage;
                    }
                }
                else
                {
                    errorMessage = "No se ha podido conectar con el servidor";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (transactionList, errorMessage);

        }
    }
}
