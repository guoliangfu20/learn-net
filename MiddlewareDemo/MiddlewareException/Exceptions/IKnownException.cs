namespace MiddlewareException.Exceptions
{
    public interface IKnownException
    {
        public string Message { get; }

        public int ErrorCode { get; }

        public object[] ErrorData { get; }
    }
}
