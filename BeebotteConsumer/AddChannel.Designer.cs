namespace BeebotteConsumer
{
    partial class AddChannel
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnAddChannel = new System.Windows.Forms.Button();
            this.chkPublic = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridResources = new System.Windows.Forms.DataGridView();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtResourceDescription = new System.Windows.Forms.TextBox();
            this.txtResourceLabel = new System.Windows.Forms.TextBox();
            this.txtResourceName = new System.Windows.Forms.TextBox();
            this.btnAddResource = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResources)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(193, 38);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(244, 20);
            this.txtName.TabIndex = 0;
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(193, 71);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(244, 20);
            this.txtLabel.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(193, 104);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(244, 20);
            this.txtDescription.TabIndex = 2;
            // 
            // btnAddChannel
            // 
            this.btnAddChannel.Location = new System.Drawing.Point(337, 506);
            this.btnAddChannel.Name = "btnAddChannel";
            this.btnAddChannel.Size = new System.Drawing.Size(132, 23);
            this.btnAddChannel.TabIndex = 3;
            this.btnAddChannel.Text = "Add Channel";
            this.btnAddChannel.UseVisualStyleBackColor = true;
            this.btnAddChannel.Click += new System.EventHandler(this.btnAddChannel_Click);
            // 
            // chkPublic
            // 
            this.chkPublic.AutoSize = true;
            this.chkPublic.Location = new System.Drawing.Point(193, 150);
            this.chkPublic.Name = "chkPublic";
            this.chkPublic.Size = new System.Drawing.Size(15, 14);
            this.chkPublic.TabIndex = 4;
            this.chkPublic.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Label";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Is public";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridResources);
            this.groupBox1.Controls.Add(this.cmbType);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtResourceDescription);
            this.groupBox1.Controls.Add(this.txtResourceLabel);
            this.groupBox1.Controls.Add(this.txtResourceName);
            this.groupBox1.Location = new System.Drawing.Point(25, 207);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 265);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resources";
            // 
            // dataGridResources
            // 
            this.dataGridResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResources.Location = new System.Drawing.Point(6, 143);
            this.dataGridResources.Name = "dataGridResources";
            this.dataGridResources.Size = new System.Drawing.Size(400, 94);
            this.dataGridResources.TabIndex = 17;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(166, 107);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(244, 21);
            this.cmbType.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Description";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Label";
            // 
            // txtResourceDescription
            // 
            this.txtResourceDescription.Location = new System.Drawing.Point(166, 77);
            this.txtResourceDescription.Name = "txtResourceDescription";
            this.txtResourceDescription.Size = new System.Drawing.Size(244, 20);
            this.txtResourceDescription.TabIndex = 11;
            // 
            // txtResourceLabel
            // 
            this.txtResourceLabel.Location = new System.Drawing.Point(166, 49);
            this.txtResourceLabel.Name = "txtResourceLabel";
            this.txtResourceLabel.Size = new System.Drawing.Size(244, 20);
            this.txtResourceLabel.TabIndex = 10;
            // 
            // txtResourceName
            // 
            this.txtResourceName.Location = new System.Drawing.Point(166, 21);
            this.txtResourceName.Name = "txtResourceName";
            this.txtResourceName.Size = new System.Drawing.Size(244, 20);
            this.txtResourceName.TabIndex = 9;
            // 
            // btnAddResource
            // 
            this.btnAddResource.Location = new System.Drawing.Point(349, 450);
            this.btnAddResource.Name = "btnAddResource";
            this.btnAddResource.Size = new System.Drawing.Size(114, 23);
            this.btnAddResource.TabIndex = 18;
            this.btnAddResource.Text = "Add Resource";
            this.btnAddResource.UseVisualStyleBackColor = true;
            this.btnAddResource.Click += new System.EventHandler(this.btnAddResource_Click);
            // 
            // AddChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 541);
            this.Controls.Add(this.btnAddResource);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkPublic);
            this.Controls.Add(this.btnAddChannel);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.txtName);
            this.Name = "AddChannel";
            this.Text = "Add Channel";
            this.Load += new System.EventHandler(this.AddChannel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResources)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnAddChannel;
        private System.Windows.Forms.CheckBox chkPublic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtResourceDescription;
        private System.Windows.Forms.TextBox txtResourceLabel;
        private System.Windows.Forms.TextBox txtResourceName;
        private System.Windows.Forms.DataGridView dataGridResources;
        private System.Windows.Forms.Button btnAddResource;
    }
}