using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class StartupForm : Form
    {
        // UI controls
        private PictureBox pictureLogo;
        private Button btnLogin;
        private Label lblWelcome;
        private Label lblSubtitle;
        private Panel contentPanel;

        public StartupForm()
        {
            InitializeComponent();
            SetupUI();
        }

        /// <summary>
        /// Sets up the UI elements and layout for the startup form.
        /// </summary>
        private void SetupUI()
        {
            // Form properties
            this.Text = "FixItSA - Municipal Services";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 800);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Attach custom background gradient paint event
            this.Paint += DrawBackgroundGradient;

            // Main content panel with rounded corners
            contentPanel = new Panel
            {
                Size = new Size(500, 650),
                BackColor = Color.White
            };

            // Center the content panel
            contentPanel.Location = new Point(
                (this.ClientSize.Width - contentPanel.Width) / 2,
                (this.ClientSize.Height - contentPanel.Height) / 2
            );

            // Draw rounded corners and shadow for the content panel
            contentPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = GetRoundedRectangle(contentPanel.ClientRectangle, 20))
                {
                    contentPanel.Region = new Region(path);
                    using (Pen shadowPen = new Pen(Color.FromArgb(30, 0, 0, 0), 10))
                    {
                        e.Graphics.DrawPath(shadowPen, path);
                    }
                }
            };
            this.Controls.Add(contentPanel);

            // Logo image (or placeholder if not found)
            pictureLogo = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(180, 180),
                Location = new Point((contentPanel.Width - 180) / 2, 60),
                BackColor = Color.Transparent
            };

            try
            {
                // Attempt to load logo image from file
                pictureLogo.Image = Image.FromFile(@"C:\Users\User\source\repos\MunicipalServicesApp\img\Yellow Black Illustrative Football Logo.png");
            }
            catch (Exception ex)
            {
                // Draw a placeholder logo if image not found
                Bitmap placeholder = new Bitmap(180, 180);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(Color.Transparent);
                    using (GraphicsPath circlePath = new GraphicsPath())
                    {
                        circlePath.AddEllipse(0, 0, 180, 180);
                        using (PathGradientBrush brush = new PathGradientBrush(circlePath))
                        {
                            brush.CenterColor = Color.FromArgb(33, 150, 243);
                            brush.SurroundColors = new Color[] { Color.FromArgb(25, 118, 210) };
                            g.FillEllipse(brush, 0, 0, 180, 180);
                        }
                    }
                    using (Font font = new Font("Segoe UI", 36, FontStyle.Bold))
                    using (SolidBrush textBrush = new SolidBrush(Color.White))
                    {
                        StringFormat sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString("FixIt", font, textBrush, new RectangleF(0, 0, 180, 180), sf);
                    }
                }
                pictureLogo.Image = placeholder;
            }

            contentPanel.Controls.Add(pictureLogo);

            // Welcome label
            lblWelcome = new Label
            {
                Text = "Welcome to FixItSA",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            lblWelcome.Location = new Point((contentPanel.Width - lblWelcome.Width) / 12, 260);
            contentPanel.Controls.Add(lblWelcome);

            // Subtitle label
            lblSubtitle = new Label
            {
                Text = "Your Local Municipal Service Portal",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 120, 140),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            lblSubtitle.Location = new Point((contentPanel.Width - lblSubtitle.Width) / 4, 330);
            contentPanel.Controls.Add(lblSubtitle);

            // Divider panel
            Panel divider = new Panel
            {
                Size = new Size(300, 2),
                BackColor = Color.FromArgb(220, 230, 240),
                Location = new Point((contentPanel.Width - 300) / 2, 360)
            };
            contentPanel.Controls.Add(divider);

            // "Get Started" login button
            btnLogin = new Button
            {
                Text = "Get Started",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(300, 55),
                Location = new Point((contentPanel.Width - 300) / 2, 400),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            // Button hover effect (enlarge and color change)
            btnLogin.MouseEnter += (s, e) =>
            {
                btnLogin.BackColor = Color.FromArgb(25, 118, 210);
                btnLogin.Size = new Size(305, 58);
                btnLogin.Location = new Point((contentPanel.Width - 305) / 2, 398);
            };
            btnLogin.MouseLeave += (s, e) =>
            {
                btnLogin.BackColor = Color.FromArgb(33, 150, 243);
                btnLogin.Size = new Size(300, 55);
                btnLogin.Location = new Point((contentPanel.Width - 300) / 2, 400);
            };

            contentPanel.Controls.Add(btnLogin);

            // Description label
            Label lblDescription = new Label
            {
                Text = "Report issues, track events, and stay connected\nwith your community services",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(120, 140, 160),
                AutoSize = true,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblDescription.Location = new Point((contentPanel.Width - lblDescription.Width) / 6, 480);
            contentPanel.Controls.Add(lblDescription);

            // Footer label
            Label lblFooter = new Label
            {
                Text = "Powered by FixItSA",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.FromArgb(160, 180, 200),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            lblFooter.Location = new Point((contentPanel.Width - lblFooter.Width) / 2, 570);
            contentPanel.Controls.Add(lblFooter);
        }

        /// <summary>
        /// Paints a vertical gradient and decorative circles as the form background.
        /// </summary>
        private void DrawBackgroundGradient(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // Draw vertical gradient
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(240, 244, 248),
                Color.FromArgb(255, 255, 255),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            // Draw large faint blue circles for decoration
            using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(8, 33, 150, 243)))
            {
                e.Graphics.FillEllipse(circleBrush, -150, -150, 500, 500);
                e.Graphics.FillEllipse(circleBrush, this.ClientSize.Width - 350, this.ClientSize.Height - 350, 500, 500);
            }
            using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(5, 52, 152, 219)))
            {
                e.Graphics.FillEllipse(circleBrush, this.ClientSize.Width - 200, -100, 400, 400);
                e.Graphics.FillEllipse(circleBrush, -100, this.ClientSize.Height - 250, 400, 400);
            }
        }

        /// <summary>
        /// Returns a GraphicsPath representing a rounded rectangle.
        /// </summary>
        private GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Handles the Get Started button click event. Opens the main form and hides the startup form.
        /// </summary>
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Ensures the application exits when the startup form is closed.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }
    }
}