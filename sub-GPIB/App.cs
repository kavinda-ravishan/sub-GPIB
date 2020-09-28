using System;

namespace sub_GPIB
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Utility.DGD(Utility.text_J1, Utility.text_J2, 1550, 1551));

            Console.Read();
        }
    }
}
