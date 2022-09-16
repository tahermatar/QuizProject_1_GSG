﻿using System;

namespace Project_1_GSG.Extension
{
    public static class StringExtensions
    {
        public static string FirstLetterToUpperCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentException("There is no first letter");
            }

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
