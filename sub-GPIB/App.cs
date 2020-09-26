using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sub_GPIB
{
    static class Program
    {
        static string text_S0 = "VAL00  77.204;VAL01  16.427;VAL02   0.295;VAL03  39.486;VAL04   0.371;VAL05   0.121;VAL06  56.222;VAL07   0.000;VAL08  10.609;VAL09  -0.758;VAL10   0.363;VAL11   0.543;VAL12 -75.284;VAL13 -71.248;VAL14 -73.429;1000;E08\n";

        static string text_SB = "S1  0.849;S2  0.528;S3  0.007;PDB -76.34;1000;E00\n";

        //1550.00 nm
        static string text_J1 = "J[11]  0.870  7.368;J[12]  0.557 -138.518;J[21]  0.646 -39.584;J[22]  0.736 -8.434;1000;E00\n";
        //1551.00 
        static string text_J2 = "J[11]  0.797 -64.374;J[12]  0.605 -66.324;J[21]  0.600 -111.640;J[22]  0.799 63.215;1000;E00\n";

        static string[] dataSeparator(string text,int infoS,int valS)
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

        static string[] S0_filter(string[] text)
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

        static string[] SB_filter(string[] text)
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

        static string[] JM_filter(string[] text)
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

        static void Main(string[] args)
        {
            string[] values1 = dataSeparator(text_S0, 17, 32);
            string[] values2 = dataSeparator(text_SB, 6, 10);
            string[] values3= dataSeparator(text_J1, 6, 14);

            string[] data1 = S0_filter(values1);
            string[] data2 = SB_filter(values2);
            string[] data3 = JM_filter(values3);

            for (int i = 0; i < data1.Length; i++)
            {
                Console.WriteLine(data1[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < data2.Length; i++)
            {
                Console.WriteLine(data2[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < data3.Length; i++)
            {
                Console.WriteLine(data3[i]);
            }
            Console.WriteLine();
            Console.Read();
        }
    }
}
