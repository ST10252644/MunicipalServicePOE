using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace MunicipalServicesApp
{
    /// <summary>
    /// Service Request Status Form - Part 3
    /// Implements BST, Graph, and MinHeap data structures for request management
    /// </summary>
    public partial class ServiceRequestStatusForm : Form
    {
        // Advanced Data Structures - Part 3
        private ServiceRequestBST requestBST;           // Binary Search Tree for O(log n) search
        private ServiceRequestGraph dependencyGraph;     // Graph for dependency management
        private ServiceRequestMinHeap priorityHeap;      // MinHeap for priority queue

        // UI Components
        private Panel headerPanel;
        private Label titleLabel;
        private IconButton btnBack;
        private IconButton btnRefresh;

        private Panel searchPanel;
        private TextBox txtSearchId;
        private IconButton btnSearch;
        private ComboBox cmbFilterStatus;
        private ComboBox cmbFilterPriority;
        private IconButton btnClearFilters;

        private DataGridView dgvRequests;
        private Panel detailsPanel;
        private RichTextBox rtbDetails;

        private Panel statsPanel;
        private Label lblTotalRequests;
        private Label lblPending;
        private Label lblInProgress;
        private Label lblCompleted;

        private Panel dependencyPanel;
        private TreeView tvDependencies;

        public ServiceRequestStatusForm()
        {
            InitializeComponent();
            InitializeDataStructures();
            SetupUI();
            LoadSampleData();
            RefreshDisplay();
        }

        /// <summary>
        /// Initialize all data structures
        /// </summary>
        private void InitializeDataStructures()
        {
            requestBST = new ServiceRequestBST();
            dependencyGraph = new ServiceRequestGraph();
            priorityHeap = new ServiceRequestMinHeap();
        }

        /// <summary>
        /// Setup the complete UI
        /// </summary>
        private void SetupUI()
        {
            // Form properties
            this.Text = "Service Request Status Tracker";
            this.Size = new Size(1500, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Modern rounded corners
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            SetupHeaderPanel();
            SetupSearchPanel();
            SetupDataGrid();
            SetupDetailsPanel();
            SetupStatsPanel();
            SetupDependencyPanel();
        }

        private void SetupHeaderPanel()
        {
            headerPanel = new Panel
            {
                Size = new Size(this.ClientSize.Width, 90),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(26, 115, 232) // Modern Blue
            };

            titleLabel = new Label
            {
                Text = "🔍 Service Request Status Tracker",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(80, 28)
            };

            btnBack = new IconButton
            {
                IconChar = IconChar.ArrowLeft,
                IconColor = Color.White,
                IconSize = 28,
                Size = new Size(55, 55),
                Location = new Point(15, 18),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(34, 139, 230),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;

            btnRefresh = new IconButton
            {
                IconChar = IconChar.SyncAlt,
                IconColor = Color.White,
                IconSize = 28,
                Size = new Size(55, 55),
                Location = new Point(this.ClientSize.Width - 80, 18),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(34, 139, 230),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(btnBack);
            headerPanel.Controls.Add(btnRefresh);
            this.Controls.Add(headerPanel);
        }

        private void SetupSearchPanel()
        {
            searchPanel = new Panel
            {
                Size = new Size(this.ClientSize.Width - 40, 90),
                Location = new Point(20, headerPanel.Bottom + 20),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            searchPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, searchPanel.Width - 1, searchPanel.Height - 1);
                }
            };

            Label lblSearch = new Label
            {
                Text = "🔎 Search Request ID:",
                Location = new Point(15, 18),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            txtSearchId = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point(15, 45),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            btnSearch = new IconButton
            {
                Text = " Search",
                IconChar = IconChar.Search,
                IconColor = Color.White,
                IconSize = 20,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Size = new Size(120, 35),
                Location = new Point(220, 42),
                BackColor = Color.FromArgb(26, 115, 232),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;

            Label lblStatus = new Label
            {
                Text = "📊 Status Filter:",
                Location = new Point(370, 18),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            cmbFilterStatus = new ComboBox
            {
                Size = new Size(160, 30),
                Location = new Point(370, 45),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cmbFilterStatus.Items.AddRange(new object[] { "All", "Pending", "InProgress", "Completed", "Rejected" });
            cmbFilterStatus.SelectedIndex = 0;
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;

            Label lblPriority = new Label
            {
                Text = "⚡ Priority Filter:",
                Location = new Point(550, 18),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            cmbFilterPriority = new ComboBox
            {
                Size = new Size(150, 30),
                Location = new Point(550, 45),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cmbFilterPriority.Items.AddRange(new object[] { "All", "HIGH", "MEDIUM", "LOW" });
            cmbFilterPriority.SelectedIndex = 0;
            cmbFilterPriority.SelectedIndexChanged += FilterChanged;

            btnClearFilters = new IconButton
            {
                Text = " Clear",
                IconChar = IconChar.Eraser,
                IconColor = Color.White,
                IconSize = 20,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Size = new Size(110, 35),
                Location = new Point(720, 42),
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnClearFilters.FlatAppearance.BorderSize = 0;
            btnClearFilters.Click += BtnClearFilters_Click;

            searchPanel.Controls.AddRange(new Control[] {
                lblSearch, txtSearchId, btnSearch,
                lblStatus, cmbFilterStatus,
                lblPriority, cmbFilterPriority,
                btnClearFilters
            });

            this.Controls.Add(searchPanel);
        }

        private void SetupDataGrid()
        {
            dgvRequests = new DataGridView
            {
                Size = new Size(920, 460),
                Location = new Point(20, searchPanel.Bottom + 20),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 45,
                RowTemplate = { Height = 35 },
                EnableHeadersVisualStyles = false
            };

            // Modern header styling
            dgvRequests.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 115, 232);
            dgvRequests.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRequests.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvRequests.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvRequests.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            // Alternating row colors
            dgvRequests.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvRequests.DefaultCellStyle.SelectionBackColor = Color.FromArgb(26, 115, 232);
            dgvRequests.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvRequests.Columns.Add("RequestId", "Request ID");
            dgvRequests.Columns.Add("Category", "Category");
            dgvRequests.Columns.Add("Status", "Status");
            dgvRequests.Columns.Add("Priority", "Priority");
            dgvRequests.Columns.Add("SubmittedDate", "Submitted");
            dgvRequests.Columns.Add("Department", "Department");
            dgvRequests.Columns.Add("Location", "Location");

            dgvRequests.Columns["RequestId"].Width = 100;
            dgvRequests.Columns["Category"].Width = 140;
            dgvRequests.Columns["Status"].Width = 110;
            dgvRequests.Columns["Priority"].Width = 90;
            dgvRequests.Columns["SubmittedDate"].Width = 110;
            dgvRequests.Columns["Department"].Width = 150;

            dgvRequests.SelectionChanged += DgvRequests_SelectionChanged;

            this.Controls.Add(dgvRequests);
        }

        private void SetupDetailsPanel()
        {
            detailsPanel = new Panel
            {
                Size = new Size(520, 310),
                Location = new Point(960, searchPanel.Bottom + 20),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            detailsPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, detailsPanel.Width - 1, detailsPanel.Height - 1);
                }
            };

            Label lblDetailsTitle = new Label
            {
                Text = "📋 Request Details",
                Location = new Point(15, 15),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(26, 115, 232)
            };

            rtbDetails = new RichTextBox
            {
                Size = new Size(490, 265),
                Location = new Point(15, 45),
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White
            };

            detailsPanel.Controls.Add(lblDetailsTitle);
            detailsPanel.Controls.Add(rtbDetails);
            this.Controls.Add(detailsPanel);
        }

        private void SetupStatsPanel()
        {
            statsPanel = new Panel
            {
                Size = new Size(920, 110),
                Location = new Point(20, dgvRequests.Bottom + 20),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None
            };

            int cardWidth = 220;
            int spacing = 10;
            int x = 0;

            lblTotalRequests = CreateStatCard("Total Requests", "0", x,
                Color.FromArgb(103, 58, 183), IconChar.ListCheck);
            x += cardWidth + spacing;

            lblPending = CreateStatCard("Pending", "0", x,
                Color.FromArgb(255, 152, 0), IconChar.Clock);
            x += cardWidth + spacing;

            lblInProgress = CreateStatCard("In Progress", "0", x,
                Color.FromArgb(3, 169, 244), IconChar.Cog);
            x += cardWidth + spacing;

            lblCompleted = CreateStatCard("Completed", "0", x,
                Color.FromArgb(76, 175, 80), IconChar.CheckCircle);

            this.Controls.Add(statsPanel);
        }

        private Label CreateStatCard(string title, string value, int x, Color color, IconChar icon)
        {
            Panel card = new Panel
            {
                Size = new Size(220, 100),
                Location = new Point(x, 5),
                BackColor = color
            };

            // Add icon
            IconPictureBox iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconColor = Color.White,
                IconSize = 40,
                Size = new Size(45, 45),
                Location = new Point(15, 15),
                BackColor = Color.Transparent
            };

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(70, 20)
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(70, 45),
                Tag = "value"
            };

            card.Controls.Add(iconBox);
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            statsPanel.Controls.Add(card);

            return lblValue;
        }

        private void SetupDependencyPanel()
        {
            dependencyPanel = new Panel
            {
                Size = new Size(520, 260),
                Location = new Point(960, detailsPanel.Bottom + 20),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            dependencyPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, dependencyPanel.Width - 1, dependencyPanel.Height - 1);
                }
            };

            Label lblDepTitle = new Label
            {
                Text = "🔗 Dependencies & Relationships",
                Location = new Point(15, 15),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(26, 115, 232)
            };

            tvDependencies = new TreeView
            {
                Size = new Size(490, 210),
                Location = new Point(15, 45),
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White
            };

            dependencyPanel.Controls.Add(lblDepTitle);
            dependencyPanel.Controls.Add(tvDependencies);
            this.Controls.Add(dependencyPanel);
        }

        /// <summary>
        /// Load sample service requests with more data
        /// </summary>
        private void LoadSampleData()
        {
            // Create sample requests with varied data
            List<ServiceRequest> sampleRequests = new List<ServiceRequest>
            {
                new ServiceRequest
                {
                    RequestId = "REQ-001",
                    Location = "123 Main St, Pretoria Central",
                    Category = "Water & Sanitation",
                    Description = "Major water pipe burst on Main Street causing flooding. Immediate attention required.",
                    SubmittedDate = DateTime.Now.AddDays(-5),
                    Status = RequestStatus.InProgress,
                    Priority = 1,
                    AssignedDepartment = "Water Works Division"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-002",
                    Location = "45 Park Ave, Centurion",
                    Category = "Roads & Transport",
                    Description = "Large pothole on Park Avenue causing traffic issues and vehicle damage.",
                    SubmittedDate = DateTime.Now.AddDays(-3),
                    Status = RequestStatus.Pending,
                    Priority = 2,
                    AssignedDepartment = "Roads Maintenance"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-003",
                    Location = "789 Oak Rd, Midrand",
                    Category = "Electricity",
                    Description = "Street light not working for 2 weeks, creating safety concerns.",
                    SubmittedDate = DateTime.Now.AddDays(-7),
                    Status = RequestStatus.Completed,
                    Priority = 3,
                    EstimatedCompletion = DateTime.Now.AddDays(-1),
                    AssignedDepartment = "Electrical Services"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-004",
                    Location = "12 River St, Johannesburg CBD",
                    Category = "Waste Management",
                    Description = "Missed garbage collection for the past week. Bins overflowing.",
                    SubmittedDate = DateTime.Now.AddDays(-2),
                    Status = RequestStatus.Pending,
                    Priority = 2,
                    AssignedDepartment = "Waste Services"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-005",
                    Location = "56 Hill Rd, Sandton",
                    Category = "Public Safety",
                    Description = "Broken traffic light at major intersection causing accidents.",
                    SubmittedDate = DateTime.Now.AddDays(-4),
                    Status = RequestStatus.InProgress,
                    Priority = 1,
                    AssignedDepartment = "Traffic Management"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-006",
                    Location = "234 Beach Rd, Durban",
                    Category = "Parks & Recreation",
                    Description = "Playground equipment damaged and unsafe for children.",
                    SubmittedDate = DateTime.Now.AddDays(-6),
                    Status = RequestStatus.InProgress,
                    Priority = 2,
                    AssignedDepartment = "Parks Department"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-007",
                    Location = "78 Valley View, Cape Town",
                    Category = "Water & Sanitation",
                    Description = "Sewer blockage affecting multiple households.",
                    SubmittedDate = DateTime.Now.AddDays(-1),
                    Status = RequestStatus.Pending,
                    Priority = 1,
                    AssignedDepartment = "Water Works Division"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-008",
                    Location = "456 Summit Rd, Bloemfontein",
                    Category = "Roads & Transport",
                    Description = "Road signage missing at dangerous curve.",
                    SubmittedDate = DateTime.Now.AddDays(-8),
                    Status = RequestStatus.Completed,
                    Priority = 1,
                    AssignedDepartment = "Roads Maintenance"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-009",
                    Location = "90 Industrial Ave, Port Elizabeth",
                    Category = "Electricity",
                    Description = "Power outage in industrial area affecting businesses.",
                    SubmittedDate = DateTime.Now.AddDays(-10),
                    Status = RequestStatus.Completed,
                    Priority = 1,
                    AssignedDepartment = "Electrical Services"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-010",
                    Location = "111 Green St, Polokwane",
                    Category = "Waste Management",
                    Description = "Illegal dumping site needs cleanup.",
                    SubmittedDate = DateTime.Now.AddHours(-12),
                    Status = RequestStatus.Pending,
                    Priority = 3,
                    AssignedDepartment = "Waste Services"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-011",
                    Location = "22 School Rd, Nelspruit",
                    Category = "Public Safety",
                    Description = "Damaged fence around school premises - security risk.",
                    SubmittedDate = DateTime.Now.AddDays(-9),
                    Status = RequestStatus.Rejected,
                    Priority = 2,
                    AssignedDepartment = "Public Safety"
                },
                new ServiceRequest
                {
                    RequestId = "REQ-012",
                    Location = "567 Market St, Pietermaritzburg",
                    Category = "Roads & Transport",
                    Description = "Pedestrian crossing faded and needs repainting.",
                    SubmittedDate = DateTime.Now.AddDays(-11),
                    Status = RequestStatus.Completed,
                    Priority = 3,
                    AssignedDepartment = "Roads Maintenance"
                }
            };

            // Add status history
            foreach (var request in sampleRequests)
            {
                request.AddStatusUpdate("Request submitted by citizen");

                if (request.Status == RequestStatus.InProgress || request.Status == RequestStatus.Completed)
                {
                    request.AddStatusUpdate("Request reviewed and validated");
                    request.AddStatusUpdate($"Assigned to: {request.AssignedDepartment}");
                    request.AddStatusUpdate("Field inspection scheduled");
                }

                if (request.Status == RequestStatus.Completed)
                {
                    request.AddStatusUpdate("Work commenced on site");
                    request.AddStatusUpdate("Work completed successfully");
                    request.AddStatusUpdate("Quality check passed");
                    request.AddStatusUpdate("Request closed");
                }

                if (request.Status == RequestStatus.Rejected)
                {
                    request.AddStatusUpdate("Request reviewed");
                    request.AddStatusUpdate("Rejected: Insufficient information provided");
                }
            }

            // Insert into data structures
            foreach (var request in sampleRequests)
            {
                requestBST.Insert(request);
                dependencyGraph.AddRequest(request);
                priorityHeap.Insert(request);
            }

            // Add dependencies (realistic scenarios)
            dependencyGraph.AddDependency("REQ-002", "REQ-001"); // Road repair depends on water pipe fix
            dependencyGraph.AddDependency("REQ-005", "REQ-001"); // Traffic light depends on water pipe fix
            dependencyGraph.AddDependency("REQ-007", "REQ-001"); // Sewer work depends on water pipe fix
        }

        /// <summary>
        /// Refresh the display with current data
        /// </summary>
        private void RefreshDisplay()
        {
            // Get all requests from BST (in sorted order)
            List<ServiceRequest> allRequests = requestBST.InOrderTraversal();

            // Apply filters if any
            allRequests = ApplyFilters(allRequests);

            // Update DataGridView
            dgvRequests.Rows.Clear();
            foreach (var request in allRequests)
            {
                int rowIndex = dgvRequests.Rows.Add(
                    request.RequestId,
                    request.Category,
                    FormatStatus(request.Status),
                    request.GetPriorityLabel(),
                    request.SubmittedDate.ToString("yyyy-MM-dd"),
                    request.AssignedDepartment,
                    request.Location
                );

                // Color-code rows by priority
                DataGridViewRow row = dgvRequests.Rows[rowIndex];
                switch (request.Priority)
                {
                    case 1: // HIGH
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(183, 28, 28);
                        break;
                    case 2: // MEDIUM
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(230, 81, 0);
                        break;
                    case 3: // LOW
                        row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(27, 94, 32);
                        break;
                }
            }

            // Update statistics
            UpdateStatistics();
        }

        private string FormatStatus(RequestStatus status)
        {
            switch (status)
            {
                case RequestStatus.InProgress:
                    return "In Progress";
                default:
                    return status.ToString();
            }
        }

        private List<ServiceRequest> ApplyFilters(List<ServiceRequest> requests)
        {
            List<ServiceRequest> filtered = requests;

            // Filter by status
            if (cmbFilterStatus.SelectedIndex > 0)
            {
                string statusFilter = cmbFilterStatus.SelectedItem.ToString();
                RequestStatus status;
                if (Enum.TryParse(statusFilter, out status))
                {
                    filtered = filtered.Where(r => r.Status == status).ToList();
                }
            }

            // Filter by priority
            if (cmbFilterPriority.SelectedIndex > 0)
            {
                string priorityFilter = cmbFilterPriority.SelectedItem.ToString();
                int priority = priorityFilter == "HIGH" ? 1 : priorityFilter == "MEDIUM" ? 2 : 3;
                filtered = filtered.Where(r => r.Priority == priority).ToList();
            }

            return filtered;
        }

        private void UpdateStatistics()
        {
            List<ServiceRequest> allRequests = requestBST.InOrderTraversal();

            lblTotalRequests.Text = allRequests.Count.ToString();
            lblPending.Text = allRequests.Count(r => r.Status == RequestStatus.Pending).ToString();
            lblInProgress.Text = allRequests.Count(r => r.Status == RequestStatus.InProgress).ToString();
            lblCompleted.Text = allRequests.Count(r => r.Status == RequestStatus.Completed).ToString();
        }

        /// <summary>
        /// Event handlers
        /// </summary>
        private void DgvRequests_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count > 0)
            {
                string requestId = dgvRequests.SelectedRows[0].Cells["RequestId"].Value.ToString();
                DisplayRequestDetails(requestId);
            }
        }

        private void DisplayRequestDetails(string requestId)
        {
            ServiceRequest request = requestBST.Search(requestId);

            if (request != null)
            {
                rtbDetails.Clear();
                rtbDetails.SelectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
                rtbDetails.SelectionColor = Color.FromArgb(26, 115, 232);
                rtbDetails.AppendText($"{request.RequestId}\n");

                rtbDetails.SelectionFont = new Font("Segoe UI", 9);
                rtbDetails.SelectionColor = Color.Black;
                rtbDetails.AppendText($"\n📍 Location:\n{request.Location}\n\n");
                rtbDetails.AppendText($"📂 Category: {request.Category}\n");
                rtbDetails.AppendText($"⚡ Priority: {request.GetPriorityLabel()}\n");
                rtbDetails.AppendText($"📊 Status: {FormatStatus(request.Status)}\n");
                rtbDetails.AppendText($"🏢 Department: {request.AssignedDepartment}\n");
                rtbDetails.AppendText($"📅 Submitted: {request.SubmittedDate:yyyy-MM-dd HH:mm}\n\n");

                rtbDetails.SelectionFont = new Font("Segoe UI", 9, FontStyle.Bold);
                rtbDetails.AppendText("📝 Description:\n");
                rtbDetails.SelectionFont = new Font("Segoe UI", 9);
                rtbDetails.AppendText($"{request.Description}\n\n");

                if (request.StatusHistory.Count > 0)
                {
                    rtbDetails.SelectionFont = new Font("Segoe UI", 9, FontStyle.Bold);
                    rtbDetails.AppendText("📋 Status History:\n");
                    rtbDetails.SelectionFont = new Font("Segoe UI", 8);
                    rtbDetails.SelectionColor = Color.FromArgb(100, 100, 100);
                    foreach (var history in request.StatusHistory)
                    {
                        rtbDetails.AppendText($"  • {history}\n");
                    }
                }

                // Display dependencies
                DisplayDependencies(requestId);
            }
        }

        private void DisplayDependencies(string requestId)
        {
            tvDependencies.Nodes.Clear();

            TreeNode rootNode = new TreeNode($"📦 {requestId}");
            rootNode.NodeFont = new Font("Segoe UI", 10, FontStyle.Bold);
            rootNode.ForeColor = Color.FromArgb(26, 115, 232);

            // Get dependencies
            List<ServiceRequest> dependencies = dependencyGraph.GetDependencies(requestId);
            if (dependencies.Count > 0)
            {
                TreeNode depNode = new TreeNode($"⬇️ Depends On ({dependencies.Count}):");
                depNode.ForeColor = Color.FromArgb(244, 67, 54);
                depNode.NodeFont = new Font("Segoe UI", 9, FontStyle.Bold);
                foreach (var dep in dependencies)
                {
                    string statusIcon = GetStatusIcon(dep.Status);
                    TreeNode node = new TreeNode($"{statusIcon} {dep.RequestId} - {dep.Category} ({FormatStatus(dep.Status)})");
                    node.ForeColor = Color.FromArgb(80, 80, 80);
                    depNode.Nodes.Add(node);
                }
                rootNode.Nodes.Add(depNode);
            }
            else
            {
                TreeNode noDep = new TreeNode("✓ No dependencies - Ready to process");
                noDep.ForeColor = Color.FromArgb(76, 175, 80);
                noDep.NodeFont = new Font("Segoe UI", 9, FontStyle.Italic);
                rootNode.Nodes.Add(noDep);
            }

            // Get dependents
            List<ServiceRequest> dependents = dependencyGraph.GetDependents(requestId);
            if (dependents.Count > 0)
            {
                TreeNode depOnNode = new TreeNode($"⬆️ Required By ({dependents.Count}):");
                depOnNode.ForeColor = Color.FromArgb(33, 150, 243);
                depOnNode.NodeFont = new Font("Segoe UI", 9, FontStyle.Bold);
                foreach (var dependent in dependents)
                {
                    string statusIcon = GetStatusIcon(dependent.Status);
                    TreeNode node = new TreeNode($"{statusIcon} {dependent.RequestId} - {dependent.Category} ({FormatStatus(dependent.Status)})");
                    node.ForeColor = Color.FromArgb(80, 80, 80);
                    depOnNode.Nodes.Add(node);
                }
                rootNode.Nodes.Add(depOnNode);
            }

            tvDependencies.Nodes.Add(rootNode);
            rootNode.ExpandAll();
        }

        private string GetStatusIcon(RequestStatus status)
        {
            switch (status)
            {
                case RequestStatus.Pending:
                    return "⏳";
                case RequestStatus.InProgress:
                    return "⚙️";
                case RequestStatus.Completed:
                    return "✅";
                case RequestStatus.Rejected:
                    return "❌";
                default:
                    return "📋";
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchId = txtSearchId.Text.Trim();

            if (string.IsNullOrEmpty(searchId))
            {
                MessageBox.Show("Please enter a Request ID to search.", "Search",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ServiceRequest request = requestBST.Search(searchId);

            if (request != null)
            {
                // Find and select the row in DataGridView
                foreach (DataGridViewRow row in dgvRequests.Rows)
                {
                    if (row.Cells["RequestId"].Value.ToString() == searchId)
                    {
                        row.Selected = true;
                        dgvRequests.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }

                DisplayRequestDetails(searchId);
            }
            else
            {
                MessageBox.Show($"Request ID '{searchId}' not found in the system.",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        private void BtnClearFilters_Click(object sender, EventArgs e)
        {
            cmbFilterStatus.SelectedIndex = 0;
            cmbFilterPriority.SelectedIndex = 0;
            txtSearchId.Clear();
            RefreshDisplay();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDisplay();
            MessageBox.Show("✅ Data refreshed successfully!\n\n" +
                $"📊 Total Requests: {requestBST.Count}\n" +
                $"🌳 BST Height: {requestBST.GetHeight()}\n" +
                $"⚖️ Tree Balanced: {(requestBST.IsBalanced() ? "Yes" : "No")}",
                "Refresh Complete",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Find and show Form1 before closing
            foreach (Form form in Application.OpenForms)
            {
                if (form is Form1)
                {
                    form.Show();
                    form.BringToFront();
                    break;
                }
            }
            this.Close();
        }

        // Import for rounded corners
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
    }
}