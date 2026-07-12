using New_Inventory_System.DAL;
using New_Inventory_System.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace New_Inventory_System
{
    public partial class projectForm : Form
    {
        private int currentProjectID = -1;
        private bool isEditMode = false;
        private ProjectDAL dal = new ProjectDAL();

        public projectForm()
        {
            InitializeComponent();
            SetupListViews();
            SetEditMode(false);
            ClearForm();
        }

        public projectForm(int projectID) : this()
        {
            LoadProject(projectID);
            SetEditMode(false);
            LoadWorkLog(projectID);
            LoadImages(projectID);
            LoadLinks(projectID);
            LoadCustomFields(projectID);
        }

        private void SetupListViews()
        {
            // Work Log columns
            lvWorkLog.Columns.Add("ID", 0);
            lvWorkLog.Columns.Add("Date", 120);
            lvWorkLog.Columns.Add("Stage", 100);
            lvWorkLog.Columns.Add("Entry", 400);

            // Links columns
            lvLinks.Columns.Add("ID", 0);
            lvLinks.Columns.Add("Type", 80);
            lvLinks.Columns.Add("Description", 200);
            lvLinks.Columns.Add("URL/Path", 400);

            // Custom Fields columns
            lvCustomFields.Columns.Add("ID", 0);
            lvCustomFields.Columns.Add("Field", 250);
            lvCustomFields.Columns.Add("Value", 500);
            lvCustomFields.View = View.Details;
            // Hide ID columns
            lvWorkLog.Columns[0].Width = 0;
            lvLinks.Columns[0].Width = 0;
            lvCustomFields.Columns[0].Width = 0;

        }

        private void SetEditMode(bool editing)
        {
            isEditMode = editing;

            cboProjectType.Enabled = editing;
            txtMake.ReadOnly = !editing;
            txtModel.ReadOnly = !editing;
            txtYear.ReadOnly = !editing;
            cboCondition.Enabled = editing;
            cboStatus.Enabled = editing;
            dtpDateStarted.Enabled = editing;
            dtpDateCompleted.Enabled = editing;
            txtNotes.ReadOnly = !editing;

            btnEdit.Enabled = !editing;
            btnSave.Enabled = editing;
            btnCancel.Enabled = editing;
            btnNew.Enabled = !editing;
            btnDelete.Enabled = !editing;
        }

        private void ClearForm()
        {
            cboProjectType.SelectedIndex = -1;
            txtMake.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtYear.Text = string.Empty;
            cboCondition.SelectedIndex = -1;
            cboStatus.SelectedIndex = -1;
            pgbProgress.Value = 0;
            dtpDateStarted.Value = DateTime.Today;
            dtpDateCompleted.Value = DateTime.Today;
            txtNotes.Text = string.Empty;

            lvWorkLog.Items.Clear();
            lvBOM.Items.Clear();
            lvLinks.Items.Clear();
            lvCustomFields.Items.Clear();
            flpImages.Controls.Clear();
        }

        private void LoadProject(int projectID)
        {
            try
            {
                Project project = dal.GetProject(projectID);

                if (project == null)
                {
                    MessageBox.Show("Project not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cboProjectType.SelectedItem = project.ProjectType;
                txtMake.Text = project.Make;
                txtModel.Text = project.Model;
                txtYear.Text = project.Year.HasValue ? project.Year.ToString() : "";
                cboCondition.SelectedItem = project.Condition ?? "";
                cboStatus.SelectedItem = project.Status ?? "";

               // Console.WriteLine(project.ProgressPercent);
                
                //Change the color of the progress bar depending on the progress %
                pgbProgress.Value = project.ProgressPercent;
                if (project.ProgressPercent == 100)
                {
                    pgbProgress.ForeColor = Color.Green;
                }
                else if (project.ProgressPercent >= 75 && project.ProgressPercent < 100)
                {
                    pgbProgress.ForeColor = Color.Blue;
                }
                else if (project.ProgressPercent >= 25 && project.ProgressPercent < 75)
                {
                    pgbProgress.ForeColor = Color.DarkOrange;
                }
                else
                {
                    pgbProgress.ForeColor = Color.Red;
                }
                    dtpDateStarted.Value = project.DateStarted ?? DateTime.Today;
                dtpDateCompleted.Value = project.DateCompleted ?? DateTime.Today;
                txtNotes.Text = project.Notes ?? "";

                currentProjectID = projectID;

                LoadWorkLog(projectID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading project: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LoadWorkLog(int projectID)
        {
            try
            {
                lvWorkLog.Items.Clear();

                var entries = dal.GetWorkLog(projectID);

                foreach (var entry in entries)
                {
                    var item = new ListViewItem(entry.LogID.ToString());
                    item.SubItems.Add(entry.LogDate.ToShortDateString());
                    item.SubItems.Add(entry.Stage ?? "");
                    item.SubItems.Add(entry.Entry);
                    lvWorkLog.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading work log: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            if (currentProjectID == -1)
                ClearForm();
            else
                LoadProject(currentProjectID);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetEditMode(true);
            currentProjectID = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboProjectType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Project Type.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMake.Text))
            {
                MessageBox.Show("Please enter a Make.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Please enter a Model.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                short? year = null;
                short parsedYear;
                if (short.TryParse(txtYear.Text, out parsedYear))
                    year = parsedYear;

                int progress = 0;
               // int.TryParse(lblProgressValue.Text.Replace("%", "").Trim(), out progress);

                var project = new Project
                {
                    ProjectID = currentProjectID,
                    ProjectType = cboProjectType.SelectedItem.ToString(),
                    Make = txtMake.Text.Trim(),
                    Model = txtModel.Text.Trim(),
                    Year = year,
                    Condition = cboCondition.SelectedIndex == -1 ? null : cboCondition.SelectedItem.ToString(),
                    Status = cboStatus.SelectedIndex == -1 ? null : cboStatus.SelectedItem.ToString(),
                    Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim(),
                    DateStarted = dtpDateStarted.Value.Date,
                    DateCompleted = null,
                    ProgressPercent = progress
                };

                if (currentProjectID == -1)
                    currentProjectID = dal.AddProject(project);
                else
                    dal.UpdateProject(project);

                SetEditMode(false);
                MessageBox.Show("Project saved successfully!", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving project: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentProjectID == -1) return;

            string projectName = txtMake.Text + " " + txtModel.Text;

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to delete {projectName}?\n\nThis cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                dal.DeleteProject(currentProjectID);
                ClearForm();
                currentProjectID = -1;
                SetEditMode(false);
                MessageBox.Show("Project deleted.", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting project: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            if (currentProjectID == -1)
            {
                MessageBox.Show("Please save the project first before adding log entries.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewEntry.Text))
            {
                MessageBox.Show("Please enter a log entry.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dal.AddWorkLogEntry(currentProjectID, txtNewEntry.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtStage.Text) ? null : txtStage.Text.Trim());

                txtNewEntry.Text = string.Empty;
                txtStage.Text = string.Empty;
                LoadWorkLog(currentProjectID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding log entry: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            if (lvWorkLog.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an entry to delete.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this log entry?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                int logID = Convert.ToInt32(lvWorkLog.SelectedItems[0].Text);
                dal.DeleteWorkLogEntry(logID);
                LoadWorkLog(currentProjectID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting log entry: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadImages(int projectID)
        {
            try
            {
                flpImages.Controls.Clear();

                var images = dal.GetProjectImages(projectID);

                foreach (var image in images)
                {
                    // Create a panel for each image thumbnail
                    Panel imgPanel = new Panel();
                    imgPanel.Size = new Size(150, 180);
                    imgPanel.BorderStyle = BorderStyle.FixedSingle;
                    imgPanel.Tag = image.ImageID;

                    // Create the PictureBox thumbnail
                    PictureBox pb = new PictureBox();
                    pb.Size = new Size(140, 140);
                    pb.Location = new Point(5, 5);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Tag = image.ImageID;

                    // Load image from path or data
                    if (image.ImageData != null)
                    {
                        using (var ms = new System.IO.MemoryStream(image.ImageData))
                            pb.Image = Image.FromStream(ms);
                    }
                    else if (!string.IsNullOrEmpty(image.ImagePath) && System.IO.File.Exists(image.ImagePath))
                    {
                        pb.Image = Image.FromFile(image.ImagePath);
                    }

                    // Caption label
                    Label caption = new Label();
                    caption.Text = image.Caption ?? "";
                    caption.Size = new Size(140, 30);
                    caption.Location = new Point(5, 148);
                    caption.TextAlign = ContentAlignment.MiddleCenter;
                    caption.Font = new Font("Tahoma", 8);

                    // Click to select
                    pb.Click += (s, e) =>
                    {
                        foreach (Control c in flpImages.Controls)
                            c.BackColor = SystemColors.Control;
                        imgPanel.BackColor = Color.LightBlue;
                        txtImageCaption.Text = image.Caption ?? "";
                    };

                    imgPanel.Controls.Add(pb);
                    imgPanel.Controls.Add(caption);
                    flpImages.Controls.Add(imgPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading images: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            if (currentProjectID == -1)
            {
                MessageBox.Show("Please save the project first before adding images.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Select Image";
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                dlg.Multiselect = true;

                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    foreach (string filePath in dlg.FileNames)
                    {
                        // Ask user — store as path or embed?
                        DialogResult embedResult = MessageBox.Show(
                            $"Embed {System.IO.Path.GetFileName(filePath)} in the database?\n\nYes = Embed (portable)\nNo = Store file path only",
                            "Storage Option",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        string caption = txtImageCaption.Text.Trim();

                        if (embedResult == DialogResult.Yes)
                        {
                            byte[] imageData = System.IO.File.ReadAllBytes(filePath);
                            dal.AddProjectImage(currentProjectID, null, imageData,
                                string.IsNullOrEmpty(caption) ? null : caption);
                        }
                        else
                        {
                            dal.AddProjectImage(currentProjectID, filePath, null,
                                string.IsNullOrEmpty(caption) ? null : caption);
                        }
                    }

                    txtImageCaption.Text = string.Empty;
                    LoadImages(currentProjectID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding image: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            // Find selected panel
            Panel selectedPanel = null;
            foreach (Control c in flpImages.Controls)
            {
                if (c.BackColor == Color.LightBlue)
                {
                    selectedPanel = c as Panel;
                    break;
                }
            }

            if (selectedPanel == null)
            {
                MessageBox.Show("Please click an image to select it first.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to remove this image?",
                "Confirm Remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                int imageID = (int)selectedPanel.Tag;
                dal.DeleteProjectImage(imageID);
                LoadImages(currentProjectID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing image: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLinks(int projectID)
        {
            try
            {
                lvLinks.Items.Clear();

                var links = dal.GetProjectLinks(projectID);

                foreach (var link in links)
                {
                    var item = new ListViewItem(link.LinkID.ToString());
                    item.SubItems.Add(link.LinkType ?? "");
                    item.SubItems.Add(link.Description ?? "");
                    item.SubItems.Add(!string.IsNullOrEmpty(link.URL) ? link.URL : link.FilePath ?? "");
                    lvLinks.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading links: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddLink_Click(object sender, EventArgs e)
        {
            if (currentProjectID == -1)
            {
                MessageBox.Show("Please save the project first before adding links.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLinkURL.Text) && string.IsNullOrWhiteSpace(txtLinkDescription.Text))
            {
                MessageBox.Show("Please enter a URL or description.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string linkType = cboLinkType.SelectedIndex == -1 ? null : cboLinkType.SelectedItem.ToString();
                string url = string.IsNullOrWhiteSpace(txtLinkURL.Text) ? null : txtLinkURL.Text.Trim();
                string description = string.IsNullOrWhiteSpace(txtLinkDescription.Text) ? null : txtLinkDescription.Text.Trim();

                // Check if it's a file path or URL
                string filePath = null;
                if (url != null && System.IO.File.Exists(url))
                {
                    filePath = url;
                    url = null;
                }

                dal.AddProjectLink(currentProjectID, linkType, url, filePath, description);

                cboLinkType.SelectedIndex = -1;
                txtLinkURL.Text = string.Empty;
                txtLinkDescription.Text = string.Empty;

                LoadLinks(currentProjectID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding link: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveLink_Click(object sender, EventArgs e)
        {
            if (lvLinks.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a link to remove.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to remove this link?",
                "Confirm Remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                int linkID = Convert.ToInt32(lvLinks.SelectedItems[0].Text);
                dal.DeleteProjectLink(linkID);
                LoadLinks(currentProjectID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing link: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvLinks_DoubleClick(object sender, EventArgs e)
        {
            if (lvLinks.SelectedItems.Count == 0) return;

            string urlOrPath = lvLinks.SelectedItems[0].SubItems[3].Text;

            if (string.IsNullOrEmpty(urlOrPath)) return;

            try
            {
                System.Diagnostics.Process.Start(urlOrPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening link: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomFields(int projectID)
        {
            try
            {
                lvCustomFields.Items.Clear();

                var fields = dal.GetCustomFields(projectID);

                foreach (var field in fields)
                {
                    var item = new ListViewItem(field.CustomFieldID.ToString());
                    item.SubItems.Add(field.FieldName);
                    item.SubItems.Add(field.FieldValue ?? "");
                    lvCustomFields.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading custom fields: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    

        private void btnAddField_Click(object sender, EventArgs e)
        {
            if (currentProjectID == -1)
            {
                MessageBox.Show("Please save the project first before adding custom fields.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFieldName.Text))
            {
                MessageBox.Show("Please enter a field name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dal.AddCustomField(
                    currentProjectID,
                    txtFieldName.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtFieldValue.Text) ? null : txtFieldValue.Text.Trim());

                txtFieldName.Text = string.Empty;
                txtFieldValue.Text = string.Empty;

                LoadCustomFields(currentProjectID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding custom field: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboComponentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComponentList();
        }

        private void btnRemoveField_Click(object sender, EventArgs e)
        {
            if (lvCustomFields.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a field to remove.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to remove this custom field?",
                "Confirm Remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                int customFieldID = Convert.ToInt32(lvCustomFields.SelectedItems[0].Text);
                dal.DeleteCustomField(customFieldID);
                LoadCustomFields(currentProjectID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing custom field: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Load the Component Drop List
        private void UpdateComponentList()
        {
            // 1. Check if the combo box actually has a selection and isn't null
            if (cboComponentType?.SelectedItem == null) return;

            try
            {
                string selectedType = cboComponentType.SelectedItem.ToString();
                var components = dal.GetComponentsByType(selectedType);

                if (components == null) return;

                var dataSource = components.Select(comp => new {
                    ID = comp[0],
                    Name = comp[1]
                }).ToList();

                cboComponent.DataSource = dataSource;
                cboComponent.DisplayMember = "Name";
                cboComponent.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                // Only show errors if the form is still visible/active
                if (!this.IsDisposed && this.Visible)
                {
                    MessageBox.Show("Error loading components: " + ex.Message, "Error");
                }
            }
        }

        private void btnAddBOMEntry_Click(object sender, EventArgs e)
        {
            if (cboComponentType?.SelectedItem == null) return;

            try
            {
                string selectedType = cboComponentType.SelectedItem.ToString();
                var components = dal.GetComponentsByType(selectedType);

              /*  if (components == null) return;

                var dataSource = components.Select(comp => new {
                    ID = comp[0],
                    Name = comp[1]
                }).ToList();

                cboComponent.DataSource = dataSource;
                cboComponent.DisplayMember = "Name";
                cboComponent.ValueMember = "ID";*/
            }
            catch (Exception ex)
            {
                // Only show errors if the form is still visible/active
                if (!this.IsDisposed && this.Visible)
                {
                    MessageBox.Show("Error loading components: " + ex.Message, "Error");
                }
            }

        }
    }
}