namespace Makiyan_Cursovaya_sem2
{
    partial class FormEditLevel
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.listBoxGameObjects = new System.Windows.Forms.ListBox();
            this.buttonAddGameElement = new System.Windows.Forms.Button();
            this.buttonRemovegameElement = new System.Windows.Forms.Button();
            this.buttonEditGameElement = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(92, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(202, 352);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "LevelName";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(13, 30);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(100, 20);
            this.name.TabIndex = 3;
            // 
            // listBoxGameObjects
            // 
            this.listBoxGameObjects.FormattingEnabled = true;
            this.listBoxGameObjects.Location = new System.Drawing.Point(162, 30);
            this.listBoxGameObjects.Name = "listBoxGameObjects";
            this.listBoxGameObjects.Size = new System.Drawing.Size(408, 264);
            this.listBoxGameObjects.TabIndex = 4;
            this.listBoxGameObjects.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonAddGameElement
            // 
            this.buttonAddGameElement.Location = new System.Drawing.Point(596, 30);
            this.buttonAddGameElement.Name = "buttonAddGameElement";
            this.buttonAddGameElement.Size = new System.Drawing.Size(75, 23);
            this.buttonAddGameElement.TabIndex = 5;
            this.buttonAddGameElement.Text = "Add";
            this.buttonAddGameElement.UseVisualStyleBackColor = true;
            this.buttonAddGameElement.Click += new System.EventHandler(this.buttonAddGameElement_Click);
            // 
            // buttonRemovegameElement
            // 
            this.buttonRemovegameElement.Location = new System.Drawing.Point(596, 88);
            this.buttonRemovegameElement.Name = "buttonRemovegameElement";
            this.buttonRemovegameElement.Size = new System.Drawing.Size(75, 23);
            this.buttonRemovegameElement.TabIndex = 6;
            this.buttonRemovegameElement.Text = "Remove";
            this.buttonRemovegameElement.UseVisualStyleBackColor = true;
            this.buttonRemovegameElement.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonEditGameElement
            // 
            this.buttonEditGameElement.Location = new System.Drawing.Point(596, 59);
            this.buttonEditGameElement.Name = "buttonEditGameElement";
            this.buttonEditGameElement.Size = new System.Drawing.Size(75, 23);
            this.buttonEditGameElement.TabIndex = 7;
            this.buttonEditGameElement.Text = "Edit";
            this.buttonEditGameElement.UseVisualStyleBackColor = true;
            this.buttonEditGameElement.Click += new System.EventHandler(this.buttonEditGameElement_Click);
            // 
            // FormEditLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 387);
            this.Controls.Add(this.buttonEditGameElement);
            this.Controls.Add(this.buttonRemovegameElement);
            this.Controls.Add(this.buttonAddGameElement);
            this.Controls.Add(this.listBoxGameObjects);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FormEditLevel";
            this.Text = "FormEditLevel";
            this.Load += new System.EventHandler(this.FormEditLevel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.ListBox listBoxGameObjects;
        private System.Windows.Forms.Button buttonAddGameElement;
        private System.Windows.Forms.Button buttonRemovegameElement;
        private System.Windows.Forms.Button buttonEditGameElement;
    }
}