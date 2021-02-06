using Microsoft.VisualStudio.TestTools.UnitTesting;
using Billing.WebApi.Console.Validators;

namespace Billing.WebApi.Tests.Validators
{
    [TestClass]
    public class UserInputValidatorTests
    {
        [TestMethod]
        public void IsValidNumber()
        {
            int minBound = 42;
            int maxBound = 10000;

            string[] numbers = {
                "2134",
                "21313-213-1",
                "430",
                 "-21331",
                 "02131",
                 "10001",
                 "939",
                 ""
            };

            for (int i = 0; i < numbers.Length; ++i)
            {
                var currentValue = numbers[i];
                if (i % 2 == 0)
                    Assert.AreEqual(UserInputValidator.IsValidNumber(currentValue, minBound, maxBound, out _), true);
                else
                    Assert.AreEqual(UserInputValidator.IsValidNumber(currentValue, minBound, maxBound, out _), false);
            }
        }

        [TestMethod]
        public void IsValidPhoneNumber()
        {
            string[] phones = {
                "+7(985)531-08-68",
                "8(3519)454-54-541",
                "8(800)454-54-54",
                 "8(800)8455412",
                 "8(3519)454-54-54",
                 ""
            };

            for (int i = 0; i < phones.Length; ++i)
            {
                var currentValue = phones[i];
                if (i % 2 == 0)
                    Assert.AreEqual(UserInputValidator.IsValidPhoneNumber(currentValue), true);
                else
                    Assert.AreEqual(UserInputValidator.IsValidPhoneNumber(currentValue), false);
            }
        }
        

        [TestMethod]
        public void IsValidDateInFormat()
        {
            var format = "dd.MM.yyyy";

            string[] dates = {
                "10.07.1998",
                "32.01.2020",
                "28.02.2000",
                 "30.02.2001",
                 "13.12.2015",
                 "13.13.2013"
            };

            for (int i = 0; i < dates.Length; ++i)
            {
                var currentValue = dates[i];
                if (i % 2 == 0)
                    Assert.AreEqual(UserInputValidator.IsValidDateInFormat(currentValue, format, out _), true);
                else
                    Assert.AreEqual(UserInputValidator.IsValidDateInFormat(currentValue, format, out _), false);
            }
        }

        [TestMethod]
        public void IsValidEmail()
        {
            string[] emails = {
                "mama_mia@mail.ru",
                "_233",
                "afj_daf@gmail.com",
                 "saakjg@",
                 "123kakkf@ya.ru",
                 ""
            };

            for (int i = 0; i < emails.Length; ++i)
            {
                var currentValue = emails[i];
                if (i % 2 == 0)
                    Assert.AreEqual(UserInputValidator.IsValidEmail(currentValue), true);
                else
                    Assert.AreEqual(UserInputValidator.IsValidEmail(currentValue), false);
            }
        }
    }
}
