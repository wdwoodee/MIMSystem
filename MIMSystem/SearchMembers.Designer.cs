namespace MIMSystem
{
    partial class SearchMembers
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
            this.btnSearchMem = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridViewMember = new System.Windows.Forms.DataGridView();
            this.contextMSDeleteMember = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除会员ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMember)).BeginInit();
            this.contextMSDeleteMember.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearchMem
            // 
            this.btnSearchMem.Location = new System.Drawing.Point(51, 37);
            this.btnSearchMem.Name = "btnSearchMem";
            this.btnSearchMem.Size = new System.Drawing.Size(75, 23);
            this.btnSearchMem.TabIndex = 0;
            this.btnSearchMem.Text = "查找会员";
            this.btnSearchMem.UseVisualStyleBackColor = true;
            this.btnSearchMem.Click += new System.EventHandler(this.btnSearchMem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(146, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(118, 21);
            this.textBox1.TabIndex = 1;
            // 
            // dataGridViewMember
            // 
            this.dataGridViewMember.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMember.Location = new System.Drawing.Point(51, 78);
            this.dataGridViewMember.Name = "dataGridViewMember";
            this.dataGridViewMember.RowTemplate.Height = 23;
            this.dataGridViewMember.Size = new System.Drawing.Size(548, 329);
            this.dataGridViewMember.TabIndex = 2;
            this.dataGridViewMember.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewMember_CellMouseUp);
            // 
            // contextMSDeleteMember
            // 
            this.contextMSDeleteMember.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除会员ToolStripMenuItem});
            this.contextMSDeleteMember.Name = "contextMSDeleteMember";
            this.contextMSDeleteMember.Size = new System.Drawing.Size(125, 26);
            // 
            // 删除会员ToolStripMenuItem
            // 
            this.删除会员ToolStripMenuItem.Name = "删除会员ToolStripMenuItem";
            this.删除会员ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除会员ToolStripMenuItem.Text = "删除会员";
            this.删除会员ToolStripMenuItem.Click += new System.EventHandler(this.删除会员ToolStripMenuItem_Click);
            // 
            // SearchMembers
            // 
            this.AcceptButton = this.btnSearchMem;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 442);
            this.ContextMenuStrip = this.contextMSDeleteMember;
            this.Controls.Add(this.dataGridViewMember);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSearchMem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SearchMembers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "孔氏布鞋积分记录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchMembers_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMember)).EndInit();
            this.contextMSDeleteMember.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearchMem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridViewMember;
        private System.Windows.Forms.ContextMenuStrip contextMSDeleteMember;
        private System.Windows.Forms.ToolStripMenuItem 删除会员ToolStripMenuItem;
    }
}