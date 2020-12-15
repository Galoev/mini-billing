﻿using System;
using System.Collections.Generic;

namespace Billing.WebApi.Console
{
    public interface IConsole
    {
        void PrintMenu();
        void AddMenu(Menu menu);
        void PrintTable<T>(IEnumerable<T> values);
        int ReadInt(string hint, int min, int max);
        int ReadInt(int min, int max);
        int ReadInt();
        DateTime ReadDate();
    }
}
