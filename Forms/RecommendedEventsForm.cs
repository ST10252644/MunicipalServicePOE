using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace MunicipalServicesApp
{
    public partial class RecommendedEventsForm : Form
    {
        // UI elements
        private Panel headerPanel;
        private FlowLayoutPanel cardsContainer;
        // List of recommended events to display
        private List<LocalEventsForm.EventInfo> recommendedEvents;
        // Category to emoji mapping
        private Dictionary<string, string> categoryImages;
        // Category to color mapping
        private Dictionary<string, Color> categoryColors;
        // Reference to parent form for navigation
        private Form parentForm; 

        // Default constructor
        public RecommendedEventsForm()
        {
            InitializeComponent2();
            this.recommendedEvents = new List<LocalEventsForm.EventInfo>();
            SetupModernUI();
            InitializeCategoryAssets();
            DisplayEventCards();
        }

        // Constructor with events list
        public RecommendedEventsForm(List<LocalEventsForm.EventInfo> events)
        {
            InitializeComponent2();
            this.recommendedEvents = events ?? new List<LocalEventsForm.EventInfo>();
            SetupModernUI();
            InitializeCategoryAssets();
            DisplayEventCards();
        }
                                                                
        // Constructor with events list and parent form
        public RecommendedEventsForm(List<LocalEventsForm.EventInfo> events, Form parent)
        {
            InitializeComponent2();
            this.recommendedEvents = events ?? new List<LocalEventsForm.EventInfo>();
            this.parentForm = parent; 
            SetupModernUI();
            InitializeCategoryAssets();
            DisplayEventCards();
        }

        // Basic form initialization
        private void InitializeComponent2()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(1200, 800);
            this.Name = "RecommendedEventsForm";
            this.Text = "Recommended Events";
            this.ResumeLayout(false);
        }

        // Sets up the modern UI look and feel
        private void SetupModernUI()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(1200, 800);
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Allow dragging the form by mouse
            this.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    NativeMethods.ReleaseCapture();
                    NativeMethods.SendMessage(this.Handle, 0xA1, 0x2, 0);
                }
            };

            CreateModernHeader();
            CreateCardsContainer();
        }

        // Initializes category emoji and color dictionaries
        private void InitializeCategoryAssets()
        {
            categoryImages = new Dictionary<string, string>
            {
                { "Community", "👥" },
                { "Roads", "🛣️" },
                { "Utilities", "💡" },
                { "Public Safety", "🚨" },
                { "Health", "⚕️" },
                { "Sports", "⚽" },
                { "Environment", "🌱" },
                { "Economy", "💼" }
            };

            categoryColors = new Dictionary<string, Color>
            {
                { "Community", Color.FromArgb(52, 152, 219) },
                { "Roads", Color.FromArgb(230, 126, 34) },
                { "Utilities", Color.FromArgb(241, 196, 15) },
                { "Public Safety", Color.FromArgb(231, 76, 60) },
                { "Health", Color.FromArgb(26, 188, 156) },
                { "Sports", Color.FromArgb(155, 89, 182) },
                { "Environment", Color.FromArgb(46, 204, 113) },
                { "Economy", Color.FromArgb(52, 73, 94) }
            };
        }

        // Creates the header panel with navigation and title
        private void CreateModernHeader()
        {
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White
            };

            // Draws a bottom border line
            headerPanel.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(220, 220, 220), 1),
                    0, headerPanel.Height - 1, headerPanel.Width, headerPanel.Height - 1);
            };

            // Back button
            IconButton backBtn = new IconButton
            {
                IconChar = IconChar.ArrowLeft,
                IconColor = Color.White,
                IconSize = 24,
                Text = " Back",
                TextAlign = ContentAlignment.MiddleRight,
                ImageAlign = ContentAlignment.MiddleLeft,
                Size = new Size(140, 50),
                Location = new Point(30, 25),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            backBtn.FlatAppearance.BorderSize = 0;
            backBtn.Click += BackBtn_Click;

            // Title icon
            IconPictureBox titleIcon = new IconPictureBox
            {
                IconChar = IconChar.Star,
                IconColor = Color.FromArgb(241, 196, 15),
                IconSize = 50,
                Size = new Size(60, 60),
                Location = new Point(this.Width / 2 - 250, 20)
            };

            // Title label
            Label titleLbl = new Label
            {
                Text = "Recommended Events for You",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(this.Width / 2 - 180, 32)
            };

            // Close button
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
            closeBtn.Click += (s, e) => this.Close();
            closeBtn.MouseEnter += (s, e) => closeBtn.IconColor = Color.White;
            closeBtn.MouseLeave += (s, e) => closeBtn.IconColor = Color.FromArgb(149, 165, 166);

            // Minimize button
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

            headerPanel.Controls.AddRange(new Control[] { backBtn, titleIcon, titleLbl, closeBtn, minBtn });
            this.Controls.Add(headerPanel);
        }

        // Handles the back button click event
        private void BackBtn_Click(object sender, EventArgs e)
        {
            if (parentForm != null)
            {
                parentForm.Show();
                this.Close();
            }
            else
            {
                // Try to show LocalEventsForm if open
                foreach (Form form in Application.OpenForms)
                {
                    if (form is LocalEventsForm)
                    {
                        form.Show();
                        this.Close();
                        return;
                    }
                }
                this.Close();
            }
        }

        // Creates the container for event cards
        private void CreateCardsContainer()
        {
            cardsContainer = new FlowLayoutPanel
            {
                Location = new Point(30, 120),
                Size = new Size(1140, 650),
                BackColor = Color.Transparent,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            this.Controls.Add(cardsContainer);
        }

        // Displays event cards or an empty message
        private void DisplayEventCards()
        {
            cardsContainer.Controls.Clear();

            if (!recommendedEvents.Any())
            {
                // Show message if no events
                Panel emptyPanel = new Panel
                {
                    Size = new Size(1100, 200),
                    BackColor = Color.White
                };

                Label emptyLabel = new Label
                {
                    Text = "No recommended events at this time.\nStart searching and viewing events to get personalized recommendations!",
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    ForeColor = Color.FromArgb(127, 140, 141),
                    AutoSize = false,
                    Size = new Size(800, 100),
                    Location = new Point(150, 50),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                emptyPanel.Controls.Add(emptyLabel);
                cardsContainer.Controls.Add(emptyPanel);
                return;
            }

            // Add a card for each event
            foreach (var ev in recommendedEvents)
            {
                Panel card = CreateEventCard(ev);
                cardsContainer.Controls.Add(card);
            }
        }

        // Creates a single event card panel
        private Panel CreateEventCard(LocalEventsForm.EventInfo ev)
        {
            Panel card = new Panel
            {
                Size = new Size(340, 420),
                BackColor = Color.White,
                Margin = new Padding(15),
                Cursor = Cursors.Hand
            };

            // Draws card shadow and border
            card.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Shadow
                Rectangle shadowRect = new Rectangle(5, 5, card.Width - 10, card.Height - 10);
                using (GraphicsPath shadowPath = GetRoundedRectangle(shadowRect, 15))
                {
                    using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                    {
                        shadowBrush.CenterColor = Color.FromArgb(30, 0, 0, 0);
                        shadowBrush.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) };
                        g.FillPath(shadowBrush, shadowPath);
                    }
                }

                // Card background and border
                Rectangle cardRect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using (GraphicsPath cardPath = GetRoundedRectangle(cardRect, 15))
                {
                    using (SolidBrush cardBrush = new SolidBrush(Color.White))
                    {
                        g.FillPath(cardBrush, cardPath);
                    }
                    using (Pen borderPen = new Pen(Color.FromArgb(230, 230, 230), 2))
                    {
                        g.DrawPath(borderPen, cardPath);
                    }
                }
            };

            // Top image/emoji panel
            Panel imagePanel = new Panel
            {
                Size = new Size(340, 200),
                Location = new Point(0, 0),
                BackColor = GetCategoryColor(ev.Category)
            };

            // Draws rounded top for image panel
            imagePanel.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, imagePanel.Width, imagePanel.Height);
                using (GraphicsPath path = GetRoundedRectangleTop(rect, 15))
                {
                    using (SolidBrush brush = new SolidBrush(GetCategoryColor(ev.Category)))
                    {
                        g.FillPath(brush, path);
                    }
                }
            };

            // Emoji label
            Label emojiLabel = new Label
            {
                Text = GetCategoryEmoji(ev.Category),
                Font = new Font("Segoe UI", 72, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(340, 140),
                Location = new Point(0, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            imagePanel.Controls.Add(emojiLabel);

            // Priority badge
            string priorityText = ev.Priority == 1 ? "HIGH PRIORITY" :
                                 ev.Priority == 2 ? "MEDIUM" : "LOW";
            Color priorityColor = ev.Priority == 1 ? Color.FromArgb(231, 76, 60) :
                                 ev.Priority == 2 ? Color.FromArgb(241, 196, 15) :
                                 Color.FromArgb(46, 204, 113);

            Label priorityBadge = new Label
            {
                Text = priorityText,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = priorityColor,
                AutoSize = false,
                Size = new Size(120, 28),
                Location = new Point(15, 15),
                TextAlign = ContentAlignment.MiddleCenter
            };
            // Draws rounded badge
            priorityBadge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = GetRoundedRectangle(new Rectangle(0, 0, priorityBadge.Width - 1, priorityBadge.Height - 1), 5))
                {
                    using (SolidBrush brush = new SolidBrush(priorityColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };
            imagePanel.Controls.Add(priorityBadge);

            card.Controls.Add(imagePanel);

            // Category label
            Label categoryLabel = new Label
            {
                Text = ev.Category.ToUpper(),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = GetCategoryColor(ev.Category),
                AutoSize = true,
                Location = new Point(20, 220)
            };
            card.Controls.Add(categoryLabel);

            // Event title
            Label titleLabel = new Label
            {
                Text = ev.Title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = false,
                Size = new Size(300, 60),
                Location = new Point(20, 245),
                TextAlign = ContentAlignment.TopLeft
            };
            card.Controls.Add(titleLabel);

            // Date icon
            IconPictureBox dateIcon = new IconPictureBox
            {
                IconChar = IconChar.Calendar,
                IconColor = Color.FromArgb(127, 140, 141),
                IconSize = 18,
                Size = new Size(24, 24),
                Location = new Point(20, 320)
            };
            card.Controls.Add(dateIcon);

            // Date label
            Label dateLabel = new Label
            {
                Text = ev.Date.ToString("MMMM dd, yyyy"),
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(50, 322)
            };
            card.Controls.Add(dateLabel);

            // Time until event label
            TimeSpan timeUntil = ev.Date - DateTime.Today;
            string timeText = timeUntil.Days == 0 ? "Today" :
                            timeUntil.Days == 1 ? "Tomorrow" :
                            $"In {timeUntil.Days} days";

            Label timeLabel = new Label
            {
                Text = timeText,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(52, 152, 219),
                AutoSize = true,
                Location = new Point(50, 348)
            };
            card.Controls.Add(timeLabel);

            // View details button
            Button viewBtn = new Button
            {
                Text = "View Details",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = GetCategoryColor(ev.Category),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(300, 45),
                Location = new Point(20, 360),
                Cursor = Cursors.Hand
            };
            viewBtn.FlatAppearance.BorderSize = 0;
            viewBtn.Click += (s, e) => ShowEventDetails(ev);

            // Button hover effect
            Color btnOriginal = viewBtn.BackColor;
            viewBtn.MouseEnter += (s, e) => viewBtn.BackColor = ControlPaint.Light(btnOriginal, 0.2f);
            viewBtn.MouseLeave += (s, e) => viewBtn.BackColor = btnOriginal;

            card.Controls.Add(viewBtn);

            // Card hover effect
            card.MouseEnter += (s, e) =>
            {
                card.BackColor = Color.FromArgb(248, 249, 250);
            };
            card.MouseLeave += (s, e) =>
            {
                card.BackColor = Color.White;
            };

            // Click events for card and image
            card.Click += (s, e) => ShowEventDetails(ev);
            imagePanel.Click += (s, e) => ShowEventDetails(ev);
            emojiLabel.Click += (s, e) => ShowEventDetails(ev);

            return card;
        }

        // Shows a message box with event details
        private void ShowEventDetails(LocalEventsForm.EventInfo ev)
        {
            string priorityText = ev.Priority == 1 ? "HIGH PRIORITY" :
                                 ev.Priority == 2 ? "MEDIUM PRIORITY" : "LOW PRIORITY";

            MessageBox.Show(
                $"{ev.Title}\n\n" +
                $"Date: {ev.Date:dddd, MMMM dd, yyyy}\n" +
                $"Category: {ev.Category}\n" +
                $"Priority: {priorityText}\n\n" +
                $"Description:\n{ev.Description}",
                "Event Details",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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

        // Returns a rounded rectangle path for the top corners only
        private GraphicsPath GetRoundedRectangleTop(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
            path.CloseFigure();
            return path;
        }

        // Gets the color for a given category
        private Color GetCategoryColor(string category)
        {
            return categoryColors.ContainsKey(category)
                ? categoryColors[category]
                : Color.FromArgb(52, 73, 94);
        }

        // Gets the emoji for a given category
        private string GetCategoryEmoji(string category)
        {
            return categoryImages.ContainsKey(category)
                ? categoryImages[category]
                : "📅";
        }
    }
}