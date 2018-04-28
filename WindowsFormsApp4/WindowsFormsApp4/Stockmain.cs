using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Stockmain : Form
    {
        

        public Stockmain()
        {
            InitializeComponent();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.MdiParent = this;
            pro.Show();
        }

        private void Stockmain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();

        }
    }
}
