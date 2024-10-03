namespace MStar.CustomException
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException(string? message) : base(message)
        {
        }
    }
}
