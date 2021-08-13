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
    public partial class Form1 : Form
    {
        char[] alphabet = new char[] { '#', ' ', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И','Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                       'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ', 'Э', 'Ю', 'Я',  '1', '2', '3', '4', '5',
                                       '6', '7', '8', '9', '0'};

        MillerTest_Class millerTest_ = new MillerTest_Class();
        Encode_Class Encode = new Encode_Class();
        Decode_Class Decode = new Decode_Class();
        Calculate_Class Calculate = new Calculate_Class();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //зашифровать
        {
            if ((textBox_p.Text != "") && (textBox_q.Text != ""))
            {
                if (textBox_q.Text != textBox_p.Text)
                {
                    long p = Convert.ToInt64(textBox_p.Text);
                    long q = Convert.ToInt64(textBox_q.Text);
                  
                    if (millerTest_.MillerRabinTest(p, 30) && millerTest_.MillerRabinTest(q, 30)) //проверка простые числа или нет
                    {
                        string sr = textBox1.Text.ToUpper();

                        long n = p * q; //вычисление модуля (перемножаем два простых числа)
                        long m = (p - 1) * (q - 1); //функция эйлера
                        long e_ = Calculate.Calculate_e(m); //вычисление числа e (оно взаимно прост , меньше и простое)
                        long d = Calculate.Calculate_d(m, e_);//вычисление d
                        //d n пара ключей (секр)
                        //e n пара ключей (откр)
                        List<string> s = new List<string>();
                        s = Encode.RSA_Encode(sr, e_, n, alphabet);
                       string sr1 = String.Join(" ", s.ToArray());
                        textBox2.Text = sr1.ToString();

                        textBox_d.Text = d.ToString();
                        textBox_n.Text = n.ToString();
                    }
                    else MessageBox.Show("p или q - не простые числа!");
                }
                else MessageBox.Show("Введите различные p и q"); 

            } else MessageBox.Show("Введите p и q");
        }

        private void button2_Click(object sender, EventArgs e) //расшифр
        {
            if ((textBox_d.Text != "") && (textBox_n.Text != ""))
            {
                long d = Convert.ToInt64(textBox_d.Text);
                long n = Convert.ToInt64(textBox_n.Text);

                string str = textBox1.Text;
                string[] s = str.Split(' ');
                string result = Decode.RSA_Decode(s, d, n, alphabet); //расшифровка

                textBox2.Text = result;
            }
            else
                MessageBox.Show("Введите секретный ключ!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}

