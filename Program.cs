using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RecordPlayMouseMove
{
    class Program
    {
        
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        public struct POINT
        {
            public int X;
            public int Y;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                POINT current_pos, prev_pos;
                List<POINT> coords = new List<POINT>();

                prev_pos.X = 0;
                prev_pos.Y = 0;


                Console.WriteLine("Press any key to start/stop recording mouse movements.");
                Console.ReadKey();
                do
                {
                    if (GetCursorPos(out current_pos))
                    {

                        if ((current_pos.X != prev_pos.X) || (current_pos.Y != prev_pos.Y))
                        {

                            Console.WriteLine("({0},{1})", current_pos.X, current_pos.Y);
                            coords.Add(current_pos);
                        }

                        prev_pos.X = current_pos.X;
                        prev_pos.Y = current_pos.Y;
                    }

                } 
                while (!Console.KeyAvailable);
                Console.ReadKey();

                Console.WriteLine("Press any key to play the recorded mouse positions.");
                Console.ReadKey();
                foreach (POINT coord in coords)
                {
                    SetCursorPos(coord.X, coord.Y);
                    System.Threading.Thread.Sleep(1);
                    if (Console.KeyAvailable) break;
                }
            }
        }
    }
}