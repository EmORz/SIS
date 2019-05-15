using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Common
{
    public class CoreValidator
    {
        public static void ThrowIfFull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentException(name);

            }
        }

        public static void ThrowIfNullOrEmpty(string text, string name)
        {
            if (string.IsNullOrEmpty(text))
            {
                //Todo something is wrong!
                throw new ArgumentException($"{name} cannot be null or empty.", name);
            }

        }
    }
}
