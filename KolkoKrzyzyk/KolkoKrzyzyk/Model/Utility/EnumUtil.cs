
using System;

namespace TicTacToe.Model.Utility
{
    class EnumUtil
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
