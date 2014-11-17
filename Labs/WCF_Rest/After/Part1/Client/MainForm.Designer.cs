namespace Client
{
    partial class MainForm
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
            System.Windows.Forms.Label categoryLabel;
            System.Windows.Forms.Label isbnLabel;
            System.Windows.Forms.Label priceLabel;
            System.Windows.Forms.Label topicLabel;
            System.Windows.Forms.Label webSiteLabel;
            System.Windows.Forms.Label label2;
            this.booksDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.getButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.categoryTextBox = new System.Windows.Forms.TextBox();
            this.isbnTextBox = new System.Windows.Forms.TextBox();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.topicTextBox = new System.Windows.Forms.TextBox();
            this.webSiteTextBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bookInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            categoryLabel = new System.Windows.Forms.Label();
            isbnLabel = new System.Windows.Forms.Label();
            priceLabel = new System.Windows.Forms.Label();
            topicLabel = new System.Windows.Forms.Label();
            webSiteLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bookInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // categoryLabel
            // 
            categoryLabel.AutoSize = true;
            categoryLabel.Location = new System.Drawing.Point(17, 18);
            categoryLabel.Name = "categoryLabel";
            categoryLabel.Size = new System.Drawing.Size(52, 13);
            categoryLabel.TabIndex = 0;
            categoryLabel.Text = "Category:";
            // 
            // isbnLabel
            // 
            isbnLabel.AutoSize = true;
            isbnLabel.Location = new System.Drawing.Point(17, 44);
            isbnLabel.Name = "isbnLabel";
            isbnLabel.Size = new System.Drawing.Size(30, 13);
            isbnLabel.TabIndex = 2;
            isbnLabel.Text = "Isbn:";
            // 
            // priceLabel
            // 
            priceLabel.AutoSize = true;
            priceLabel.Location = new System.Drawing.Point(228, 44);
            priceLabel.Name = "priceLabel";
            priceLabel.Size = new System.Drawing.Size(34, 13);
            priceLabel.TabIndex = 4;
            priceLabel.Text = "Price:";
            // 
            // topicLabel
            // 
            topicLabel.AutoSize = true;
            topicLabel.Location = new System.Drawing.Point(228, 18);
            topicLabel.Name = "topicLabel";
            topicLabel.Size = new System.Drawing.Size(37, 13);
            topicLabel.TabIndex = 8;
            topicLabel.Text = "Topic:";
            // 
            // webSiteLabel
            // 
            webSiteLabel.AutoSize = true;
            webSiteLabel.Location = new System.Drawing.Point(17, 104);
            webSiteLabel.Name = "webSiteLabel";
            webSiteLabel.Size = new System.Drawing.Size(54, 13);
            webSiteLabel.TabIndex = 10;
            webSiteLabel.Text = "Web Site:";
            // 
            // booksDropDown
            // 
            this.booksDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.booksDropDown.DisplayMember = "Title";
            this.booksDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.booksDropDown.FormattingEnabled = true;
            this.booksDropDown.Location = new System.Drawing.Point(58, 17);
            this.booksDropDown.Name = "booksDropDown";
            this.booksDropDown.Size = new System.Drawing.Size(264, 21);
            this.booksDropDown.TabIndex = 0;
            this.booksDropDown.SelectedIndexChanged += new System.EventHandler(this.booksDropDown_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Books:";
            // 
            // getButton
            // 
            this.getButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getButton.Location = new System.Drawing.Point(328, 16);
            this.getButton.Name = "getButton";
            this.getButton.Size = new System.Drawing.Size(39, 23);
            this.getButton.TabIndex = 2;
            this.getButton.Text = "&Get";
            this.getButton.UseVisualStyleBackColor = true;
            this.getButton.Click += new System.EventHandler(this.getButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(374, 15);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(67, 23);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "&Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(15, 197);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(426, 260);
            this.webBrowser1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(categoryLabel);
            this.panel1.Controls.Add(this.categoryTextBox);
            this.panel1.Controls.Add(isbnLabel);
            this.panel1.Controls.Add(this.isbnTextBox);
            this.panel1.Controls.Add(priceLabel);
            this.panel1.Controls.Add(this.priceTextBox);
            this.panel1.Controls.Add(topicLabel);
            this.panel1.Controls.Add(this.topicTextBox);
            this.panel1.Controls.Add(webSiteLabel);
            this.panel1.Controls.Add(this.webSiteTextBox);
            this.panel1.Location = new System.Drawing.Point(15, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 134);
            this.panel1.TabIndex = 5;
            // 
            // categoryTextBox
            // 
            this.categoryTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bookInfoBindingSource, "Category", true));
            this.categoryTextBox.Location = new System.Drawing.Point(77, 15);
            this.categoryTextBox.Name = "categoryTextBox";
            this.categoryTextBox.ReadOnly = true;
            this.categoryTextBox.Size = new System.Drawing.Size(134, 20);
            this.categoryTextBox.TabIndex = 1;
            // 
            // isbnTextBox
            // 
            this.isbnTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bookInfoBindingSource, "Isbn", true));
            this.isbnTextBox.Location = new System.Drawing.Point(77, 41);
            this.isbnTextBox.Name = "isbnTextBox";
            this.isbnTextBox.ReadOnly = true;
            this.isbnTextBox.Size = new System.Drawing.Size(134, 20);
            this.isbnTextBox.TabIndex = 3;
            // 
            // priceTextBox
            // 
            this.priceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.priceTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bookInfoBindingSource, "Price", true));
            this.priceTextBox.Location = new System.Drawing.Point(275, 44);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.ReadOnly = true;
            this.priceTextBox.Size = new System.Drawing.Size(132, 20);
            this.priceTextBox.TabIndex = 5;
            // 
            // topicTextBox
            // 
            this.topicTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.topicTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bookInfoBindingSource, "Topic", true));
            this.topicTextBox.Location = new System.Drawing.Point(275, 15);
            this.topicTextBox.Name = "topicTextBox";
            this.topicTextBox.ReadOnly = true;
            this.topicTextBox.Size = new System.Drawing.Size(132, 20);
            this.topicTextBox.TabIndex = 9;
            // 
            // webSiteTextBox
            // 
            this.webSiteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webSiteTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bookInfoBindingSource, "WebSite", true));
            this.webSiteTextBox.Location = new System.Drawing.Point(77, 101);
            this.webSiteTextBox.Name = "webSiteTextBox";
            this.webSiteTextBox.ReadOnly = true;
            this.webSiteTextBox.Size = new System.Drawing.Size(330, 20);
            this.webSiteTextBox.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 74);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(46, 13);
            label2.TabIndex = 12;
            label2.Text = "Authors:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bookInfoBindingSource, "AuthorsString", true));
            this.textBox1.Location = new System.Drawing.Point(76, 71);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(330, 20);
            this.textBox1.TabIndex = 13;
            // 
            // bookInfoBindingSource
            // 
            this.bookInfoBindingSource.DataSource = typeof(Client.BookInfo);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 469);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.getButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.booksDropDown);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Best .NET Books";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bookInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox booksDropDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button getButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox categoryTextBox;
        private System.Windows.Forms.BindingSource bookInfoBindingSource;
        private System.Windows.Forms.TextBox isbnTextBox;
        private System.Windows.Forms.TextBox priceTextBox;
        private System.Windows.Forms.TextBox topicTextBox;
        private System.Windows.Forms.TextBox webSiteTextBox;
        private System.Windows.Forms.TextBox textBox1;
    }
}

