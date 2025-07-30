namespace DetectorExample
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            appsListbox = new ListBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            searchAppsBtn = new Button();
            autoScanTimer = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // appsListbox
            // 
            appsListbox.Dock = DockStyle.Fill;
            appsListbox.FormattingEnabled = true;
            appsListbox.ItemHeight = 15;
            appsListbox.Location = new Point(3, 3);
            appsListbox.Name = "appsListbox";
            appsListbox.Size = new Size(794, 388);
            appsListbox.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(appsListbox, 0, 0);
            tableLayoutPanel1.Controls.Add(searchAppsBtn, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 87.55556F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.4444447F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // searchAppsBtn
            // 
            searchAppsBtn.Dock = DockStyle.Fill;
            searchAppsBtn.Location = new Point(3, 397);
            searchAppsBtn.Name = "searchAppsBtn";
            searchAppsBtn.Size = new Size(794, 50);
            searchAppsBtn.TabIndex = 1;
            searchAppsBtn.Text = "Search for fullscreen apps";
            searchAppsBtn.UseVisualStyleBackColor = true;
            searchAppsBtn.Click += searchAppsBtn_Click;
            // 
            // autoScanTimer
            // 
            autoScanTimer.Enabled = true;
            autoScanTimer.Interval = 1000;
            autoScanTimer.Tick += autoScanTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Fullscreen Detector Library Example Use";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox appsListbox;
        private TableLayoutPanel tableLayoutPanel1;
        private Button searchAppsBtn;
        private System.Windows.Forms.Timer autoScanTimer;
    }
}
