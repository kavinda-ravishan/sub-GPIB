using System;

namespace sub_GPIB
{
    static class Program
    {

        static void Main(string[] args)
        {

            double[] jValues1 = Utility.JonesString2Double(Utility.text_J1);
            double[] jValues2 = Utility.JonesString2Double(Utility.text_J2);

            JonesMatPol mat1 = Utility.JonesDoubleArray2JonesMat(jValues1);
            JonesMatPol mat2 = Utility.JonesDoubleArray2JonesMat(jValues2);

            CMath.PrintJonesMAT(CMath.Pol2Car(mat1));
            Console.WriteLine();
            CMath.PrintJonesMAT(CMath.Pol2Car(mat2));

            Console.Read();
        }
    }
}
