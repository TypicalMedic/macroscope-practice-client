using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Services.Interfaces
{
    interface IPalindromeValidator
    {
        bool IsValid(string value);
    }
}
