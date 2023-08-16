namespace StiQr_SIMTEL.Shared
{
    public class ResponseAPI<T>
    {
        public bool IsCorrect { get; set; }
        public T? Value { get; set; }
        public string? Message { get; set; }
    }
}
