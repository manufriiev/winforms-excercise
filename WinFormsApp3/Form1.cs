using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Threading;
using System.Data;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        Task curSave;
        public const string GROUPS = "groups";
        public ViewTypes view = new ViewTypes();

        public AsyncQueue<Task> tasks = new AsyncQueue<Task>();
        public Queue<int> confirmedRows = new Queue<int>();

        public Dictionary<string, int> groups = new Dictionary<string, int>();

        object oldValue;
        public Form1()
        {
            InitializeComponent();
        }

        int confirmedRow = -1;

        private void button1_Click(object sender, EventArgs e)
        {
            var name = nameBox.Text;
            var department = departmentBox.Text;
            if (name != null && department != null && yearBox.Text != null)
            {
                var year = int.Parse(yearBox.Text);
                string query = "INSERT INTO unigroups(name, department, start) VALUES (@name, @department, @start)";
                using (SqlConnection conn = new DatabaseConnection().getConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@department", department);
                        cmd.Parameters.AddWithValue("@start", year);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            nameBox.Text = null;
            departmentBox.Text = null;
            yearBox.Text = null;
            view.cur = ViewTypes.GROUPS;
            UpdateGrid();
            MessageBox.Show("Success!");
        }

        void UpdateGrid()
        {
            if (!tasks.IsEmpty)
            {
                MessageBox.Show("sowwy wait a bit");
                return;
            }
            confirmedRows.Clear();
            using (SqlConnection conn = new DatabaseConnection().getConnection())
            {
                conn.Open();

                string query = view.cur == ViewTypes.GROUPS
                    ? "SELECT * FROM unigroups"
                    : "SELECT id, CONCAT(lastname, ', ', firstname) AS [name], address, group_id, [group] FROM students_groups;";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.Columns.Clear();
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = dt;

                    if (view.cur == ViewTypes.GROUPS)
                    {
                        AddColumn("id", "Group ID", 70);
                        AddColumn("name", "Group Name", 150);
                        AddColumn("department", "Department", 150);
                        AddColumn("start", "Start");
                        AddSaveButton();
                        dataGridView1.Columns["id"].Visible = checkedListBox1.GetItemChecked(0);
                    }
                    else
                    {
                        updateGroupsDict();
                        AddColumn("id", "Student ID", 70);
                        AddColumn("name", "Student Name", 150);
                        AddColumn("address", "Address", 150);
                        AddGroupSelectionColumn("group", "Group");
                        AddSaveButton();
                        dataGridView1.Columns["id"].Visible = checkedListBox2.GetItemChecked(0);
                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Cells["savebutton"].Value = "Save";
                    }
                }
            }
        }
        void AddColumn(string property, string header, int width = 100)
        {
            CheckedListBox relevant = (view.cur == ViewTypes.GROUPS) ? checkedListBox1 : checkedListBox2;
            int index = relevant.FindString(header);
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Width = width,
                Name = property,
                HeaderText = header,
                DataPropertyName = property,
                Visible = index != -1 ? relevant.GetItemChecked(index) : true
            });
        }
        void AddGroupSelectionColumn(string property, string header, bool visible = true)
        {
            CheckedListBox relevant = (view.cur == ViewTypes.GROUPS) ? checkedListBox1 : checkedListBox2;
            int index = relevant.FindString(header);
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn
            {
                Name = property,
                HeaderText = header,
                DataPropertyName = "group_id",
                DataSource = groups.Select(g => new
                {
                    Id = g.Value,
                    Name = g.Key
                }).ToList(),
                DisplayMember = "Name",
                ValueMember = "Id",
                ValueType = typeof(int),
                Visible = index != -1 ? relevant.GetItemChecked(index) : true
            };

            dataGridView1.Columns.Add(column);
        }
        private void AddSaveButton()
        {
            var save = new DataGridViewButtonColumn
            {
                Name = "savebutton",
                HeaderText = "Save",
                Text = "Save",
                UseColumnTextForButtonValue = false
            };

            dataGridView1.Columns.Add(save);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
            for (int i = 1; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, true);
            }
            view.cur = ViewTypes.GROUPS;
            UpdateGrid();
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                DataGridViewRow row = senderGrid.Rows[e.RowIndex];
                var values = row.Cells.Cast<DataGridViewCell>()
                .ToDictionary(c => c.OwningColumn.Name, c => c.Value);

                string viewAtClick = view.cur;

                senderGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Salmon;
                var buttonCell = senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];

                buttonCell.Value = "Saving...";
                buttonCell.ReadOnly = true;
                senderGrid.InvalidateCell(buttonCell);

                Task task = new Task(() =>
                {
                    Save(values, viewAtClick);
                });

                tasks.Enqueue(task);
                task.Start();
                await task;
                await tasks.DequeueAsync();
                senderGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                timer1.Interval = 2000;
                timer1.Enabled = true;
                buttonCell.Value = "Save";
                buttonCell.ReadOnly = false;
                confirmedRows.Enqueue(e.RowIndex);
            }
        }
        private int fibbonachi(int i)
        {
            if (i <= 1) return 1;
            return fibbonachi(i - 1) + fibbonachi(i - 2);
        }
        private void Save(Dictionary<string, object> values, String viewAtClick)
        {
            fibbonachi(45);

            if (viewAtClick == ViewTypes.GROUPS)
            {
                var id = values["id"];
                var name = values["name"];
                var department = values["department"];
                var yearstr = values["start"];

                if (name != null && department != null && yearstr != null)
                {
                    string query = "UPDATE unigroups " +
                        "SET name = @name, department = @department, start = @start where id = @id;";

                    using (SqlConnection conn = new DatabaseConnection().getConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@department", department);
                            cmd.Parameters.AddWithValue("@start", yearstr);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                string query = "UPDATE students_groups " +
                    "SET LastName = @lastname, FirstName = @firstname," +
                    "Address = @address, group_id = @group_id WHERE id = @id;";

                using (SqlConnection conn = new DatabaseConnection().getConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        string[] namesplit = (values["name"]?.ToString() ?? "").Split(',');

                        cmd.Parameters.AddWithValue("@lastname", namesplit[0].Trim());
                        cmd.Parameters.AddWithValue("@firstname", namesplit[1].Trim());
                        cmd.Parameters.AddWithValue("@address", values["address"]);
                        cmd.Parameters.AddWithValue("@group_id", values["group"]);
                        cmd.Parameters.AddWithValue("@id", values["id"]);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private async void timer1_tick(object sender, EventArgs e)
        {
            if (confirmedRows.Count > 0)
            {
                int rowToClear = confirmedRows.Dequeue();
                dataGridView1.Rows[rowToClear].DefaultCellStyle.BackColor = Color.White;
            }
            if (confirmedRows.Count == 0)
            {
                timer1.Enabled = false;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (view.cur == ViewTypes.STUDENTS
                && senderGrid.Columns[e.ColumnIndex].Name == "name"
                && (senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "").Split(',').Length != 2)
            {
                senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldValue != null ? oldValue : null;
                MessageBox.Show("Please enter name in format \"Fname, Lname\"");
                oldValue = null;
                return;
            }

            senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightYellow;
        }

        public void updateGroupsDict()
        {
            using (SqlConnection conn = new DatabaseConnection().getConnection())
            {
                conn.Open();
                string query = "SELECT id, [name] FROM unigroups;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        groups[reader.GetString("name")] = reader.GetInt32("id");
                    }
                }
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            Button sendbutton = (Button)sender;

            view.cur = sendbutton.Text == "Groups" ? ViewTypes.GROUPS : ViewTypes.STUDENTS;
            UpdateGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            view.cur = ViewTypes.STUDENTS;
            UpdateGrid();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            oldValue = senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //CheckedListBox checkedListBox = (CheckedListBox)sender;

            //string columnName = checkedListBox.Items[e.Index].ToString();

            //if (dataGridView1.Columns.Contains(columnName))
            //{
            //    dataGridView1.Columns[columnName].Visible =
            //        e.NewValue == CheckState.Checked;
            //}
        }

    }

    public class ViewTypes
    {
        public const string GROUPS = "groups";
        public const string STUDENTS = "students";
        public string cur = GROUPS;
    }
}