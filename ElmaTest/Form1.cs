using Calc;
using System;
using System.Windows.Forms;
using HistoryFile;

namespace ElmaTest
{
    public partial class Form1 : Form
    {
        private Helper Calc { get; set; }
        private string ActiveOperation { get; set; }
        private History HistoryFiles { get; set; }
        
        public Form1()
        {
            InitializeComponent();
            Calc = new Helper();
            HistoryFiles = new History();
            var methods = Calc.metods();
            var count = 0;
            gbActions.SuspendLayout();

            foreach (var m in methods)
            {
                CreateRadioButton(m.Name, count++);
            }

            gbActions.ResumeLayout();
            HistoryFiles.HistoryLoad(ref rtbHistory);
        }

        private void CreateRadioButton(string Name, int index)
        {
            var rbBtn = new RadioButton();
            gbActions.Controls.Add(rbBtn);
            
            rbBtn.AutoSize = true;
            rbBtn.Location = new System.Drawing.Point(7, 15 + index * 18);
            rbBtn.Name = "rbBtn" + index.ToString();
            rbBtn.Size = new System.Drawing.Size(53, 17);
            rbBtn.TabIndex = 5;
            rbBtn.TabStop = true;
            rbBtn.Tag = Name;
            rbBtn.Text = Name;
            rbBtn.UseVisualStyleBackColor = true;
            rbBtn.CheckedChanged += new EventHandler(radio_CheckedChanged);
        }

        private void btnActions_Click(object sender, EventArgs e)
        {
            double x = 0;
            double y = 0;

            if (!double.TryParse(tbX.Text, out x) || !double.TryParse(tbY.Text, out y) || ActiveOperation == null)
            {
                rtbHistory.Text += string.Format("При вводе данных была ошибка " + Environment.NewLine);
                return;
            }

            var calcType = Calc.GetType();
            var method = calcType.GetMethod(ActiveOperation);
            var result = method.Invoke(Calc, new object[] { x, y });

            lblResult.Text = result.ToString();
            rtbHistory.Text += string.Format("{0} {1} {2} = {3}{4}", x, ActiveOperation, y, result, Environment.NewLine);
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb == null)
            {
                return;
            }
            ActiveOperation = rb.Tag.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HistoryFiles.HistorySave(ref rtbHistory);
        }
    }
}
