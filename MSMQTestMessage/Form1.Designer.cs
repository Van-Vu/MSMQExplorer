namespace MSMQTestMessage
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
            this.theGrid = new System.Windows.Forms.DataGridView();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cboMessageList = new System.Windows.Forms.ComboBox();
            this.btnLoadMessage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.theGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // theGrid
            // 
            this.theGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Property,
            this.Value});
            this.theGrid.Location = new System.Drawing.Point(12, 106);
            this.theGrid.Name = "theGrid";
            this.theGrid.Size = new System.Drawing.Size(435, 270);
            this.theGrid.TabIndex = 0;
            // 
            // Property
            // 
            this.Property.HeaderText = "Property";
            this.Property.Name = "Property";
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(434, 20);
            this.textBox1.TabIndex = 1;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(478, 10);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(124, 23);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load Assembly";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cboMessageList
            // 
            this.cboMessageList.FormattingEnabled = true;
            this.cboMessageList.Location = new System.Drawing.Point(13, 54);
            this.cboMessageList.Name = "cboMessageList";
            this.cboMessageList.Size = new System.Drawing.Size(434, 21);
            this.cboMessageList.TabIndex = 3;
            // 
            // btnLoadMessage
            // 
            this.btnLoadMessage.Location = new System.Drawing.Point(478, 51);
            this.btnLoadMessage.Name = "btnLoadMessage";
            this.btnLoadMessage.Size = new System.Drawing.Size(124, 23);
            this.btnLoadMessage.TabIndex = 4;
            this.btnLoadMessage.Text = "Load message";
            this.btnLoadMessage.UseVisualStyleBackColor = true;
            this.btnLoadMessage.Click += new System.EventHandler(this.btnLoadMessage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 535);
            this.Controls.Add(this.btnLoadMessage);
            this.Controls.Add(this.cboMessageList);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.theGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.theGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView theGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox cboMessageList;
        private System.Windows.Forms.Button btnLoadMessage;
    }
}

