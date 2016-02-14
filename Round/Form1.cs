using System;
using System.Windows.Forms;

namespace Round
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Timer timer = new Timer {Interval = 100};
            timer.Tick += OnTimerIvent;
            timer.Start();
        }

        private void OnTimerIvent(object source, EventArgs myEventArgs)
        {
            foreach (Round round in panel1.Controls)
            {
                round.MovePos();
            }
        }

        private void pnl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                panel1.Controls.Add(new Round(e.Location));
        }

    }
}
