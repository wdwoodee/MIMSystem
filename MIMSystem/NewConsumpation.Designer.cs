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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labConType = new System.Windows.Forms.Label();
            this.labMobile = new System.Windows.Forms.Label();
            this.tetBoxMobile = new System.Windows.Forms.TextBox();
            this.tetBoxConAmount = new System.Windows.Forms.TextBox();
            this.tetBoxIntgeralChange = new System.Windows.Forms.TextBox();
            this.labConAmount = new System.Windows.Forms.Label();
            this.labIntegralChange = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(295, 294);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(409, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "消费",
            "积分兑换",
            "加钱购"});
            this.comboBox1.Location = new System.Drawing.Point(224, 71);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(146, 20);
            this.comboBox1.TabIndex = 2;
            // 
            // labConType
            // 
            this.labConType.AutoSize = true;
            this.labConType.Location = new System.Drawing.Point(153, 74);
            this.labConType.Name = "labConType";
            this.labConType.Size = new System.Drawing.Size(65, 12);
            this.labConType.TabIndex = 3;
            this.labConType.Text = "消费类型：";
            // 
            // labMobile
            // 
            this.labMobile.AutoSize = true;
            this.labMobile.Location = new System.Drawing.Point(177, 119);
            this.labMobile.Name = "labMobile";
            this.labMobile.Size = new System.Drawing.Size(41, 12);
            this.labMobile.TabIndex = 4;
            this.labMobile.Text = "电话：";
            // 
            // tetBoxMobile
            // 
            this.tetBoxMobile.Location = new System.Drawing.Point(224, 116);
            this.tetBoxMobile.Name = "tetBoxMobile";
            this.tetBoxMobile.Size = new System.Drawing.Size(146, 21);
            this.tetBoxMobile.TabIndex = 5;
            // 
            // tetBoxConAmount
            // 
            this.tetBoxConAmount.Location = new System.Drawing.Point(224, 164);
            this.tetBoxConAmount.Name = "tetBoxConAmount";
            this.tetBoxConAmount.Size = new System.Drawing.Size(146, 21);
            this.tetBoxConAmount.TabIndex = 6;
            // 
            // tetBoxIntgeralChange
            // 
            this.tetBoxIntgeralChange.Location = new System.Drawing.Point(224, 218);
            this.tetBoxIntgeralChange.Name = "tetBoxIntgeralChange";
            this.tetBoxIntgeralChange.Size = new System.Drawing.Size(146, 21);
            this.tetBoxIntgeralChange.TabIndex = 7;
            // 
            // labConAmount
            // 
            this.labConAmount.AutoSize = true;
            this.labConAmount.Location = new System.Drawing.Point(155, 172);
            this.labConAmount.Name = "labConAmount";
            this.labConAmount.Size = new System.Drawing.Size(65, 12);
            this.labConAmount.TabIndex = 8;
            this.labConAmount.Text = "消费金额：";
            // 
            // labIntegralChange
            // 
            this.labIntegralChange.AutoSize = true;
            this.labIntegralChange.Location = new System.Drawing.Point(153, 221);
            this.labIntegralChange.Name = "labIntegralChange";
            this.labIntegralChange.Size = new System.Drawing.Size(65, 12);
            this.labIntegralChange.TabIndex = 9;
            this.labIntegralChange.Text = "积分变更：";
            // 
            // NewConsumpation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 360);
            this.Controls.Add(this.labIntegralChange);
            this.Controls.Add(this.labConAmount);
            this.Controls.Add(this.tetBoxIntgeralChange);
            this.Controls.Add(this.tetBoxConAmount);
            this.Controls.Add(this.tetBoxMobile);
            this.Controls.Add(this.labMobile);
            this.Controls.Add(this.labConType);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "NewConsumpation";
            this.Text = "NewConsumpation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label labConType;
        private System.Windows.Forms.Label labMobile;
        private System.Windows.Forms.TextBox tetBoxMobile;
        private System.Windows.Forms.TextBox tetBoxConAmount;
        private System.Windows.Forms.TextBox tetBoxIntgeralChange;
        private System.Windows.Forms.Label labConAmount;
        private System.Windows.Forms.Label labIntegralChange;
    }
}