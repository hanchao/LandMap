namespace LandMap
{
    partial class WeatherForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.labelOne = new System.Windows.Forms.Label();
            this.pictureBoxOneFrom = new System.Windows.Forms.PictureBox();
            this.pictureBoxOneTo = new System.Windows.Forms.PictureBox();
            this.labelLive = new System.Windows.Forms.Label();
            this.pictureBoxTwoFrom = new System.Windows.Forms.PictureBox();
            this.pictureBoxTwoTo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBoxThreeTo = new System.Windows.Forms.PictureBox();
            this.pictureBoxThreeFrom = new System.Windows.Forms.PictureBox();
            this.labelTwo = new System.Windows.Forms.Label();
            this.labelThree = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOneFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOneTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTwoFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTwoTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThreeTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThreeFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "请输入城市：";
            // 
            // textBoxCity
            // 
            this.textBoxCity.Location = new System.Drawing.Point(95, 15);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(226, 21);
            this.textBoxCity.TabIndex = 2;
            // 
            // buttonQuery
            // 
            this.buttonQuery.Location = new System.Drawing.Point(352, 15);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(75, 23);
            this.buttonQuery.TabIndex = 3;
            this.buttonQuery.Text = "查询\r\n";
            this.buttonQuery.UseVisualStyleBackColor = true;
            this.buttonQuery.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // labelOne
            // 
            this.labelOne.AutoSize = true;
            this.labelOne.Location = new System.Drawing.Point(12, 150);
            this.labelOne.Name = "labelOne";
            this.labelOne.Size = new System.Drawing.Size(53, 12);
            this.labelOne.TabIndex = 5;
            this.labelOne.Text = "明天天气";
            // 
            // pictureBoxOneFrom
            // 
            this.pictureBoxOneFrom.Location = new System.Drawing.Point(14, 64);
            this.pictureBoxOneFrom.Name = "pictureBoxOneFrom";
            this.pictureBoxOneFrom.Size = new System.Drawing.Size(70, 65);
            this.pictureBoxOneFrom.TabIndex = 8;
            this.pictureBoxOneFrom.TabStop = false;
            // 
            // pictureBoxOneTo
            // 
            this.pictureBoxOneTo.Location = new System.Drawing.Point(152, 64);
            this.pictureBoxOneTo.Name = "pictureBoxOneTo";
            this.pictureBoxOneTo.Size = new System.Drawing.Size(70, 65);
            this.pictureBoxOneTo.TabIndex = 11;
            this.pictureBoxOneTo.TabStop = false;
            // 
            // labelLive
            // 
            this.labelLive.AutoSize = true;
            this.labelLive.Location = new System.Drawing.Point(268, 71);
            this.labelLive.Name = "labelLive";
            this.labelLive.Size = new System.Drawing.Size(53, 12);
            this.labelLive.TabIndex = 12;
            this.labelLive.Text = "天气实况";
            // 
            // pictureBoxTwoFrom
            // 
            this.pictureBoxTwoFrom.Location = new System.Drawing.Point(14, 180);
            this.pictureBoxTwoFrom.Name = "pictureBoxTwoFrom";
            this.pictureBoxTwoFrom.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxTwoFrom.TabIndex = 13;
            this.pictureBoxTwoFrom.TabStop = false;
            // 
            // pictureBoxTwoTo
            // 
            this.pictureBoxTwoTo.Location = new System.Drawing.Point(55, 180);
            this.pictureBoxTwoTo.Name = "pictureBoxTwoTo";
            this.pictureBoxTwoTo.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxTwoTo.TabIndex = 14;
            this.pictureBoxTwoTo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "—";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "—";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(197, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "—";
            // 
            // pictureBoxThreeTo
            // 
            this.pictureBoxThreeTo.Location = new System.Drawing.Point(216, 180);
            this.pictureBoxThreeTo.Name = "pictureBoxThreeTo";
            this.pictureBoxThreeTo.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxThreeTo.TabIndex = 18;
            this.pictureBoxThreeTo.TabStop = false;
            // 
            // pictureBoxThreeFrom
            // 
            this.pictureBoxThreeFrom.Location = new System.Drawing.Point(173, 180);
            this.pictureBoxThreeFrom.Name = "pictureBoxThreeFrom";
            this.pictureBoxThreeFrom.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxThreeFrom.TabIndex = 17;
            this.pictureBoxThreeFrom.TabStop = false;
            // 
            // labelTwo
            // 
            this.labelTwo.AutoSize = true;
            this.labelTwo.Location = new System.Drawing.Point(12, 214);
            this.labelTwo.Name = "labelTwo";
            this.labelTwo.Size = new System.Drawing.Size(53, 12);
            this.labelTwo.TabIndex = 20;
            this.labelTwo.Text = "后天天气";
            // 
            // labelThree
            // 
            this.labelThree.AutoSize = true;
            this.labelThree.Location = new System.Drawing.Point(171, 214);
            this.labelThree.Name = "labelThree";
            this.labelThree.Size = new System.Drawing.Size(65, 12);
            this.labelThree.TabIndex = 21;
            this.labelThree.Text = "大后天天气";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // WeatherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 299);
            this.Controls.Add(this.labelThree);
            this.Controls.Add(this.labelTwo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBoxThreeTo);
            this.Controls.Add(this.pictureBoxThreeFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxTwoTo);
            this.Controls.Add(this.pictureBoxTwoFrom);
            this.Controls.Add(this.labelLive);
            this.Controls.Add(this.pictureBoxOneTo);
            this.Controls.Add(this.pictureBoxOneFrom);
            this.Controls.Add(this.labelOne);
            this.Controls.Add(this.buttonQuery);
            this.Controls.Add(this.textBoxCity);
            this.Controls.Add(this.label2);
            this.Name = "WeatherForm";
            this.Text = "天气";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOneFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOneTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTwoFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTwoTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThreeTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThreeFrom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.Button buttonQuery;
        private System.Windows.Forms.Label labelOne;
        private System.Windows.Forms.PictureBox pictureBoxOneFrom;
        private System.Windows.Forms.PictureBox pictureBoxOneTo;
        private System.Windows.Forms.Label labelLive;
        private System.Windows.Forms.PictureBox pictureBoxTwoFrom;
        private System.Windows.Forms.PictureBox pictureBoxTwoTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBoxThreeTo;
        private System.Windows.Forms.PictureBox pictureBoxThreeFrom;
        private System.Windows.Forms.Label labelTwo;
        private System.Windows.Forms.Label labelThree;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;

    }
}

