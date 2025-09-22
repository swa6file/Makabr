using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class InfoConstruction : Form
    {
        public InfoConstruction(int allsalery, int electricians, int painters, int craneOperators, int generalWorkers)
        {
            InitializeComponent();
            AllSalerysWorkers.Text = Convert.ToString(allsalery);
            Electricians.Text = Convert.ToString(electricians);
            Painters.Text = Convert.ToString(painters);
            CraneOperators.Text = Convert.ToString(craneOperators);
            GeneralWorkers.Text = Convert.ToString(generalWorkers);
        }


    }
}
