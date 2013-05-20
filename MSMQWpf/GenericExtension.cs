using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSMQWpf
{
    using System.ComponentModel;

    public static class GenericExtension
    {
        public static bool Is(this string input, Type targetType)
        {
            try
            {
                TypeDescriptor.GetConverter(targetType).ConvertFromString(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static object Parse(this string input, Type targetType)
        {
            try
            {
                return TypeDescriptor.GetConverter(targetType).ConvertFromString(input);
            }
            catch
            {
                return null;
            }
        }
    }
}
