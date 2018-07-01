using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MIMSystem
{
    partial class NewConsumpation: Form
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbBoxConType = new System.Windows.Forms.ComboBox();
            this.labConType = new System.Windows.Forms.Label();
            this.labMobile = new System.Windows.Forms.Label();
            this.txtBoxMobile = new System.Windows.Forms.TextBox();
            this.txtBoxConAmount = new System.Windows.Forms.TextBox();
            this.txtBoxIntegral = new System.Windows.Forms.TextBox();
            this.labConAmount = new System.Windows.Forms.Label();
            this.labIntegralChange = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(333, 296);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(445, 296);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbBoxConType
            // 
            this.cmbBoxConType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxConType.FormattingEnabled = true;
            this.cmbBoxConType.Items.AddRange(new object[] {
            "消费",
            "积分兑换",
            "加钱购"});
            this.cmbBoxConType.Location = new System.Drawing.Point(262, 66);
            this.cmbBoxConType.Name = "cmbBoxConType";
            this.cmbBoxConType.Size = new System.Drawing.Size(146, 20);
            this.cmbBoxConType.TabIndex = 2;
            // 
            // labConType
            // 
            this.labConType.AutoSize = true;
            this.labConType.Location = new System.Drawing.Point(175, 69);
            this.labConType.Name = "labConType";
            this.labConType.Size = new System.Drawing.Size(65, 12);
            this.labConType.TabIndex = 3;
            this.labConType.Text = "消费类型：";
            // 
            // labMobile
            // 
            this.labMobile.AutoSize = true;
            this.labMobile.Location = new System.Drawing.Point(199, 119);
            this.labMobile.Name = "labMobile";
            this.labMobile.Size = new System.Drawing.Size(41, 12);
            this.labMobile.TabIndex = 4;
            this.labMobile.Text = "电话：";
            // 
            // txtBoxMobile
            // 
            this.txtBoxMobile.Location = new System.Drawing.Point(262, 116);
            this.txtBoxMobile.Name = "txtBoxMobile";
            this.txtBoxMobile.Size = new System.Drawing.Size(146, 21);
            this.txtBoxMobile.TabIndex = 5;
            // 
            // txtBoxConAmount
            // 
            this.txtBoxConAmount.Location = new System.Drawing.Point(262, 163);
            this.txtBoxConAmount.Name = "txtBoxConAmount";
            this.txtBoxConAmount.Size = new System.Drawing.Size(146, 21);
            this.txtBoxConAmount.TabIndex = 6;
            // 
            // txtBoxIntegral
            // 
            this.txtBoxIntegral.AcceptsReturn = true;
            this.txtBoxIntegral.Location = new System.Drawing.Point(262, 221);
            this.txtBoxIntegral.Name = "txtBoxIntegral";
            this.txtBoxIntegral.Size = new System.Drawing.Size(146, 21);
            this.txtBoxIntegral.TabIndex = 7;
            // 
            // labConAmount
            // 
            this.labConAmount.AutoSize = true;
            this.labConAmount.Location = new System.Drawing.Point(175, 172);
            this.labConAmount.Name = "labConAmount";
            this.labConAmount.Size = new System.Drawing.Size(65, 12);
            this.labConAmount.TabIndex = 8;
            this.labConAmount.Text = "消费金额：";
            // 
            // labIntegralChange
            // 
            this.labIntegralChange.AutoSize = true;
            this.labIntegralChange.Location = new System.Drawing.Point(199, 224);
            this.labIntegralChange.Name = "labIntegralChange";
            this.labIntegralChange.Size = new System.Drawing.Size(41, 12);
            this.labIntegralChange.TabIndex = 9;
            this.labIntegralChange.Text = "积分：";
            // 
            // NewConsumpation
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 364);
            this.Controls.Add(this.labIntegralChange);
            this.Controls.Add(this.labConAmount);
            this.Controls.Add(this.txtBoxIntegral);
            this.Controls.Add(this.txtBoxConAmount);
            this.Controls.Add(this.txtBoxMobile);
            this.Controls.Add(this.labMobile);
            this.Controls.Add(this.labConType);
            this.Controls.Add(this.cmbBoxConType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "NewConsumpation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "孔氏布鞋积分记录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewConsumpation_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbBoxConType;
        private System.Windows.Forms.Label labConType;
        private System.Windows.Forms.Label labMobile;
        private System.Windows.Forms.TextBox txtBoxMobile;
        private System.Windows.Forms.TextBox txtBoxConAmount;
        private System.Windows.Forms.TextBox txtBoxIntegral;
        private System.Windows.Forms.Label labConAmount;
        private System.Windows.Forms.Label labIntegralChange;
        private ToolTip toolTip1;
    }
}