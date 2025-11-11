namespace MunicipalServicesApp
{
    partial class Form1
    {
        // Container for designer components
        private System.ComponentModel.IContainer components = null;

        // Dispose resources used by the form
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // Method to initialize and configure UI components
        private void InitializeComponent()
        {
            btnReportIssues = new Button();
            btnLocalEvents = new Button();
            btnServiceStatus = new Button();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblCard1 = new Label();
            lblCard2 = new Label();
            lblCard3 = new Label();
            lblDesc1 = new Label();
            lblDesc2 = new Label();
            lblDesc3 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // btnReportIssues
            // 
            btnReportIssues.BackColor = Color.FromArgb(56, 142, 60);
            btnReportIssues.Cursor = Cursors.Hand;
            btnReportIssues.FlatAppearance.BorderSize = 0;
            btnReportIssues.FlatAppearance.MouseDownBackColor = Color.FromArgb(46, 125, 50);
            btnReportIssues.FlatAppearance.MouseOverBackColor = Color.FromArgb(67, 160, 71);
            btnReportIssues.FlatStyle = FlatStyle.Flat;
            btnReportIssues.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnReportIssues.ForeColor = Color.White;
            btnReportIssues.Location = new Point(23, 107);
            btnReportIssues.Margin = new Padding(3, 4, 3, 4);
            btnReportIssues.Name = "btnReportIssues";
            btnReportIssues.Size = new Size(229, 60);
            btnReportIssues.TabIndex = 0;
            btnReportIssues.Text = "Get Started";
            btnReportIssues.UseVisualStyleBackColor = false;
            btnReportIssues.Click += btnReportIssues_Click;
            // 
            // btnLocalEvents
            // 
            btnLocalEvents.BackColor = Color.FromArgb(255, 152, 0);
            btnLocalEvents.Cursor = Cursors.Hand;
            btnLocalEvents.FlatAppearance.BorderSize = 0;
            btnLocalEvents.FlatAppearance.MouseDownBackColor = Color.FromArgb(245, 124, 0);
            btnLocalEvents.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 167, 38);
            btnLocalEvents.FlatStyle = FlatStyle.Flat;
            btnLocalEvents.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLocalEvents.ForeColor = Color.White;
            btnLocalEvents.Location = new Point(23, 107);
            btnLocalEvents.Margin = new Padding(3, 4, 3, 4);
            btnLocalEvents.Name = "btnLocalEvents";
            btnLocalEvents.Size = new Size(229, 60);
            btnLocalEvents.TabIndex = 1;
            btnLocalEvents.Text = "View Events";
            btnLocalEvents.UseVisualStyleBackColor = false;
            btnLocalEvents.Click += btnLocalEvents_Click;
            // 
            // btnServiceStatus
            // 
            btnServiceStatus.BackColor = Color.FromArgb(155, 89, 182);
            btnServiceStatus.Cursor = Cursors.Hand;
            btnServiceStatus.Enabled = true;
            btnServiceStatus.FlatAppearance.BorderSize = 0;
            btnServiceStatus.FlatAppearance.MouseDownBackColor = Color.FromArgb(142, 68, 173);
            btnServiceStatus.FlatAppearance.MouseOverBackColor = Color.FromArgb(171, 108, 204);
            btnServiceStatus.FlatStyle = FlatStyle.Flat;
            btnServiceStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnServiceStatus.ForeColor = Color.White;
            btnServiceStatus.Location = new Point(23, 107);
            btnServiceStatus.Margin = new Padding(3, 4, 3, 4);
            btnServiceStatus.Name = "btnServiceStatus";
            btnServiceStatus.Size = new Size(229, 60);
            btnServiceStatus.TabIndex = 2;
            btnServiceStatus.Text = "Get Started";
            btnServiceStatus.UseVisualStyleBackColor = false;
            btnServiceStatus.Click += btnServiceStatus_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle.Location = new Point(114, 133);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(667, 72);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "Municipal Services Portal";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 14F);
            lblSubtitle.ForeColor = Color.FromArgb(108, 117, 125);
            lblSubtitle.Location = new Point(114, 225);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(518, 32);
            lblSubtitle.TabIndex = 4;
            lblSubtitle.Text = "Your one-stop platform for community services";
            // 
            // lblCard1
            // 
            lblCard1.AutoSize = true;
            lblCard1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCard1.ForeColor = Color.FromArgb(56, 142, 60);
            lblCard1.Location = new Point(23, 27);
            lblCard1.Name = "lblCard1";
            lblCard1.Size = new Size(168, 32);
            lblCard1.TabIndex = 1;
            lblCard1.Text = "Report Issues";
            // 
            // lblCard2
            // 
            lblCard2.AutoSize = true;
            lblCard2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCard2.ForeColor = Color.FromArgb(255, 152, 0);
            lblCard2.Location = new Point(23, 27);
            lblCard2.Name = "lblCard2";
            lblCard2.Size = new Size(154, 32);
            lblCard2.TabIndex = 2;
            lblCard2.Text = "Local Events";
            // 
            // lblCard3
            // 
            lblCard3.AutoSize = true;
            lblCard3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCard3.ForeColor = Color.FromArgb(156, 39, 176);
            lblCard3.Location = new Point(23, 27);
            lblCard3.Name = "lblCard3";
            lblCard3.Size = new Size(172, 32);
            lblCard3.TabIndex = 3;
            lblCard3.Text = "Service Status";
            // 
            // lblDesc1
            // 
            lblDesc1.Font = new Font("Segoe UI", 9F);
            lblDesc1.ForeColor = Color.FromArgb(108, 117, 125);
            lblDesc1.Location = new Point(23, 67);
            lblDesc1.Name = "lblDesc1";
            lblDesc1.Size = new Size(183, 33);
            lblDesc1.TabIndex = 2;
            lblDesc1.Text = "Report community issues and problems";
            // 
            // lblDesc2
            // 
            lblDesc2.Font = new Font("Segoe UI", 9F);
            lblDesc2.ForeColor = Color.FromArgb(108, 117, 125);
            lblDesc2.Location = new Point(23, 67);
            lblDesc2.Name = "lblDesc2";
            lblDesc2.Size = new Size(183, 33);
            lblDesc2.TabIndex = 3;
            lblDesc2.Text = "View upcoming events and announcements";
            // 
            // lblDesc3
            // 
            lblDesc3.Font = new Font("Segoe UI", 9F);
            lblDesc3.ForeColor = Color.FromArgb(108, 117, 125);
            lblDesc3.Location = new Point(23, 67);
            lblDesc3.Name = "lblDesc3";
            lblDesc3.Size = new Size(183, 33);
            lblDesc3.TabIndex = 4;
            lblDesc3.Text = "Track your service requests and updates";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(lblDesc1);
            panel1.Controls.Add(lblCard1);
            panel1.Controls.Add(btnReportIssues);
            panel1.Location = new Point(114, 333);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(229, 187);
            panel1.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(lblDesc2);
            panel2.Controls.Add(lblCard2);
            panel2.Controls.Add(btnLocalEvents);
            panel2.Location = new Point(434, 333);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(229, 187);
            panel2.TabIndex = 6;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(lblDesc3);
            panel3.Controls.Add(lblCard3);
            panel3.Controls.Add(btnServiceStatus);
            panel3.Location = new Point(754, 333);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(229, 187);
            panel3.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1129, 567);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Municipal Services Portal";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();


        }

        #endregion

        // Designer fields for UI controls
        private Button btnReportIssues;
        private Button btnLocalEvents;
        private Button btnServiceStatus;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label lblCard1;
        private Label lblCard2;
        private Label lblCard3;
        private Label lblDesc1;
        private Label lblDesc2;
        private Label lblDesc3;
    }
}