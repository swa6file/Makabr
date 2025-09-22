namespace WindowsFormsApp1
{
    partial class InfoConstruction
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
            this.label5 = new System.Windows.Forms.Label();
            this.AllSalerysWorkers = new System.Windows.Forms.TextBox();
            this.CraneOperators = new System.Windows.Forms.TextBox();
            this.Painters = new System.Windows.Forms.TextBox();
            this.Electricians = new System.Windows.Forms.TextBox();
            this.GeneralWorkers = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Общая зарплата работников";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Электриков";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Маляров";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Крановщиков";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Разнорабочих";
            // 
            // AllSalerysWorkers
            // 
            this.AllSalerysWorkers.Location = new System.Drawing.Point(407, 52);
            this.AllSalerysWorkers.Name = "AllSalerysWorkers";
            this.AllSalerysWorkers.ReadOnly = true;
            this.AllSalerysWorkers.Size = new System.Drawing.Size(207, 31);
            this.AllSalerysWorkers.TabIndex = 5;
            // 
            // CraneOperators
            // 
            this.CraneOperators.Location = new System.Drawing.Point(407, 207);
            this.CraneOperators.Name = "CraneOperators";
            this.CraneOperators.ReadOnly = true;
            this.CraneOperators.Size = new System.Drawing.Size(87, 31);
            this.CraneOperators.TabIndex = 6;
            // 
            // Painters
            // 
            this.Painters.Location = new System.Drawing.Point(407, 153);
            this.Painters.Name = "Painters";
            this.Painters.ReadOnly = true;
            this.Painters.Size = new System.Drawing.Size(87, 31);
            this.Painters.TabIndex = 7;
            // 
            // Electricians
            // 
            this.Electricians.Location = new System.Drawing.Point(407, 103);
            this.Electricians.Name = "Electricians";
            this.Electricians.ReadOnly = true;
            this.Electricians.Size = new System.Drawing.Size(87, 31);
            this.Electricians.TabIndex = 8;
            // 
            // GeneralWorkers
            // 
            this.GeneralWorkers.Location = new System.Drawing.Point(407, 261);
            this.GeneralWorkers.Name = "GeneralWorkers";
            this.GeneralWorkers.ReadOnly = true;
            this.GeneralWorkers.Size = new System.Drawing.Size(87, 31);
            this.GeneralWorkers.TabIndex = 9;
            // 
            // InfoConstruction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 372);
            this.Controls.Add(this.GeneralWorkers);
            this.Controls.Add(this.Electricians);
            this.Controls.Add(this.Painters);
            this.Controls.Add(this.CraneOperators);
            this.Controls.Add(this.AllSalerysWorkers);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "InfoConstruction";
            this.Text = "InfoConstruction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox AllSalerysWorkers;
        private System.Windows.Forms.TextBox CraneOperators;
        private System.Windows.Forms.TextBox Painters;
        private System.Windows.Forms.TextBox Electricians;
        private System.Windows.Forms.TextBox GeneralWorkers;
    }
}