namespace ClientSide.PalindromeValidator.Interfaces
{
    interface IPalindromeValidator
    {
        bool IsValid(string value);
        Task<bool> IsValidAsync(string value);
    }
}
