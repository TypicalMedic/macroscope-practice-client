using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.PalindromeValidator.Interfaces
{
    interface IPalindromeValidator
    {
        bool IsValid(string value);
        Task<bool> IsValidAsync(string value);
    }
}
