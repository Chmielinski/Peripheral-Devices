namespace WindowsFormsApp2
{
    partial class Form1
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
            this.latTitle = new System.Windows.Forms.Label();
            this.latValue = new System.Windows.Forms.Label();
            this.longTitle = new System.Windows.Forms.Label();
            this.longValue = new System.Windows.Forms.Label();
            this.altTitle = new System.Windows.Forms.Label();
            this.altValue = new System.Windows.Forms.Label();
            this.wbMap = new System.Windows.Forms.WebBrowser();
            this.btPair = new System.Windows.Forms.Button();
            this.btShowMap = new System.Windows.Forms.Button();
            this.satNumValue = new System.Windows.Forms.Label();
            this.satNumTitle = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // latTitle
            // 
            this.latTitle.AutoSize = true;
            this.latTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.latTitle.Location = new System.Drawing.Point(21, 47);
            this.latTitle.Name = "latTitle";
            this.latTitle.Size = new System.Drawing.Size(149, 17);
            this.latTitle.TabIndex = 0;
            this.latTitle.Text = "Długość geograficzna:";
            // 
            // latValue
            // 
            this.latValue.AutoSize = true;
            this.latValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.latValue.Location = new System.Drawing.Point(21, 76);
            this.latValue.Name = "latValue";
            this.latValue.Size = new System.Drawing.Size(59, 17);
            this.latValue.TabIndex = 1;
            this.latValue.Text = "latValue";
            // 
            // longTitle
            // 
            this.longTitle.AutoSize = true;
            this.longTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.longTitle.Location = new System.Drawing.Point(21, 106);
            this.longTitle.Name = "longTitle";
            this.longTitle.Size = new System.Drawing.Size(164, 17);
            this.longTitle.TabIndex = 2;
            this.longTitle.Text = "Szerokość geograficzna:";
            // 
            // longValue
            // 
            this.longValue.AutoSize = true;
            this.longValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.longValue.Location = new System.Drawing.Point(21, 136);
            this.longValue.Name = "longValue";
            this.longValue.Size = new System.Drawing.Size(71, 17);
            this.longValue.TabIndex = 3;
            this.longValue.Text = "longValue";
            // 
            // altTitle
            // 
            this.altTitle.AutoSize = true;
            this.altTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.altTitle.Location = new System.Drawing.Point(21, 169);
            this.altTitle.Name = "altTitle";
            this.altTitle.Size = new System.Drawing.Size(76, 17);
            this.altTitle.TabIndex = 4;
            this.altTitle.Text = "Wysokość:";
            // 
            // altValue
            // 
            this.altValue.AutoSize = true;
            this.altValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.altValue.Location = new System.Drawing.Point(21, 199);
            this.altValue.Name = "altValue";
            this.altValue.Size = new System.Drawing.Size(59, 17);
            this.altValue.TabIndex = 5;
            this.altValue.Text = "altValue";
            // 
            // wbMap
            // 
            this.wbMap.Location = new System.Drawing.Point(357, 12);
            this.wbMap.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbMap.Name = "wbMap";
            this.wbMap.Size = new System.Drawing.Size(971, 426);
            this.wbMap.TabIndex = 10;
            // 
            // btPair
            // 
            this.btPair.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.btPair.Location = new System.Drawing.Point(24, 345);
            this.btPair.Name = "btPair";
            this.btPair.Size = new System.Drawing.Size(149, 52);
            this.btPair.TabIndex = 11;
            this.btPair.Text = "Sparuj z nadajnikiem";
            this.btPair.UseVisualStyleBackColor = true;
            this.btPair.Click += new System.EventHandler(this.btPair_Click);
            // 
            // btShowMap
            // 
            this.btShowMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.btShowMap.Location = new System.Drawing.Point(179, 345);
            this.btShowMap.Name = "btShowMap";
            this.btShowMap.Size = new System.Drawing.Size(149, 52);
            this.btShowMap.TabIndex = 12;
            this.btShowMap.Text = "Wyświetl na mapie";
            this.btShowMap.UseVisualStyleBackColor = true;
            this.btShowMap.Click += new System.EventHandler(this.btShowMap_Click);
            // 
            // satNumValue
            // 
            this.satNumValue.AutoSize = true;
            this.satNumValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.satNumValue.Location = new System.Drawing.Point(21, 258);
            this.satNumValue.Name = "satNumValue";
            this.satNumValue.Size = new System.Drawing.Size(92, 17);
            this.satNumValue.TabIndex = 14;
            this.satNumValue.Text = "satNumValue";
            // 
            // satNumTitle
            // 
            this.satNumTitle.AutoSize = true;
            this.satNumTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.satNumTitle.Location = new System.Drawing.Point(21, 228);
            this.satNumTitle.Name = "satNumTitle";
            this.satNumTitle.Size = new System.Drawing.Size(94, 17);
            this.satNumTitle.TabIndex = 13;
            this.satNumTitle.Text = "Liczba satelit:";
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.timerLabel.Location = new System.Drawing.Point(21, 315);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(74, 17);
            this.timerLabel.TabIndex = 15;
            this.timerLabel.Text = "timerLabel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(21, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Czas namierzania:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.satNumValue);
            this.Controls.Add(this.satNumTitle);
            this.Controls.Add(this.btShowMap);
            this.Controls.Add(this.btPair);
            this.Controls.Add(this.wbMap);
            this.Controls.Add(this.altValue);
            this.Controls.Add(this.altTitle);
            this.Controls.Add(this.longValue);
            this.Controls.Add(this.longTitle);
            this.Controls.Add(this.latValue);
            this.Controls.Add(this.latTitle);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label latTitle;
        private System.Windows.Forms.Label latValue;
        private System.Windows.Forms.Label longTitle;
        private System.Windows.Forms.Label longValue;
        private System.Windows.Forms.Label altTitle;
        private System.Windows.Forms.Label altValue;
        private System.Windows.Forms.Label satNumTitle;
        private System.Windows.Forms.Label satNumValue;
        private System.Windows.Forms.WebBrowser wbMap;
        private System.Windows.Forms.Button btPair;
        private System.Windows.Forms.Button btShowMap;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.Label label1;
    }
}

