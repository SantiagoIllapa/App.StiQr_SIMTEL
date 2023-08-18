namespace StiQr_SIMTEL.Shared
{
    public class ResponseAPI<T>
    {
        public bool IsSuccess { get; set; }
        public T? Content { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
