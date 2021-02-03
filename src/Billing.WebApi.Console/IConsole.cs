using Billing.WebApi.Console.Models;
using System;
using System.Collections.Generic;

namespace Billing.WebApi.Console
{
    public interface IConsole
    {
        void PrintTable<T>(IEnumerable<T> values);
        void PrintMenu(Menu menu);
        void PrintErrorMessage(string errorMessage);
        void PrintInfoMessage(string message);

        int ReadNumberWithHint(string hint, int minBound, int maxBound);
        DateTime ReadDateWithHint(string hint);
        string ReadLineWithHint(string hint, bool isRequired);
        string ReadPhoneNumberWithHint(string hint);

        Customer ReadCustomer();
        CreateOrder ReadOrder(List<GoodInfo> goodsInfo);
        CreateGood ReadGood(List<Component> componentInfo);
    }
}
