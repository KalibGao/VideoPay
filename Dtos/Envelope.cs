using System;

namespace VideoPay.Dtos
{
    public class Envelope : Envelope<string>
    {
        protected Envelope(int status, string errorMessage)
            : base(status, string.Empty, errorMessage)
        {
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(0, result, string.Empty);
        }

        public static Envelope Ok()
        {
            return new Envelope(0, string.Empty);
        }

        public static Envelope Error(int status, string errorMessage)
        {
            return new Envelope(status, errorMessage);
        }
    }
}