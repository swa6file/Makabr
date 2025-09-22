namespace WindowsFormsApp1
{
    partial class ChangeWorkerForm
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
            this.change_name = new System.Windows.Forms.TextBox();
            this.change_age = new System.Windows.Forms.TextBox();
            this.change_salary = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.change_worker2 = new System.Windows.Forms.Button();
            this.change_specialization = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // change_name
            // 
            this.change_name.Location = new System.Drawing.Point(278, 103);
            this.change_name.Name = "change_name";
            this.change_name.Size = new System.Drawing.Size(100, 31);
            this.change_name.TabIndex = 0;
            // 
            // change_age
            // 
            this.change_age.Location = new System.Drawing.Point(278, 157);
            this.change_age.Name = "change_age";
            this.change_age.Size = new System.Drawing.Size(100, 31);
            this.change_age.TabIndex = 1;
            // 
            // change_salary
            // 
            this.change_salary.Location = new System.Drawing.Point(278, 254);
            this.change_salary.Name = "change_salary";
            this.change_salary.Size = new System.Drawing.Size(100, 31);
            this.change_salary.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Возраст";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Специализация";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(97, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Зарплата";
            // 
            // change_worker2
            // 
            this.change_worker2.Location = new System.Drawing.Point(140, 349);
            this.change_worker2.Name = "change_worker2";
            this.change_worker2.Size = new System.Drawing.Size(183, 53);
            this.change_worker2.TabIndex = 7;
            this.change_worker2.Text = "Изменить";
            this.change_worker2.UseVisualStyleBackColor = true;
            this.change_worker2.Click += new System.EventHandler(this.change_worker2_Click);
            // 
            // change_specialization
            // 
            this.change_specialization.FormattingEnabled = true;
            this.change_specialization.Location = new System.Drawing.Point(278, 207);
            this.change_specialization.Name = "change_specialization";
            this.change_specialization.Size = new System.Drawing.Size(121, 33);
            this.change_specialization.TabIndex = 8;
            // 
            // ChangeWorkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 470);
            this.Controls.Add(this.change_specialization);
            this.Controls.Add(this.change_worker2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.change_salary);
            this.Controls.Add(this.change_age);
            this.Controls.Add(this.change_name);
            this.Name = "ChangeWorkerForm";
            this.Text = "ChangeWorkerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox change_name;
        private System.Windows.Forms.TextBox change_age;
        private System.Windows.Forms.TextBox change_salary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button change_worker2;
        private System.Windows.Forms.ComboBox change_specialization;
    }
}