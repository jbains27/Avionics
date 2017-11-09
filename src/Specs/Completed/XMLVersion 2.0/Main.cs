// Decompiled with JetBrains decompiler
// Type: WindowsFormsApplication2.Form1
// Assembly: WindowsFormsApplication2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F26F28AE-C020-4D78-9CDC-CDBBF01F0617
// Assembly location: C:\Users\Sumit\Desktop\Please change the extension to .exe from .exe1 (1)\WindowsFormsApplication2.exe

using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public class Main : Form
    {
        private string sd = "";
        private string ed = "";
        public string fpath = "";
        public string fpath_new = "";
        private IContainer components;
        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
        private OpenFileDialog openFileDialog1;
        private Button button2;
        private TextBox textBox3;
        private SaveFileDialog saveFileDialog1;
        private Label label3;

        public Main()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.openFileDialog1 = new OpenFileDialog();
            this.button2 = new Button();
            this.textBox3 = new TextBox();
            this.saveFileDialog1 = new SaveFileDialog();
            this.label3 = new Label();
            this.SuspendLayout();
            this.button1.Location = new Point(421, 189);
            this.button1.Name = "button1";
            this.button1.Size = new Size(112, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate XML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.textBox1.Location = new Point(147, 79);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox2.Location = new Point(147, 130);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(100, 20);
            this.textBox2.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(35, 79);
            this.label1.Name = "label1";
            this.label1.Size = new Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start Date";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(38, 133);
            this.label2.Name = "label2";
            this.label2.Size = new Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "End Date";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new CancelEventHandler(this.openFileDialog1_FileOk);
            this.button2.Location = new Point(233, 189);
            this.button2.Name = "button2";
            this.button2.Size = new Size(100, 28);
            this.button2.TabIndex = 5;
            this.button2.Text = "Select Database";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.textBox3.Location = new Point(41, 194);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(186, 20);
            this.textBox3.TabIndex = 6;
            this.saveFileDialog1.FileOk += new CancelEventHandler(this.saveFileDialog1_FileOk);
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Mangal", 13f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label3.Location = new Point(113, 9);
            this.label3.Name = "label3";
            this.label3.Size = new Size(304, 30);
            this.label3.TabIndex = 7;
            this.label3.Text = "Shop Finding Details - Avionics";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            //this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(549, 261);
            this.Controls.Add((Control)this.label3);
            this.Controls.Add((Control)this.textBox3);
            this.Controls.Add((Control)this.button2);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.textBox2);
            this.Controls.Add((Control)this.textBox1);
            this.Controls.Add((Control)this.button1);
            this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.Name = "Xml Generation";
            this.Text = "Xml Generation";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "" && this.textBox2.Text == "")
            {
                MessageBox.Show("Please enter a valid Date");
            }
            if (this.textBox3.Text == "")
            {
                MessageBox.Show("Select Database File");
            }
            else
            {
                try
                {
                    if (Convert.ToDateTime(this.textBox1.Text) < Convert.ToDateTime(this.textBox2.Text))
                    {
                        this.GenerateXML(Convert.ToDateTime(this.textBox1.Text), Convert.ToDateTime(this.textBox2.Text));
                    }
                    else
                    {
                        MessageBox.Show("Please Check Start and End Dates");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        internal void GenerateXML(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                this.sd = string.Format("{0:MM/dd/yyyy}", (object)StartDate);
                this.ed = string.Format("{0:MM/dd/yyyy}", (object)EndDate);
                OleDbConnection oleDbConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source='" + this.fpath + "';Jet OLEDB:Database Password=''");
                oleDbConnection.Open();

                //OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT DISTINCT OprCode as OPR,Customer FROM tblRepairHistory WHERE SerialNumber LIKE 'cj%' AND OprCode not in(NULL,'0') AND DateReceived Between #" + this.sd + "# AND #" + this.ed + "# ", oleDbConnection).ExecuteReader();
                //new OleDbDataAdapter(new OleDbCommand("SELECT DISTINCT OprCode as OPR,Customer FROM tblRepairHistory WHERE SerialNumber LIKE 'cj%' AND OprCode not in(NULL,'0') AND DateReceived Between #" + this.sd + "# AND #" + this.ed + "# ", oleDbConnection)).Fill(new DataSet());
                var command = "SELECT DISTINCT OprCode as OPR,Customer FROM tblRepairHistory WHERE ((SerialNumber LIKE 'cj%') OR (SerialNumber LIKE 'ck%')) AND OprCode not in(NULL,'0') AND DateReturned Between #" + this.sd + "# AND #" + this.ed + "# ";

                OleDbDataReader oleDbDataReader = new OleDbCommand(command, oleDbConnection).ExecuteReader();
                new OleDbDataAdapter(new OleDbCommand(command, oleDbConnection)).Fill(new DataSet());

                StringBuilder stringBuilder = new StringBuilder();
                if (oleDbDataReader.HasRows)
                {
                    stringBuilder.Append("<?xml version='1.0' encoding='UTF-8' ?> ");
                    stringBuilder.Append("<ATA_InformationSet xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:noNamespaceSchemaLocation='Modular_Schema\\ATA_InformationSet.xsd'  id='R2009.1' version='1.0'>");
                    while (oleDbDataReader.Read())
                    {
                        stringBuilder.Append("<ReliabilityData>");
                        stringBuilder.Append("<ShopFindings version='2.00'>");
                        stringBuilder.Append("<HDR_Segment>");
                        stringBuilder.Append("<CHG>N</CHG>");
                        stringBuilder.Append("<ROC>10933</ROC>");
                        stringBuilder.Append("<RDT>" + StartDate.ToString("yyyy-MM-dd") + "</RDT>");
                        stringBuilder.Append("<RSD>" + EndDate.ToString("yyyy-MM-dd") + "</RSD>");
                        stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + oleDbDataReader.GetValue(0).ToString() + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                        stringBuilder.Append("<RON>Avionic Instruments LLC</RON>");
                        if (oleDbDataReader.GetValue(0).ToString() == "ZZZZZ")
                            stringBuilder.Append("<WHO>" + oleDbDataReader.GetValue(1).ToString() + "</WHO>");
                        stringBuilder.Append("</HDR_Segment>");
                        stringBuilder.Append(this.GenerateShopFindingsDetails(oleDbDataReader.GetValue(0).ToString(), oleDbDataReader.GetValue(1).ToString(), oleDbConnection));
                        stringBuilder.Append("</ShopFindings>");
                        stringBuilder.Append("</ReliabilityData>");
                    }
                    stringBuilder.Append("</ATA_InformationSet>");
                }
                string str = "report_" + DateTime.Now.Date.ToString("MM-dd-yy") + ".xml";
                Directory.CreateDirectory(".\\reports");
                if (File.Exists(".\\reports\\" + str))
                {
                    int num = (int)this.saveFileDialog1.ShowDialog();
                    TextWriter textWriter = (TextWriter)new StreamWriter(this.fpath_new);
                    Directory.GetParent(this.fpath_new).ToString();
                    Process.Start(Directory.GetParent(this.fpath_new).ToString());
                    textWriter.Write(stringBuilder.ToString());
                    textWriter.Close();
                }
                else
                {
                    TextWriter textWriter = (TextWriter)new StreamWriter(".\\reports\\" + str);
                    Process.Start(".\\reports");
                    textWriter.Write(stringBuilder.ToString());
                    textWriter.Close();
                }
                oleDbConnection.Close();
                Application.Exit();
            }
            catch (OleDbException ex)
            {
                throw ex;
            }
        }

        internal string GenerateShopFindingsDetails(string OprCode, string customer, OleDbConnection conn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (OprCode != string.Empty)
            {
                OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT * From tblRepairHistory WHERE OprCode = '" + OprCode + "' AND Customer = '" + customer + "'AND (SerialNumber LIKE 'cj%' OR SerialNumber LIKE 'ck%') AND DateReturned Between #" + this.sd + "# AND #" + this.ed + "#  Order by DateReturned DESC", conn).ExecuteReader();
                new OleDbDataAdapter(new OleDbCommand("SELECT * From tblRepairHistory WHERE OprCode = '" + OprCode + "' AND Customer = '" + customer + "' AND (SerialNumber LIKE 'cj%' OR SerialNumber LIKE 'ck%') AND DateReturned Between #" + this.sd + "# AND #" + this.ed + "#  Order by DateReturned DESC", conn)).Fill(new DataSet());
                if (oleDbDataReader.HasRows)
                {
                    while (oleDbDataReader.Read())
                    {
                        stringBuilder.Append("<ShopFindingsDetails>");
                        stringBuilder.Append(this.GenerateRCS(oleDbDataReader.GetValue(0).ToString(), conn));
                        stringBuilder.Append(this.GenerateSAS(oleDbDataReader.GetValue(0).ToString(), conn));
                        stringBuilder.Append(this.GenerateRLS(oleDbDataReader.GetValue(0).ToString(), conn));
                        stringBuilder.Append(this.GenerateAID(oleDbDataReader.GetValue(0).ToString(), conn));
                        stringBuilder.Append("</ShopFindingsDetails>");
                    }
                }
            }
            return stringBuilder.ToString();
        }

        internal string GenerateAID(string RANumber, OleDbConnection conn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT MFR as MFR , AircraftModel as AMC,AircraftMFRSN as AIN From tblRepairHistory WHERE RANumber = " + RANumber, conn).ExecuteReader();
            if (oleDbDataReader.HasRows)
            {
                while (oleDbDataReader.Read())
                {
                    if (oleDbDataReader.GetValue(0).ToString() == "" || oleDbDataReader.GetValue(1).ToString() == "" || oleDbDataReader.GetValue(2).ToString() == "")
                    {
                        stringBuilder.ToString();
                    }
                    else
                    {
                        stringBuilder.Append("<AID_Segment>");
                        stringBuilder.Append("<MFR>" + oleDbDataReader.GetValue(0).ToString() + "</MFR>");
                        stringBuilder.Append("<" + oleDbDataReader.GetName(1).ToString() + ">" + oleDbDataReader.GetValue(1).ToString() + "</" + oleDbDataReader.GetName(1).ToString() + ">");
                        stringBuilder.Append("<" + oleDbDataReader.GetName(2).ToString() + ">" + oleDbDataReader.GetValue(2).ToString() + "</" + oleDbDataReader.GetName(2).ToString() + ">");
                        stringBuilder.Append("</AID_Segment>");
                    }
                }
            }
            return stringBuilder.ToString();
        }

        internal string GenerateEID(string RANumber, OleDbConnection conn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT AircraftEngType as AET,EngPosCode as EPC, AircraftEngMode as AEM From tblRepairHistory WHERE RANumber = " + RANumber, conn).ExecuteReader();
            if (oleDbDataReader.HasRows)
            {
                stringBuilder.Append("<EID_Segment>");
                while (oleDbDataReader.Read())
                {
                    stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + oleDbDataReader.GetValue(0).ToString() + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(1).ToString() + ">" + oleDbDataReader.GetValue(1).ToString() + "</" + oleDbDataReader.GetName(1).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(2).ToString() + ">" + oleDbDataReader.GetValue(2).ToString() + "</" + oleDbDataReader.GetName(2).ToString() + ">");
                }
                stringBuilder.Append("</EID_Segment>");
            }
            return stringBuilder.ToString();
        }

        internal string GenerateAPI(string RANumber, OleDbConnection conn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT AircraftEngType as AET,APUSN as EMS From tblRepairHistory WHERE RANumber = " + RANumber, conn).ExecuteReader();
            if (oleDbDataReader.HasRows)
            {
                stringBuilder.Append("<API_Segment>");
                while (oleDbDataReader.Read())
                {
                    stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + oleDbDataReader.GetValue(0).ToString() + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(1).ToString() + ">" + oleDbDataReader.GetValue(1).ToString() + "</" + oleDbDataReader.GetName(1).ToString() + ">");
                }
                stringBuilder.Append("</API_Segment>");
            }
            return stringBuilder.ToString();
        }

        internal string GenerateRCS(string RANumber, OleDbConnection conn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT tblRepairHistory.RANumber as SFI, tblRepairHistory.DateReceived as MRD, tblNewUnits.PartNumber as MPN, tblRepairHistory.SerialNumber as SER,tblRepairHistory.SupRemType as RRC, tblRepairHistory.FFFCD as FFC, tblRepairHistory.FFICD as FFI,tblRepairHistory.FFCRRCD as FCR, tblRepairHistory.FFCAMCD as FAC, tblRepairHistory.FFCABCD as FBC, tblRepairHistory.HSFCD as FHS, tblRepairHistory.RemovedPMODL as PML  From tblRepairHistory, tblNewUnits WHERE tblRepairHistory.SerialNumber = tblNewUnits.SerialNumber AND tblRepairHistory.RANumber = " + RANumber, conn).ExecuteReader();
            if (oleDbDataReader.HasRows)
            {
                stringBuilder.Append("<RCS_Segment>");
                while (oleDbDataReader.Read())
                {
                    stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + oleDbDataReader.GetValue(0).ToString() + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(1).ToString() + ">" + Convert.ToDateTime(oleDbDataReader.GetValue(1).ToString()).ToString("yyyy-MM-dd") + "</" + oleDbDataReader.GetName(1).ToString() + ">");
                    stringBuilder.Append("<MFR>10933</MFR>");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(2).ToString() + ">" + oleDbDataReader.GetValue(2).ToString() + "</" + oleDbDataReader.GetName(2).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(3).ToString() + ">" + oleDbDataReader.GetValue(3).ToString() + "</" + oleDbDataReader.GetName(3).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(4).ToString() + ">" + oleDbDataReader.GetValue(4).ToString() + "</" + oleDbDataReader.GetName(4).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(5).ToString() + ">" + oleDbDataReader.GetValue(5).ToString() + "</" + oleDbDataReader.GetName(5).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(6).ToString() + ">" + oleDbDataReader.GetValue(6).ToString() + "</" + oleDbDataReader.GetName(6).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(7).ToString() + ">" + oleDbDataReader.GetValue(7).ToString() + "</" + oleDbDataReader.GetName(7).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(8).ToString() + ">NA</" + oleDbDataReader.GetName(8).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(9).ToString() + ">NA</" + oleDbDataReader.GetName(9).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(10).ToString() + ">" + oleDbDataReader.GetValue(10).ToString() + "</" + oleDbDataReader.GetName(10).ToString() + ">");
                    if (oleDbDataReader.GetValue(11).ToString() != string.Empty)
                    {
                        stringBuilder.Append("<" + oleDbDataReader.GetName(11).ToString() + ">" + oleDbDataReader.GetValue(11).ToString() + "</" + oleDbDataReader.GetName(11).ToString() + ">");
                    }
                }
                stringBuilder.Append("</RCS_Segment>");
            }
            return stringBuilder.ToString();
        }

        internal string GenerateSAS(string RANumber, OleDbConnection conn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT PartsChanged as [INT], SRLCD as SHL, SFAIND as RFI, ModsIncSvcBulleting as MAT From tblRepairHistory WHERE RANumber = " + RANumber, conn).ExecuteReader();
            if (oleDbDataReader.HasRows)
            {
                stringBuilder.Append("<SAS_Segment>");
                while (oleDbDataReader.Read())
                {
                    stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + oleDbDataReader.GetValue(0).ToString() + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(1).ToString() + ">R2</" + oleDbDataReader.GetName(1).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(2).ToString() + ">1</" + oleDbDataReader.GetName(2).ToString() + ">");
                    if (oleDbDataReader.GetValue(3).ToString() != string.Empty)
                    {
                        stringBuilder.Append("<" + oleDbDataReader.GetName(3).ToString() + ">" + oleDbDataReader.GetValue(3).ToString() + "</" + oleDbDataReader.GetName(3).ToString() + ">");
                    }
                }
                stringBuilder.Append("</SAS_Segment>");
            }
            return stringBuilder.ToString();
        }

        internal string GenerateSUS(string RANumber, OleDbConnection conn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT tblRepairHistory.DateReturned as SHD, tblRepairHistory.ShippedMFRPN  as MPN, tblRepairHistory.ShippedMFRSN as SER, tblRepairHistory.ShippedPMODL as PML  From tblRepairHistory WHERE RANumber = " + RANumber, conn).ExecuteReader();
            if (oleDbDataReader.HasRows)
            {
                stringBuilder.Append("<SUS_Segment>");
                while (oleDbDataReader.Read())
                {
                    if (oleDbDataReader.GetValue(0).ToString() != string.Empty)
                        stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + Convert.ToDateTime(oleDbDataReader.GetValue(0).ToString()).ToString("yyyy-MM-dd") + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                    else
                        stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + DateTime.Now.ToString("yyyy-MM-dd") + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                    stringBuilder.Append("<MFR>10933</MFR>");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(1).ToString() + ">" + oleDbDataReader.GetValue(1).ToString() + "</" + oleDbDataReader.GetName(1).ToString() + ">");
                    stringBuilder.Append("<" + oleDbDataReader.GetName(2).ToString() + ">" + oleDbDataReader.GetValue(2).ToString() + "</" + oleDbDataReader.GetName(2).ToString() + ">");
                    if (oleDbDataReader.GetValue(3).ToString() != string.Empty)
                    {
                        stringBuilder.Append("<" + oleDbDataReader.GetName(3).ToString() + ">" + oleDbDataReader.GetValue(3).ToString() + "</" + oleDbDataReader.GetName(3).ToString() + ">");
                    }
                }
                stringBuilder.Append("</SUS_Segment>");
            }
            return stringBuilder.ToString();
        }

        internal string GenerateRLS(string RANumber, OleDbConnection conn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT tblNewUnits.PartNumber as MPN,tblNewUnits.SerialNumber as SER,tblRepairHistory.RemovalDate as RED From tblRepairHistory, tblNewUnits WHERE tblRepairHistory.SerialNumber = tblNewUnits.SerialNumber AND RANumber = " + RANumber, conn).ExecuteReader();
            if (oleDbDataReader.HasRows)
            {
                while (oleDbDataReader.Read())
                {
                    if (oleDbDataReader.GetValue(2).ToString() == "")
                    {
                        stringBuilder.ToString();
                    }
                    else
                    {
                        stringBuilder.Append("<RLS_Segment>");
                        stringBuilder.Append("<MFR>" + (object)10933 + "</MFR>");
                        stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + oleDbDataReader.GetValue(0).ToString() + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                        stringBuilder.Append("<" + oleDbDataReader.GetName(1).ToString() + ">" + oleDbDataReader.GetValue(1).ToString() + "</" + oleDbDataReader.GetName(1).ToString() + ">");
                        stringBuilder.Append("<" + oleDbDataReader.GetName(2).ToString() + ">" + Convert.ToDateTime(oleDbDataReader.GetValue(2).ToString()).ToString("yyyy-MM-dd") + "</" + oleDbDataReader.GetName(2).ToString() + ">");
                        stringBuilder.Append("</RLS_Segment>");
                    }
                }
            }
            return stringBuilder.ToString();
        }

        internal string GenerateLNK(string RANumber, OleDbConnection conn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT RANumber as RTI From tblRepairHistory WHERE RANumber = " + RANumber, conn).ExecuteReader();
            if (oleDbDataReader.HasRows)
            {
                stringBuilder.Append("<LNK_Segment>");
                while (oleDbDataReader.Read())
                    stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + oleDbDataReader.GetValue(0).ToString() + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                stringBuilder.Append("</LNK_Segment>");
            }
            return stringBuilder.ToString();
        }

        internal string GenerateATT(string RANumber, OleDbConnection conn)
        {
            StringBuilder stringBuilder = new StringBuilder();
            OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT TCRCD as TRF, ODT From tblRepairHistory WHERE RANumber = " + RANumber, conn).ExecuteReader();
            if (oleDbDataReader.HasRows)
            {
                stringBuilder.Append("<ATT_Segment>");
                while (oleDbDataReader.Read())
                {
                    if (oleDbDataReader.GetValue(0).ToString() != string.Empty)
                    {
                        stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + oleDbDataReader.GetValue(0).ToString() + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                        stringBuilder.Append("<" + oleDbDataReader.GetName(1).ToString() + ">" + oleDbDataReader.GetValue(1).ToString() + "</" + oleDbDataReader.GetName(1).ToString() + ">");
                    }
                    else
                        stringBuilder.Append("<" + oleDbDataReader.GetName(0).ToString() + ">" + oleDbDataReader.GetValue(0).ToString() + "</" + oleDbDataReader.GetName(0).ToString() + ">");
                }
                stringBuilder.Append("</ATT_Segment>");
            }
            return stringBuilder.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Please select the file";
            this.openFileDialog1.InitialDirectory = "C:";
            //this.openFileDialog1.Filter = "Microsoft Office Access Database(*.mdb)|*.accdb";
            this.openFileDialog1.Filter = "Access 2007 (*.accdb)|*accdb";
            this.openFileDialog1.FileName = string.Empty;
            int num = (int)this.openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Stream stream = this.openFileDialog1.OpenFile();
            if (stream == null)
                return;
            this.textBox3.Text = this.openFileDialog1.FileName.ToString();
            this.fpath = this.textBox3.Text;
            stream.Close();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            this.fpath_new = this.saveFileDialog1.FileName + ".xml";
        }
    }
}
