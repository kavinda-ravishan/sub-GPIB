using System;

namespace sub_GPIB
{
    static class Program
    {
        static void Main(string[] args)
        {
            double start = 1545;
            double end = 1555;
            double stepSize = 1;

            int steps = (int)((end - start) / stepSize) + 3;

            double[] wavelenght = new double[steps];

            //find wavelenghts need to mesure
            for (int i = 0; i < steps; i++)
            {
                wavelenght[i] = (start - stepSize) + (i * stepSize);
            }

            //for save PAT9300 JM information
            string[] jStrings = new string[steps];

            double[] DGDval = new double[2];
            double[,] DGDs = new double[steps - 2, 2];

            for (int i = 0; i < steps; i++)
            {
                if (i < 2)
                {
                    Console.WriteLine("Set Source  WL - " + wavelenght[i].ToString());
                    Console.WriteLine("Set PAT9000 WL - " + wavelenght[i].ToString());
                    System.Threading.Thread.Sleep(1000);

                    Console.WriteLine("Read JM        - " + wavelenght[i].ToString());
                    jStrings[i] = i.ToString();//put JM data here

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Set Source  WL - " + wavelenght[i].ToString());
                    Console.WriteLine("Set PAT9000 WL - " + wavelenght[i].ToString());
                    System.Threading.Thread.Sleep(1000);

                    Console.WriteLine("Read JM        - " + wavelenght[i].ToString());
                    jStrings[i] = i.ToString();//put JM data here

                    DGDval = Utility.DGD(Utility.text_J1, Utility.text_J2, wavelenght[i - 2], wavelenght[i]);//put jString here
                    DGDs[i - 2, 0] = DGDval[0];
                    DGDs[i - 2, 1] = DGDval[1];

                    Console.WriteLine(DGDval[0]);
                    Console.WriteLine(DGDval[1]);
                    Console.WriteLine();
                }
            }

            //Console.WriteLine();

            //for (int i = 0; i < steps - 2; i++)
            //{
            //    Console.WriteLine(DGDs[i, 0]);
            //    Console.WriteLine(DGDs[i, 1]);
            //    Console.WriteLine();
            //}

            Console.Read();
        }
    }
}
