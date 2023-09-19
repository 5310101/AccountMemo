using AccountMemo_Domain;
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

        public static void InputField_Display(this string textDisplay)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(textDisplay);
            Console.ResetColor();
        }

        public static void Custom_Display(this string textDisplay, ConsoleColor BackGroundColor, ConsoleColor ForegroundColor)
        {
            Console.BackgroundColor = BackGroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.WriteLine(textDisplay);
            Console.ResetColor();
        }

        public static void Display_ToUser(InfoType infoType, bool isSuccess, string information = "")
        {
            if(infoType == InfoType.UpdateFunction)
            {
                if (isSuccess == false)
                {
                    "Cannot update.".Error_Display();
                }
                else
                {
                    "Updated successfully.".Normal_Display();
                }
            }
            else if (infoType == InfoType.DeleteFunction)
            {
                if (isSuccess == false)
                {
                    "Cannot delete.".Error_Display();
                }
                else
                {
                    "deleted successfully.".Normal_Display();
                }
            }
            if (infoType == InfoType.CreateFunction)
            {
                if (isSuccess == false)
                {
                    "Cannot create.".Error_Display();
                }
                else
                {
                    "Created successfully.".Normal_Display();
                }
            }
        }
    }
}
