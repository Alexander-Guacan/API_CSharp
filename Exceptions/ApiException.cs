using System.Globalization;

namespace API_CSharp.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base()
        {
        }

        public ApiException(string errorMessage) : base(errorMessage)
        {
        }

        public ApiException(string errorMessage, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, errorMessage, args))
        {
        }
    }
}
