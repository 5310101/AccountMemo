using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMemo_Console
{
    public static class StaticMethod
    {
        public static void Normal_Display(this string textDisplay)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(textDisplay);
            Console.ResetColor();
        }

        public static void Error_Display(this string textDisplay)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(textDisplay);
            Console.ResetColor();
        }

        public static void Title_Display(this string textDisplay)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(textDisplay);
            Console.ResetColor();
        }

        public static void Custom_Display(this string textDisplay, ConsoleColor BackGroundColor, ConsoleColor ForegroundColor)
        {
            Console.BackgroundColor = BackGroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.WriteLine(textDisplay);
            Console.ResetColor();
        }
    }
}
