namespace Sample_Billing_App
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRefreshPreview = new System.Windows.Forms.Button();
            this.btnLoadSampleData = new System.Windows.Forms.Button();
            this.btnNewInvoice = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cboPrinters = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvItems = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblItemTotal = new System.Windows.Forms.Label();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.numRate = new System.Windows.Forms.NumericUpDown();
            this.numMRP = new System.Windows.Forms.NumericUpDown();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.cboItems = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cboPaymentType = new System.Windows.Forms.ComboBox();
            this.txtCustomerGSTIN = new System.Windows.Forms.TextBox();
            this.txtCustomerMobile = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMRP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlPreview);
            this.splitContainer1.Size = new System.Drawing.Size(1200, 800);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 800);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRefreshPreview);
            this.groupBox3.Controls.Add(this.btnLoadSampleData);
            this.groupBox3.Controls.Add(this.btnNewInvoice);
            this.groupBox3.Controls.Add(this.btnClear);
            this.groupBox3.Controls.Add(this.btnPrint);
            this.groupBox3.Controls.Add(this.cboPrinters);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtInvoiceNumber);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(460, 130);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Print Options";
            // 
            // btnRefreshPreview
            // 
            this.btnRefreshPreview.Location = new System.Drawing.Point(145, 90);
            this.btnRefreshPreview.Name = "btnRefreshPreview";
            this.btnRefreshPreview.Size = new System.Drawing.Size(120, 30);
            this.btnRefreshPreview.TabIndex = 6;
            this.btnRefreshPreview.Text = "Refresh Preview";
            this.btnRefreshPreview.UseVisualStyleBackColor = true;
            // 
            // btnLoadSampleData
            // 
            this.btnLoadSampleData.Location = new System.Drawing.Point(15, 90);
            this.btnLoadSampleData.Name = "btnLoadSampleData";
            this.btnLoadSampleData.Size = new System.Drawing.Size(120, 30);
            this.btnLoadSampleData.TabIndex = 5;
            this.btnLoadSampleData.Text = "Load Sample Data";
            this.btnLoadSampleData.UseVisualStyleBackColor = true;
            // 
            // btnNewInvoice
            // 
            this.btnNewInvoice.Location = new System.Drawing.Point(350, 60);
            this.btnNewInvoice.Name = "btnNewInvoice";
            this.btnNewInvoice.Size = new System.Drawing.Size(100, 30);
            this.btnNewInvoice.TabIndex = 4;
            this.btnNewInvoice.Text = "New Invoice";
            this.btnNewInvoice.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(240, 60);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear Items";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(130, 60);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print Invoice";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // cboPrinters
            // 
            this.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrinters.FormattingEnabled = true;
            this.cboPrinters.Location = new System.Drawing.Point(80, 25);
            this.cboPrinters.Name = "cboPrinters";
            this.cboPrinters.Size = new System.Drawing.Size(370, 23);
            this.cboPrinters.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Printer:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvItems);
            this.groupBox2.Controls.Add(this.btnRemoveItem);
            this.groupBox2.Location = new System.Drawing.Point(12, 400);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 200);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Invoice Items";
            // 
            // lvItems
            // 
            this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvItems.FullRowSelect = true;
            this.lvItems.GridLines = true;
            this.lvItems.Location = new System.Drawing.Point(15, 25);
            this.lvItems.Name = "lvItems";
            this.lvItems.Size = new System.Drawing.Size(430, 140);
            this.lvItems.TabIndex = 1;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No.";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Item Name";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Description";
            this.columnHeader3.Width = 60;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Qty";
            this.columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "MRP";
            this.columnHeader5.Width = 50;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Rate";
            this.columnHeader6.Width = 50;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Total";
            this.columnHeader7.Width = 60;
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Enabled = false;
            this.btnRemoveItem.Location = new System.Drawing.Point(350, 170);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(95, 25);
            this.btnRemoveItem.TabIndex = 0;
            this.btnRemoveItem.Text = "Remove Item";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblItemTotal);
            this.groupBox1.Controls.Add(this.btnAddItem);
            this.groupBox1.Controls.Add(this.numRate);
            this.groupBox1.Controls.Add(this.numMRP);
            this.groupBox1.Controls.Add(this.numQuantity);
            this.groupBox1.Controls.Add(this.txtItemDescription);
            this.groupBox1.Controls.Add(this.cboItems);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 250);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 140);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Item";
            // 
            // lblItemTotal
            // 
            this.lblItemTotal.AutoSize = true;
            this.lblItemTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblItemTotal.Location = new System.Drawing.Point(350, 110);
            this.lblItemTotal.Name = "lblItemTotal";
            this.lblItemTotal.Size = new System.Drawing.Size(70, 15);
            this.lblItemTotal.TabIndex = 11;
            this.lblItemTotal.Text = "Total: ₹0.00";
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(250, 105);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(95, 25);
            this.btnAddItem.TabIndex = 10;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            // 
            // numRate
            // 
            this.numRate.DecimalPlaces = 2;
            this.numRate.Location = new System.Drawing.Point(350, 75);
            this.numRate.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRate.Name = "numRate";
            this.numRate.Size = new System.Drawing.Size(95, 23);
            this.numRate.TabIndex = 9;
            // 
            // numMRP
            // 
            this.numMRP.DecimalPlaces = 2;
            this.numMRP.Location = new System.Drawing.Point(250, 75);
            this.numMRP.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMRP.Name = "numMRP";
            this.numMRP.Size = new System.Drawing.Size(95, 23);
            this.numMRP.TabIndex = 8;
            // 
            // numQuantity
            // 
            this.numQuantity.DecimalPlaces = 3;
            this.numQuantity.Location = new System.Drawing.Point(150, 75);
            this.numQuantity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(95, 23);
            this.numQuantity.TabIndex = 7;
            this.numQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Location = new System.Drawing.Point(150, 45);
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.Size = new System.Drawing.Size(295, 23);
            this.txtItemDescription.TabIndex = 6;
            // 
            // cboItems
            // 
            this.cboItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItems.FormattingEnabled = true;
            this.cboItems.Location = new System.Drawing.Point(150, 15);
            this.cboItems.Name = "cboItems";
            this.cboItems.Size = new System.Drawing.Size(295, 23);
            this.cboItems.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Rate:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "Description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Item:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "MRP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Quantity:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cboPaymentType);
            this.groupBox4.Controls.Add(this.txtCustomerGSTIN);
            this.groupBox4.Controls.Add(this.txtCustomerMobile);
            this.groupBox4.Controls.Add(this.txtCustomerName);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(12, 120);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(460, 120);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Customer Details";
            // 
            // cboPaymentType
            // 
            this.cboPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentType.FormattingEnabled = true;
            this.cboPaymentType.Location = new System.Drawing.Point(350, 85);
            this.cboPaymentType.Name = "cboPaymentType";
            this.cboPaymentType.Size = new System.Drawing.Size(95, 23);
            this.cboPaymentType.TabIndex = 7;
            // 
            // txtCustomerGSTIN
            // 
            this.txtCustomerGSTIN.Location = new System.Drawing.Point(150, 85);
            this.txtCustomerGSTIN.Name = "txtCustomerGSTIN";
            this.txtCustomerGSTIN.Size = new System.Drawing.Size(150, 23);
            this.txtCustomerGSTIN.TabIndex = 6;
            // 
            // txtCustomerMobile
            // 
            this.txtCustomerMobile.Location = new System.Drawing.Point(150, 55);
            this.txtCustomerMobile.Name = "txtCustomerMobile";
            this.txtCustomerMobile.Size = new System.Drawing.Size(295, 23);
            this.txtCustomerMobile.TabIndex = 5;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(150, 25);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(295, 23);
            this.txtCustomerName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mobile:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "GSTIN:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(280, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 15);
            this.label10.TabIndex = 3;
            this.label10.Text = "Payment:";
            // 
            // pnlPreview
            // 
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Location = new System.Drawing.Point(0, 0);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(696, 800);
            this.pnlPreview.TabIndex = 0;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(350, 25);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(100, 23);
            this.txtInvoiceNumber.TabIndex = 3;
            this.txtInvoiceNumber.Text = "1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(280, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 15);
            this.label11.TabIndex = 2;
            this.label11.Text = "Inv. No.:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TUKZO ABC - Invoice Printing System";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMRP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel panel1;
        private GroupBox groupBox4;
        private TextBox txtCustomerName;
        private Label label2;
        private Label label1;
        private TextBox txtCustomerMobile;
        private ComboBox cboPaymentType;
        private TextBox txtCustomerGSTIN;
        private Label label9;
        private Label label10;
        private GroupBox groupBox1;
        private ComboBox cboItems;
        private Label label5;
        private Label label4;
        private Label label3;
        private TextBox txtItemDescription;
        private Label label6;
        private NumericUpDown numQuantity;
        private NumericUpDown numMRP;
        private NumericUpDown numRate;
        private Label label7;
        private Button btnAddItem;
        private Label lblItemTotal;
        private GroupBox groupBox2;
        private ListView lvItems;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private Button btnRemoveItem;
        private GroupBox groupBox3;
        private ComboBox cboPrinters;
        private Label label8;
        private Button btnPrint;
        private Button btnClear;
        private Button btnNewInvoice;
        private Button btnLoadSampleData;
        private Button btnRefreshPreview;
        private Panel pnlPreview;
        private TextBox txtInvoiceNumber;
        private Label label11;
    }
}
