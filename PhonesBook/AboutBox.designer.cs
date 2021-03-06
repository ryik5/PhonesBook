﻿namespace PhonesBook
{
    partial class AboutBox
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.labelOcupiedRAM = new System.Windows.Forms.Label();
            this.labelOS = new System.Windows.Forms.Label();
            this.labelPC = new System.Windows.Forms.Label();
            this.labelFile = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelPath = new System.Windows.Forms.Label();
            this.buttonNo = new System.Windows.Forms.Button();
            this.buttonYes = new System.Windows.Forms.Button();
            this.labelProductNameData = new System.Windows.Forms.Label();
            this.labelCopyrightData = new System.Windows.Forms.Label();
            this.labelVersionData = new System.Windows.Forms.Label();
            this.labelPathData = new System.Windows.Forms.Label();
            this.labelFileData = new System.Windows.Forms.Label();
            this.labelPCData = new System.Windows.Forms.Label();
            this.labelOSData = new System.Windows.Forms.Label();
            this.labelOcupiedRAMData = new System.Windows.Forms.Label();
            this.labelDiscription = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelOcupiedRAM, 1, 7);
            this.tableLayoutPanel.Controls.Add(this.labelOS, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.labelPC, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.labelFile, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelPath, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.buttonNo, 1, 8);
            this.tableLayoutPanel.Controls.Add(this.buttonYes, 2, 8);
            this.tableLayoutPanel.Controls.Add(this.labelProductNameData, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.labelCopyrightData, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.labelVersionData, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.labelPathData, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.labelFileData, 2, 4);
            this.tableLayoutPanel.Controls.Add(this.labelPCData, 2, 5);
            this.tableLayoutPanel.Controls.Add(this.labelOSData, 2, 6);
            this.tableLayoutPanel.Controls.Add(this.labelOcupiedRAMData, 2, 7);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 9;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(627, 237);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.BackgroundImage = global::PhonesBook.Properties.Resources.LogoRYIK;
            this.logoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Location = new System.Drawing.Point(1, 1);
            this.logoPictureBox.Margin = new System.Windows.Forms.Padding(1);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 8);
            this.logoPictureBox.Size = new System.Drawing.Size(186, 193);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // labelOcupiedRAM
            // 
            this.labelOcupiedRAM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOcupiedRAM.Location = new System.Drawing.Point(191, 171);
            this.labelOcupiedRAM.Name = "labelOcupiedRAM";
            this.labelOcupiedRAM.Size = new System.Drawing.Size(88, 24);
            this.labelOcupiedRAM.TabIndex = 26;
            this.labelOcupiedRAM.Text = "In RAM";
            this.labelOcupiedRAM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelOS
            // 
            this.labelOS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOS.Location = new System.Drawing.Point(191, 147);
            this.labelOS.Name = "labelOS";
            this.labelOS.Size = new System.Drawing.Size(88, 24);
            this.labelOS.TabIndex = 25;
            this.labelOS.Text = "OS";
            this.labelOS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPC
            // 
            this.labelPC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPC.Location = new System.Drawing.Point(191, 123);
            this.labelPC.Name = "labelPC";
            this.labelPC.Size = new System.Drawing.Size(88, 24);
            this.labelPC.TabIndex = 24;
            this.labelPC.Text = "PC";
            this.labelPC.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelFile
            // 
            this.labelFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFile.Location = new System.Drawing.Point(191, 99);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(88, 24);
            this.labelFile.TabIndex = 23;
            this.labelFile.Text = "File";
            this.labelFile.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelProductName
            // 
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Location = new System.Drawing.Point(191, 6);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(88, 21);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Название ПО";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelVersion
            // 
            this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersion.Location = new System.Drawing.Point(191, 51);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(88, 24);
            this.labelVersion.TabIndex = 21;
            this.labelVersion.Text = "Версия";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Location = new System.Drawing.Point(191, 27);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(88, 24);
            this.labelCopyright.TabIndex = 20;
            this.labelCopyright.Text = "Авторские права";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPath
            // 
            this.labelPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPath.Location = new System.Drawing.Point(191, 75);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(88, 24);
            this.labelPath.TabIndex = 22;
            this.labelPath.Text = "Path";
            this.labelPath.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonNo
            // 
            this.buttonNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonNo.Location = new System.Drawing.Point(191, 198);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(88, 36);
            this.buttonNo.TabIndex = 28;
            this.buttonNo.Text = "Cancel";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            // 
            // buttonYes
            // 
            this.buttonYes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonYes.Location = new System.Drawing.Point(285, 198);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(339, 36);
            this.buttonYes.TabIndex = 27;
            this.buttonYes.Text = "Ok";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            // 
            // labelProductNameData
            // 
            this.labelProductNameData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductNameData.Location = new System.Drawing.Point(285, 6);
            this.labelProductNameData.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.labelProductNameData.Name = "labelProductNameData";
            this.labelProductNameData.Size = new System.Drawing.Size(339, 21);
            this.labelProductNameData.TabIndex = 29;
            // 
            // labelCopyrightData
            // 
            this.labelCopyrightData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyrightData.Location = new System.Drawing.Point(285, 27);
            this.labelCopyrightData.Name = "labelCopyrightData";
            this.labelCopyrightData.Size = new System.Drawing.Size(339, 24);
            this.labelCopyrightData.TabIndex = 30;
            // 
            // labelVersionData
            // 
            this.labelVersionData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersionData.Location = new System.Drawing.Point(285, 51);
            this.labelVersionData.Name = "labelVersionData";
            this.labelVersionData.Size = new System.Drawing.Size(339, 24);
            this.labelVersionData.TabIndex = 31;
            // 
            // labelPathData
            // 
            this.labelPathData.AutoEllipsis = true;
            this.labelPathData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPathData.Location = new System.Drawing.Point(285, 75);
            this.labelPathData.Name = "labelPathData";
            this.labelPathData.Size = new System.Drawing.Size(339, 24);
            this.labelPathData.TabIndex = 32;
            // 
            // labelFileData
            // 
            this.labelFileData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFileData.Location = new System.Drawing.Point(285, 99);
            this.labelFileData.Name = "labelFileData";
            this.labelFileData.Size = new System.Drawing.Size(339, 24);
            this.labelFileData.TabIndex = 33;
            // 
            // labelPCData
            // 
            this.labelPCData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPCData.Location = new System.Drawing.Point(285, 123);
            this.labelPCData.Name = "labelPCData";
            this.labelPCData.Size = new System.Drawing.Size(339, 24);
            this.labelPCData.TabIndex = 34;
            // 
            // labelOSData
            // 
            this.labelOSData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOSData.Location = new System.Drawing.Point(285, 147);
            this.labelOSData.Name = "labelOSData";
            this.labelOSData.Size = new System.Drawing.Size(339, 24);
            this.labelOSData.TabIndex = 35;
            // 
            // labelOcupiedRAMData
            // 
            this.labelOcupiedRAMData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOcupiedRAMData.Location = new System.Drawing.Point(285, 171);
            this.labelOcupiedRAMData.Name = "labelOcupiedRAMData";
            this.labelOcupiedRAMData.Size = new System.Drawing.Size(339, 24);
            this.labelOcupiedRAMData.TabIndex = 36;
            // 
            // labelDiscription
            // 
            this.labelDiscription.Location = new System.Drawing.Point(0, 0);
            this.labelDiscription.Name = "labelDiscription";
            this.labelDiscription.Size = new System.Drawing.Size(100, 23);
            this.labelDiscription.TabIndex = 0;
            // 
            // AboutBox
            // 
            this.AcceptButton = this.buttonYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 243);
            this.Controls.Add(this.tableLayoutPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutBox";
            this.TopMost = true;
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label labelDiscription;
        private System.Windows.Forms.Label labelOcupiedRAM;
        private System.Windows.Forms.Label labelOS;
        private System.Windows.Forms.Label labelPC;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelProductNameData;
        private System.Windows.Forms.Label labelCopyrightData;
        private System.Windows.Forms.Label labelVersionData;
        private System.Windows.Forms.Label labelPathData;
        private System.Windows.Forms.Label labelFileData;
        private System.Windows.Forms.Label labelPCData;
        private System.Windows.Forms.Label labelOSData;
        private System.Windows.Forms.Label labelOcupiedRAMData;
    }
}
