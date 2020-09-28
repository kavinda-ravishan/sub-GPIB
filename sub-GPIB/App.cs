using System;

namespace sub_GPIB
{
    static class Program
    {
        static void DGD(double w1,double w2, string jString1, string jString2)
        {
            Console.WriteLine(((w1 + w2) / 2).ToString() + "____" + jString1 + "____" + jString2);
        }

        static void Main(string[] args)
        {
            //double[] dgd = Utility.DGD(Utility.text_J1, Utility.text_J2, 1550, 1551);

            //Console.WriteLine(dgd[0]);
            //Console.WriteLine(dgd[1]);

            double start = 1550;
            double end = 1561;
            double stepSize = 1;

            int steps = (int)((end - start) / stepSize) + 3;

            double[] wavelenght = new double[steps];

            for (int i = 0; i < steps; i++)
            {
                wavelenght[i] = (start - stepSize) + (i * stepSize);
            }

            string[] jStrings = new string[steps];

            for (int i = 0; i < steps; i++)
            {
                jStrings[i] = ". . .";
            }

            for (int i = 0; i < steps; i++)
            {
                if (i < 2)
                {
                    Console.WriteLine("Read - " + wavelenght[i].ToString());
                    jStrings[i] = i.ToString() + "_" + wavelenght[i].ToString(); // read from pol
                }
                else
                {
                    Console.WriteLine("Read - " + wavelenght[i].ToString());
                    jStrings[i] = i.ToString() + "_" + wavelenght[i].ToString(); // read from pol

                    DGD(wavelenght[i - 2], wavelenght[i], jStrings[i - 2], jStrings[i]);
                }
            }

            Console.Read();
        }
    }
}
