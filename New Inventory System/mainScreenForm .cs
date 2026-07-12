using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Inventory_System
{
    public partial class mainScreenForm : Form
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        string hoverDescription = "";
        private bool isShoppingHover = false;

        private void ApplyRetroTheme(bool isRetro)
        {
            
            if (isRetro)
            {

                // 1. Disable the modern 'Theme' engine for this specific window
                SetWindowTheme(this.Handle, "", "");
                // 2. Set the 2000s 'Industrial Grey' and Sharp Font
                this.BackColor = Color.FromArgb(212, 208, 200);
                this.Font = new Font("Tahoma", 8.25f);
                this.FormBorderStyle = FormBorderStyle.Fixed3D; // The heavy 3D frame
            
                
            }
            else
            {
                // 1. Re-enable modern Windows 11 'Fluent' styling
                SetWindowTheme(this.Handle, null, null);

                // 2. Restore modern White/Segoe UI look
                this.BackColor = SystemColors.Control;
                this.Font = new Font("Segoe UI", 9f);
                this.FormBorderStyle = FormBorderStyle.Sizable; // Modern resizable frame
            }



            // 3. Update all buttons/inputs on the form
            foreach (Control c in this.Controls)
            {
                UpdateControlStyle(c, isRetro);
            }
        }

        private void btnComponents_Paint(object sender, PaintEventArgs e)
        {
            // 1. Draw a subtle vertical "pinstripe" texture
            using (Pen texturePen = new Pen(Color.FromArgb(20, Color.White), 1))
            {
                for (int i = 0; i < btnComponents.Width; i += 3)
                {
                    e.Graphics.DrawLine(texturePen, i, 0, i, btnComponents.Height);
                }
            }

            // 2. Add a "Gloss" gradient to the top half
            using (LinearGradientBrush gloss = new LinearGradientBrush(
                new Rectangle(0, 0, btnComponents.Width, btnComponents.Height / 2),
                Color.FromArgb(30, Color.White),
                Color.Transparent,
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(gloss, 0, 0, btnComponents.Width, btnComponents.Height / 2);
            }
        }

        private void UpdateControlStyle(Control c, bool isRetro)
        {
            if (isRetro)
            {
                // 1. Strip the modern "Skin" off this specific component
                SetWindowTheme(c.Handle, "", "");

                // 2. If it's a button, give it that 3D tactile feel
                if (c is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Standard;
                    btn.BackColor = Color.FromArgb(212, 208, 200); // Classic Grey
                    
                }

            }
            else
            {
                // 1. Re-apply the modern Windows 11 "Fluent" skin
                SetWindowTheme(c.Handle, null, null);

                // 2. Return buttons to the modern "System" look
                if (c is Button btn)
                {
                    btn.FlatStyle = FlatStyle.System;
                    btn.UseVisualStyleBackColor = true;
                }
            }
            foreach (Control child in c.Controls)
            {
                UpdateControlStyle(child, isRetro);
            }

        }




            public mainScreenForm()
        {
            InitializeComponent();
        }

        
        

        //Create a PDF
        private void GenerateShoppingListPDF()
        {
            GlobalFontSettings.FontResolver = new WindowsFontResolver();
            try
            {
                string connectionString = System.Configuration.ConfigurationManager
                    .ConnectionStrings["InventoryDB"].ConnectionString;

                var shoppingItems = new List<(string TubeName, int TotalNeeded, int HaveOnHand, int Shortage)>();
                var lowStockItems = new List<(string TubeName, int TotalNeeded, int HaveOnHand, int Surplus)>();

                using (var conn = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    conn.Open();

                    // Must buy items
                    using (var cmd = new System.Data.SqlClient.SqlCommand("usp_GetReplenishmentList", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 60;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shoppingItems.Add((
                                    reader["Tube Name"].ToString(),
                                    Convert.ToInt32(reader["TotalNeeded"]),
                                    Convert.ToInt32(reader["HaveOnHand"]),
                                    Convert.ToInt32(reader["Shortage"])
                                ));
                            }
                        }
                    }

                    // Low stock items
                    string lowStockQuery = @"
                SELECT t.[Tube Name], SUM(b.QuantityNeeded) AS TotalNeeded,
                       t.[Quantity Owned] AS HaveOnHand,
                       (t.[Quantity Owned] - SUM(b.QuantityNeeded)) AS Surplus
                FROM RadioBOM b
                JOIN Tubes t ON t.TubeID = b.TubeID
                GROUP BY t.[Tube Name], t.[Quantity Owned]
                HAVING (t.[Quantity Owned] - SUM(b.QuantityNeeded)) >= 0
                AND t.[Quantity Owned] < (SUM(b.QuantityNeeded) * 2)
                ORDER BY Surplus ASC";

                    using (var cmd = new System.Data.SqlClient.SqlCommand(lowStockQuery, conn))
                    {
                        cmd.CommandTimeout = 60;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lowStockItems.Add((
                                    reader["Tube Name"].ToString(),
                                    Convert.ToInt32(reader["TotalNeeded"]),
                                    Convert.ToInt32(reader["HaveOnHand"]),
                                    Convert.ToInt32(reader["Surplus"])
                                ));
                            }
                        }
                    }
                }

                // Build the PDF
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Shopping List";
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont titleFont = new XFont("Arial", 18, XFontStyleEx.Bold);
                XFont headerFont = new XFont("Arial", 12, XFontStyleEx.Bold);
                XFont bodyFont = new XFont("Arial", 11, XFontStyleEx.Regular);
                XFont smallFont = new XFont("Arial", 9, XFontStyleEx.Italic);

                double y = 40;
                double margin = 50;
                double pageWidth = page.Width - margin * 2;

                // Title
                gfx.DrawString("Inventory System — Shopping List", titleFont, XBrushes.Black,
                    new XRect(margin, y, pageWidth, 30), XStringFormats.TopLeft);
                y += 30;

                gfx.DrawString("Generated: " + DateTime.Now.ToString("MMMM dd, yyyy"), smallFont, XBrushes.Gray,
                    new XRect(margin, y, pageWidth, 20), XStringFormats.TopLeft);
                y += 30;

                // Must Buy section
                gfx.DrawString("MUST BUY", headerFont, XBrushes.DarkRed,
                    new XRect(margin, y, pageWidth, 20), XStringFormats.TopLeft);
                y += 15;
                gfx.DrawLine(XPens.DarkRed, margin, y, margin + pageWidth, y);
                y += 15;

                if (shoppingItems.Count == 0)
                {
                    gfx.DrawString("No items needed — you're fully stocked!", bodyFont, XBrushes.Black,
                        new XRect(margin, y, pageWidth, 20), XStringFormats.TopLeft);
                    y += 25;
                }
                else
                {
                    foreach (var item in shoppingItems)
                    {
                        string line = $"{item.TubeName}   —   Need: {item.TotalNeeded}   On Hand: {item.HaveOnHand}   Short by: {Math.Abs(item.Shortage)}";
                        gfx.DrawString(line, bodyFont, XBrushes.Black,
                            new XRect(margin, y, pageWidth, 20), XStringFormats.TopLeft);
                        y += 22;
                    }
                }

                y += 20;

                // Low Stock section
                gfx.DrawString("LOW STOCK — CONSIDER BUYING", headerFont, XBrushes.DarkOrange,
                    new XRect(margin, y, pageWidth, 20), XStringFormats.TopLeft);
                y += 15;
                gfx.DrawLine(XPens.DarkOrange, margin, y, margin + pageWidth, y);
                y += 15;

                if (lowStockItems.Count == 0)
                {
                    gfx.DrawString("No low stock warnings.", bodyFont, XBrushes.Black,
                        new XRect(margin, y, pageWidth, 20), XStringFormats.TopLeft);
                }
                else
                {
                    foreach (var item in lowStockItems)
                    {
                        string line = $"{item.TubeName}   —   Need: {item.TotalNeeded}   On Hand: {item.HaveOnHand}   Surplus: {item.Surplus}";
                        gfx.DrawString(line, bodyFont, XBrushes.Black,
                            new XRect(margin, y, pageWidth, 20), XStringFormats.TopLeft);
                        y += 22;
                    }
                }

                // Save dialog
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF Files|*.pdf";
                saveDialog.FileName = "ShoppingList_" + DateTime.Now.ToString("yyyyMMdd");
                saveDialog.Title = "Save Shopping List";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    document.Save(saveDialog.FileName);
                    System.Diagnostics.Process.Start(saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void pbPreview_Paint(object sender, PaintEventArgs e)
        {
            if (!string.IsNullOrEmpty(hoverDescription))
            {
                // 1. Draw Text with wrapping
                RectangleF textBounds = new RectangleF(20, 20, pbPreview.Width - 100, pbPreview.Height - 100);
                e.Graphics.DrawString(hoverDescription, new Font("Tahoma", 10), Brushes.White, textBounds);
           
            }
        }


        private void btnComponents_MouseEnter(object sender, EventArgs e)
        {

            btnComponents.Cursor = Cursors.Hand;
            btnComponents.FlatStyle = FlatStyle.Flat;
            hoverDescription = "COMPONENTS & PARTS\n\n- Resistors\n- Capacitors\n- ICs\n- Vacuum Tubes";

            // This is the "Magic Button" that forces the text to appear
            pbPreview.Invalidate();
        }

        private void btnComponents_MouseLeave(object sender, EventArgs e)
        {
            hoverDescription = "";
            btnComponents.FlatStyle = FlatStyle.Standard;
            pbPreview.Invalidate();
        }

        
            private void btnEquipment_MouseEnter(object sender, EventArgs e)
        {

            btnComponents.Cursor = Cursors.Hand;
            btnComponents.FlatStyle = FlatStyle.Flat;
            hoverDescription = "EQUIPMENT & TOOLS\n\n- Hand Tools\n- Test Equipment\n- Custom Tools";

            // This is the "Magic Button" that forces the text to appear
            pbPreview.Invalidate();
        }

        private void btnEquipment_MouseLeave(object sender, EventArgs e)
        {
            hoverDescription = "";
            btnComponents.FlatStyle = FlatStyle.Standard;
            pbPreview.Invalidate();
        }



        private void btnProjects_MouseEnter(object sender, EventArgs e)
        {

            btnComponents.Cursor = Cursors.Hand;
            btnComponents.FlatStyle = FlatStyle.Flat;
            hoverDescription = "PROJECTS\n\n- Current Projects\n- Completed Projects\n- Future Projects";

            // This is the "Magic Button" that forces the text to appear
            pbPreview.Invalidate();
        }

        private void btnProjects_MouseLeave(object sender, EventArgs e)
        {
            hoverDescription = "";
            btnComponents.FlatStyle = FlatStyle.Standard;
            pbPreview.Invalidate();
        }


        private void btnShopping_MouseEnter(object sender, EventArgs e)
        {
            isShoppingHover = true;
            btnShopping.Cursor = Cursors.Hand;
            btnShopping.FlatStyle = FlatStyle.Flat;
            hoverDescription = "SHOPPING LIST\n\nGenerates a PDF shopping list of all components you need to buy.\n\nIncludes low stock warnings for items running below twice the required quantity.\n\nSave and print directly from your PDF viewer.";
            pbPreview.Invalidate();
        }

        private void btnShopping_MouseLeave(object sender, EventArgs e)
        {
            isShoppingHover = false;
            hoverDescription = "";
            btnShopping.FlatStyle = FlatStyle.Standard;
            pbPreview.Invalidate();
        }

        private void btnShopping_Click(object sender, MouseEventArgs e)
        {            
                GenerateShoppingListPDF();            
        }

        private void btnProjects_Click(object sender, EventArgs e)
        {
            projectListForm projectList = new projectListForm();
            projectList.FormClosed += (s, args) => this.Show();
            this.Hide();
            projectList.Show();
        }
    }
}
