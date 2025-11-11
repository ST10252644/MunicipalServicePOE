namespace MunicipalServicesApp
{
    partial class LocalEventsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvEvents = new DataGridView();
            cmbCategoryFilter = new ComboBox();
            dtpDateFilter = new DateTimePicker();
            chkDateFilter = new CheckBox();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnClearFilters = new Button();
            lstRecommendations = new ListBox();
            lstSearchHistory = new ListBox();
            btnBack = new Button();
            lblStats = new Label();
            progressBar = new ProgressBar();
            lblTitle = new Label();
            lblCategoryFilter = new Label();
            lblSearch = new Label();
            lblRecommendations = new Label();
            lblSearchHistory = new Label();
            panelFilters = new Panel();
            panelRecommendations = new Panel();
            panelHistory = new Panel();

            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            panelFilters.SuspendLayout();
            panelRecommendations.SuspendLayout();
            panelHistory.SuspendLayout();
            SuspendLayout();

            // --- DATAGRID ---
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.BackgroundColor = Color.White;
            dgvEvents.BorderStyle = BorderStyle.None;
            dgvEvents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvEvents.ColumnHeadersHeight = 30;
            dgvEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvEvents.Location = new Point(27, 370);
            dgvEvents.Margin = new Padding(4, 5, 4, 5);
            dgvEvents.MultiSelect = false;
            dgvEvents.Name = "dgvEvents";
            dgvEvents.ReadOnly = true;
            dgvEvents.RowHeadersVisible = false;
            dgvEvents.RowHeadersWidth = 51;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.Size = new Size(867, 525);
            dgvEvents.TabIndex = 0;
            dgvEvents.CellDoubleClick += dgvEvents_CellDoubleClick;

            // --- LABELS ---
            lblCategoryFilter = new Label();
            lblCategoryFilter.AutoSize = true;
            lblCategoryFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategoryFilter.Location = new Point(13, 18);
            lblCategoryFilter.Name = "lblCategoryFilter";
            lblCategoryFilter.Size = new Size(100, 20);
            lblCategoryFilter.Text = "Category:";

            lblSearch = new Label();
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSearch.Location = new Point(13, 100);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(100, 20);
            lblSearch.Text = "Search Events:";

            lblRecommendations = new Label();
            lblRecommendations.AutoSize = true;
            lblRecommendations.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRecommendations.Location = new Point(10, 10);
            lblRecommendations.Name = "lblRecommendations";
            lblRecommendations.Size = new Size(150, 23);
            lblRecommendations.Text = "Recommendations";

            lblSearchHistory = new Label();
            lblSearchHistory.AutoSize = true;
            lblSearchHistory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearchHistory.Location = new Point(10, 10);
            lblSearchHistory.Name = "lblSearchHistory";
            lblSearchHistory.Size = new Size(120, 23);
            lblSearchHistory.Text = "Search History";

            // --- CATEGORY FILTER ---
            cmbCategoryFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryFilter.Font = new Font("Segoe UI", 10F);
            cmbCategoryFilter.FormattingEnabled = true;
            cmbCategoryFilter.Location = new Point(13, 54);
            cmbCategoryFilter.Name = "cmbCategoryFilter";
            cmbCategoryFilter.Size = new Size(239, 31);
            cmbCategoryFilter.TabIndex = 1;

            // --- DATE FILTER ---
            dtpDateFilter.Enabled = false;
            dtpDateFilter.Font = new Font("Segoe UI", 10F);
            dtpDateFilter.Location = new Point(293, 54);
            dtpDateFilter.Name = "dtpDateFilter";
            dtpDateFilter.Size = new Size(265, 30);
            dtpDateFilter.TabIndex = 2;

            // --- CHECKBOX ---
            chkDateFilter.AutoSize = true;
            chkDateFilter.Font = new Font("Segoe UI", 9F);
            chkDateFilter.Location = new Point(293, 18);
            chkDateFilter.Name = "chkDateFilter";
            chkDateFilter.Size = new Size(120, 24);
            chkDateFilter.TabIndex = 3;
            chkDateFilter.Text = "Filter by Date";
            chkDateFilter.UseVisualStyleBackColor = true;
            chkDateFilter.CheckedChanged += chkDateFilter_CheckedChanged;

            // --- SEARCH ---
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(13, 131);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(399, 30);
            txtSearch.TabIndex = 4;

            // --- SEARCH BUTTON ---
            btnSearch.BackColor = Color.FromArgb(33, 150, 243);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(427, 126);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(133, 46);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;

            // --- CLEAR FILTERS ---
            btnClearFilters.BackColor = Color.FromArgb(158, 158, 158);
            btnClearFilters.FlatStyle = FlatStyle.Flat;
            btnClearFilters.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClearFilters.ForeColor = Color.White;
            btnClearFilters.Location = new Point(573, 126);
            btnClearFilters.Name = "btnClearFilters";
            btnClearFilters.Size = new Size(133, 46);
            btnClearFilters.TabIndex = 6;
            btnClearFilters.Text = "Clear Filters";
            btnClearFilters.UseVisualStyleBackColor = false;
            btnClearFilters.Click += btnClearFilters_Click;

            // --- LISTBOXES ---
            lstRecommendations.FormattingEnabled = true;
            lstRecommendations.ItemHeight = 20;
            lstRecommendations.Location = new Point(10, 45);
            lstRecommendations.Name = "lstRecommendations";
            lstRecommendations.Size = new Size(360, 264);
            lstRecommendations.TabIndex = 0;

            lstSearchHistory.FormattingEnabled = true;
            lstSearchHistory.ItemHeight = 20;
            lstSearchHistory.Location = new Point(10, 45);
            lstSearchHistory.Name = "lstSearchHistory";
            lstSearchHistory.Size = new Size(360, 204);
            lstSearchHistory.TabIndex = 0;

            // --- BACK BUTTON ---
            btnBack.BackColor = Color.FromArgb(120, 120, 120);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(27, 28);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(180, 50);
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;

            // --- STATS AND PROGRESS ---
            lblStats.AutoSize = true;
            lblStats.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStats.Location = new Point(30, 920);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(200, 23);
            lblStats.Text = "Total Events: 0";

            progressBar.Location = new Point(30, 950);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(860, 25);
            progressBar.TabIndex = 0;

            // --- TITLE ---
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 150, 243);
            lblTitle.Location = new Point(360, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(480, 46);
            lblTitle.Text = "Local Events & Announcements";

            // --- FILTER PANEL ---
            panelFilters.BackColor = Color.FromArgb(245, 245, 245);
            panelFilters.BorderStyle = BorderStyle.FixedSingle;
            panelFilters.Controls.Add(lblCategoryFilter);
            panelFilters.Controls.Add(cmbCategoryFilter);
            panelFilters.Controls.Add(chkDateFilter);
            panelFilters.Controls.Add(dtpDateFilter);
            panelFilters.Controls.Add(lblSearch);
            panelFilters.Controls.Add(txtSearch);
            panelFilters.Controls.Add(btnSearch);
            panelFilters.Controls.Add(btnClearFilters);
            panelFilters.Location = new Point(27, 108);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(866, 200);
            panelFilters.TabIndex = 0;

            // --- RECOMMENDATIONS PANEL ---
            panelRecommendations.BackColor = Color.FromArgb(255, 248, 225);
            panelRecommendations.BorderStyle = BorderStyle.FixedSingle;
            panelRecommendations.Controls.Add(lblRecommendations);
            panelRecommendations.Controls.Add(lstRecommendations);
            panelRecommendations.Location = new Point(920, 108);
            panelRecommendations.Name = "panelRecommendations";
            panelRecommendations.Size = new Size(386, 320);
            panelRecommendations.TabIndex = 0;

            // --- HISTORY PANEL ---
            panelHistory.BackColor = Color.FromArgb(243, 229, 245);
            panelHistory.BorderStyle = BorderStyle.FixedSingle;
            panelHistory.Controls.Add(lblSearchHistory);
            panelHistory.Controls.Add(lstSearchHistory);
            panelHistory.Location = new Point(920, 460);
            panelHistory.Name = "panelHistory";
            panelHistory.Size = new Size(386, 270);
            panelHistory.TabIndex = 0;

            // --- FORM ---
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1400, 900);
            Controls.Add(panelHistory);
            Controls.Add(panelRecommendations);
            Controls.Add(panelFilters);
            Controls.Add(lblTitle);
            Controls.Add(btnBack);
            Controls.Add(dgvEvents);
            Controls.Add(lblStats);
            Controls.Add(progressBar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LocalEventsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Municipal Services - Local Events";

            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            panelRecommendations.ResumeLayout(false);
            panelRecommendations.PerformLayout();
            panelHistory.ResumeLayout(false);
            panelHistory.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvEvents;
        private ComboBox cmbCategoryFilter;
        private DateTimePicker dtpDateFilter;
        private CheckBox chkDateFilter;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnClearFilters;
        private ListBox lstRecommendations;
        private ListBox lstSearchHistory;
        private Button btnBack;
        private Label lblStats;
        private ProgressBar progressBar;
        private Label lblTitle;
        private Label lblCategoryFilter;
        private Label lblSearch;
        private Label lblRecommendations;
        private Label lblSearchHistory;
        private Panel panelFilters;
        private Panel panelRecommendations;
        private Panel panelHistory;
    }
}