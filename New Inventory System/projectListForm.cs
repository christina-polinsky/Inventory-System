using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Inventory_System
{
    public partial class projectListForm : Form
    {
        private string connectionString = System.Configuration.ConfigurationManager
            .ConnectionStrings["InventoryDB"].ConnectionString;

        public projectListForm()
        {
            InitializeComponent();
            SetupListView();
            PopulateFilterCombo();
            LoadProjects();
        }



        private void SetupListView()
        {
            lvProjects.Columns.Add("ID", 0);      // Hidden ID column
            lvProjects.Columns.Add("Type", 80);
            lvProjects.Columns.Add("Make", 120);
            lvProjects.Columns.Add("Model", 150);
            lvProjects.Columns.Add("Year", 60);
            lvProjects.Columns.Add("Condition", 90);
            lvProjects.Columns.Add("Status", 120);
            lvProjects.Columns.Add("Started", 100);
        }

        private void PopulateFilterCombo()
        {
            cboFilter.SelectedIndex = 0; // Default to "All"
        }

        private void LoadProjects()
        {
            try
            {
                lvProjects.Items.Clear();

                string filterType = cboFilter.SelectedIndex <= 0 ? null : cboFilter.SelectedItem.ToString();
                string searchText = txtSearch.Text.Trim();

                using (var conn = new System.Data.SqlClient.SqlConnection(connectionString))
                using (var cmd = new System.Data.SqlClient.SqlCommand("usp_GetProjects", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;
                    cmd.Parameters.AddWithValue("@ProjectType", (object)filterType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", DBNull.Value);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string make = reader["Make"].ToString();
                            string model = reader["Model"].ToString();

                            // Apply search filter client-side
                            if (!string.IsNullOrEmpty(searchText) &&
                                !make.ToLower().Contains(searchText.ToLower()) &&
                                !model.ToLower().Contains(searchText.ToLower()))
                                continue;

                            var item = new ListViewItem(reader["ProjectID"].ToString());
                            item.SubItems.Add(reader["ProjectType"].ToString());
                            item.SubItems.Add(make);
                            item.SubItems.Add(model);
                            item.SubItems.Add(reader["Year"] == DBNull.Value ? "" : reader["Year"].ToString());
                            item.SubItems.Add(reader["Condition"] == DBNull.Value ? "" : reader["Condition"].ToString());
                            item.SubItems.Add(reader["Status"] == DBNull.Value ? "" : reader["Status"].ToString());
                            item.SubItems.Add(reader["DateStarted"] == DBNull.Value ? "" : Convert.ToDateTime(reader["DateStarted"]).ToShortDateString());

                            lvProjects.Items.Add(item);
                        }
                    }
                }

                lblStatus.Text = $"{lvProjects.Items.Count} projects";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading projects: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            projectForm projectDetails = new projectForm();
            projectDetails.ShowDialog();
            LoadProjects(); // Refresh list after returning
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenSelectedProject();
        }

        private void lvProjects_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedProject();
        }

        private void OpenSelectedProject()
        {
            if (lvProjects.SelectedItems.Count == 0) return;

            int projectID = Convert.ToInt32(lvProjects.SelectedItems[0].Text);
            projectForm projectDetails = new projectForm(projectID);
            projectDetails.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvProjects.SelectedItems.Count == 0) return;

            string projectName = lvProjects.SelectedItems[0].SubItems[2].Text + " " +
                                 lvProjects.SelectedItems[0].SubItems[3].Text;

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to delete {projectName}?\n\nThis cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                int projectID = Convert.ToInt32(lvProjects.SelectedItems[0].Text);

                using (var conn = new System.Data.SqlClient.SqlConnection(connectionString))
                using (var cmd = new System.Data.SqlClient.SqlCommand("usp_DeleteProject", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;
                    cmd.Parameters.AddWithValue("@ProjectID", projectID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadProjects();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting project: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    



    }
}
