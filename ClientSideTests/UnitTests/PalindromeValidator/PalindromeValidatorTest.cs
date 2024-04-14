using ClientSide.PalindromeValidator.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSideTests.UnitTests.PalindromeValidator
{
    [TestClass]
    public class PalindromeValidatorTest
    {
        // для работы тестов сервер должен работать
        private readonly PalindromeValidatorFromServer validator;
        public PalindromeValidatorTest()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5015")           
            };
            validator = new (client);
        }

        #region IsValid string
        [TestMethod]
        public void IsValid_OKEmpty_True()
        {
            string input = "";

            bool result = validator.IsValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_OK_True()
        {
            string input = "1234321";

            bool result = validator.IsValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_OK_False()
        {
            string input = "123qwl32ldk1";

            bool result = validator.IsValid(input);

            Assert.IsFalse(result);
            
        }
        #endregion

        #region IsValidAsync string
        [TestMethod]
        public async Task IsValid_OKEmpty_TrueAsync()
        {
            string input = "";

            bool result = await validator.IsValidAsync(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task IsValid_OK_TrueAsync()
        {
            string input = "1234321";

            bool result = await validator.IsValidAsync(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task IsValid_OK_FalseAsync()
        {
            string input = "123qwl32ldk1";

            bool result = await validator.IsValidAsync(input);

            Assert.IsFalse(result);

        }
        #endregion
    }
}
