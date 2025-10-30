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
using Model;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Форма содержащая информацию о выбранном работнике
    /// </summary>
    public partial class WorkerInfo : Form
    {
        /// <summary>
        /// Иницилизация полей работника
        /// </summary>
        /// <param name="worker"> Выбранный работник </param>
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
