namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textName = new System.Windows.Forms.TextBox();
            this.Age = new System.Windows.Forms.TextBox();
            this.Salary = new System.Windows.Forms.TextBox();
            this.comboSpecializatiion = new System.Windows.Forms.ComboBox();
            this.Add = new System.Windows.Forms.Button();
            this.DeleteSelectedWorker = new System.Windows.Forms.Button();
            this.SortedWorkers = new System.Windows.Forms.Button();
            this.ChangeWorker = new System.Windows.Forms.Button();
            this.workers_list = new System.Windows.Forms.ListBox();
            this.InformationAboutConstruction = new System.Windows.Forms.Button();
            this.ResetSort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Возраст";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Специальность";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(86, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Зарплата";
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(195, 130);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(100, 31);
            this.textName.TabIndex = 4;
            // 
            // Age
            // 
            this.Age.Location = new System.Drawing.Point(195, 180);
            this.Age.Name = "Age";
            this.Age.Size = new System.Drawing.Size(100, 31);
            this.Age.TabIndex = 5;
            // 
            // Salary
            // 
            this.Salary.Location = new System.Drawing.Point(208, 279);
            this.Salary.Name = "Salary";
            this.Salary.Size = new System.Drawing.Size(100, 31);
            this.Salary.TabIndex = 6;
            // 
            // comboSpecializatiion
            // 
            this.comboSpecializatiion.FormattingEnabled = true;
            this.comboSpecializatiion.Location = new System.Drawing.Point(267, 227);
            this.comboSpecializatiion.Name = "comboSpecializatiion";
            this.comboSpecializatiion.Size = new System.Drawing.Size(121, 33);
            this.comboSpecializatiion.TabIndex = 7;
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(67, 354);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(296, 57);
            this.Add.TabIndex = 8;
            this.Add.Text = "Добавить";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // DeleteSelectedWorker
            // 
            this.DeleteSelectedWorker.Location = new System.Drawing.Point(891, 591);
            this.DeleteSelectedWorker.Name = "DeleteSelectedWorker";
            this.DeleteSelectedWorker.Size = new System.Drawing.Size(296, 50);
            this.DeleteSelectedWorker.TabIndex = 10;
            this.DeleteSelectedWorker.Text = "Удалить";
            this.DeleteSelectedWorker.UseVisualStyleBackColor = true;
            this.DeleteSelectedWorker.Click += new System.EventHandler(this.DeleteSelectedWorker_Click);
            // 
            // SortedWorkers
            // 
            this.SortedWorkers.Location = new System.Drawing.Point(593, 668);
            this.SortedWorkers.Name = "SortedWorkers";
            this.SortedWorkers.Size = new System.Drawing.Size(296, 51);
            this.SortedWorkers.TabIndex = 11;
            this.SortedWorkers.Text = "Сортировать";
            this.SortedWorkers.UseVisualStyleBackColor = true;
            this.SortedWorkers.Click += new System.EventHandler(this.SortedWorkers_Click);
            // 
            // ChangeWorker
            // 
            this.ChangeWorker.Location = new System.Drawing.Point(593, 591);
            this.ChangeWorker.Name = "ChangeWorker";
            this.ChangeWorker.Size = new System.Drawing.Size(292, 50);
            this.ChangeWorker.TabIndex = 12;
            this.ChangeWorker.Text = "Редактировать";
            this.ChangeWorker.UseVisualStyleBackColor = true;
            this.ChangeWorker.Click += new System.EventHandler(this.ChangeWorker_Click);
            // 
            // workers_list
            // 
            this.workers_list.FormattingEnabled = true;
            this.workers_list.ItemHeight = 25;
            this.workers_list.Location = new System.Drawing.Point(593, 26);
            this.workers_list.Name = "workers_list";
            this.workers_list.Size = new System.Drawing.Size(880, 529);
            this.workers_list.TabIndex = 16;
            this.workers_list.DoubleClick += new System.EventHandler(this.workers_list_DoubleClick);
            // 
            // InformationAboutConstruction
            // 
            this.InformationAboutConstruction.Location = new System.Drawing.Point(1193, 591);
            this.InformationAboutConstruction.Name = "InformationAboutConstruction";
            this.InformationAboutConstruction.Size = new System.Drawing.Size(296, 51);
            this.InformationAboutConstruction.TabIndex = 17;
            this.InformationAboutConstruction.Text = "Информация о стройке";
            this.InformationAboutConstruction.UseVisualStyleBackColor = true;
            this.InformationAboutConstruction.Click += new System.EventHandler(this.InformationAboutConstruction_Click);
            // 
            // ResetSort
            // 
            this.ResetSort.Location = new System.Drawing.Point(895, 669);
            this.ResetSort.Name = "ResetSort";
            this.ResetSort.Size = new System.Drawing.Size(296, 50);
            this.ResetSort.TabIndex = 18;
            this.ResetSort.Text = "Сбросить";
            this.ResetSort.UseVisualStyleBackColor = true;
            this.ResetSort.Click += new System.EventHandler(this.ResetSort_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1503, 887);
            this.Controls.Add(this.ResetSort);
            this.Controls.Add(this.InformationAboutConstruction);
            this.Controls.Add(this.workers_list);
            this.Controls.Add(this.ChangeWorker);
            this.Controls.Add(this.SortedWorkers);
            this.Controls.Add(this.DeleteSelectedWorker);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.comboSpecializatiion);
            this.Controls.Add(this.Salary);
            this.Controls.Add(this.Age);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Стройка";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.TextBox Age;
        private System.Windows.Forms.TextBox Salary;
        private System.Windows.Forms.ComboBox comboSpecializatiion;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button DeleteSelectedWorker;
        private System.Windows.Forms.Button SortedWorkers;
        private System.Windows.Forms.Button ChangeWorker;
        private System.Windows.Forms.Button InformationAboutConstruction;
        public System.Windows.Forms.ListBox workers_list;
        private System.Windows.Forms.Button ResetSort;
    }
}

