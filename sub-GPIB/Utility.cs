using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sub_GPIB
{
    static class Utility
    {
        //---------------------- For testing ------------------------------------------------------------------------------------------------//
        public static string text_S0 = "VAL00  77.204;VAL01  16.427;VAL02   0.295;VAL03  39.486;VAL04   0.371;VAL05   0.121;VAL06  56.222;VAL07   0.000;VAL08  10.609;VAL09  -0.758;VAL10   0.363;VAL11   0.543;VAL12 -75.284;VAL13 -71.248;VAL14 -73.429;1000;E08\n";

        public static string text_SB = "S1  0.849;S2  0.528;S3  0.007;PDB -76.34;1000;E00\n";

        public static string text_J1_1 = "J[11]  0.902 -45.363;J[12]  0.383 -179.843;J[21]  0.420 -4.662;J[22]  0.931 46.226;1000;E00\n";
        //1550.01 nm
        public static string text_J1_2 = "J[11]  0.907 -44.460;J[12]  0.405 -178.349;J[21]  0.416 -3.936;J[22]  0.917 44.923;1000;E00\n";

        //1550.00 nm
        public static string text_J1 = "J[11]  0.870  7.368;J[12]  0.557 -138.518;J[21]  0.646 -39.584;J[22]  0.736 -8.434;1000;E00\n";
        //1551.00 
        public static string text_J2 = "J[11]  0.797 -64.374;J[12]  0.605 -66.324;J[21]  0.600 -111.640;J[22]  0.799 63.215;1000;E00\n";
        //-----------------------------------------------------------------------------------------------------------------------------------//
        public static string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }
        public static string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }

        public const double C = 299792458;

        public static string[] DataSeparator(string text, int infoS, int valS)
        {
            string[] info = new string[infoS];
            string[] values = new string[valS];

            int j = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ';')
                {
                    j++;
                }
                else
                {
                    info[j] += text[i];
                }
            }

            j = 0;

            for (int i = 0; i < info.Length; i++)
            {
                for (int k = 0; k < info[i].Length; k++)
                {
                    if (info[i][k] == ' ')
                    {
                        while (info[i][k + 1] == ' ')
                        {
                            k++;
                        }
                        j++;
                    }
                    else
                    {
                        values[j] += info[i][k];
                    }
                }
                j++;
            }

            return values;
        }

        public static string[] S0_filter(string[] text)
        {
            string[] S0 = new string[17];

            for (int i = 0; i < 15; i++)
            {
                S0[i] = text[i * 2 + 1];
            }

            S0[15] = text[30];
            S0[16] = text[31].Substring(0, 3);

            return S0;
        }

        public static string[] SB_filter(string[] text)
        {
            string[] SB = new string[6];

            for (int i = 0; i < 4; i++)
            {
                SB[i] = text[i * 2 + 1];
            }

            SB[4] = text[8];
            SB[5] = text[9].Substring(0, 3);

            return SB;
        }

        public static string[] JM_filter(string[] text)
        {
            string[] JM = new string[10];

            for (int i = 0; i < 4; i++)
            {
                JM[i * 2] = text[i * 3 + 1];
                JM[i * 2 + 1] = text[i * 3 + 2];
            }

            JM[8] = text[12];
            JM[9] = text[13].Substring(0, 3);

            return JM;
        }

        public static double[] JonesString2Double(string text)
        {
            string[] values = DataSeparator(text, 6, 14);

            string[] data = JM_filter(values);

            double[] jonesMat = new double[8];

            for (int i = 0; i < 8; i++)
            {
                jonesMat[i] = Convert.ToDouble(data[i]);
            }

            return jonesMat;
        }

        //Jones double array comes with angle degries function convert it to red
        public static JonesMatPol JonesDoubleArray2JonesMat(double[] jonesValues)
        {
            JonesMatPol mat;

            mat.J11.mod = jonesValues[0];
            mat.J11.ang = CMath.Deg2Red(jonesValues[1]);

            mat.J12.mod = jonesValues[2];
            mat.J12.ang = CMath.Deg2Red(jonesValues[3]);

            mat.J21.mod = jonesValues[4];
            mat.J21.ang = CMath.Deg2Red(jonesValues[5]);

            mat.J22.mod = jonesValues[6];
            mat.J22.ang = CMath.Deg2Red(jonesValues[7]);

            return mat;
        }

        public static double Wavelength2Frequency(double wavelength)//wavelength in nm and frequency in THz
        {
            return C / (wavelength * 1000);
        }

        public static double[] DGD(string j1, string j2, double w1, double w2)
        {
            double[] jValues1 = JonesString2Double(j1);
            double[] jValues2 = JonesString2Double(j2);

            JonesMatPol mat1 = JonesDoubleArray2JonesMat(jValues1);
            JonesMatPol mat2 = JonesDoubleArray2JonesMat(jValues2);

            JonesMatCar J1 = CMath.Pol2Car(mat1);
            JonesMatCar J1Inv = CMath.Inverse(J1);

            JonesMatCar J2 = CMath.Pol2Car(mat2);

            JonesMatCar J1_J2Inv = J2 * J1Inv;

            ComplexCar[] complexCars = CMath.Eigenvalues(J1_J2Inv);

            ComplexPol[] complexPols = new ComplexPol[2];

            complexPols[0] = CMath.Car2Pol(complexCars[0]);
            complexPols[1] = CMath.Car2Pol(complexCars[1]);

            double Ang = complexPols[0].ang - complexPols[1].ang;

            double f1 = Wavelength2Frequency(w1);
            double f2 = Wavelength2Frequency(w2);

            double[] DGD_W = { Ang / (f1 - f2), (w1 + w2) / 2 };

            return DGD_W;
        }
    }
}
