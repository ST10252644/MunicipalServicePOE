using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Timer = System.Windows.Forms.Timer;

namespace MunicipalServicesApp
{
    public partial class Form1 : Form
    {
        // Header panel and logo icon for the modern UI header
                                                                                                                    private Panel headerPanel;
        private IconPictureBox logoIcon;

        public Form1()
        {
            InitializeComponent();
            SetupModernUI();    // Set up the modern look and feel of the form
            SetupModernCards(); // Set up the main feature cards
        }

        // Configures the main form's appearance and behavior
        private void SetupModernUI()
        {
            this.FormBorderStyle = FormBorderStyle.None; // Remove default window border
            this.Size = new Size(1200, 750);             // Set form size
            this.StartPosition = FormStartPosition.CenterScreen; // Center on screen
            this.BackColor = Color.FromArgb(245, 247, 250);      // Light background

            this.MouseDown += Form_MouseDown; // Enable drag-to-move for borderless form

            CreateModernHeader();     // Add custom header
            CreateWindowControls();   // Add custom close/minimize buttons
            HideDesignerControls();   // Hide legacy designer controls
        }

        // Hides old labels from the designer to avoid UI clutter
        private void HideDesignerControls()
        {
            lblTitle.Visible = false;
            lblSubtitle.Visible = false;
            lblCard1.Visible = false;
            lblCard2.Visible = false;
            lblCard3.Visible = false;
            lblDesc1.Visible = false;
            lblDesc2.Visible = false;
            lblDesc3.Visible = false;
        }

        // Allows the user to drag the window by clicking anywhere on the form
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
        }

        // Creates a modern header with a logo, title, and subtitle
        private void CreateModernHeader()
        {
            headerPanel = new Panel
            {
                Size = new Size(this.Width, 120),
                Location = new Point(0, 0),
                BackColor = Color.White
            };

            // Draws a subtle shadow line at the bottom of the header
            headerPanel.Paint += (s, e) =>
            {
                using (Pen shadowPen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    e.Graphics.DrawLine(shadowPen, 0, headerPanel.Height - 1, headerPanel.Width, headerPanel.Height - 1);
                }
            };

            // Logo icon (FontAwesome)
            logoIcon = new IconPictureBox
            {
                IconChar = IconChar.City,
                IconColor = Color.FromArgb(52, 152, 219),
                IconSize = 60,
                Size = new Size(70, 70),
                Location = new Point(60, 25)
            };

            // Main title label
            Label titleLabel = new Label
            {
                Text = "Municipal Services Portal",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(140, 30)
            };

            // Subtitle label
            Label subtitleLabel = new Label
            {
                Text = "Your one-stop platform for community services",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(140, 90)
            };

            // Add controls to header and bring to front
            headerPanel.Controls.AddRange(new Control[] { logoIcon, titleLabel, subtitleLabel });
            this.Controls.Add(headerPanel);
            headerPanel.BringToFront();
        }

        // Adds custom close and minimize buttons to the form
        private void CreateWindowControls()
        {
            // Close button (FontAwesome icon)
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
            closeBtn.Click += (s, e) => Application.Exit(); // Exit app on click
            closeBtn.MouseEnter += (s, e) => closeBtn.IconColor = Color.White;
            closeBtn.MouseLeave += (s, e) => closeBtn.IconColor = Color.FromArgb(149, 165, 166);

            // Minimize button (FontAwesome icon)
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
            minBtn.Click += (s, e) => this.WindowState = FormWindowState.Minimized; // Minimize on click

            this.Controls.Add(closeBtn);
            this.Controls.Add(minBtn);
            closeBtn.BringToFront();
            minBtn.BringToFront();
        }

        // Sets up the three main feature cards in the center of the form
        private void SetupModernCards()
        {
            int cardWidth = 320;
            int cardHeight = 340;
            int spacing = 40;
            int startY = 250; // Position below header

            // Calculate horizontal center position for three cards
            int totalWidth = (cardWidth * 3) + (spacing * 2);
            int startX = (this.Width - totalWidth) / 2;

            // Position and size each card panel
            panel1.Location = new Point(startX, startY);
            panel1.Size = new Size(cardWidth, cardHeight);

            panel2.Location = new Point(startX + cardWidth + spacing, startY);
            panel2.Size = new Size(cardWidth, cardHeight);

            panel3.Location = new Point(startX + (cardWidth + spacing) * 2, startY);
            panel3.Size = new Size(cardWidth, cardHeight);

            // Create each card with icon, title, description, and button
            CreateEnhancedCard(panel1, Color.FromArgb(46, 204, 113), IconChar.FileAlt,
                "Report Issues", "Submit and track community problems and concerns", btnReportIssues, true);

            CreateEnhancedCard(panel2, Color.FromArgb(241, 196, 15), IconChar.CalendarAlt,
                "Local Events", "Discover upcoming community events and announcements", btnLocalEvents, true);

            CreateEnhancedCard(panel3, Color.FromArgb(155, 89, 182), IconChar.ChartLine,
                "Service Status", "Track your service requests and view updates", btnServiceStatus, true);
        }

        // Creates a styled card with icon, title, description, and button
        private void CreateEnhancedCard(Panel card, Color accentColor, IconChar icon,
            string title, string description, Button btn, bool isEnabled)
        {
            card.BackColor = Color.White;
            card.Paint += (s, e) => DrawModernCard(e.Graphics, card, accentColor);

            card.Controls.Clear(); // Remove any previous controls

            // Icon at the top of the card
            IconPictureBox iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconColor = accentColor,
                IconSize = 80,
                Size = new Size(90, 90),
                Location = new Point((card.Width - 90) / 2, 50),
                BackColor = Color.Transparent
            };

            // Card title
            Label titleLbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = false,
                Size = new Size(card.Width - 40, 50),
                Location = new Point(20, 160),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            // Card description
            Label descLbl = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = false,
                Size = new Size(card.Width - 40, 60),
                Location = new Point(20, 210),
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.Transparent
            };

            // Style the action button
            btn.Size = new Size(220, 55);
            btn.Location = new Point((card.Width - 220) / 2, 270);
            btn.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = isEnabled ? accentColor : Color.FromArgb(189, 195, 199);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = isEnabled ? Cursors.Hand : Cursors.No;
            btn.Text = isEnabled ? "Get Started →" : "Coming Soon";

            // Add hover effect for enabled buttons
            if (isEnabled)
            {
                Color originalColor = accentColor;
                btn.MouseEnter += (s, e) =>
                {
                    btn.BackColor = ControlPaint.Light(originalColor, 0.3f);
                };
                btn.MouseLeave += (s, e) =>
                {
                    btn.BackColor = originalColor;
                };
            }

            // Add all controls to the card
            card.Controls.AddRange(new Control[] { iconBox, titleLbl, descLbl, btn });

            // Add hover effect for the card (lift on hover)
            card.MouseEnter += (s, e) => EnhancedCard_MouseEnter(card);
            card.MouseLeave += (s, e) => EnhancedCard_MouseLeave(card);

            // Add hover effect for all child controls except the button
            foreach (Control ctrl in card.Controls)
            {
                if (ctrl != btn)
                {
                    ctrl.MouseEnter += (s, e) => EnhancedCard_MouseEnter(card);
                    ctrl.MouseLeave += (s, e) => EnhancedCard_MouseLeave(card);
                }
            }
        }

        // Draws the card with rounded corners, shadow, accent bar, and border
        private void DrawModernCard(Graphics g, Panel card, Color accentColor)
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

            // Draw accent bar at the top
            Rectangle accentRect = new Rectangle(0, 0, card.Width, 8);
            using (GraphicsPath accentPath = GetRoundedRectangle(accentRect, 20))
            using (SolidBrush accentBrush = new SolidBrush(accentColor))
            {
                g.FillPath(accentBrush, accentPath);
            }

            // Draw card border
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

        // Moves the card up slightly on mouse enter (hover effect)
        private void EnhancedCard_MouseEnter(Panel card)
        {
            card.Location = new Point(card.Location.X, card.Location.Y - 10);
            card.Invalidate();
        }

        // Moves the card back down on mouse leave
        private void EnhancedCard_MouseLeave(Panel card)
        {
            card.Location = new Point(card.Location.X, card.Location.Y + 10);
            card.Invalidate();
        }

        // Handles the click event for the "Report Issues" button
        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            AnimateButtonClick(btnReportIssues, () =>
            {
                ReportIssuesForm reportForm = new ReportIssuesForm();
                reportForm.Show();
                this.Hide();
            });
        }

        // Handles the click event for the "Local Events" button
        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            AnimateButtonClick(btnLocalEvents, () =>
            {
                LocalEventsForm eventsForm = new LocalEventsForm();
                eventsForm.Show();
                this.Hide();
            });
        }

        // Animates the button click with a color flash, then executes the callback
        private void AnimateButtonClick(Button btn, Action callback)
        {
            Color originalColor = btn.BackColor;
            btn.BackColor = ControlPaint.Dark(originalColor, 0.2f);

            Timer timer = new Timer { Interval = 150 };
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                btn.BackColor = originalColor;
                callback?.Invoke();
            };
            timer.Start();
        }

        // Handles the click event for the "Service Status" button
        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            AnimateButtonClick(btnServiceStatus, () =>
            {
                ServiceRequestStatusForm statusForm = new ServiceRequestStatusForm();
                statusForm.Show();
                this.Hide();
            });
        }

    }

    // Native methods for enabling drag-to-move on borderless forms
    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    }

}