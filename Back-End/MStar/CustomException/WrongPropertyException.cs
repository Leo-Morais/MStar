namespace MStar.CustomException
{
    public class WrongPropertyException : Exception
    {
        public WrongPropertyException(string? message) : base(message)
        {
        }
    }
}
