using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorNumeralSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static bool SignFlag = false;
        public static double result = 0;
        public static string lastSign = null;
        public static int previusSystem =10;
        private void IsEqualBtn_Click(object sender, EventArgs e)
        {
            IsEqual();
            CalcBox.Text = "";
        }

        private void setNumber(string num)
        {
            if(!SignFlag )
                Solution.Text += num;
            else
                Solution.Text = num;
                SignFlag = false;

        }

        private void button16_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = 0;
            string keyValue = e.KeyChar.ToString();
            if (Int32.TryParse(keyValue, out a))
            {
                setNumber(keyValue);
            }
        }

        private void button16_KeyDown(object sender, KeyEventArgs e)
        {
            int keyValue = e.KeyValue;
            if (!Solution.Text.Contains(".") &&( e.KeyValue == 190 || e.KeyValue == 110 || e.KeyValue == 188))
                Solution.Text = Solution.Text + ".";

            if (keyValue == 107) SignButtonClicked("+");
            if (keyValue == 109) SignButtonClicked("-");
            if (keyValue == 106) SignButtonClicked("x");
            if (keyValue == 111) SignButtonClicked("/");

            if (keyValue==46)
            Solution.Text ="0";
            if (keyValue == 8)
            {
                ClearOne();
            }
            //Solution.Text = keyValue.ToString(); show sign value
        }

        private void Solution_TextChanged(object sender, EventArgs e)
        {
            if (Solution.Text != "0" && Solution.Text.StartsWith("0") && !Solution.Text.Contains("."))
                Solution.Text = Solution.Text.Remove(0, 1);
            if (Solution.TextLength > 16)
                Solution.Text = Solution.Text.Remove(16);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            setNumber("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            setNumber("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            setNumber("9");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            setNumber("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            setNumber("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            setNumber("6");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setNumber("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setNumber("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setNumber("3");
        }

        private void button0_Click(object sender, EventArgs e)
        {
            setNumber("0");
        }

        private void plus_Click(object sender, EventArgs e)
        {
            SignButtonClicked("+");

        }

        private void minus_Click(object sender, EventArgs e)
        {
            SignButtonClicked("-");
        }

        private void div_Click(object sender, EventArgs e)
        {
            SignButtonClicked("/");
        }

        private void mul_Click(object sender, EventArgs e)
        {
            SignButtonClicked("x");
        }
        private void coma_Click(object sender, EventArgs e)
        {
            if (!Solution.Text.Contains("."))
            {
                Solution.Text +=  ".";
            }

        }

        private void SignButtonClicked(string sign)
        {
            int CurrentNumeralSystem = NumeralList.SelectedIndex + 2;
            ShowSign(sign);
            if (lastSign != null) IsEqual();
            lastSign = sign;
            if (CurrentNumeralSystem != 10)//if not decimal
                Convert(10, previusSystem);
            NumeralList.Enabled = false; 
            result = double.Parse(Solution.Text);
            if (CurrentNumeralSystem != 10)//if not decimal
                Convert(CurrentNumeralSystem);
        }

        private void ShowSign(string sign)
        {
            if (SignFlag == false)
            {
                CalcBox.Text += Solution.Text + " " + sign + " ";
            }
            else
            {
                CalcBox.Text = CalcBox.Text.Remove(CalcBox.TextLength - 3, 3);
                CalcBox.Text += " " + sign + " ";
            }
            SignFlag = true;
        }

        private void IsEqual()
        {
            NumeralList.Enabled = true;
            int CurrentNumeralSystem = NumeralList.SelectedIndex + 2;
            double lastNumber = 0;
            if(CurrentNumeralSystem != 10)//if not decimal
                Convert(10, previusSystem);
            
            try
            {
                lastNumber = double.Parse(Solution.Text);
                if (lastSign == "+")
                    Solution.Text = (lastNumber + result).ToString();
                if (lastSign == "-")
                    Solution.Text = (result - lastNumber).ToString();
                if (lastSign == "/")
                    Solution.Text = (result / lastNumber).ToString();
                if (lastSign == "x")
                    Solution.Text = (lastNumber * result).ToString();

                if (CurrentNumeralSystem != 10)//if not decimal
                    Convert(CurrentNumeralSystem);

            }
            catch
            {
                Solution.Text = "Error"; 
            }
            finally
            {
                result = 0;
                lastSign = null;
            }
        }

        private void NumeralList_SelectedIndexChanged(object sender, EventArgs e)
        {

            int CurrentNumeralSystem = NumeralList.SelectedIndex + 2;
        
                Convert(10, previusSystem);
                Convert(CurrentNumeralSystem);
            
            previusSystem = NumeralList.SelectedIndex + 2;
        }
        private void Convert( int CurrentNumeralSystem, int prevSystem=10)
        {
            NumeralSystemLabel.Text = "(" + CurrentNumeralSystem.ToString() + ")";
            long lastNumber = Int64.Parse(Solution.Text);
            long value = 0;
            double solution = 0;
            int i = 0;
            do
            {
                value = lastNumber % CurrentNumeralSystem;
                lastNumber = lastNumber / CurrentNumeralSystem;
                solution += value * Math.Pow(prevSystem, i);
                i++;
            }
            while (lastNumber != 0);

            Solution.Text = solution.ToString();
        }




        private void button11_Click(object sender, EventArgs e)
        {
            ClearOne();
        }

        void ClearOne()
        {
            if (Solution.TextLength > 1)
                Solution.Text = Solution.Text.Remove(Solution.TextLength - 1, 1);
            else Solution.Text = "0";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Solution.Text = "0";
        }
    }
}
