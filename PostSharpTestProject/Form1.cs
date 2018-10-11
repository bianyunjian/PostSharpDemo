using PostSharpSample.Multithreading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostSharpTestProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Multithreading instance = new Multithreading();
            instance.Work((obj) =>
            {
                this.label1.Text = obj.ToString();
            });

        }
    }
}
