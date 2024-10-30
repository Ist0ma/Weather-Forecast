namespace Weather_Forecast
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
            CitySelectTextBox = new TextBox();
            label1 = new Label();
            SearchResultlistBox = new ListBox();
            CitiesListPanel = new Panel();
            SuspendLayout();
            // 
            // CitySelectTextBox
            // 
            CitySelectTextBox.Location = new Point(86, 12);
            CitySelectTextBox.Name = "CitySelectTextBox";
            CitySelectTextBox.Size = new Size(177, 23);
            CitySelectTextBox.TabIndex = 0;
            CitySelectTextBox.TextChanged += CitySelectTextBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 1;
            label1.Text = "Select city";
            // 
            // SearchResultlistBox
            // 
            SearchResultlistBox.BorderStyle = BorderStyle.None;
            SearchResultlistBox.FormattingEnabled = true;
            SearchResultlistBox.ItemHeight = 15;
            SearchResultlistBox.Location = new Point(86, 32);
            SearchResultlistBox.Name = "SearchResultlistBox";
            SearchResultlistBox.Size = new Size(177, 150);
            SearchResultlistBox.TabIndex = 2;
            SearchResultlistBox.Visible = false;
            SearchResultlistBox.SelectedValueChanged += SearchResultlistBox_SelectedValueChanged;
            // 
            // CitiesListPanel
            // 
            CitiesListPanel.AutoSize = true;
            CitiesListPanel.Location = new Point(4, 50);
            CitiesListPanel.Name = "CitiesListPanel";
            CitiesListPanel.Size = new Size(300, 100);
            CitiesListPanel.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(307, 211);
            Controls.Add(CitiesListPanel);
            Controls.Add(SearchResultlistBox);
            Controls.Add(label1);
            Controls.Add(CitySelectTextBox);
            MinimumSize = new Size(300, 250);
            Name = "Form1";
            Text = "WeatherForecast";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox CitySelectTextBox;
        private Label label1;
        private ListBox SearchResultlistBox;
        private Panel CitiesListPanel;
    }
}
