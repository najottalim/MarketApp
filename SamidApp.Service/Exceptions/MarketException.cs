namespace SamidApp.Service.Exceptions;

public class MarketException : Exception
{
    public int Code { get; set; }
    public MarketException(int code, string message)
        : base(message)
    {
        this.Code = code;
    }
}