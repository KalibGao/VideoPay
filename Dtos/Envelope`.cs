using System;
namespace VideoPay.Dtos
{
    public class Envelope<T>
    {
        public int Status { get; }
        public T Result { get; }
        public string ErrorMessage { get; }
        public DateTime TimeGenerated { get; }

        protected internal Envelope(int status, T result, string errorMessage)
        {
            Status = status;
            Result = result;
            ErrorMessage = errorMessage;
            TimeGenerated = DateTime.UtcNow;
        }
    }



}