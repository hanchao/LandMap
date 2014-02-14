namespace PoiIndexBuilder
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonBuildIndex = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSrc = new System.Windows.Forms.Button();
            this.textBoxSrc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDes = new System.Windows.Forms.TextBox();
            this.buttonDes = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.axSuperWorkspace1 = new AxSuperMapLib.AxSuperWorkspace();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.axSuperWorkspace1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBuildIndex
            // 
            this.buttonBuildIndex.Location = new System.Drawing.Point(226, 132);
            this.buttonBuildIndex.Name = "buttonBuildIndex";
            this.buttonBuildIndex.Size = new System.Drawing.Size(75, 23);
            this.buttonBuildIndex.TabIndex = 0;
            this.buttonBuildIndex.Text = "创建索引";
            this.buttonBuildIndex.UseVisualStyleBackColor = true;
            this.buttonBuildIndex.Click += new System.EventHandler(this.buttonBuildIndex_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "POI数据：";
            // 
            // buttonSrc
            // 
            this.buttonSrc.Location = new System.Drawing.Point(441, 12);
            this.buttonSrc.Name = "buttonSrc";
            this.buttonSrc.Size = new System.Drawing.Size(75, 23);
            this.buttonSrc.TabIndex = 2;
            this.buttonSrc.Text = "选择";
            this.buttonSrc.UseVisualStyleBackColor = true;
            this.buttonSrc.Click += new System.EventHandler(this.buttonSrc_Click);
            // 
            // textBoxSrc
            // 
            this.textBoxSrc.Location = new System.Drawing.Point(79, 13);
            this.textBoxSrc.Name = "textBoxSrc";
            this.textBoxSrc.Size = new System.Drawing.Size(356, 21);
            this.textBoxSrc.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "输出索引：";
            // 
            // textBoxDes
            // 
            this.textBoxDes.Location = new System.Drawing.Point(79, 62);
            this.textBoxDes.Name = "textBoxDes";
            this.textBoxDes.Size = new System.Drawing.Size(356, 21);
            this.textBoxDes.TabIndex = 6;
            // 
            // buttonDes
            // 
            this.buttonDes.Location = new System.Drawing.Point(441, 61);
            this.buttonDes.Name = "buttonDes";
            this.buttonDes.Size = new System.Drawing.Size(75, 23);
            this.buttonDes.TabIndex = 5;
            this.buttonDes.Text = "选择";
            this.buttonDes.UseVisualStyleBackColor = true;
            this.buttonDes.Click += new System.EventHandler(this.buttonDes_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "关键字：";
            // 
            // buttonQuery
            // 
            this.buttonQuery.Location = new System.Drawing.Point(441, 175);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(75, 23);
            this.buttonQuery.TabIndex = 5;
            this.buttonQuery.Text = "查询测试";
            this.buttonQuery.UseVisualStyleBackColor = true;
            this.buttonQuery.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(79, 176);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(356, 21);
            this.textBoxKey.TabIndex = 6;
            // 
            // axSuperWorkspace1
            // 
            this.axSuperWorkspace1.Enabled = true;
            this.axSuperWorkspace1.Location = new System.Drawing.Point(1, 132);
            this.axSuperWorkspace1.Name = "axSuperWorkspace1";
            this.axSuperWorkspace1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSuperWorkspace1.OcxState")));
            this.axSuperWorkspace1.Size = new System.Drawing.Size(32, 32);
            this.axSuperWorkspace1.TabIndex = 7;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 103);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(505, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 209);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.axSuperWorkspace1);
            this.Controls.Add(this.textBoxKey);
            this.Controls.Add(this.textBoxDes);
            this.Controls.Add(this.buttonQuery);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonDes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSrc);
            this.Controls.Add(this.buttonSrc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonBuildIndex);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axSuperWorkspace1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBuildIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSrc;
        private System.Windows.Forms.TextBox textBoxSrc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDes;
        private System.Windows.Forms.Button buttonDes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonQuery;
        private System.Windows.Forms.TextBox textBoxKey;
        private AxSuperMapLib.AxSuperWorkspace axSuperWorkspace1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

