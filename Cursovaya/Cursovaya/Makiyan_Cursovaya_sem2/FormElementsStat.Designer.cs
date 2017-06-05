namespace Makiyan_Cursovaya_sem2
{
    partial class FormElementsStat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormElementsStat));
            this.name = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelXCoord = new System.Windows.Forms.Label();
            this.labelYCoord = new System.Windows.Forms.Label();
            this.listBoxCollide = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name.Location = new System.Drawing.Point(106, 9);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(70, 27);
            this.name.TabIndex = 0;
            this.name.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(8, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(8, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y:";
            // 
            // labelXCoord
            // 
            this.labelXCoord.AutoSize = true;
            this.labelXCoord.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelXCoord.Location = new System.Drawing.Point(31, 71);
            this.labelXCoord.Name = "labelXCoord";
            this.labelXCoord.Size = new System.Drawing.Size(53, 21);
            this.labelXCoord.TabIndex = 3;
            this.labelXCoord.Text = "label4";
            // 
            // labelYCoord
            // 
            this.labelYCoord.AutoSize = true;
            this.labelYCoord.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelYCoord.Location = new System.Drawing.Point(31, 94);
            this.labelYCoord.Name = "labelYCoord";
            this.labelYCoord.Size = new System.Drawing.Size(53, 21);
            this.labelYCoord.TabIndex = 4;
            this.labelYCoord.Text = "label4";
            // 
            // listBoxCollide
            // 
            this.listBoxCollide.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxCollide.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxCollide.FormattingEnabled = true;
            this.listBoxCollide.ItemHeight = 21;
            this.listBoxCollide.Location = new System.Drawing.Point(12, 173);
            this.listBoxCollide.Name = "listBoxCollide";
            this.listBoxCollide.Size = new System.Drawing.Size(260, 172);
            this.listBoxCollide.TabIndex = 5;
            this.listBoxCollide.SelectedIndexChanged += new System.EventHandler(this.listBoxCollide_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(8, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Collides with";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(7, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Position";
            // 
            // FormElementsStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(284, 353);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxCollide);
            this.Controls.Add(this.labelYCoord);
            this.Controls.Add(this.labelXCoord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "FormElementsStat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ForrmElementsStat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelXCoord;
        private System.Windows.Forms.Label labelYCoord;
        private System.Windows.Forms.ListBox listBoxCollide;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
    }
}