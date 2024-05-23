namespace CourseSummaryDesign
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Eraser = new System.Windows.Forms.Button();
            this.ForwardButton = new System.Windows.Forms.Button();
            this.BackBotton = new System.Windows.Forms.Button();
            this.Properites = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.NewFileToolTrip = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileToolTrip = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFileToolTrip = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MoreColorButton = new System.Windows.Forms.Button();
            this.GreenButton = new System.Windows.Forms.Button();
            this.BlueButton = new System.Windows.Forms.Button();
            this.RedButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CommonCusor = new System.Windows.Forms.Button();
            this.Rectangle = new System.Windows.Forms.Button();
            this.Line = new System.Windows.Forms.Button();
            this.Oval = new System.Windows.Forms.Button();
            this.DrawPanel = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移到最底层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移到最上层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Panels = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawPanel)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.Panels.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.Properites);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1071, 130);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Eraser);
            this.groupBox3.Controls.Add(this.ForwardButton);
            this.groupBox3.Controls.Add(this.BackBotton);
            this.groupBox3.Location = new System.Drawing.Point(586, 31);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(266, 85);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "工具";
            // 
            // Eraser
            // 
            this.Eraser.BackgroundImage = global::CourseSummaryDesign.Properties.Resources.橡皮;
            this.Eraser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Eraser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Eraser.Location = new System.Drawing.Point(191, 30);
            this.Eraser.Name = "Eraser";
            this.Eraser.Size = new System.Drawing.Size(39, 34);
            this.Eraser.TabIndex = 0;
            this.Eraser.UseVisualStyleBackColor = true;
            this.Eraser.Click += new System.EventHandler(this.Eraser_Click);
            // 
            // ForwardButton
            // 
            this.ForwardButton.BackgroundImage = global::CourseSummaryDesign.Properties.Resources.Redo;
            this.ForwardButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ForwardButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ForwardButton.Location = new System.Drawing.Point(117, 30);
            this.ForwardButton.Name = "ForwardButton";
            this.ForwardButton.Size = new System.Drawing.Size(39, 34);
            this.ForwardButton.TabIndex = 0;
            this.ForwardButton.UseVisualStyleBackColor = true;
            this.ForwardButton.Click += new System.EventHandler(this.ForwardButton_Click);
            // 
            // BackBotton
            // 
            this.BackBotton.BackgroundImage = global::CourseSummaryDesign.Properties.Resources.Undo;
            this.BackBotton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackBotton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BackBotton.Location = new System.Drawing.Point(42, 31);
            this.BackBotton.Name = "BackBotton";
            this.BackBotton.Size = new System.Drawing.Size(40, 33);
            this.BackBotton.TabIndex = 0;
            this.BackBotton.UseVisualStyleBackColor = true;
            this.BackBotton.Click += new System.EventHandler(this.BackBotton_Click);
            // 
            // Properites
            // 
            this.Properites.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Properites.Location = new System.Drawing.Point(881, 46);
            this.Properites.Name = "Properites";
            this.Properites.Size = new System.Drawing.Size(158, 49);
            this.Properites.TabIndex = 2;
            this.Properites.Text = "属性";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Gainsboro;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1071, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewFileToolTrip,
            this.打开ToolStripMenuItem,
            this.OpenFileToolTrip,
            this.保存ToolStripMenuItem,
            this.SaveFileToolTrip});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(53, 24);
            this.toolStripButton1.Text = "文件";
            // 
            // NewFileToolTrip
            // 
            this.NewFileToolTrip.Name = "NewFileToolTrip";
            this.NewFileToolTrip.Size = new System.Drawing.Size(152, 26);
            this.NewFileToolTrip.Text = "新建";
            this.NewFileToolTrip.Click += new System.EventHandler(this.NewFileToolTrip_Click);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // OpenFileToolTrip
            // 
            this.OpenFileToolTrip.Name = "OpenFileToolTrip";
            this.OpenFileToolTrip.Size = new System.Drawing.Size(152, 26);
            this.OpenFileToolTrip.Text = "打开图片";
            this.OpenFileToolTrip.Click += new System.EventHandler(this.OpenFileToolTrip_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // SaveFileToolTrip
            // 
            this.SaveFileToolTrip.Name = "SaveFileToolTrip";
            this.SaveFileToolTrip.Size = new System.Drawing.Size(152, 26);
            this.SaveFileToolTrip.Text = "保存图片";
            this.SaveFileToolTrip.Click += new System.EventHandler(this.SaveFileToolTrip_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MoreColorButton);
            this.groupBox2.Controls.Add(this.GreenButton);
            this.groupBox2.Controls.Add(this.BlueButton);
            this.groupBox2.Controls.Add(this.RedButton);
            this.groupBox2.Location = new System.Drawing.Point(312, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 96);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "颜色";
            // 
            // MoreColorButton
            // 
            this.MoreColorButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MoreColorButton.Location = new System.Drawing.Point(190, 24);
            this.MoreColorButton.Name = "MoreColorButton";
            this.MoreColorButton.Size = new System.Drawing.Size(39, 61);
            this.MoreColorButton.TabIndex = 1;
            this.MoreColorButton.Text = "更多";
            this.MoreColorButton.UseVisualStyleBackColor = true;
            this.MoreColorButton.Click += new System.EventHandler(this.MoreColorButton_Click);
            // 
            // GreenButton
            // 
            this.GreenButton.BackColor = System.Drawing.Color.Green;
            this.GreenButton.Location = new System.Drawing.Point(127, 29);
            this.GreenButton.Name = "GreenButton";
            this.GreenButton.Size = new System.Drawing.Size(35, 35);
            this.GreenButton.TabIndex = 0;
            this.GreenButton.UseVisualStyleBackColor = false;
            this.GreenButton.Click += new System.EventHandler(this.GreenButton_Click);
            // 
            // BlueButton
            // 
            this.BlueButton.BackColor = System.Drawing.Color.Blue;
            this.BlueButton.Location = new System.Drawing.Point(72, 29);
            this.BlueButton.Name = "BlueButton";
            this.BlueButton.Size = new System.Drawing.Size(35, 35);
            this.BlueButton.TabIndex = 0;
            this.BlueButton.UseVisualStyleBackColor = false;
            this.BlueButton.Click += new System.EventHandler(this.BlueButton_Click);
            // 
            // RedButton
            // 
            this.RedButton.BackColor = System.Drawing.Color.Red;
            this.RedButton.Location = new System.Drawing.Point(18, 29);
            this.RedButton.Name = "RedButton";
            this.RedButton.Size = new System.Drawing.Size(35, 35);
            this.RedButton.TabIndex = 0;
            this.RedButton.UseVisualStyleBackColor = false;
            this.RedButton.Click += new System.EventHandler(this.RedButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CommonCusor);
            this.groupBox1.Controls.Add(this.Rectangle);
            this.groupBox1.Controls.Add(this.Line);
            this.groupBox1.Controls.Add(this.Oval);
            this.groupBox1.Location = new System.Drawing.Point(46, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 93);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "形状";
            // 
            // CommonCusor
            // 
            this.CommonCusor.BackgroundImage = global::CourseSummaryDesign.Properties.Resources.指针__1_;
            this.CommonCusor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CommonCusor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CommonCusor.Location = new System.Drawing.Point(190, 28);
            this.CommonCusor.Name = "CommonCusor";
            this.CommonCusor.Size = new System.Drawing.Size(54, 54);
            this.CommonCusor.TabIndex = 1;
            this.CommonCusor.UseVisualStyleBackColor = true;
            this.CommonCusor.Click += new System.EventHandler(this.CommonCusor_Click);
            // 
            // Rectangle
            // 
            this.Rectangle.BackgroundImage = global::CourseSummaryDesign.Properties.Resources.形状_矩形;
            this.Rectangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rectangle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Rectangle.Location = new System.Drawing.Point(128, 26);
            this.Rectangle.Name = "Rectangle";
            this.Rectangle.Size = new System.Drawing.Size(56, 59);
            this.Rectangle.TabIndex = 0;
            this.Rectangle.UseVisualStyleBackColor = true;
            this.Rectangle.Click += new System.EventHandler(this.Rectangle_Click);
            // 
            // Line
            // 
            this.Line.BackgroundImage = global::CourseSummaryDesign.Properties.Resources.直线;
            this.Line.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Line.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Line.Location = new System.Drawing.Point(66, 26);
            this.Line.Name = "Line";
            this.Line.Size = new System.Drawing.Size(56, 59);
            this.Line.TabIndex = 0;
            this.Line.UseVisualStyleBackColor = true;
            this.Line.Click += new System.EventHandler(this.Line_Click);
            // 
            // Oval
            // 
            this.Oval.BackColor = System.Drawing.Color.Gainsboro;
            this.Oval.BackgroundImage = global::CourseSummaryDesign.Properties.Resources.椭圆;
            this.Oval.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Oval.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.Oval.FlatAppearance.BorderSize = 5;
            this.Oval.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Oval.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Oval.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Oval.Location = new System.Drawing.Point(6, 26);
            this.Oval.Name = "Oval";
            this.Oval.Size = new System.Drawing.Size(54, 59);
            this.Oval.TabIndex = 0;
            this.Oval.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Oval.UseVisualStyleBackColor = false;
            this.Oval.Click += new System.EventHandler(this.Oval_Click);
            // 
            // DrawPanel
            // 
            this.DrawPanel.BackColor = System.Drawing.Color.White;
            this.DrawPanel.ContextMenuStrip = this.contextMenuStrip1;
            this.DrawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawPanel.Location = new System.Drawing.Point(5, 5);
            this.DrawPanel.Name = "DrawPanel";
            this.DrawPanel.Size = new System.Drawing.Size(1061, 562);
            this.DrawPanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DrawPanel.TabIndex = 1;
            this.DrawPanel.TabStop = false;
            this.DrawPanel.SizeChanged += new System.EventHandler(this.DrawPanel_SizeChanged);
            this.DrawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawPanel_MouseDown);
            this.DrawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawPanel_MouseMove);
            this.DrawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawPanel_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem,
            this.移到最底层ToolStripMenuItem,
            this.移到最上层ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 100);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 上移ToolStripMenuItem
            // 
            this.上移ToolStripMenuItem.Enabled = false;
            this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
            this.上移ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.上移ToolStripMenuItem.Text = "上移";
            this.上移ToolStripMenuItem.Click += new System.EventHandler(this.上移ToolStripMenuItem_Click);
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Enabled = false;
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.下移ToolStripMenuItem.Text = "下移";
            this.下移ToolStripMenuItem.Click += new System.EventHandler(this.下移ToolStripMenuItem_Click);
            // 
            // 移到最底层ToolStripMenuItem
            // 
            this.移到最底层ToolStripMenuItem.Enabled = false;
            this.移到最底层ToolStripMenuItem.Name = "移到最底层ToolStripMenuItem";
            this.移到最底层ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.移到最底层ToolStripMenuItem.Text = "移到最底层";
            this.移到最底层ToolStripMenuItem.Click += new System.EventHandler(this.移到最底层ToolStripMenuItem_Click);
            // 
            // 移到最上层ToolStripMenuItem
            // 
            this.移到最上层ToolStripMenuItem.Enabled = false;
            this.移到最上层ToolStripMenuItem.Name = "移到最上层ToolStripMenuItem";
            this.移到最上层ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.移到最上层ToolStripMenuItem.Text = "移到最上层";
            this.移到最上层ToolStripMenuItem.Click += new System.EventHandler(this.移到最上层ToolStripMenuItem_Click);
            // 
            // Panels
            // 
            this.Panels.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Panels.Controls.Add(this.DrawPanel);
            this.Panels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panels.Location = new System.Drawing.Point(0, 130);
            this.Panels.Name = "Panels";
            this.Panels.Padding = new System.Windows.Forms.Padding(5);
            this.Panels.Size = new System.Drawing.Size(1071, 572);
            this.Panels.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 702);
            this.Controls.Add(this.Panels);
            this.Controls.Add(this.panel1);
            this.Name = "MainWindow";
            this.Text = "Panel";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DrawPanel)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.Panels.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Button Rectangle;
        private Button Line;
        private Button Oval;
        private PictureBox DrawPanel;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripButton1;
        private ToolStripMenuItem NewFileToolTrip;
        private ToolStripMenuItem OpenFileToolTrip;
        private ToolStripMenuItem SaveFileToolTrip;
        private Button CommonCusor;
        private Panel Panels;
        private Label Properites;
        private Button MoreColorButton;
        private Button GreenButton;
        private Button BlueButton;
        private Button RedButton;
        private ColorDialog colorDialog1;
        private GroupBox groupBox3;
        private Button ForwardButton;
        private Button BackBotton;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 上移ToolStripMenuItem;
        private ToolStripMenuItem 下移ToolStripMenuItem;
        private ToolStripMenuItem 移到最底层ToolStripMenuItem;
        private ToolStripMenuItem 移到最上层ToolStripMenuItem;
        private ToolStripMenuItem 打开ToolStripMenuItem;
        private ToolStripMenuItem 保存ToolStripMenuItem;
        private Button Eraser;
    }
}