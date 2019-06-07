using System;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lstControllersView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listOutput = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstControllersView
            // 
            this.lstControllersView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstControllersView.Location = new System.Drawing.Point(4, 2);
            this.lstControllersView.Name = "lstControllersView";
            this.lstControllersView.Size = new System.Drawing.Size(748, 219);
            this.lstControllersView.TabIndex = 0;
            this.lstControllersView.UseCompatibleStateImageBehavior = false;
            this.lstControllersView.View = System.Windows.Forms.View.Details;
            this.lstControllersView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.lstControllersView.DoubleClick += new System.EventHandler(this.lstControllersView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "SystemName";
            this.columnHeader1.Width = 133;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = " IP adress";
            this.columnHeader2.Width = 121;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Version";
            // 
            // listOutput
            // 
            this.listOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.listOutput.Location = new System.Drawing.Point(4, 228);
            this.listOutput.Name = "listOutput";
            this.listOutput.Size = new System.Drawing.Size(748, 210);
            this.listOutput.TabIndex = 1;
            this.listOutput.UseCompatibleStateImageBehavior = false;
            this.listOutput.View = System.Windows.Forms.View.Details;
            this.listOutput.SelectedIndexChanged += new System.EventHandler(this.listOutput_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Data";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 450);
            this.Controls.Add(this.listOutput);
            this.Controls.Add(this.lstControllersView);
            this.Name = "Form1";
            this.Text = "Robot Controllers";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        private System.Windows.Forms.ListView lstControllersView;
        private System.Windows.Forms.ListView listOutput;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}

