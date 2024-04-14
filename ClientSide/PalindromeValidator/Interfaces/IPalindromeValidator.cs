namespace ClientSide.PalindromeValidator.Interfaces
{
    public interface IPalindromeValidator
    {
        bool IsValid(string value);
        Task<bool> IsValidAsync(string value);
    }
}
