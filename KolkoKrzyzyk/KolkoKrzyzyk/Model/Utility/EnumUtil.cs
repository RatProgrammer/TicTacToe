
using System;
using System.Linq;

namespace TicTacToe.Model.Utility
{
    class EnumUtil
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T Parse<T>(string value)
        {
           return (T) Enum.Parse(typeof(T),Enum.GetNames(typeof(T)).FirstOrDefault(value.Contains));
        }
    }
}
