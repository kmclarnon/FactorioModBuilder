﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Extensions
{
    /// <summary>
    /// Enumeration extension methods
    /// </summary>
    public static class EnumMethods
    {
        /// <summary>
        /// Returns the description attribute value of an enum if it exists, otherwise returns ToString()
        /// </summary>
        /// <param name="e">The enum to search for a description</param>
        /// <returns>The value of the description attribute, if it exists, otherwise ToString()</returns>
        public static string GetDescription(this Enum e)
        {
            if (e == null)
                return String.Empty;

            FieldInfo fInfo = e.GetType().GetField(e.ToString());
            object[] attribs = fInfo.GetCustomAttributes(false);

            if (attribs.Length == 0)
                return e.ToString();
            else
            {
                var res = attribs.Where(o => o is DescriptionAttribute);
                if (!res.Any())
                    return e.ToString();
                else
                    return ((DescriptionAttribute)res.First()).Description;
            }
        }
    }
}
