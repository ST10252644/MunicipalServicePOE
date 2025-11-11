using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace MunicipalServicesApp
{
    public partial class ReportIssuesForm : Form
    {
        // Represents a reported issue
        public class Issue
        {
            public string Location { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public string AttachmentPath { get; set; }
            public DateTime ReportedDate { get; set; }
        }

        // Stores all reported issues
        public static List<Issue> issues = new List<Issue>();
        private string attachedFilePath = "";
        private Panel headerPanel;

        public ReportIssuesForm()
        {
            InitializeComponent();
            SetupModernUI();
            UpdateProgressBar();
        }

        // Sets up the modern UI for the form
        private void SetupModernUI()
        {
            // Form styling
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(950, 950);
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Enable drag for borderless form
            this.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    NativeMethods.ReleaseCapture();
                    NativeMethods.SendMessage(this.Handle, 0xA1, 0x2, 0);
                }
            };

            CreateModernHeader();
            StyleFormControls();
            SetupFieldEvents();
        }

        // Creates the modern header panel with icons and buttons
        private void CreateModernHeader()
        {
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 140,
                BackColor = Color.White
            };

            // Draws a bottom border line for the header
            headerPanel.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(220, 220, 220), 1),
                    0, headerPanel.Height - 1, headerPanel.Width, headerPanel.Height - 1);
            };

            // Icon for the header
            IconPictureBox titleIcon = new IconPictureBox
            {
                IconChar = IconChar.FileAlt,
                IconColor = Color.FromArgb(46, 204, 113),
                IconSize = 60,
                Size = new Size(70, 70),
                Location = new Point(60, 30)
            };

            // Main title label
            Label titleLabel = new Label
            {
                Text = "Report an Issue",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(140, 30)
            };

            // Subtitle label
            Label subtitleLabel = new Label
            {
                Text = "Help us identify and resolve community issues",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(140, 100)
            };

            // Close button (top right)
            IconButton closeBtn = new IconButton
            {
                IconChar = IconChar.Times,
                IconColor = Color.FromArgb(149, 165, 166),
                IconSize = 20,
                Size = new Size(45, 45),
                Location = new Point(this.Width - 55, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60);
            closeBtn.Click += (s, e) => Application.Exit();
            closeBtn.MouseEnter += (s, e) => closeBtn.IconColor = Color.White;
            closeBtn.MouseLeave += (s, e) => closeBtn.IconColor = Color.FromArgb(149, 165, 166);

            // Minimize button (top right)
            IconButton minBtn = new IconButton
            {
                IconChar = IconChar.WindowMinimize,
                IconColor = Color.FromArgb(149, 165, 166),
                IconSize = 20,
                Size = new Size(45, 45),
                Location = new Point(this.Width - 100, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            minBtn.FlatAppearance.BorderSize = 0;
            minBtn.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            // Back button (top right)
            IconButton backBtn = new IconButton
            {
                IconChar = IconChar.ArrowLeft,
                IconColor = Color.White,
                IconSize = 20,
                Text = " Back",
                TextAlign = ContentAlignment.MiddleRight,
                ImageAlign = ContentAlignment.MiddleLeft,
                Size = new Size(120, 45),
                Location = new Point(this.Width - 240, 38),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            backBtn.FlatAppearance.BorderSize = 0;
            backBtn.Click += btnBack_Click;

            // Add controls to header panel
            headerPanel.Controls.AddRange(new Control[] { titleIcon, titleLabel, subtitleLabel, backBtn, closeBtn, minBtn });
            this.Controls.Add(headerPanel);
            headerPanel.BringToFront();
        }

        // Styles and positions all form controls
        private void StyleFormControls()
        {
            // Position main card panel with better spacing
            cardPanel.Location = new Point(75, 160);
            cardPanel.Size = new Size(800, 700);
            cardPanel.BackColor = Color.White;
            cardPanel.Paint += (s, e) => DrawModernCard(e.Graphics, cardPanel);

            // Layout variables
            int leftMargin = 35;
            int rightMargin = 35;
            int controlWidth = cardPanel.Width - leftMargin - rightMargin - 20;
            int yPos = 30;
            int sectionSpacing = 75;

            // Location label and textbox
            lblLocation.Location = new Point(leftMargin, yPos);
            lblLocation.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblLocation.ForeColor = Color.FromArgb(46, 204, 113);

            txtLocation.Location = new Point(leftMargin, yPos + 35);
            txtLocation.Size = new Size(controlWidth, 32);
            txtLocation.Font = new Font("Segoe UI", 11);
            txtLocation.BorderStyle = BorderStyle.FixedSingle;

            // Category label and combobox
            yPos += sectionSpacing;
            lblCategory.Location = new Point(leftMargin, yPos);
            lblCategory.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblCategory.ForeColor = Color.FromArgb(46, 204, 113);

            cmbCategory.Location = new Point(leftMargin, yPos + 35);
            cmbCategory.Size = new Size(controlWidth, 32);
            cmbCategory.Font = new Font("Segoe UI", 11);
            cmbCategory.FlatStyle = FlatStyle.Flat;

            // Description label and richtextbox
            yPos += sectionSpacing + 15;
            lblDescription.Location = new Point(leftMargin, yPos);
            lblDescription.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblDescription.ForeColor = Color.FromArgb(46, 204, 113);

            rtbDescription.Location = new Point(leftMargin, yPos + 35);
            rtbDescription.Size = new Size(controlWidth, 160);
            rtbDescription.Font = new Font("Segoe UI", 11);
            rtbDescription.BorderStyle = BorderStyle.FixedSingle;

            // Attachment button and label
            yPos += 210;
            btnAttach.Location = new Point(leftMargin, yPos);
            btnAttach.Size = new Size(180, 50);
            btnAttach.BackColor = Color.FromArgb(241, 196, 15);
            btnAttach.ForeColor = Color.White;
            btnAttach.FlatStyle = FlatStyle.Flat;
            btnAttach.FlatAppearance.BorderSize = 0;
            btnAttach.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAttach.Cursor = Cursors.Hand;

            lblAttachment.Location = new Point(leftMargin + 190, yPos + 17);
            lblAttachment.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblAttachment.ForeColor = Color.FromArgb(127, 140, 141);
            lblAttachment.AutoSize = true;

            // Progress label and progress bar
            yPos += 70;
            lblProgress.Location = new Point(leftMargin, yPos);
            lblProgress.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblProgress.ForeColor = Color.FromArgb(127, 140, 141);

            progressBarCompletion.Location = new Point(leftMargin, yPos + 30);
            progressBarCompletion.Size = new Size(controlWidth, 10);
            progressBarCompletion.ForeColor = Color.FromArgb(46, 204, 113);

            // Submit button
            yPos += 60;
            btnSubmit.Location = new Point(leftMargin, yPos);
            btnSubmit.Size = new Size(controlWidth, 55);
            btnSubmit.BackColor = Color.FromArgb(46, 204, 113);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btnSubmit.Cursor = Cursors.Hand;

            // Engagement message label
            yPos += 80;
            lblEngagement.Location = new Point(leftMargin, yPos);
            lblEngagement.Size = new Size(controlWidth, 40);
            lblEngagement.Font = new Font("Segoe UI", 11, FontStyle.Italic);
            lblEngagement.ForeColor = Color.FromArgb(46, 204, 113);
            lblEngagement.TextAlign = ContentAlignment.MiddleCenter;

            // Add hover effects for buttons
            Color attachOriginal = btnAttach.BackColor;
            btnAttach.MouseEnter += (s, e) => btnAttach.BackColor = ControlPaint.Light(attachOriginal, 0.2f);
            btnAttach.MouseLeave += (s, e) => btnAttach.BackColor = attachOriginal;

            Color submitOriginal = btnSubmit.BackColor;
            btnSubmit.MouseEnter += (s, e) => btnSubmit.BackColor = ControlPaint.Light(submitOriginal, 0.2f);
            btnSubmit.MouseLeave += (s, e) => btnSubmit.BackColor = submitOriginal;
        }

        // Draws a modern card with shadow, accent, and border
        private void DrawModernCard(Graphics g, Panel card)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw shadow
            Rectangle shadowRect = new Rectangle(10, 10, card.Width - 20, card.Height - 20);
            using (GraphicsPath shadowPath = GetRoundedRectangle(shadowRect, 20))
            {
                using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                {
                    shadowBrush.CenterColor = Color.FromArgb(30, 0, 0, 0);
                    shadowBrush.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) };
                    g.FillPath(shadowBrush, shadowPath);
                }
            }

            // Draw card background
            Rectangle cardRect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
            using (GraphicsPath cardPath = GetRoundedRectangle(cardRect, 20))
            using (SolidBrush cardBrush = new SolidBrush(Color.White))
            {
                g.FillPath(cardBrush, cardPath);
            }

            // Draw top accent
            Rectangle accentRect = new Rectangle(0, 0, card.Width, 6);
            using (GraphicsPath accentPath = GetRoundedRectangle(accentRect, 20))
            using (SolidBrush accentBrush = new SolidBrush(Color.FromArgb(46, 204, 113)))
            {
                g.FillPath(accentBrush, accentPath);
            }

            // Draw border
            using (GraphicsPath borderPath = GetRoundedRectangle(cardRect, 20))
            using (Pen borderPen = new Pen(Color.FromArgb(230, 230, 230), 2))
            {
                g.DrawPath(borderPen, borderPath);
            }
        }

        // Returns a rounded rectangle path for drawing
        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Sets up events for form fields to update progress bar
        private void SetupFieldEvents()
        {
            txtLocation.TextChanged += (s, e) => UpdateProgressBar();
            cmbCategory.SelectedIndexChanged += (s, e) => UpdateProgressBar();
            rtbDescription.TextChanged += (s, e) => UpdateProgressBar();
        }

        // Updates the progress bar and label based on form completion
        private void UpdateProgressBar()
        {
            int completedFields = 0;
            int totalFields = 4;

            if (!string.IsNullOrWhiteSpace(txtLocation.Text) && txtLocation.Text.Trim().Length >= 3)
                completedFields++;

            if (cmbCategory.SelectedIndex >= 0)
                completedFields++;

            if (!string.IsNullOrWhiteSpace(rtbDescription.Text) && rtbDescription.Text.Trim().Length >= 10)
                completedFields++;

            if (!string.IsNullOrEmpty(attachedFilePath))
                completedFields++;

            progressBarCompletion.Maximum = totalFields;
            progressBarCompletion.Value = completedFields;

            int percentage = (int)((completedFields / (double)totalFields) * 100);
            lblProgress.Text = $"Form Completion: {percentage}%";

            if (percentage == 100)
                lblProgress.ForeColor = Color.FromArgb(46, 204, 113);
            else if (percentage >= 50)
                lblProgress.ForeColor = Color.FromArgb(241, 196, 15);
            else
                lblProgress.ForeColor = Color.FromArgb(127, 140, 141);
        }

        // Handles the attach button click event to select a file
        private void btnAttach_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif|Document Files|*.pdf;*.docx;*.doc|All Files|*.*";
                openFile.Title = "Select a file to attach";
                openFile.Multiselect = false;

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(openFile.FileName);
                    // Check file size (max 10MB)
                    if (fileInfo.Length > 10 * 1024 * 1024)
                    {
                        MessageBox.Show("File size cannot exceed 10MB. Please select a smaller file.",
                            "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    attachedFilePath = openFile.FileName;
                    string fileName = Path.GetFileName(attachedFilePath);
                    lblAttachment.Text = $"✓ Attached: {fileName}";
                    lblAttachment.ForeColor = Color.FromArgb(46, 204, 113);
                    UpdateProgressBar();
                }
            }
        }

        // Handles the submit button click event to validate and save the issue
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            Issue newIssue = new Issue
            {
                Location = txtLocation.Text.Trim(),
                Category = cmbCategory.SelectedItem.ToString(),
                Description = rtbDescription.Text.Trim(),
                AttachmentPath = string.IsNullOrEmpty(attachedFilePath) ? "None" : attachedFilePath,
                ReportedDate = DateTime.Now
            };

            issues.Add(newIssue);
            ShowSuccessMessage();
            ClearForm();
        }

        // Validates the form fields before submission
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                ShowValidationError("Please enter the location of the issue.");
                txtLocation.Focus();
                return false;
            }

            if (txtLocation.Text.Trim().Length < 3)
            {
                ShowValidationError("Location must be at least 3 characters long.");
                txtLocation.Focus();
                return false;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                ShowValidationError("Please select a category for your issue.");
                cmbCategory.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                ShowValidationError("Please provide a description of the issue.");
                rtbDescription.Focus();
                return false;
            }

            if (rtbDescription.Text.Trim().Length < 10)
            {
                ShowValidationError("Description must be at least 10 characters long for clarity.");
                rtbDescription.Focus();
                return false;
            }

            return true;
        }

        // Shows a validation error message
        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Please Complete All Fields",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Shows a success message after successful submission
        private void ShowSuccessMessage()
        {
            lblEngagement.Text = "✓ Thank you! Your report has been submitted successfully.";
            lblEngagement.ForeColor = Color.FromArgb(46, 204, 113);

            string issueNumber = $"#{issues.Count:D4}";
            MessageBox.Show($"Issue reported successfully!\n\nIssue Number: {issueNumber}\nDate: {DateTime.Now:MMM dd, yyyy HH:mm}\n\nOur team will review your report and take appropriate action.",
                "Submission Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Handles the back button click event to return to the main form
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 mainForm = new Form1();
            mainForm.Show();
        }

        // Clears all form fields and resets the UI
        private void ClearForm()
        {
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            rtbDescription.Clear();
            attachedFilePath = "";
            lblAttachment.Text = "(No file selected)";
            lblAttachment.ForeColor = Color.FromArgb(127, 140, 141);
            lblEngagement.Text = "Help us improve your community!";
            lblEngagement.ForeColor = Color.FromArgb(46, 204, 113);

            UpdateProgressBar();
            txtLocation.Focus();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}