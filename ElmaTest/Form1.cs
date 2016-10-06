using Calc;
using System;
using System.Windows.Forms;

namespace ElmaTest
{
    public partial class Form1 : Form
    {
        private Helper Calc { get; set; }


        public Form1()
        {
            InitializeComponent();
        }

        private void btnActions_Click(object sender, EventArgs e)
        {
            Calc = new Helper();
            double x = 0;
            double y = 0;
            double.TryParse(tbX.Text, out x);
            double.TryParse(tbY.Text, out y);

            var result = Calc.Sum(x, y);
            Calc.Sum(x, y);
            lblResult.Text = result.ToString();

        }
    }
}
