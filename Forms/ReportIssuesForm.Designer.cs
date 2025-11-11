namespace MunicipalServicesApp
{
    partial class ReportIssuesForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblLocation = new Label();
            txtLocation = new TextBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblDescription = new Label();
            rtbDescription = new RichTextBox();
            btnAttach = new Button();
            lblAttachment = new Label();
            btnSubmit = new Button();
            lblEngagement = new Label();
            cardPanel = new Panel();
            lblProgress = new Label();
            progressBarCompletion = new ProgressBar();
            lblTitle = new Label();
            lblSubtitle = new Label();

            SuspendLayout();

            lblTitle.Text = "Report an Issue";
            lblTitle.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(50, 20);
            Controls.Add(lblTitle);

            lblSubtitle.Text = "Help us identify and resolve community issues";
            lblSubtitle.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            lblSubtitle.ForeColor = Color.FromArgb(127, 140, 141);
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(50, 70);
            Controls.Add(lblSubtitle);

            cardPanel.BackColor = Color.White;
            cardPanel.Size = new Size(800, 580);
            cardPanel.Location = new Point(50, 130);
            cardPanel.Padding = new Padding(30);
            cardPanel.Paint += (s, e) => DrawModernCard(e.Graphics, cardPanel);

            int y = 20;
            int spacing = 90;

            lblLocation.Text = "Location:";
            lblLocation.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblLocation.ForeColor = Color.FromArgb(46, 204, 113);
            lblLocation.AutoSize = true;
            lblLocation.Location = new Point(20, y);
            cardPanel.Controls.Add(lblLocation);

            txtLocation.PlaceholderText = "Enter the specific location of the issue";
            txtLocation.Font = new Font("Segoe UI", 11);
            txtLocation.BorderStyle = BorderStyle.FixedSingle;
            txtLocation.Size = new Size(480, 25);
            txtLocation.Location = new Point(20, y + 30);
            cardPanel.Controls.Add(txtLocation);

            y += spacing;
            lblCategory.Text = "Category:";
            lblCategory.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblCategory.ForeColor = Color.FromArgb(46, 204, 113);
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(20, y);
            cardPanel.Controls.Add(lblCategory);

            cmbCategory.Items.AddRange(new object[] { "Sanitation", "Roads", "Utilities", "Parks & Recreation", "Public Safety", "Other" });
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Font = new Font("Segoe UI", 11);
            cmbCategory.FlatStyle = FlatStyle.Flat;
            cmbCategory.Size = new Size(480, 35);
            cmbCategory.Location = new Point(20, y + 35);
            cmbCategory.BackColor = Color.White;
            cmbCategory.Paint += (s, e) =>
            {
                Rectangle rect = new Rectangle(0, 0, cmbCategory.Width - 1, cmbCategory.Height - 1);
                e.Graphics.DrawRectangle(Pens.Gray, rect);
            };
            cardPanel.Controls.Add(cmbCategory);

            y += spacing;
            lblDescription.Text = "Description:";
            lblDescription.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblDescription.ForeColor = Color.FromArgb(46, 204, 113);
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, y);
            cardPanel.Controls.Add(lblDescription);

            rtbDescription.Font = new Font("Segoe UI", 11);
            rtbDescription.BorderStyle = BorderStyle.FixedSingle;
            rtbDescription.Size = new Size(480, 150);
            rtbDescription.Location = new Point(20, y + 35);
            cardPanel.Controls.Add(rtbDescription);

            y += 180;
            btnAttach.Text = "Attach File";
            btnAttach.BackColor = Color.FromArgb(241, 196, 15);
            btnAttach.ForeColor = Color.White;
            btnAttach.FlatStyle = FlatStyle.Flat;
            btnAttach.FlatAppearance.BorderSize = 0;
            btnAttach.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAttach.Cursor = Cursors.Hand;
            btnAttach.Size = new Size(200, 50);
            btnAttach.Location = new Point(20, y);
            btnAttach.Click += btnAttach_Click;
            cardPanel.Controls.Add(btnAttach);

            lblAttachment.Text = "(No file selected)";
            lblAttachment.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblAttachment.ForeColor = Color.FromArgb(127, 140, 141);
            lblAttachment.AutoSize = true;
            lblAttachment.Location = new Point(230, y + 15);
            cardPanel.Controls.Add(lblAttachment);

            y += 90;
            lblProgress.Text = "Form Completion: 0%";
            lblProgress.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblProgress.ForeColor = Color.FromArgb(127, 140, 141);
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(20, y);
            cardPanel.Controls.Add(lblProgress);

            progressBarCompletion.Location = new Point(20, y + 25);
            progressBarCompletion.Size = new Size(480, 8);
            progressBarCompletion.ForeColor = Color.FromArgb(46, 204, 113);
            progressBarCompletion.Style = ProgressBarStyle.Continuous;
            cardPanel.Controls.Add(progressBarCompletion);

            y += 70;
            btnSubmit.Text = "Submit Issue Report";
            btnSubmit.BackColor = Color.FromArgb(46, 204, 113);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.Size = new Size(480, 50);
            btnSubmit.Location = new Point(20, y);
            btnSubmit.Click += btnSubmit_Click;
            cardPanel.Controls.Add(btnSubmit);

            y += 70;
            lblEngagement.Text = "Help us improve your community!";
            lblEngagement.Font = new Font("Segoe UI", 11, FontStyle.Italic);
            lblEngagement.ForeColor = Color.FromArgb(46, 204, 113);
            lblEngagement.AutoSize = true;
            lblEngagement.Location = new Point(20, y);
            cardPanel.Controls.Add(lblEngagement);

            ClientSize = new Size(900, 800);
            Controls.Add(cardPanel);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Report Issues - Municipal Services";

            ResumeLayout(false);
        }

        #endregion

        private Label lblLocation;
        private TextBox txtLocation;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblDescription;
        private RichTextBox rtbDescription;
        private Button btnAttach;
        private Label lblAttachment;
        private Button btnSubmit;
        private Label lblEngagement;
        private Panel cardPanel;
        private Label lblProgress;
        private ProgressBar progressBarCompletion;
        private Label lblTitle;
        private Label lblSubtitle;
    }
}
