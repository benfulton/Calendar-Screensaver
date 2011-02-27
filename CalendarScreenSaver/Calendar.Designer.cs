using System.Windows.Forms;
namespace CalendarScreenSaver
{
    partial class Calendar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridCalendar = new System.Windows.Forms.DataGridView();
            this.Sunday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tuesday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wednesday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Thursday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Friday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Saturday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblMonthName = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // gridCalendar
            // 
            this.gridCalendar.AllowUserToAddRows = false;
            this.gridCalendar.AllowUserToDeleteRows = false;
            this.gridCalendar.AllowUserToResizeColumns = false;
            this.gridCalendar.AllowUserToResizeRows = false;
            this.gridCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridCalendar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCalendar.BackgroundColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridCalendar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCalendar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sunday,
            this.Monday,
            this.Tuesday,
            this.Wednesday,
            this.Thursday,
            this.Friday,
            this.Saturday});
            this.gridCalendar.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.LawnGreen;
            dataGridViewCellStyle2.Format = "D";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCalendar.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridCalendar.EnableHeadersVisualStyles = false;
            this.gridCalendar.GridColor = System.Drawing.Color.LawnGreen;
            this.gridCalendar.Location = new System.Drawing.Point(22, 54);
            this.gridCalendar.Name = "gridCalendar";
            this.gridCalendar.ReadOnly = true;
            this.gridCalendar.RowHeadersVisible = false;
            this.gridCalendar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridCalendar.Size = new System.Drawing.Size(749, 441);
            this.gridCalendar.TabIndex = 0;
            this.gridCalendar.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridCalendar_CellFormatting);
            // 
            // Sunday
            // 
            this.Sunday.HeaderText = "Sunday";
            this.Sunday.Name = "Sunday";
            this.Sunday.ReadOnly = true;
            // 
            // Monday
            // 
            this.Monday.HeaderText = "Monday";
            this.Monday.Name = "Monday";
            this.Monday.ReadOnly = true;
            // 
            // Tuesday
            // 
            this.Tuesday.HeaderText = "Tuesday";
            this.Tuesday.Name = "Tuesday";
            this.Tuesday.ReadOnly = true;
            // 
            // Wednesday
            // 
            this.Wednesday.HeaderText = "Wednesday";
            this.Wednesday.Name = "Wednesday";
            this.Wednesday.ReadOnly = true;
            // 
            // Thursday
            // 
            this.Thursday.HeaderText = "Thursday";
            this.Thursday.Name = "Thursday";
            this.Thursday.ReadOnly = true;
            // 
            // Friday
            // 
            this.Friday.HeaderText = "Friday";
            this.Friday.Name = "Friday";
            this.Friday.ReadOnly = true;
            // 
            // Saturday
            // 
            this.Saturday.HeaderText = "Saturday";
            this.Saturday.Name = "Saturday";
            this.Saturday.ReadOnly = true;
            // 
            // lblMonthName
            // 
            this.lblMonthName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMonthName.AutoSize = true;
            this.lblMonthName.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthName.Location = new System.Drawing.Point(292, 13);
            this.lblMonthName.Name = "lblMonthName";
            this.lblMonthName.Size = new System.Drawing.Size(208, 37);
            this.lblMonthName.TabIndex = 1;
            this.lblMonthName.Text = "January 2011";
            // 
            // timer1
            // 
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(793, 507);
            this.Controls.Add(this.lblMonthName);
            this.Controls.Add(this.gridCalendar);
            this.ForeColor = System.Drawing.Color.LawnGreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Calendar";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Calendar_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Calendar_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Calendar_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Calendar_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.gridCalendar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView gridCalendar;
        private DataGridViewTextBoxColumn Sunday;
        private DataGridViewTextBoxColumn Monday;
        private DataGridViewTextBoxColumn Tuesday;
        private DataGridViewTextBoxColumn Wednesday;
        private DataGridViewTextBoxColumn Thursday;
        private DataGridViewTextBoxColumn Friday;
        private DataGridViewTextBoxColumn Saturday;
        private Label lblMonthName;
        private Timer timer1;
    }
}

