namespace WinFormsApp3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            label1 = new Label();
            nameBox = new TextBox();
            departmentBox = new TextBox();
            label2 = new Label();
            yearBox = new TextBox();
            label3 = new Label();
            splitContainer1 = new SplitContainer();
            checkedListBox2 = new CheckedListBox();
            checkedListBox1 = new CheckedListBox();
            button3 = new Button();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(30, 227);
            button1.Name = "button1";
            button1.Size = new Size(193, 23);
            button1.TabIndex = 0;
            button1.Text = "Add Group";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 78);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 1;
            label1.Text = "name";
            // 
            // nameBox
            // 
            nameBox.Location = new Point(123, 75);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(100, 23);
            nameBox.TabIndex = 2;
            // 
            // departmentBox
            // 
            departmentBox.Location = new Point(123, 123);
            departmentBox.Name = "departmentBox";
            departmentBox.Size = new Size(100, 23);
            departmentBox.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 126);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 3;
            label2.Text = "department";
            // 
            // yearBox
            // 
            yearBox.Location = new Point(123, 171);
            yearBox.Name = "yearBox";
            yearBox.Size = new Size(100, 23);
            yearBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 174);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 5;
            label3.Text = "start (YYYY)";
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(12, 12);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(checkedListBox2);
            splitContainer1.Panel1.Controls.Add(checkedListBox1);
            splitContainer1.Panel1.Controls.Add(button3);
            splitContainer1.Panel1.Controls.Add(button2);
            splitContainer1.Panel1.Controls.Add(nameBox);
            splitContainer1.Panel1.Controls.Add(yearBox);
            splitContainer1.Panel1.Controls.Add(button1);
            splitContainer1.Panel1.Controls.Add(label3);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(departmentBox);
            splitContainer1.Panel1.Controls.Add(label2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.AutoScroll = true;
            splitContainer1.Panel2.Controls.Add(dataGridView1);
            splitContainer1.Size = new Size(920, 430);
            splitContainer1.SplitterDistance = 250;
            splitContainer1.TabIndex = 7;
            // 
            // checkedListBox2
            // 
            checkedListBox2.FormattingEnabled = true;
            checkedListBox2.Items.AddRange(new object[] { "Student ID", "Student Name", "Address", "Group" });
            checkedListBox2.Location = new Point(133, 304);
            checkedListBox2.Name = "checkedListBox2";
            checkedListBox2.Size = new Size(99, 76);
            checkedListBox2.TabIndex = 9;
            checkedListBox2.ItemCheck += checkedListBox_ItemCheck;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "Group ID", "Group Name", "Department", "Start" });
            checkedListBox1.Location = new Point(28, 304);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(99, 76);
            checkedListBox1.TabIndex = 8;
            checkedListBox1.ItemCheck += checkedListBox_ItemCheck;
            // 
            // button3
            // 
            button3.Location = new Point(133, 386);
            button3.Name = "button3";
            button3.Size = new Size(99, 23);
            button3.TabIndex = 7;
            button3.Text = "Students";
            button3.UseVisualStyleBackColor = true;
            button3.Click += view_button_Click;
            // 
            // button2
            // 
            button2.Location = new Point(28, 386);
            button2.Name = "button2";
            button2.Size = new Size(99, 23);
            button2.TabIndex = 1;
            button2.Text = "Groups";
            button2.UseVisualStyleBackColor = true;
            button2.Click += view_button_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = SystemColors.ActiveCaption;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(660, 427);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            // 
            // timer1
            // 
            timer1.Tick += timer1_tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 450);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Label label1;
        private TextBox nameBox;
        private TextBox departmentBox;
        private Label label2;
        private TextBox yearBox;
        private Label label3;
        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private Button button3;
        private Button button2;
        private CheckedListBox checkedListBox2;
        private CheckedListBox checkedListBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}
