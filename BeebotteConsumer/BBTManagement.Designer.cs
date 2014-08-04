namespace BeebotteConsumer
{
    partial class BBTManagement
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabChannelMgt = new System.Windows.Forms.TabPage();
            this.dataGridResources = new System.Windows.Forms.DataGridView();
            this.DeleteResource = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Write = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Read = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupResources = new System.Windows.Forms.GroupBox();
            this.btnAddResource = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddChannel = new System.Windows.Forms.Button();
            this.dataGridChannels = new System.Windows.Forms.DataGridView();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.GetChannelResources = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabControl1.SuspendLayout();
            this.tabChannelMgt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResources)).BeginInit();
            this.groupResources.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridChannels)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabChannelMgt);
            this.tabControl1.Location = new System.Drawing.Point(-2, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(802, 521);
            this.tabControl1.TabIndex = 0;
            // 
            // tabChannelMgt
            // 
            this.tabChannelMgt.Controls.Add(this.dataGridResources);
            this.tabChannelMgt.Controls.Add(this.groupResources);
            this.tabChannelMgt.Controls.Add(this.groupBox1);
            this.tabChannelMgt.Location = new System.Drawing.Point(4, 22);
            this.tabChannelMgt.Name = "tabChannelMgt";
            this.tabChannelMgt.Padding = new System.Windows.Forms.Padding(3);
            this.tabChannelMgt.Size = new System.Drawing.Size(794, 495);
            this.tabChannelMgt.TabIndex = 0;
            this.tabChannelMgt.Text = "Channel Management";
            this.tabChannelMgt.UseVisualStyleBackColor = true;
            // 
            // dataGridResources
            // 
            this.dataGridResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResources.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeleteResource,
            this.Write,
            this.Read});
            this.dataGridResources.Location = new System.Drawing.Point(39, 294);
            this.dataGridResources.Name = "dataGridResources";
            this.dataGridResources.Size = new System.Drawing.Size(721, 150);
            this.dataGridResources.TabIndex = 11;
            this.dataGridResources.Visible = false;
            this.dataGridResources.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridResources_CellClick);
            // 
            // DeleteResource
            // 
            this.DeleteResource.HeaderText = "Delete";
            this.DeleteResource.Name = "DeleteResource";
            this.DeleteResource.Text = "Delete";
            this.DeleteResource.UseColumnTextForButtonValue = true;
            // 
            // Write
            // 
            this.Write.HeaderText = "Write Data";
            this.Write.Name = "Write";
            this.Write.Text = "Write";
            this.Write.UseColumnTextForButtonValue = true;
            // 
            // Read
            // 
            this.Read.HeaderText = "Read";
            this.Read.Name = "Read";
            this.Read.Text = "Read";
            this.Read.UseColumnTextForButtonValue = true;
            // 
            // groupResources
            // 
            this.groupResources.Controls.Add(this.btnAddResource);
            this.groupResources.Location = new System.Drawing.Point(26, 256);
            this.groupResources.Name = "groupResources";
            this.groupResources.Size = new System.Drawing.Size(740, 211);
            this.groupResources.TabIndex = 13;
            this.groupResources.TabStop = false;
            this.groupResources.Text = "Resources";
            this.groupResources.Visible = false;
            // 
            // btnAddResource
            // 
            this.btnAddResource.Location = new System.Drawing.Point(629, 9);
            this.btnAddResource.Name = "btnAddResource";
            this.btnAddResource.Size = new System.Drawing.Size(105, 23);
            this.btnAddResource.TabIndex = 0;
            this.btnAddResource.Text = "Add Resource";
            this.btnAddResource.UseVisualStyleBackColor = true;
            this.btnAddResource.Click += new System.EventHandler(this.btnAddResource_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddChannel);
            this.groupBox1.Controls.Add(this.dataGridChannels);
            this.groupBox1.Location = new System.Drawing.Point(26, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 193);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channels";
            // 
            // btnAddChannel
            // 
            this.btnAddChannel.Location = new System.Drawing.Point(629, 8);
            this.btnAddChannel.Name = "btnAddChannel";
            this.btnAddChannel.Size = new System.Drawing.Size(105, 23);
            this.btnAddChannel.TabIndex = 11;
            this.btnAddChannel.Text = "Add Channel";
            this.btnAddChannel.UseVisualStyleBackColor = true;
            this.btnAddChannel.Click += new System.EventHandler(this.btnAddChannel_Click);
            // 
            // dataGridChannels
            // 
            this.dataGridChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridChannels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Delete,
            this.GetChannelResources});
            this.dataGridChannels.Location = new System.Drawing.Point(13, 37);
            this.dataGridChannels.Name = "dataGridChannels";
            this.dataGridChannels.Size = new System.Drawing.Size(721, 129);
            this.dataGridChannels.TabIndex = 10;
            this.dataGridChannels.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridChannels_CellClick);
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.Text = "Delete";
            this.Delete.UseColumnTextForButtonValue = true;
            // 
            // GetChannelResources
            // 
            this.GetChannelResources.HeaderText = "Get Resources";
            this.GetChannelResources.Name = "GetChannelResources";
            this.GetChannelResources.Text = "Get Resources";
            this.GetChannelResources.UseColumnTextForButtonValue = true;
            // 
            // BBTManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 552);
            this.Controls.Add(this.tabControl1);
            this.Name = "BBTManagement";
            this.Text = "Beebotte Console";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabChannelMgt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResources)).EndInit();
            this.groupResources.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridChannels)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabChannelMgt;
        private System.Windows.Forms.DataGridView dataGridChannels;
        private System.Windows.Forms.DataGridView dataGridResources;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.DataGridViewButtonColumn GetChannelResources;
        private System.Windows.Forms.GroupBox groupResources;
        private System.Windows.Forms.Button btnAddResource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddChannel;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteResource;
        private System.Windows.Forms.DataGridViewButtonColumn Write;
        private System.Windows.Forms.DataGridViewButtonColumn Read;


    }
}

