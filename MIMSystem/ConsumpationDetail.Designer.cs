namespace MIMSystem
{
    partial class ConsumpationDetail
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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewDetail = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDetail = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除此次消费记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).BeginInit();
            this.contextMenuStripDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewDetail
            // 
            this.dataGridViewDetail.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetail.ContextMenuStrip = this.contextMenuStripDetail;
            this.dataGridViewDetail.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewDetail.Location = new System.Drawing.Point(57, 26);
            this.dataGridViewDetail.Name = "dataGridViewDetail";
            this.dataGridViewDetail.RowTemplate.Height = 23;
            this.dataGridViewDetail.Size = new System.Drawing.Size(586, 382);
            this.dataGridViewDetail.TabIndex = 0;
            this.dataGridViewDetail.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewDetail_CellMouseUp);
            // 
            // contextMenuStripDetail
            // 
            this.contextMenuStripDetail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除此次消费记录ToolStripMenuItem});
            this.contextMenuStripDetail.Name = "contextMenuStripDetail";
            this.contextMenuStripDetail.Size = new System.Drawing.Size(173, 26);
            // 
            // 删除此次消费记录ToolStripMenuItem
            // 
            this.删除此次消费记录ToolStripMenuItem.Name = "删除此次消费记录ToolStripMenuItem";
            this.删除此次消费记录ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.删除此次消费记录ToolStripMenuItem.Text = "删除此次消费记录";
            this.删除此次消费记录ToolStripMenuItem.Click += new System.EventHandler(this.删除此次消费记录ToolStripMenuItem_Click);
            // 
            // ConsumpationDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 440);
            this.Controls.Add(this.dataGridViewDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ConsumpationDetail";
            this.Text = "会员积分管理系统";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).EndInit();
            this.contextMenuStripDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDetail;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDetail;
        private System.Windows.Forms.ToolStripMenuItem 删除此次消费记录ToolStripMenuItem;
    }
}