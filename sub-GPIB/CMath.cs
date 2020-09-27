using System;

namespace sub_GPIB
{
    struct ComplexCar
    {
        public double real;
        public double imag;
    }

    struct ComplexPol
    {
        public double mod;
        public double ang; // in rad
    }

    struct JonesMatCar
    {
        public ComplexCar J11;
        public ComplexCar J12;
        public ComplexCar J21;
        public ComplexCar J22;
    }

    struct JonesMatPol
    {
        public ComplexPol J11;
        public ComplexPol J12;
        public ComplexPol J21;
        public ComplexPol J22;
    }

    static class CMath
    {
        #region print
        public static void PrintComplex(ComplexCar complexCar)
        {
            if (complexCar.imag >= 0)
            {
                Console.Write(complexCar.real + " +" + complexCar.imag + " i");
            }
            else
            {
                Console.Write(complexCar.real + " " + complexCar.imag + " i");
            }
        }

        public static void PrintComplex(ComplexPol complexPol)
        {
            Console.Write(complexPol.mod + " |_" + complexPol.ang);
        }

        public static void PrintJonesMAT(JonesMatCar mat)
        {
            PrintComplex(mat.J11);
            Console.Write("  ");
            PrintComplex(mat.J12);
            Console.WriteLine();
            PrintComplex(mat.J21);
            Console.Write("  ");
            PrintComplex(mat.J22);
            Console.WriteLine();
        }

        public static void PrintJonesMAT(JonesMatPol mat)
        {
            PrintComplex(mat.J11);
            Console.Write("  ");
            PrintComplex(mat.J12);
            Console.WriteLine();
            PrintComplex(mat.J21);
            Console.Write("  ");
            PrintComplex(mat.J22);
            Console.WriteLine();
        }
        #endregion

        public static double Deg2Red(double deg)
        {
            return (Math.PI * deg) / 180;
        }

        public static ComplexCar Pol2Car(ComplexPol complexPol)
        {
            ComplexCar complexCar;

            complexCar.real = complexPol.mod * Math.Cos(complexPol.ang);
            complexCar.imag = complexPol.mod * Math.Sin(complexPol.ang);

            return complexCar;
        }

        public static JonesMatCar Pol2Car(JonesMatPol jonesMatPol)
        {
            JonesMatCar jonesMatCar;

            jonesMatCar.J11 = Pol2Car(jonesMatPol.J11);
            jonesMatCar.J12 = Pol2Car(jonesMatPol.J12);
            jonesMatCar.J21 = Pol2Car(jonesMatPol.J21);
            jonesMatCar.J22 = Pol2Car(jonesMatPol.J22);

            return jonesMatCar;
        }
    }
}
