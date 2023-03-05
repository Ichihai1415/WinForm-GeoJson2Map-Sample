namespace WinForm_GeoJson2Map_Sample
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.MapImg = new System.Windows.Forms.PictureBox();
            this.DrawMapB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Lat1 = new System.Windows.Forms.NumericUpDown();
            this.Lon1 = new System.Windows.Forms.NumericUpDown();
            this.Lat2 = new System.Windows.Forms.NumericUpDown();
            this.Lon2 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.MapImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lon2)).BeginInit();
            this.SuspendLayout();
            // 
            // MapImg
            // 
            this.MapImg.Location = new System.Drawing.Point(0, 0);
            this.MapImg.Name = "MapImg";
            this.MapImg.Size = new System.Drawing.Size(500, 500);
            this.MapImg.TabIndex = 0;
            this.MapImg.TabStop = false;
            this.MapImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapImg_MouseDown);
            this.MapImg.MouseEnter += new System.EventHandler(this.MapImg_MouseEnter);
            this.MapImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapImg_MouseMove);
            this.MapImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapImg_MouseUp);
            // 
            // DrawMapB
            // 
            this.DrawMapB.Location = new System.Drawing.Point(400, 0);
            this.DrawMapB.Name = "DrawMapB";
            this.DrawMapB.Size = new System.Drawing.Size(75, 23);
            this.DrawMapB.TabIndex = 2;
            this.DrawMapB.Text = "描画";
            this.DrawMapB.UseVisualStyleBackColor = true;
            this.DrawMapB.Click += new System.EventHandler(this.DrawMapB_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(475, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "北緯　　　　　　°東経　　　　　　°～北緯　　　　　　°東経　　　　　　°";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lat1
            // 
            this.Lat1.DecimalPlaces = 2;
            this.Lat1.Location = new System.Drawing.Point(32, 2);
            this.Lat1.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.Lat1.Name = "Lat1";
            this.Lat1.Size = new System.Drawing.Size(51, 19);
            this.Lat1.TabIndex = 5;
            this.Lat1.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // Lon1
            // 
            this.Lon1.DecimalPlaces = 2;
            this.Lon1.Location = new System.Drawing.Point(128, 2);
            this.Lon1.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.Lon1.Name = "Lon1";
            this.Lon1.Size = new System.Drawing.Size(51, 19);
            this.Lon1.TabIndex = 6;
            this.Lon1.Value = new decimal(new int[] {
            125,
            0,
            0,
            0});
            // 
            // Lat2
            // 
            this.Lat2.DecimalPlaces = 2;
            this.Lat2.Location = new System.Drawing.Point(238, 2);
            this.Lat2.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.Lat2.Name = "Lat2";
            this.Lat2.Size = new System.Drawing.Size(51, 19);
            this.Lat2.TabIndex = 7;
            this.Lat2.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // Lon2
            // 
            this.Lon2.DecimalPlaces = 2;
            this.Lon2.Location = new System.Drawing.Point(335, 2);
            this.Lon2.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.Lon2.Name = "Lon2";
            this.Lon2.Size = new System.Drawing.Size(51, 19);
            this.Lon2.TabIndex = 8;
            this.Lon2.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.DrawMapB);
            this.Controls.Add(this.Lon2);
            this.Controls.Add(this.Lat2);
            this.Controls.Add(this.Lon1);
            this.Controls.Add(this.Lat1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MapImg);
            this.Name = "Form1";
            this.Text = "サンプル";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.MapImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lat2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lon2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MapImg;
        private System.Windows.Forms.Button DrawMapB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown Lat1;
        private System.Windows.Forms.NumericUpDown Lon1;
        private System.Windows.Forms.NumericUpDown Lat2;
        private System.Windows.Forms.NumericUpDown Lon2;
    }
}

