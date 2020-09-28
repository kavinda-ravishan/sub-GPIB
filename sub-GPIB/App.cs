using System;

namespace sub_GPIB
{
    static class Program
    {

        static void Main(string[] args)
        {
            //double[] jValues1 = Utility.JonesString2Double(Utility.text_J1);
            //double[] jValues2 = Utility.JonesString2Double(Utility.text_J2);

            //JonesMatPol mat1 = Utility.JonesDoubleArray2JonesMat(jValues1);
            //JonesMatPol mat2 = Utility.JonesDoubleArray2JonesMat(jValues2);

            //JonesMatCar J1 = CMath.Pol2Car(mat1);
            //JonesMatCar J1Inv = CMath.Inverse(J1);

            //JonesMatCar J2 = CMath.Pol2Car(mat2);

            //JonesMatCar J1_J2Inv = J2 * J1Inv;

            //ComplexCar[] complexCars = CMath.Eigenvalues(J1_J2Inv);

            //CMath.Print(complexCars[0]);
            //Console.WriteLine();
            //CMath.Print(complexCars[1]);
            //Console.WriteLine();

            Console.WriteLine(CMath.Red2Deg(3.5*Math.PI));
            //Console.WriteLine(CMath.Deg2Red(90*6));
            //Console.WriteLine(361%360);

            Console.Read();
        }
    }
}
