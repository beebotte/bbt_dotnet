namespace BeebotteConsumer
{
    partial class ReadData
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
            this.dataGridData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridData)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridData
            // 
            this.dataGridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridData.Location = new System.Drawing.Point(0, 0);
            this.dataGridData.Name = "dataGridData";
            this.dataGridData.Size = new System.Drawing.Size(468, 224);
            this.dataGridData.TabIndex = 1;
            // 
            // ReadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 262);
            this.Controls.Add(this.dataGridData);
            this.Name = "ReadData";
            this.Text = "Read Data";
            this.Load += new System.EventHandler(this.ReadData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridData;
    }
}