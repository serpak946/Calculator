namespace Calculator
{
    partial class Калькулятор
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Калькулятор));
            this.menuPanel = new System.Windows.Forms.Panel();
            this.trigCalculatorButton = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.graphicCalculatorButton = new System.Windows.Forms.Button();
            this.mathCalculatorButton = new System.Windows.Forms.Button();
            this.simpleCalculatorButton = new System.Windows.Forms.Button();
            this.buttonHamburger = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelControl = new System.Windows.Forms.Panel();
            this.buttonMinimie = new System.Windows.Forms.Button();
            this.buttonMaximize = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panelHistory = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuPanel.SuspendLayout();
            this.panelControl.SuspendLayout();
            this.panelHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.menuPanel.Controls.Add(this.trigCalculatorButton);
            this.menuPanel.Controls.Add(this.graphicCalculatorButton);
            this.menuPanel.Controls.Add(this.mathCalculatorButton);
            this.menuPanel.Controls.Add(this.simpleCalculatorButton);
            this.menuPanel.Controls.Add(this.buttonHamburger);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuPanel.Location = new System.Drawing.Point(5, 45);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(45, 618);
            this.menuPanel.TabIndex = 2;
            // 
            // trigCalculatorButton
            // 
            this.trigCalculatorButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.trigCalculatorButton.FlatAppearance.BorderSize = 0;
            this.trigCalculatorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.trigCalculatorButton.ImageIndex = 6;
            this.trigCalculatorButton.ImageList = this.imageList1;
            this.trigCalculatorButton.Location = new System.Drawing.Point(0, 380);
            this.trigCalculatorButton.Name = "trigCalculatorButton";
            this.trigCalculatorButton.Size = new System.Drawing.Size(45, 110);
            this.trigCalculatorButton.TabIndex = 8;
            this.toolTip.SetToolTip(this.trigCalculatorButton, "Тригонометрический калькултор");
            this.trigCalculatorButton.UseVisualStyleBackColor = true;
            this.trigCalculatorButton.Visible = false;
            this.trigCalculatorButton.Click += new System.EventHandler(this.trigCalculatorButton_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Калькулятор.png");
            this.imageList1.Images.SetKeyName(1, "Калькулятор_свет.png");
            this.imageList1.Images.SetKeyName(2, "Лабораторный.png");
            this.imageList1.Images.SetKeyName(3, "Лабораторный_свет.png");
            this.imageList1.Images.SetKeyName(4, "График.png");
            this.imageList1.Images.SetKeyName(5, "График_свет.png");
            this.imageList1.Images.SetKeyName(6, "Тригонометрия.png");
            this.imageList1.Images.SetKeyName(7, "Тригонометрия_свет.png");
            // 
            // graphicCalculatorButton
            // 
            this.graphicCalculatorButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.graphicCalculatorButton.FlatAppearance.BorderSize = 0;
            this.graphicCalculatorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.graphicCalculatorButton.ImageIndex = 4;
            this.graphicCalculatorButton.ImageList = this.imageList1;
            this.graphicCalculatorButton.Location = new System.Drawing.Point(0, 270);
            this.graphicCalculatorButton.Name = "graphicCalculatorButton";
            this.graphicCalculatorButton.Size = new System.Drawing.Size(45, 110);
            this.graphicCalculatorButton.TabIndex = 7;
            this.toolTip.SetToolTip(this.graphicCalculatorButton, "Графический калькулятор");
            this.graphicCalculatorButton.UseVisualStyleBackColor = true;
            this.graphicCalculatorButton.Visible = false;
            this.graphicCalculatorButton.Click += new System.EventHandler(this.graphicCalculatorButton_Click);
            // 
            // mathCalculatorButton
            // 
            this.mathCalculatorButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.mathCalculatorButton.FlatAppearance.BorderSize = 0;
            this.mathCalculatorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mathCalculatorButton.ImageIndex = 2;
            this.mathCalculatorButton.ImageList = this.imageList1;
            this.mathCalculatorButton.Location = new System.Drawing.Point(0, 160);
            this.mathCalculatorButton.Name = "mathCalculatorButton";
            this.mathCalculatorButton.Size = new System.Drawing.Size(45, 110);
            this.mathCalculatorButton.TabIndex = 6;
            this.toolTip.SetToolTip(this.mathCalculatorButton, "Математический калькулятор");
            this.mathCalculatorButton.UseVisualStyleBackColor = true;
            this.mathCalculatorButton.Visible = false;
            this.mathCalculatorButton.Click += new System.EventHandler(this.mathCalculatorButton_Click);
            // 
            // simpleCalculatorButton
            // 
            this.simpleCalculatorButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.simpleCalculatorButton.FlatAppearance.BorderSize = 0;
            this.simpleCalculatorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simpleCalculatorButton.ImageIndex = 0;
            this.simpleCalculatorButton.ImageList = this.imageList1;
            this.simpleCalculatorButton.Location = new System.Drawing.Point(0, 50);
            this.simpleCalculatorButton.Name = "simpleCalculatorButton";
            this.simpleCalculatorButton.Size = new System.Drawing.Size(45, 110);
            this.simpleCalculatorButton.TabIndex = 5;
            this.toolTip.SetToolTip(this.simpleCalculatorButton, "Обычный калькулятор");
            this.simpleCalculatorButton.UseVisualStyleBackColor = true;
            this.simpleCalculatorButton.Visible = false;
            this.simpleCalculatorButton.Click += new System.EventHandler(this.simpleCalculatorButton_Click);
            // 
            // buttonHamburger
            // 
            this.buttonHamburger.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonHamburger.FlatAppearance.BorderSize = 0;
            this.buttonHamburger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHamburger.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonHamburger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(255)))), ((int)(((byte)(247)))));
            this.buttonHamburger.Location = new System.Drawing.Point(0, 0);
            this.buttonHamburger.Name = "buttonHamburger";
            this.buttonHamburger.Size = new System.Drawing.Size(45, 50);
            this.buttonHamburger.TabIndex = 4;
            this.buttonHamburger.Text = "☰";
            this.buttonHamburger.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolTip
            // 
            this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.toolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(255)))), ((int)(((byte)(247)))));
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(50, 45);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(447, 618);
            this.panelMain.TabIndex = 0;
            // 
            // panelControl
            // 
            this.panelControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(83)))), ((int)(((byte)(92)))));
            this.panelControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControl.Controls.Add(this.buttonMinimie);
            this.panelControl.Controls.Add(this.buttonMaximize);
            this.panelControl.Controls.Add(this.buttonClose);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(5, 5);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(672, 40);
            this.panelControl.TabIndex = 2;
            this.panelControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelControl_MouseDown);
            // 
            // buttonMinimie
            // 
            this.buttonMinimie.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonMinimie.FlatAppearance.BorderSize = 0;
            this.buttonMinimie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimie.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonMinimie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(205)))), ((int)(((byte)(196)))));
            this.buttonMinimie.Location = new System.Drawing.Point(550, 0);
            this.buttonMinimie.Name = "buttonMinimie";
            this.buttonMinimie.Size = new System.Drawing.Size(40, 38);
            this.buttonMinimie.TabIndex = 9;
            this.buttonMinimie.TabStop = false;
            this.buttonMinimie.Text = "🗕";
            this.buttonMinimie.UseVisualStyleBackColor = true;
            this.buttonMinimie.Click += new System.EventHandler(this.buttonMinimie_Click);
            // 
            // buttonMaximize
            // 
            this.buttonMaximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonMaximize.FlatAppearance.BorderSize = 0;
            this.buttonMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMaximize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonMaximize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(109)))));
            this.buttonMaximize.Location = new System.Drawing.Point(590, 0);
            this.buttonMaximize.Name = "buttonMaximize";
            this.buttonMaximize.Size = new System.Drawing.Size(40, 38);
            this.buttonMaximize.TabIndex = 10;
            this.buttonMaximize.TabStop = false;
            this.buttonMaximize.Text = "🗖";
            this.buttonMaximize.UseVisualStyleBackColor = true;
            this.buttonMaximize.Click += new System.EventHandler(this.buttonMaximize_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.buttonClose.Location = new System.Drawing.Point(630, 0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 38);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "🗙";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // panelHistory
            // 
            this.panelHistory.Controls.Add(this.listBox1);
            this.panelHistory.Controls.Add(this.label1);
            this.panelHistory.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelHistory.Location = new System.Drawing.Point(497, 45);
            this.panelHistory.Name = "panelHistory";
            this.panelHistory.Size = new System.Drawing.Size(180, 618);
            this.panelHistory.TabIndex = 3;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.listBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(255)))), ((int)(((byte)(247)))));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 50;
            this.listBox1.Location = new System.Drawing.Point(0, 52);
            this.listBox1.Name = "listBox1";
            this.listBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listBox1.Size = new System.Drawing.Size(180, 566);
            this.listBox1.TabIndex = 1;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(144)))), ((int)(((byte)(144)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(109)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "История";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Калькулятор
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(682, 668);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHistory);
            this.Controls.Add(this.menuPanel);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(540, 535);
            this.Name = "Калькулятор";
            this.Opacity = 0.95D;
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Калькулятор";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Калькулятор_KeyPress);
            this.menuPanel.ResumeLayout(false);
            this.panelControl.ResumeLayout(false);
            this.panelHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button buttonHamburger;
        private System.Windows.Forms.Button trigCalculatorButton;
        private System.Windows.Forms.Button graphicCalculatorButton;
        private System.Windows.Forms.Button mathCalculatorButton;
        private System.Windows.Forms.Button simpleCalculatorButton;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button buttonMinimie;
        private System.Windows.Forms.Button buttonMaximize;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panelHistory;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox listBox1;
    }
}

