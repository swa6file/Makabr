namespace WindowsFormsApp1
{
    partial class SortWorkers
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
            this.sort = new System.Windows.Forms.Button();
            this.fspecialization = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.start_age = new System.Windows.Forms.TextBox();
            this.start_salary = new System.Windows.Forms.TextBox();
            this.find_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.last_salary = new System.Windows.Forms.TextBox();
            this.end_age = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sort
            // 
            this.sort.Location = new System.Drawing.Point(241, 351);
            this.sort.Name = "sort";
            this.sort.Size = new System.Drawing.Size(190, 38);
            this.sort.TabIndex = 0;
            this.sort.Text = "Сортировать";
            this.sort.UseVisualStyleBackColor = true;
            this.sort.Click += new System.EventHandler(this.sort_Click);
            // 
            // fspecialization
            // 
            this.fspecialization.FormattingEnabled = true;
            this.fspecialization.Location = new System.Drawing.Point(217, 157);
            this.fspecialization.Name = "fspecialization";
            this.fspecialization.Size = new System.Drawing.Size(151, 33);
            this.fspecialization.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Возраст";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Специализация";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 11;
            // 
            // start_age
            // 
            this.start_age.Location = new System.Drawing.Point(217, 94);
            this.start_age.Name = "start_age";
            this.start_age.Size = new System.Drawing.Size(97, 31);
            this.start_age.TabIndex = 6;
            // 
            // start_salary
            // 
            this.start_salary.Location = new System.Drawing.Point(217, 215);
            this.start_salary.Name = "start_salary";
            this.start_salary.Size = new System.Drawing.Size(139, 31);
            this.start_salary.TabIndex = 7;
            // 
            // find_name
            // 
            this.find_name.Location = new System.Drawing.Point(217, 37);
            this.find_name.Name = "find_name";
            this.find_name.Size = new System.Drawing.Size(151, 31);
            this.find_name.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Зарплата";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(177, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 25);
            this.label7.TabIndex = 12;
            this.label7.Text = "от";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(177, 221);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 25);
            this.label8.TabIndex = 13;
            this.label8.Text = "от";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(320, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 25);
            this.label9.TabIndex = 14;
            this.label9.Text = "до";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(362, 218);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 25);
            this.label10.TabIndex = 15;
            this.label10.Text = "до";
            // 
            // last_salary
            // 
            this.last_salary.Location = new System.Drawing.Point(404, 215);
            this.last_salary.Name = "last_salary";
            this.last_salary.Size = new System.Drawing.Size(139, 31);
            this.last_salary.TabIndex = 16;
            // 
            // end_age
            // 
            this.end_age.Location = new System.Drawing.Point(362, 94);
            this.end_age.Name = "end_age";
            this.end_age.Size = new System.Drawing.Size(97, 31);
            this.end_age.TabIndex = 17;
            // 
            // SortWorkers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 452);
            this.Controls.Add(this.end_age);
            this.Controls.Add(this.last_salary);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.find_name);
            this.Controls.Add(this.start_salary);
            this.Controls.Add(this.start_age);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fspecialization);
            this.Controls.Add(this.sort);
            this.Name = "SortWorkers";
            this.Text = "SortWorkers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sort;
        private System.Windows.Forms.ComboBox fspecialization;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox start_age;
        private System.Windows.Forms.TextBox start_salary;
        private System.Windows.Forms.TextBox find_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox last_salary;
        private System.Windows.Forms.TextBox end_age;
    }
}