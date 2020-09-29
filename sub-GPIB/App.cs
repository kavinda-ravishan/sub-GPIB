using System;
using NationalInstruments.NI4882;

namespace sub_GPIB
{
    static class Program
    {
        static string MsgWaveLenghtSrc(double waveLenght = 1551.120)
        {
            return ":WAVElength " + waveLenght.ToString() + "nm";
        }

        static string MsgPowerSrc(double power = 1000)
        {
            return ":POWer " + power.ToString() + "uW";
        }

        static string MsgWaveLenghtPol(double waveLenght = 1551.12)
        {
            return "L " + waveLenght.ToString() + ";X;";
        }

        static void Main(string[] args)
        {
            Device PolarizationAnalyzer = new Device(0, 9, 0);
            Device Source = new Device(0, 24, 0);

            double start = 1550;
            double end = 1551.1;
            double stepSize = 0.01;

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

            int delay = 1000;

            Source.Write(Utility.ReplaceCommonEscapeSequences(MsgPowerSrc(1000))); // set power to 1000uW
            Source.Write(Utility.ReplaceCommonEscapeSequences(":OUTPut 1")); // turn on the laser
            Console.WriteLine("Set Source  WL - " + start.ToString());
            Source.Write(Utility.ReplaceCommonEscapeSequences(MsgWaveLenghtSrc(start)));//change wavelength source
            PolarizationAnalyzer.Write(Utility.ReplaceCommonEscapeSequences("PO;X;"));

            for (int i = 0; i < steps; i++)
            {
                if (i < 2)
                {
                    Console.WriteLine("Set Source  WL - " + wavelenght[i].ToString());
                    Source.Write(Utility.ReplaceCommonEscapeSequences(MsgWaveLenghtSrc(wavelenght[i])));//change wavelength source

                    Console.WriteLine("Set PAT9000 WL - " + wavelenght[i].ToString());
                    PolarizationAnalyzer.Write(Utility.ReplaceCommonEscapeSequences(MsgWaveLenghtPol(wavelenght[i])));//change wavelength pol
                    System.Threading.Thread.Sleep(delay);

                    Console.WriteLine("Read JM        - " + wavelenght[i].ToString());
                    PolarizationAnalyzer.Write(Utility.ReplaceCommonEscapeSequences("JM;X"));
                    jStrings[i] = Utility.InsertCommonEscapeSequences(PolarizationAnalyzer.ReadString());//put JM data here

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Set Source  WL - " + wavelenght[i].ToString());
                    Source.Write(Utility.ReplaceCommonEscapeSequences(MsgWaveLenghtSrc(wavelenght[i])));//change wavelength source

                    Console.WriteLine("Set PAT9000 WL - " + wavelenght[i].ToString());
                    PolarizationAnalyzer.Write(Utility.ReplaceCommonEscapeSequences(MsgWaveLenghtPol(wavelenght[i])));//change wavelength pol
                    System.Threading.Thread.Sleep(delay);

                    Console.WriteLine("Read JM        - " + wavelenght[i].ToString());
                    PolarizationAnalyzer.Write(Utility.ReplaceCommonEscapeSequences("JM;X"));
                    jStrings[i] = Utility.InsertCommonEscapeSequences(PolarizationAnalyzer.ReadString());//put JM data here

                    DGDval = Utility.DGD(Utility.text_J1, Utility.text_J2, wavelenght[i - 2], wavelenght[i]);//put jString here
                    DGDs[i - 2, 0] = DGDval[0];
                    DGDs[i - 2, 1] = DGDval[1];

                    Console.WriteLine(DGDval[0]);
                    Console.WriteLine(DGDval[1]);
                    Console.WriteLine();
                }
            }

            Source.Write(Utility.ReplaceCommonEscapeSequences(":OUTPut 0")); // turn off the laser


            PolarizationAnalyzer.Dispose();
            Source.Dispose();

            Console.Read();
        }
    }
}
