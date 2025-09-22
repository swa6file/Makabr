using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogical;

namespace WindowsFormsApp1
{
    public partial class WorkerInfo : Form
    {
        public WorkerInfo(Worker worker)
        {
            InitializeComponent();

            textBoxName.Text = worker.Name;
            textBoxAge.Text = worker.Age.ToString();
            textBoxSpec.Text = worker.Specialization.ToString();
            textBoxSalary.Text = worker.Salary.ToString();

        }



    }
}
