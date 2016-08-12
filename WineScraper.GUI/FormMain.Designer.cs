namespace WineScraper.GUI
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tcTabs = new System.Windows.Forms.TabControl();
            this.tpScraper = new System.Windows.Forms.TabPage();
            this.gbWTSO = new System.Windows.Forms.GroupBox();
            this.lblWTSORatingText = new System.Windows.Forms.Label();
            this.lblWTSOMemberRating = new System.Windows.Forms.Label();
            this.lblWTSOShipping = new System.Windows.Forms.Label();
            this.nudWTSOInterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnToggleWTSOScrape = new System.Windows.Forms.Button();
            this.lblWTSOPrice = new System.Windows.Forms.Label();
            this.lblWTSOTitle = new System.Windows.Forms.Label();
            this.pbWTSOItem = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbLastBottle = new System.Windows.Forms.GroupBox();
            this.lblLBShipping = new System.Windows.Forms.Label();
            this.lblLBPrice = new System.Windows.Forms.Label();
            this.lblLBTitle = new System.Windows.Forms.Label();
            this.pbLastBottleItem = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnToggleLBScrape = new System.Windows.Forms.Button();
            this.nudLBInterval = new System.Windows.Forms.NumericUpDown();
            this.tpWTSOHistorical = new System.Windows.Forms.TabPage();
            this.txtWTSOPassword = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWTSOUser = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnHistoricalCrawl = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.nudWTSOHistoricalEnd = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudWTSOHistoricalStart = new System.Windows.Forms.NumericUpDown();
            this.pbWTSOHistorical = new System.Windows.Forms.PictureBox();
            this.lblWTSOHistoricalProcessItem = new System.Windows.Forms.Label();
            this.toolTipLB = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWTSOHistoricalTitle = new WineScraper.GUI.GrowLabel();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcTabs.SuspendLayout();
            this.tpScraper.SuspendLayout();
            this.gbWTSO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWTSOInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWTSOItem)).BeginInit();
            this.gbLastBottle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLastBottleItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLBInterval)).BeginInit();
            this.tpWTSOHistorical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWTSOHistoricalEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWTSOHistoricalStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWTSOHistorical)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcTabs
            // 
            this.tcTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcTabs.Controls.Add(this.tpScraper);
            this.tcTabs.Controls.Add(this.tpWTSOHistorical);
            this.tcTabs.Location = new System.Drawing.Point(0, 24);
            this.tcTabs.Name = "tcTabs";
            this.tcTabs.SelectedIndex = 0;
            this.tcTabs.Size = new System.Drawing.Size(720, 639);
            this.tcTabs.TabIndex = 0;
            this.tcTabs.SizeChanged += new System.EventHandler(this.tcTabs_SizeChanged);
            // 
            // tpScraper
            // 
            this.tpScraper.Controls.Add(this.gbWTSO);
            this.tpScraper.Controls.Add(this.gbLastBottle);
            this.tpScraper.Location = new System.Drawing.Point(4, 22);
            this.tpScraper.Name = "tpScraper";
            this.tpScraper.Padding = new System.Windows.Forms.Padding(3);
            this.tpScraper.Size = new System.Drawing.Size(712, 613);
            this.tpScraper.TabIndex = 0;
            this.tpScraper.Text = "Scraper";
            this.tpScraper.UseVisualStyleBackColor = true;
            this.tpScraper.SizeChanged += new System.EventHandler(this.tpScraper_SizeChanged);
            // 
            // gbWTSO
            // 
            this.gbWTSO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWTSO.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbWTSO.Controls.Add(this.lblWTSORatingText);
            this.gbWTSO.Controls.Add(this.lblWTSOMemberRating);
            this.gbWTSO.Controls.Add(this.lblWTSOShipping);
            this.gbWTSO.Controls.Add(this.nudWTSOInterval);
            this.gbWTSO.Controls.Add(this.label3);
            this.gbWTSO.Controls.Add(this.btnToggleWTSOScrape);
            this.gbWTSO.Controls.Add(this.lblWTSOPrice);
            this.gbWTSO.Controls.Add(this.lblWTSOTitle);
            this.gbWTSO.Controls.Add(this.pbWTSOItem);
            this.gbWTSO.Controls.Add(this.label6);
            this.gbWTSO.Location = new System.Drawing.Point(0, 288);
            this.gbWTSO.Name = "gbWTSO";
            this.gbWTSO.Size = new System.Drawing.Size(712, 305);
            this.gbWTSO.TabIndex = 8;
            this.gbWTSO.TabStop = false;
            this.gbWTSO.Text = "WTSO";
            // 
            // lblWTSORatingText
            // 
            this.lblWTSORatingText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblWTSORatingText.AutoSize = true;
            this.lblWTSORatingText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWTSORatingText.Location = new System.Drawing.Point(224, 160);
            this.lblWTSORatingText.Name = "lblWTSORatingText";
            this.lblWTSORatingText.Size = new System.Drawing.Size(154, 17);
            this.lblWTSORatingText.TabIndex = 11;
            this.lblWTSORatingText.Text = "WTSO Member Rating:";
            this.lblWTSORatingText.Visible = false;
            // 
            // lblWTSOMemberRating
            // 
            this.lblWTSOMemberRating.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblWTSOMemberRating.AutoSize = true;
            this.lblWTSOMemberRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWTSOMemberRating.Location = new System.Drawing.Point(376, 160);
            this.lblWTSOMemberRating.Name = "lblWTSOMemberRating";
            this.lblWTSOMemberRating.Size = new System.Drawing.Size(128, 17);
            this.lblWTSOMemberRating.TabIndex = 10;
            this.lblWTSOMemberRating.Text = "<Unknown Title>";
            this.lblWTSOMemberRating.Visible = false;
            // 
            // lblWTSOShipping
            // 
            this.lblWTSOShipping.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblWTSOShipping.AutoSize = true;
            this.lblWTSOShipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWTSOShipping.Location = new System.Drawing.Point(224, 80);
            this.lblWTSOShipping.Name = "lblWTSOShipping";
            this.lblWTSOShipping.Size = new System.Drawing.Size(113, 17);
            this.lblWTSOShipping.TabIndex = 9;
            this.lblWTSOShipping.Text = "<Unknown Title>";
            // 
            // nudWTSOInterval
            // 
            this.nudWTSOInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudWTSOInterval.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudWTSOInterval.Location = new System.Drawing.Point(592, 263);
            this.nudWTSOInterval.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
            this.nudWTSOInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWTSOInterval.Name = "nudWTSOInterval";
            this.nudWTSOInterval.Size = new System.Drawing.Size(104, 20);
            this.nudWTSOInterval.TabIndex = 7;
            this.nudWTSOInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudWTSOInterval.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudWTSOInterval.ValueChanged += new System.EventHandler(this.nudWTSOInterval_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(448, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Refresh Interval (sec):";
            // 
            // btnToggleWTSOScrape
            // 
            this.btnToggleWTSOScrape.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleWTSOScrape.Location = new System.Drawing.Point(504, 167);
            this.btnToggleWTSOScrape.Name = "btnToggleWTSOScrape";
            this.btnToggleWTSOScrape.Size = new System.Drawing.Size(192, 80);
            this.btnToggleWTSOScrape.TabIndex = 4;
            this.btnToggleWTSOScrape.Text = "Stop Scrape";
            this.btnToggleWTSOScrape.UseVisualStyleBackColor = true;
            this.btnToggleWTSOScrape.Click += new System.EventHandler(this.btnToggleWTSOScrape_Click);
            // 
            // lblWTSOPrice
            // 
            this.lblWTSOPrice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblWTSOPrice.AutoSize = true;
            this.lblWTSOPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWTSOPrice.Location = new System.Drawing.Point(224, 56);
            this.lblWTSOPrice.Name = "lblWTSOPrice";
            this.lblWTSOPrice.Size = new System.Drawing.Size(32, 17);
            this.lblWTSOPrice.TabIndex = 3;
            this.lblWTSOPrice.Text = "$$$";
            // 
            // lblWTSOTitle
            // 
            this.lblWTSOTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblWTSOTitle.AutoSize = true;
            this.lblWTSOTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWTSOTitle.Location = new System.Drawing.Point(224, 24);
            this.lblWTSOTitle.Name = "lblWTSOTitle";
            this.lblWTSOTitle.Size = new System.Drawing.Size(113, 17);
            this.lblWTSOTitle.TabIndex = 2;
            this.lblWTSOTitle.Text = "<Unknown Title>";
            // 
            // pbWTSOItem
            // 
            this.pbWTSOItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbWTSOItem.Location = new System.Drawing.Point(16, 48);
            this.pbWTSOItem.Name = "pbWTSOItem";
            this.pbWTSOItem.Size = new System.Drawing.Size(192, 239);
            this.pbWTSOItem.TabIndex = 1;
            this.pbWTSOItem.TabStop = false;
            this.pbWTSOItem.SizeChanged += new System.EventHandler(this.pbWTSOItem_SizeChanged);
            this.pbWTSOItem.DoubleClick += new System.EventHandler(this.pbWTSOItem_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Current Offer";
            // 
            // gbLastBottle
            // 
            this.gbLastBottle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLastBottle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbLastBottle.Controls.Add(this.lblLBShipping);
            this.gbLastBottle.Controls.Add(this.lblLBPrice);
            this.gbLastBottle.Controls.Add(this.lblLBTitle);
            this.gbLastBottle.Controls.Add(this.pbLastBottleItem);
            this.gbLastBottle.Controls.Add(this.label1);
            this.gbLastBottle.Controls.Add(this.label2);
            this.gbLastBottle.Controls.Add(this.btnToggleLBScrape);
            this.gbLastBottle.Controls.Add(this.nudLBInterval);
            this.gbLastBottle.Location = new System.Drawing.Point(0, 16);
            this.gbLastBottle.Name = "gbLastBottle";
            this.gbLastBottle.Size = new System.Drawing.Size(712, 272);
            this.gbLastBottle.TabIndex = 0;
            this.gbLastBottle.TabStop = false;
            this.gbLastBottle.Text = "Last Bottle";
            // 
            // lblLBShipping
            // 
            this.lblLBShipping.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLBShipping.AutoSize = true;
            this.lblLBShipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLBShipping.Location = new System.Drawing.Point(268, 72);
            this.lblLBShipping.Name = "lblLBShipping";
            this.lblLBShipping.Size = new System.Drawing.Size(113, 17);
            this.lblLBShipping.TabIndex = 13;
            this.lblLBShipping.Text = "<Unknown Title>";
            // 
            // lblLBPrice
            // 
            this.lblLBPrice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLBPrice.AutoSize = true;
            this.lblLBPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLBPrice.Location = new System.Drawing.Point(268, 48);
            this.lblLBPrice.Name = "lblLBPrice";
            this.lblLBPrice.Size = new System.Drawing.Size(32, 17);
            this.lblLBPrice.TabIndex = 12;
            this.lblLBPrice.Text = "$$$";
            // 
            // lblLBTitle
            // 
            this.lblLBTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLBTitle.AutoSize = true;
            this.lblLBTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLBTitle.Location = new System.Drawing.Point(268, 16);
            this.lblLBTitle.Name = "lblLBTitle";
            this.lblLBTitle.Size = new System.Drawing.Size(113, 17);
            this.lblLBTitle.TabIndex = 11;
            this.lblLBTitle.Text = "<Unknown Title>";
            // 
            // pbLastBottleItem
            // 
            this.pbLastBottleItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLastBottleItem.Location = new System.Drawing.Point(8, 40);
            this.pbLastBottleItem.Name = "pbLastBottleItem";
            this.pbLastBottleItem.Size = new System.Drawing.Size(232, 224);
            this.pbLastBottleItem.TabIndex = 10;
            this.pbLastBottleItem.TabStop = false;
            this.pbLastBottleItem.SizeChanged += new System.EventHandler(this.pbLastBottleItem_SizeChanged);
            this.pbLastBottleItem.DoubleClick += new System.EventHandler(this.pbLastBottleItem_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Current Offer";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(464, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Refresh Interval (sec):";
            // 
            // btnToggleLBScrape
            // 
            this.btnToggleLBScrape.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleLBScrape.Location = new System.Drawing.Point(504, 144);
            this.btnToggleLBScrape.Name = "btnToggleLBScrape";
            this.btnToggleLBScrape.Size = new System.Drawing.Size(192, 80);
            this.btnToggleLBScrape.TabIndex = 4;
            this.btnToggleLBScrape.Text = "Stop Scrape";
            this.btnToggleLBScrape.UseVisualStyleBackColor = true;
            this.btnToggleLBScrape.Click += new System.EventHandler(this.btnToggleLBScrape_Click);
            // 
            // nudLBInterval
            // 
            this.nudLBInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLBInterval.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudLBInterval.Location = new System.Drawing.Point(616, 240);
            this.nudLBInterval.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
            this.nudLBInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLBInterval.Name = "nudLBInterval";
            this.nudLBInterval.Size = new System.Drawing.Size(80, 20);
            this.nudLBInterval.TabIndex = 7;
            this.nudLBInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudLBInterval.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudLBInterval.ValueChanged += new System.EventHandler(this.nudLBInterval_ValueChanged);
            // 
            // tpWTSOHistorical
            // 
            this.tpWTSOHistorical.Controls.Add(this.txtWTSOPassword);
            this.tpWTSOHistorical.Controls.Add(this.label10);
            this.tpWTSOHistorical.Controls.Add(this.txtWTSOUser);
            this.tpWTSOHistorical.Controls.Add(this.label9);
            this.tpWTSOHistorical.Controls.Add(this.btnHistoricalCrawl);
            this.tpWTSOHistorical.Controls.Add(this.label8);
            this.tpWTSOHistorical.Controls.Add(this.nudWTSOHistoricalEnd);
            this.tpWTSOHistorical.Controls.Add(this.label7);
            this.tpWTSOHistorical.Controls.Add(this.nudWTSOHistoricalStart);
            this.tpWTSOHistorical.Controls.Add(this.pbWTSOHistorical);
            this.tpWTSOHistorical.Controls.Add(this.lblWTSOHistoricalProcessItem);
            this.tpWTSOHistorical.Controls.Add(this.lblWTSOHistoricalTitle);
            this.tpWTSOHistorical.Location = new System.Drawing.Point(4, 22);
            this.tpWTSOHistorical.Name = "tpWTSOHistorical";
            this.tpWTSOHistorical.Padding = new System.Windows.Forms.Padding(3);
            this.tpWTSOHistorical.Size = new System.Drawing.Size(712, 613);
            this.tpWTSOHistorical.TabIndex = 1;
            this.tpWTSOHistorical.Text = "WTSO Historical";
            this.tpWTSOHistorical.UseVisualStyleBackColor = true;
            this.tpWTSOHistorical.SizeChanged += new System.EventHandler(this.tpWTSOHistorical_SizeChanged);
            // 
            // txtWTSOPassword
            // 
            this.txtWTSOPassword.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtWTSOPassword.Location = new System.Drawing.Point(408, 307);
            this.txtWTSOPassword.Name = "txtWTSOPassword";
            this.txtWTSOPassword.PasswordChar = '*';
            this.txtWTSOPassword.Size = new System.Drawing.Size(288, 20);
            this.txtWTSOPassword.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(408, 283);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 17);
            this.label10.TabIndex = 15;
            this.label10.Text = "Password:";
            // 
            // txtWTSOUser
            // 
            this.txtWTSOUser.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtWTSOUser.Location = new System.Drawing.Point(408, 251);
            this.txtWTSOUser.Name = "txtWTSOUser";
            this.txtWTSOUser.Size = new System.Drawing.Size(288, 20);
            this.txtWTSOUser.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(408, 227);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 17);
            this.label9.TabIndex = 13;
            this.label9.Text = "User Name (email):";
            // 
            // btnHistoricalCrawl
            // 
            this.btnHistoricalCrawl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistoricalCrawl.Location = new System.Drawing.Point(408, 511);
            this.btnHistoricalCrawl.Name = "btnHistoricalCrawl";
            this.btnHistoricalCrawl.Size = new System.Drawing.Size(288, 88);
            this.btnHistoricalCrawl.TabIndex = 12;
            this.btnHistoricalCrawl.Text = "Start Historical Crawl";
            this.btnHistoricalCrawl.UseVisualStyleBackColor = true;
            this.btnHistoricalCrawl.Click += new System.EventHandler(this.btnHistoricalCrawl_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(408, 479);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Crawl Range End";
            // 
            // nudWTSOHistoricalEnd
            // 
            this.nudWTSOHistoricalEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudWTSOHistoricalEnd.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudWTSOHistoricalEnd.Location = new System.Drawing.Point(568, 479);
            this.nudWTSOHistoricalEnd.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudWTSOHistoricalEnd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWTSOHistoricalEnd.Name = "nudWTSOHistoricalEnd";
            this.nudWTSOHistoricalEnd.Size = new System.Drawing.Size(128, 20);
            this.nudWTSOHistoricalEnd.TabIndex = 11;
            this.nudWTSOHistoricalEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudWTSOHistoricalEnd.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(408, 439);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Crawl Range Start";
            // 
            // nudWTSOHistoricalStart
            // 
            this.nudWTSOHistoricalStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudWTSOHistoricalStart.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudWTSOHistoricalStart.Location = new System.Drawing.Point(568, 439);
            this.nudWTSOHistoricalStart.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudWTSOHistoricalStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWTSOHistoricalStart.Name = "nudWTSOHistoricalStart";
            this.nudWTSOHistoricalStart.Size = new System.Drawing.Size(128, 20);
            this.nudWTSOHistoricalStart.TabIndex = 9;
            this.nudWTSOHistoricalStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudWTSOHistoricalStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // pbWTSOHistorical
            // 
            this.pbWTSOHistorical.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbWTSOHistorical.Location = new System.Drawing.Point(16, 32);
            this.pbWTSOHistorical.Name = "pbWTSOHistorical";
            this.pbWTSOHistorical.Size = new System.Drawing.Size(376, 567);
            this.pbWTSOHistorical.TabIndex = 4;
            this.pbWTSOHistorical.TabStop = false;
            // 
            // lblWTSOHistoricalProcessItem
            // 
            this.lblWTSOHistoricalProcessItem.AutoSize = true;
            this.lblWTSOHistoricalProcessItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWTSOHistoricalProcessItem.Location = new System.Drawing.Point(16, 8);
            this.lblWTSOHistoricalProcessItem.Name = "lblWTSOHistoricalProcessItem";
            this.lblWTSOHistoricalProcessItem.Size = new System.Drawing.Size(148, 17);
            this.lblWTSOHistoricalProcessItem.TabIndex = 3;
            this.lblWTSOHistoricalProcessItem.Text = "Currently Not Running";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(720, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lblWTSOHistoricalTitle
            // 
            this.lblWTSOHistoricalTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWTSOHistoricalTitle.Location = new System.Drawing.Point(408, 32);
            this.lblWTSOHistoricalTitle.MaxWidth = 0;
            this.lblWTSOHistoricalTitle.Name = "lblWTSOHistoricalTitle";
            this.lblWTSOHistoricalTitle.Size = new System.Drawing.Size(124, 17);
            this.lblWTSOHistoricalTitle.TabIndex = 5;
            this.lblWTSOHistoricalTitle.Text = "<No Item Loaded>";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseWizardToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // databaseWizardToolStripMenuItem
            // 
            this.databaseWizardToolStripMenuItem.Name = "databaseWizardToolStripMenuItem";
            this.databaseWizardToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.databaseWizardToolStripMenuItem.Text = "Database Wizard";
            this.databaseWizardToolStripMenuItem.Click += new System.EventHandler(this.databaseWizardToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(720, 662);
            this.Controls.Add(this.tcTabs);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormMain_Layout);
            this.tcTabs.ResumeLayout(false);
            this.tpScraper.ResumeLayout(false);
            this.gbWTSO.ResumeLayout(false);
            this.gbWTSO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWTSOInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWTSOItem)).EndInit();
            this.gbLastBottle.ResumeLayout(false);
            this.gbLastBottle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLastBottleItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLBInterval)).EndInit();
            this.tpWTSOHistorical.ResumeLayout(false);
            this.tpWTSOHistorical.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWTSOHistoricalEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWTSOHistoricalStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWTSOHistorical)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbTabs;
        private System.Windows.Forms.TabPage pgMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tcTabs;
        private System.Windows.Forms.TabPage tpScraper;
        private System.Windows.Forms.GroupBox gbLastBottle;
        private System.Windows.Forms.TabPage tpWTSOHistorical;
        private System.Windows.Forms.NumericUpDown nudLBInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnToggleLBScrape;
        private System.Windows.Forms.ToolTip toolTipLB;
        private System.Windows.Forms.GroupBox gbWTSO;
        private System.Windows.Forms.NumericUpDown nudWTSOInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnToggleWTSOScrape;
        private System.Windows.Forms.Label lblWTSOPrice;
        private System.Windows.Forms.Label lblWTSOTitle;
        private System.Windows.Forms.PictureBox pbWTSOItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblWTSOShipping;
        private System.Windows.Forms.Label lblWTSORatingText;
        private System.Windows.Forms.Label lblWTSOMemberRating;
        private System.Windows.Forms.Label lblLBShipping;
        private System.Windows.Forms.Label lblLBPrice;
        private System.Windows.Forms.Label lblLBTitle;
        private System.Windows.Forms.PictureBox pbLastBottleItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbWTSOHistorical;
        private System.Windows.Forms.Label lblWTSOHistoricalProcessItem;
        private System.Windows.Forms.TextBox txtWTSOPassword;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWTSOUser;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnHistoricalCrawl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudWTSOHistoricalEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudWTSOHistoricalStart;
        private GrowLabel lblWTSOHistoricalTitle;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseWizardToolStripMenuItem;
    }
}

