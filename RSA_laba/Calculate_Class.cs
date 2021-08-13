using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace RSA_laba
{
    class Calculate_Class
    {
        MillerTest_Class millerTest_ = new MillerTest_Class();
        public long Calculate_e(long m) //нахождение e
        {
            long e_long = m - 1;
            BigInteger e = new BigInteger(e_long);

            while (millerTest_.MillerRabinTest(e, 30) != true && VzaimProst(e_long, m) != true)
            {
                e--;
                e_long--;
            }
            return e_long;
        }

        public bool VzaimProst(long e, long m) //взаимнопрост
        {
            while ((e != 0) && (m != 0)) //алгоритм Евклида проверка на взаим прост e c m
            {
                if (e > m)
                {
                    e = e % m;
                }
                else
                {
                    m = m % e;
                }
            }
            if ((m + e) != 1) return false;
            else return true;
        }

        public long Calculate_d(long m, long e)  //вычисление параметра 
        {
            long d = 1;
            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    d++;
            }
            return d;
        }
    }
}
