using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsciiConverter
{
    class Program
    {
        private static char[] AsciiPalette = Properties.Resources.AsciiGradient.ToCharArray();
        static void Main(string[] args)
        {
            Console.WriteLine("Please input path to image");
            string imagePath = Console.ReadLine();
            Bitmap rawimage = new Bitmap(@imagePath);
            Console.WriteLine("Please choose a palette:");
            Console.WriteLine("1. Simple: @&%#FH(/O*>=,.- ");
            Console.WriteLine("2. Blocks:  ░▒▓");
            Console.WriteLine("3. Advanced: $@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,  ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    AsciiPalette = Properties.Resources.AsciiGradientSimple.ToCharArray();
                    break;
                case 2:
                    AsciiPalette = Properties.Resources.AsciiGradientBlocks.ToCharArray();
                    break;
                case 3:
                    AsciiPalette = Properties.Resources.AsciiGradient.ToCharArray();
                    break;
            }
            Console.WriteLine("Would you like to have two color setting enabled?");
            choice = int.Parse(Console.ReadLine());
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            PrintAscii(rawimage,choice);
            Console.ReadLine();
        }

        static void PrintAscii(Bitmap image, int setting)
        {
            float XScale = 1;
            float YScale = 2.1f;
            float AdditionalShit = 2;
            /*while (true)
            {
                try{

                    Console.SetWindowSize((int)(image.Width / (XScale*AdditionalShit)), (int)(image.Height / (YScale*AdditionalShit)));
                    break;
                }
                catch(ArgumentOutOfRangeException e)
                {
                    AdditionalShit *= 1.05f;
                }

            }*/
            for(int x = 0; x < image.Width/(XScale * AdditionalShit) ; x++)
            {
                for(int y = 0; y < image.Height/(YScale* AdditionalShit); y++)
                {
                    float value = (float)(image.GetPixel((int)(x *XScale * AdditionalShit), (int)(y *YScale * AdditionalShit)).R);
                    PrintAsciiPoint(x,y,value, setting);
                }
            }
        }

        static void PrintAsciiPoint(int PixelX, int PixelY, float Value, int setting)
        {
            int AsciiIndex;
            char AsciiChar;
            switch (setting)
            {
                case 1:
                    AsciiIndex = (int)((Value / 256f) * AsciiPalette.Length * 2);
                    if (AsciiIndex > AsciiPalette.Length)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        AsciiChar = AsciiPalette[AsciiIndex / 2];

                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        AsciiChar = AsciiPalette[(AsciiPalette.Length - 1) - AsciiIndex / 2];

                    }
                    break;
                default:
                    AsciiIndex = (int)((Value / 256f) * AsciiPalette.Length);
                    AsciiChar = AsciiPalette[AsciiIndex];
                    break;
            }
           
            Console.SetCursorPosition(PixelX, PixelY);
            Console.Write(AsciiChar);
        }
    }
}
