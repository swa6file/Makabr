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
    /// <summary>
    ///  Форма содержащая главную информацию о стройке.
    /// </summary>
    public partial class InfoConstruction : Form
    {
        /// <summary>
        /// Конструктор подгружающий в форму основные данные о стройке.
        /// </summary>
        /// <param name="allsalery">Общая зарплата работников</param>
        /// <param name="electricians">Кол-во электриков</param>
        /// <param name="painters">Кол-во Маляров</param>
        /// <param name="craneOperators">Кол-во Крановщиков</param>
        /// <param name="generalWorkers">Кол-во Разнорабочих</param>
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
