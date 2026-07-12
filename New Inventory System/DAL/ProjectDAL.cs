using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_Inventory_System.DAL
{
    using New_Inventory_System.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class ProjectDAL
    {
        private string connectionString = ConfigurationManager
            .ConnectionStrings["InventoryDB"].ConnectionString;

        // =============================================
        // Get a single project by ID
        // =============================================
        public Project GetProject(int projectID)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_GetProjects", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectType", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", DBNull.Value);
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapProject(reader);
                }
            }
            return null;
        }

        // =============================================
        // Get all projects with optional filters
        // =============================================
        public List<Project> GetProjects(string projectType = null, string status = null)
        {
            var projects = new List<Project>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_GetProjects", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectType", (object)projectType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ProjectID", DBNull.Value);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        projects.Add(MapProject(reader));
                }
            }
            return projects;
        }

        // =============================================
        // Add a new project
        // =============================================
        public int AddProject(Project project)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_AddProject", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;

                cmd.Parameters.AddWithValue("@ProjectType", project.ProjectType);
                cmd.Parameters.AddWithValue("@Make", project.Make);
                cmd.Parameters.AddWithValue("@Model", project.Model);
                cmd.Parameters.AddWithValue("@Year", (object)project.Year ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Condition", (object)project.Condition ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object)project.Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Notes", (object)project.Notes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DateStarted", (object)project.DateStarted ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DateCompleted", (object)project.DateCompleted ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ProgressPercent", project.ProgressPercent);
                cmd.Parameters.AddWithValue("@RepoURL", (object)project.RepoURL ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LanguageStack", (object)project.LanguageStack ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@VersionNumber", (object)project.VersionNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LicenseType", (object)project.LicenseType ?? DBNull.Value);

                var newID = new SqlParameter("@NewProjectID", SqlDbType.Int);
                newID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(newID);

                conn.Open();
                cmd.ExecuteNonQuery();
                return (int)newID.Value;
            }
        }

        // =============================================
        // Update an existing project
        // =============================================
        public void UpdateProject(Project project)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_UpdateProject", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;

                cmd.Parameters.AddWithValue("@ProjectID", project.ProjectID);
                cmd.Parameters.AddWithValue("@ProjectType", project.ProjectType);
                cmd.Parameters.AddWithValue("@Make", project.Make);
                cmd.Parameters.AddWithValue("@Model", project.Model);
                cmd.Parameters.AddWithValue("@Year", (object)project.Year ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Condition", (object)project.Condition ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object)project.Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Notes", (object)project.Notes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DateStarted", (object)project.DateStarted ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DateCompleted", (object)project.DateCompleted ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ProgressPercent", project.ProgressPercent);
                cmd.Parameters.AddWithValue("@RepoURL", (object)project.RepoURL ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LanguageStack", (object)project.LanguageStack ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@VersionNumber", (object)project.VersionNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LicenseType", (object)project.LicenseType ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =============================================
        // Delete a project
        // =============================================
        public void DeleteProject(int projectID)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_DeleteProject", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =============================================
        // Get work log entries for a project
        // =============================================
        public List<WorkLogEntry> GetWorkLog(int projectID)
        {
            var entries = new List<WorkLogEntry>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_GetProjectWorkLog", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entries.Add(new WorkLogEntry
                        {
                            LogID = Convert.ToInt32(reader["LogID"]),
                            ProjectID = projectID,
                            LogDate = Convert.ToDateTime(reader["LogDate"]),
                            Entry = reader["Entry"].ToString(),
                            Stage = reader["Stage"] == DBNull.Value ? "" : reader["Stage"].ToString()
                        });
                    }
                }
            }
            return entries;
        }

        // =============================================
        // Add a work log entry
        // =============================================
        public int AddWorkLogEntry(int projectID, string entry, string stage = null)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_AddWorkLogEntry", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                cmd.Parameters.AddWithValue("@Entry", entry);
                cmd.Parameters.AddWithValue("@Stage", (object)stage ?? DBNull.Value);

                var newID = new SqlParameter("@NewLogID", SqlDbType.Int);
                newID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(newID);

                conn.Open();
                cmd.ExecuteNonQuery();
                return (int)newID.Value;
            }
        }

        // =============================================
        // Delete a work log entry
        // =============================================
        public void DeleteWorkLogEntry(int logID)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_DeleteWorkLogEntry", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@LogID", logID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =============================================
        // Private helper to map a reader row to a Project
        // =============================================
        private Project MapProject(SqlDataReader reader)
        {
            return new Project
            {
                ProjectID = Convert.ToInt32(reader["ProjectID"]),
                ProjectType = reader["ProjectType"].ToString(),
                Make = reader["Make"].ToString(),
                Model = reader["Model"].ToString(),
                Year = reader["Year"] == DBNull.Value ? (short?)null : Convert.ToInt16(reader["Year"]),
                Condition = reader["Condition"] == DBNull.Value ? null : reader["Condition"].ToString(),
                Status = reader["Status"] == DBNull.Value ? null : reader["Status"].ToString(),
                Notes = reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                DateStarted = reader["DateStarted"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DateStarted"]),
                DateCompleted = reader["DateCompleted"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DateCompleted"]),
                ProgressPercent = reader["ProgressPercent"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProgressPercent"]),
                RepoURL = reader["RepoURL"] == DBNull.Value ? null : reader["RepoURL"].ToString(),
                LanguageStack = reader["LanguageStack"] == DBNull.Value ? null : reader["LanguageStack"].ToString(),
                VersionNumber = reader["VersionNumber"] == DBNull.Value ? null : reader["VersionNumber"].ToString(),
                LicenseType = reader["LicenseType"] == DBNull.Value ? null : reader["LicenseType"].ToString()
            };
        }

        public List<ProjectImage> GetProjectImages(int projectID)
        {
            var images = new List<ProjectImage>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_GetProjectImages", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        images.Add(new ProjectImage
                        {
                            ImageID = Convert.ToInt32(reader["ImageID"]),
                            ProjectID = projectID,
                            ImagePath = reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"].ToString(),
                            ImageData = reader["ImageData"] == DBNull.Value ? null : (byte[])reader["ImageData"],
                            Caption = reader["Caption"] == DBNull.Value ? null : reader["Caption"].ToString(),
                            DateAdded = Convert.ToDateTime(reader["DateAdded"])
                        });
                    }
                }
            }
            return images;
        }

        public int AddProjectImage(int projectID, string imagePath, byte[] imageData, string caption)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_AddProjectImage", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                cmd.Parameters.AddWithValue("@ImagePath", (object)imagePath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ImageData", (object)imageData ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Caption", (object)caption ?? DBNull.Value);

                var newID = new SqlParameter("@NewImageID", SqlDbType.Int);
                newID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(newID);

                conn.Open();
                cmd.ExecuteNonQuery();
                return (int)newID.Value;
            }
        }

        public void DeleteProjectImage(int imageID)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_DeleteProjectImage", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ImageID", imageID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<string[]> GetComponentsByType(string componentType)
        {
            var components = new List<string[]>();
            string query = "";

            switch (componentType)
            {
                case "Tube":
                    query = "SELECT TubeID, [Tube Name] AS DisplayName FROM Tubes ORDER BY [Tube Name]";
                    break;
                case "Capacitor":
                    query = "SELECT CapacitorID, CAST(Capacitance AS NVARCHAR) + ' ' + Unit + ' ' + ISNULL(CapType, '') AS DisplayName FROM Capacitors ORDER BY Capacitance";
                    break;
                case "Resistor":
                    query = "SELECT ResistorID, CAST(Resistance AS NVARCHAR) + ' ' + Unit + ' ' + ISNULL(ResType, '') AS DisplayName FROM Resistors ORDER BY Resistance";
                    break;
                case "Switch":
                    query = "SELECT SwitchID, ISNULL(SwitchType, '') + ' ' + ISNULL(Poles, '') AS DisplayName FROM Switches ORDER BY SwitchType";
                    break;
                case "LED":
                    query = "SELECT LEDID, ISNULL(Color, '') + ' ' + ISNULL(Size, '') + ' LED' AS DisplayName FROM LEDs ORDER BY Color";
                    break;
                default:
                    return components;
            }

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 60;
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        components.Add(new string[]
                        {
                    reader[0].ToString(),   // ID
                    reader[1].ToString()    // Display name
                        });
                    }
                }
            }
            return components;
        }

        public List<ProjectLink> GetProjectLinks(int projectID)
        {
            var links = new List<ProjectLink>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_GetProjectLinks", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        links.Add(new ProjectLink
                        {
                            LinkID = Convert.ToInt32(reader["LinkID"]),
                            ProjectID = projectID,
                            LinkType = reader["LinkType"] == DBNull.Value ? null : reader["LinkType"].ToString(),
                            URL = reader["URL"] == DBNull.Value ? null : reader["URL"].ToString(),
                            FilePath = reader["FilePath"] == DBNull.Value ? null : reader["FilePath"].ToString(),
                            Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString()
                        });
                    }
                }
            }
            return links;
        }

        public int AddProjectLink(int projectID, string linkType, string url, string filePath, string description)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_AddProjectLink", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                cmd.Parameters.AddWithValue("@LinkType", (object)linkType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@URL", (object)url ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FilePath", (object)filePath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);

                var newID = new SqlParameter("@NewLinkID", SqlDbType.Int);
                newID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(newID);

                conn.Open();
                cmd.ExecuteNonQuery();
                return (int)newID.Value;
            }
        }

        public void DeleteProjectLink(int linkID)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_DeleteProjectLink", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@LinkID", linkID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProjectLink(int linkID, string linkType, string url, string filePath, string description)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_UpdateProjectLink", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@LinkID", linkID);
                cmd.Parameters.AddWithValue("@LinkType", (object)linkType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@URL", (object)url ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FilePath", (object)filePath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<ProjectCustomField> GetCustomFields(int projectID)
        {
            var fields = new List<ProjectCustomField>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_GetCustomFields", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fields.Add(new ProjectCustomField
                        {
                            CustomFieldID = Convert.ToInt32(reader["CustomFieldID"]),
                            ProjectID = projectID,
                            FieldName = reader["FieldName"].ToString(),
                            FieldValue = reader["FieldValue"] == DBNull.Value ? null : reader["FieldValue"].ToString(),
                            SortOrder = Convert.ToInt32(reader["SortOrder"])
                        });
                    }
                }
            }
            return fields;
        }

        public int AddCustomField(int projectID, string fieldName, string fieldValue, int sortOrder = 0)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_AddCustomField", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                cmd.Parameters.AddWithValue("@FieldName", fieldName);
                cmd.Parameters.AddWithValue("@FieldValue", (object)fieldValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SortOrder", sortOrder);

                var newID = new SqlParameter("@NewCustomFieldID", SqlDbType.Int);
                newID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(newID);

                conn.Open();
                cmd.ExecuteNonQuery();
                return (int)newID.Value;
            }
        }

        public void DeleteCustomField(int customFieldID)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_DeleteCustomField", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@CustomFieldID", customFieldID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCustomField(int customFieldID, string fieldName, string fieldValue, int sortOrder = 0)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_UpdateCustomField", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@CustomFieldID", customFieldID);
                cmd.Parameters.AddWithValue("@FieldName", fieldName);
                cmd.Parameters.AddWithValue("@FieldValue", (object)fieldValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SortOrder", sortOrder);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // =============================================
        // Add a component to the Project BOM
        // =============================================
        public void AddProjectComponent(int projectID, string componentType, int componentID, int quantity = 1)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_AddProjectComponent", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                cmd.Parameters.AddWithValue("@ComponentType", componentType);
                cmd.Parameters.AddWithValue("@ComponentID", componentID);
                cmd.Parameters.AddWithValue("@Quantity", quantity);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =============================================
        // Remove a component from the Project BOM
        // =============================================
        public void DeleteProjectComponent(int bomID)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("usp_DeleteProjectComponent", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@BOMID", bomID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
