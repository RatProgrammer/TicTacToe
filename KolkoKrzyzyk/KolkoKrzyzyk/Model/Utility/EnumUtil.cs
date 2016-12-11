
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TicTacToe.Model.Utility
{
    class EnumUtil
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T GetFirstEnumElement<T>(string value)
        {
           return (T) Enum.Parse(typeof(T),Enum.GetNames(typeof(T)).FirstOrDefault(value.Contains));
        }

        public static List<T> GetListOfEnumElement<T>(string value)
        {
            var enumNames = Enum.GetNames(typeof(T)).ToList();
            var enumsContainNames = enumNames.FindAll(x => x.Contains(value));
            var result = new List<T>();
            foreach (var l in enumsContainNames)
            {
                var enumElement = GetFirstEnumElement<T>(l);
                result.Add(enumElement);
            }
            return result;
        }
    }
}
