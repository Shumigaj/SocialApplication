namespace SocialApplication.Business.ExceptionHandling
{
    public class ErrorDetails
    {
        public ErrorDetails(string message, object data)
            : this(message)
        {
            Data = data;
        }

        public ErrorDetails(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
