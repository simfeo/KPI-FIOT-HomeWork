namespace MarketInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditLevel));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.listBoxGameObjects = new System.Windows.Forms.ListBox();
            this.buttonAddGameElement = new System.Windows.Forms.Button();
            this.buttonRemovegameElement = new System.Windows.Forms.Button();
            this.buttonEditGameElement = new System.Windows.Forms.Button();
            this.buttonStat = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(17, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 35);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(63, 340);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 35);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Level name";
            // 
            // name
            // 
            this.name.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name.Location = new System.Drawing.Point(12, 43);
            this.name.MaxLength = 20;
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(134, 29);
            this.name.TabIndex = 3;
            this.name.Text = "Level 1";
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
            // 
            // listBoxGameObjects
            // 
            this.listBoxGameObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxGameObjects.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxGameObjects.FormattingEnabled = true;
            this.listBoxGameObjects.ItemHeight = 21;
            this.listBoxGameObjects.Location = new System.Drawing.Point(162, 43);
            this.listBoxGameObjects.Name = "listBoxGameObjects";
            this.listBoxGameObjects.Size = new System.Drawing.Size(202, 277);
            this.listBoxGameObjects.TabIndex = 4;
            this.listBoxGameObjects.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonAddGameElement
            // 
            this.buttonAddGameElement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddGameElement.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddGameElement.Location = new System.Drawing.Point(370, 43);
            this.buttonAddGameElement.Name = "buttonAddGameElement";
            this.buttonAddGameElement.Size = new System.Drawing.Size(95, 35);
            this.buttonAddGameElement.TabIndex = 5;
            this.buttonAddGameElement.Text = "Add";
            this.buttonAddGameElement.UseVisualStyleBackColor = true;
            this.buttonAddGameElement.Click += new System.EventHandler(this.buttonAddGameElement_Click);
            // 
            // buttonRemovegameElement
            // 
            this.buttonRemovegameElement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemovegameElement.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRemovegameElement.Location = new System.Drawing.Point(369, 125);
            this.buttonRemovegameElement.Name = "buttonRemovegameElement";
            this.buttonRemovegameElement.Size = new System.Drawing.Size(95, 35);
            this.buttonRemovegameElement.TabIndex = 6;
            this.buttonRemovegameElement.Text = "Remove";
            this.buttonRemovegameElement.UseVisualStyleBackColor = true;
            this.buttonRemovegameElement.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonEditGameElement
            // 
            this.buttonEditGameElement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditGameElement.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEditGameElement.Location = new System.Drawing.Point(370, 84);
            this.buttonEditGameElement.Name = "buttonEditGameElement";
            this.buttonEditGameElement.Size = new System.Drawing.Size(95, 35);
            this.buttonEditGameElement.TabIndex = 7;
            this.buttonEditGameElement.Text = "Edit";
            this.buttonEditGameElement.UseVisualStyleBackColor = true;
            this.buttonEditGameElement.Click += new System.EventHandler(this.buttonEditGameElement_Click);
            // 
            // buttonStat
            // 
            this.buttonStat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStat.Location = new System.Drawing.Point(369, 166);
            this.buttonStat.Name = "buttonStat";
            this.buttonStat.Size = new System.Drawing.Size(95, 35);
            this.buttonStat.TabIndex = 8;
            this.buttonStat.Text = "Statistics";
            this.buttonStat.UseVisualStyleBackColor = true;
            this.buttonStat.Click += new System.EventHandler(this.buttonStat_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(158, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "List of levels elements";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(374, 207);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(90, 56);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "Actions above game elements  of current level";
            // 
            // FormEditLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(476, 387);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonStat);
            this.Controls.Add(this.buttonEditGameElement);
            this.Controls.Add(this.buttonRemovegameElement);
            this.Controls.Add(this.buttonAddGameElement);
            this.Controls.Add(this.listBoxGameObjects);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(492, 426);
            this.Name = "FormEditLevel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Battle City Constructor: Level Contructor";
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
        private System.Windows.Forms.Button buttonStat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}