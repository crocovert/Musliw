using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Musliw
{
    public partial class Avancement : Form
    {
        public Avancement()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}