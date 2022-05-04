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


            Console.WriteLine("Would you like to display the image while its printing?");
            choice = int.Parse(Console.ReadLine());
            bool displaychoice = true;
            int colorchoice;
            if (choice == 0)
            {
                displaychoice = false;
                colorchoice = 0;
            }
            else
            {
                Console.WriteLine("Would you like to have two color setting enabled?");
                colorchoice = int.Parse(Console.ReadLine());
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

            }
            Console.CursorVisible = false;

            PrintAscii(rawimage, colorchoice, imagePath, displaychoice);
            Console.ReadLine();
        }

        static void PrintAscii(Bitmap image, int setting, string path, bool displaysetting)
        {
            float XScale = 1;
            float YScale = 2.1f;
            float AdditionalShit = 1;
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
            String[] AsciiText = new string[image.Height / (int)(YScale * AdditionalShit)];
            for(int y = 0; y < image.Height / (YScale * AdditionalShit); y++)
            {
                for(int x = 0; x < image.Width / (XScale * AdditionalShit); x++)
                {
                    Color pixelColor = image.GetPixel((int)(x * XScale * AdditionalShit), (int)(y * YScale * AdditionalShit));
                    float value = (float)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B);
                    char AddedChar = PrintAsciiPoint(x,y,value, setting,displaysetting);
                    AsciiText[y] += AddedChar;
                }
            }
            if(setting != 1) PrintAsciiToFile(AsciiText,path);
        }

        static char PrintAsciiPoint(int PixelX, int PixelY, float Value, int setting, bool DisplaySetting)
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


            if (DisplaySetting)
            {
                Console.SetCursorPosition(PixelX, PixelY);
                Console.Write(AsciiChar);
            }
            return AsciiChar;
        }

        static void PrintAsciiToFile(string[] Lines, string path)
        {
            System.IO.File.WriteAllLines(path.Substring(0, path.Length - 4) + "_asciiFile.txt", Lines);
        }
    }
}
