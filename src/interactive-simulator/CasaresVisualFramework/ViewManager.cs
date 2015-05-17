using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace CasaresVisualFramework
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ViewManager : System.Windows.Forms.Form
	{
		private ArrayList Spaces = new ArrayList();
		public Space Unemployed;
		public Space Inactive;
		public Space OpenedCompanies;
		private System.ComponentModel.IContainer components;

		CasaresModel.Main mainModel;
		ArrayList companyViews = new ArrayList();
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label lblProfit;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TrackBar trackDismissals;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label lblDismissals;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TrackBar trackReservation;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label lblDecay;
		private System.Windows.Forms.TrackBar trackDiscouragement;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label lblDiscouragement;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TrackBar trackProfit;
		ArrayList personViews = new ArrayList();
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label lblActivity;
		private WindowsApplication5.PocketChart pocketActivity;
		private WindowsApplication5.PocketChart pocketUnemployment;
		private WindowsApplication5.PocketChart pocketCompanies;
		private WindowsApplication5.PocketChart pocketProfit;
		private System.Windows.Forms.Label lblCompaniesCount;
		private System.Windows.Forms.Label lblProfitChart;
		private WindowsApplication5.PocketChart pocketSalary;
		private WindowsApplication5.PocketChart pocketIncome;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button cmdPause;
		private System.Windows.Forms.Button button4;

		bool updating = false;
		public ViewManager()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Restart();
		}
		private void Restart()
		{
			mainModel = new CasaresModel.Main();
			mainModel.Load();
			updating = true;
			trackDiscouragement.Value = mainModel.Casares.MaximumUnemploymentLength;
			trackDismissals.Value = (int) (100 * mainModel.Casares.MaximumDismissals);
			trackProfit.Value = (int) (100 * mainModel.Casares.MaximumProfit);
			trackReservation.Value = (int) (100 * mainModel.Casares.ExpectationsDecay);
			UpdateParameters();
			updating = false;

			CreateSpaces();
			CreatePersons();
			CreateCompanies();
			pocketCompanies.ResetChart();
			pocketActivity.ResetChart();
			pocketUnemployment.ResetChart();
			pocketCompanies.ResetChart();
			pocketProfit.ResetChart();
			pocketSalary.ResetChart();

			pocketCompanies.SetMaxScale( mainModel.Casares.CompaniesCount);
			pocketSalary.SetMaxScale(40); 
			pocketIncome.SetMaxScale(40);

			pocketActivity.SetBackColor (Color.FromArgb(150,150,255));
			pocketUnemployment.SetBackColor (Color.FromArgb(255,140,255));
			pocketCompanies.SetBackColor (Color.FromArgb(185,255,185));
			pocketProfit.SetBackColor (Color.FromArgb(255,255,179));
			pocketSalary.SetBackColor (Color.FromArgb(15,238,216));
		}


		public void CreateCompanies()
		{
			companyViews = new ArrayList();
			// Crea las 40 companies
			foreach(CasaresModel.Company c in mainModel.Casares.Companies.Collection)
			{
				CompanyView cview;
				cview = (CompanyView) OpenedCompanies.CreateEntity(c);
				// Adds the employees
				foreach(CasaresModel.Person employee in c._employees)
				{
					PersonView pv = new PersonView(employee);
					cview.AddPerson(pv);
					personViews.Add(pv);
				}
				companyViews.Add(cview);
			}
		}

		private void CreateSpaces()
		{
			int xoffset = 12;
			int yoffset = 48;

			Unemployed = new Space(4, new Point(0 + xoffset, 2 + yoffset), typeof(PersonGroup), this, true);
			Inactive = new Space(4, new Point(0 + xoffset, 302 + yoffset), typeof(PersonGroup), this, true);
			
			OpenedCompanies = new Space(5, new Point(Unemployed.Width + xoffset, 2 + yoffset), typeof(CompanyView), this);
			
			PersonView.DownwardCorridor = 258 + xoffset;
			PersonView.UpwardCorridor = 266 + xoffset;

			// Listo
			Spaces = new ArrayList();
			Spaces.AddRange(new object[] { Unemployed, Inactive, 
											 OpenedCompanies, /*RiskyCompanies,
											 ClosedCompanies*/ });
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ViewManager));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label18 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.lblDiscouragement = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.trackProfit = new System.Windows.Forms.TrackBar();
			this.label5 = new System.Windows.Forms.Label();
			this.lblProfit = new System.Windows.Forms.Label();
			this.trackDismissals = new System.Windows.Forms.TrackBar();
			this.label17 = new System.Windows.Forms.Label();
			this.lblDismissals = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.trackReservation = new System.Windows.Forms.TrackBar();
			this.label23 = new System.Windows.Forms.Label();
			this.lblDecay = new System.Windows.Forms.Label();
			this.trackDiscouragement = new System.Windows.Forms.TrackBar();
			this.label25 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.cmdPause = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel8 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.pocketActivity = new WindowsApplication5.PocketChart();
			this.pocketUnemployment = new WindowsApplication5.PocketChart();
			this.pocketCompanies = new WindowsApplication5.PocketChart();
			this.pocketSalary = new WindowsApplication5.PocketChart();
			this.pocketIncome = new WindowsApplication5.PocketChart();
			this.pocketProfit = new WindowsApplication5.PocketChart();
			this.lblActivity = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.lblCompaniesCount = new System.Windows.Forms.Label();
			this.lblProfitChart = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackProfit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackDismissals)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackReservation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackDiscouragement)).BeginInit();
			this.panel2.SuspendLayout();
			this.panel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// timer2
			// 
			this.timer2.Enabled = true;
			this.timer2.Interval = 1000;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Gray;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(724, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(148, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Elapsed: 2 months";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Gray;
			this.label2.Location = new System.Drawing.Point(12, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(252, 24);
			this.label2.TabIndex = 4;
			this.label2.Text = "People Unemployed";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Gray;
			this.label3.Location = new System.Drawing.Point(12, 320);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(252, 24);
			this.label3.TabIndex = 4;
			this.label3.Text = "People Inactive";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Gray;
			this.label4.Location = new System.Drawing.Point(284, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(340, 24);
			this.label4.TabIndex = 4;
			this.label4.Text = "Companies";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Gray;
			this.panel1.Controls.Add(this.label18);
			this.panel1.Controls.Add(this.label26);
			this.panel1.Controls.Add(this.lblDiscouragement);
			this.panel1.Controls.Add(this.label16);
			this.panel1.Controls.Add(this.label15);
			this.panel1.Controls.Add(this.label14);
			this.panel1.Controls.Add(this.trackProfit);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.lblProfit);
			this.panel1.Controls.Add(this.trackDismissals);
			this.panel1.Controls.Add(this.label17);
			this.panel1.Controls.Add(this.lblDismissals);
			this.panel1.Controls.Add(this.label20);
			this.panel1.Controls.Add(this.label21);
			this.panel1.Controls.Add(this.label22);
			this.panel1.Controls.Add(this.trackReservation);
			this.panel1.Controls.Add(this.label23);
			this.panel1.Controls.Add(this.lblDecay);
			this.panel1.Controls.Add(this.trackDiscouragement);
			this.panel1.Controls.Add(this.label25);
			this.panel1.Controls.Add(this.label28);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.cmdPause);
			this.panel1.Controls.Add(this.button4);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 597);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1008, 112);
			this.panel1.TabIndex = 5;
			// 
			// label18
			// 
			this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label18.BackColor = System.Drawing.Color.Gray;
			this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label18.ForeColor = System.Drawing.Color.Black;
			this.label18.Location = new System.Drawing.Point(312, 68);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(28, 20);
			this.label18.TabIndex = 7;
			this.label18.Text = "100";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label26
			// 
			this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label26.BackColor = System.Drawing.Color.Gray;
			this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label26.ForeColor = System.Drawing.Color.Black;
			this.label26.Location = new System.Drawing.Point(664, 68);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(28, 20);
			this.label26.TabIndex = 7;
			this.label26.Text = "24";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblDiscouragement
			// 
			this.lblDiscouragement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblDiscouragement.BackColor = System.Drawing.Color.Gray;
			this.lblDiscouragement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblDiscouragement.ForeColor = System.Drawing.Color.Black;
			this.lblDiscouragement.Location = new System.Drawing.Point(636, 44);
			this.lblDiscouragement.Name = "lblDiscouragement";
			this.lblDiscouragement.Size = new System.Drawing.Size(68, 20);
			this.lblDiscouragement.TabIndex = 7;
			this.lblDiscouragement.Text = "3%";
			this.lblDiscouragement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label16
			// 
			this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label16.BackColor = System.Drawing.Color.Gray;
			this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label16.ForeColor = System.Drawing.Color.Black;
			this.label16.Location = new System.Drawing.Point(18, 66);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(8, 20);
			this.label16.TabIndex = 7;
			this.label16.Text = "0";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label15
			// 
			this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label15.BackColor = System.Drawing.Color.Gray;
			this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label15.ForeColor = System.Drawing.Color.Black;
			this.label15.Location = new System.Drawing.Point(138, 66);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(28, 20);
			this.label15.TabIndex = 7;
			this.label15.Text = "100";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label14
			// 
			this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label14.BackColor = System.Drawing.Color.Gray;
			this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label14.ForeColor = System.Drawing.Color.Black;
			this.label14.Location = new System.Drawing.Point(24, 44);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(96, 20);
			this.label14.TabIndex = 7;
			this.label14.Text = "Profit baseline:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackProfit
			// 
			this.trackProfit.LargeChange = 20;
			this.trackProfit.Location = new System.Drawing.Point(24, 64);
			this.trackProfit.Maximum = 100;
			this.trackProfit.Name = "trackProfit";
			this.trackProfit.Size = new System.Drawing.Size(120, 42);
			this.trackProfit.SmallChange = 5;
			this.trackProfit.TabIndex = 6;
			this.trackProfit.TickFrequency = 10;
			this.trackProfit.Scroll += new System.EventHandler(this.trackProfit_Scroll);
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.label5.Dock = System.Windows.Forms.DockStyle.Top;
			this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.Gray;
			this.label5.Location = new System.Drawing.Point(0, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(1008, 24);
			this.label5.TabIndex = 5;
			this.label5.Text = "Labor Market Simulation Settings";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblProfit
			// 
			this.lblProfit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblProfit.BackColor = System.Drawing.Color.Gray;
			this.lblProfit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProfit.ForeColor = System.Drawing.Color.Black;
			this.lblProfit.Location = new System.Drawing.Point(120, 44);
			this.lblProfit.Name = "lblProfit";
			this.lblProfit.Size = new System.Drawing.Size(56, 20);
			this.lblProfit.TabIndex = 7;
			this.lblProfit.Text = "3%";
			this.lblProfit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// trackDismissals
			// 
			this.trackDismissals.LargeChange = 20;
			this.trackDismissals.Location = new System.Drawing.Point(200, 64);
			this.trackDismissals.Maximum = 100;
			this.trackDismissals.Name = "trackDismissals";
			this.trackDismissals.Size = new System.Drawing.Size(120, 42);
			this.trackDismissals.SmallChange = 5;
			this.trackDismissals.TabIndex = 6;
			this.trackDismissals.TickFrequency = 10;
			this.trackDismissals.Scroll += new System.EventHandler(this.trackDismissals_Scroll);
			// 
			// label17
			// 
			this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label17.BackColor = System.Drawing.Color.Gray;
			this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label17.ForeColor = System.Drawing.Color.Black;
			this.label17.Location = new System.Drawing.Point(200, 44);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(92, 20);
			this.label17.TabIndex = 7;
			this.label17.Text = "Dismissals rate:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblDismissals
			// 
			this.lblDismissals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblDismissals.BackColor = System.Drawing.Color.Gray;
			this.lblDismissals.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblDismissals.ForeColor = System.Drawing.Color.Black;
			this.lblDismissals.Location = new System.Drawing.Point(292, 44);
			this.lblDismissals.Name = "lblDismissals";
			this.lblDismissals.Size = new System.Drawing.Size(56, 20);
			this.lblDismissals.TabIndex = 7;
			this.lblDismissals.Text = "3%";
			this.lblDismissals.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label20
			// 
			this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label20.BackColor = System.Drawing.Color.Gray;
			this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label20.ForeColor = System.Drawing.Color.Black;
			this.label20.Location = new System.Drawing.Point(192, 68);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(8, 20);
			this.label20.TabIndex = 7;
			this.label20.Text = "0";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label21
			// 
			this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label21.BackColor = System.Drawing.Color.Gray;
			this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label21.ForeColor = System.Drawing.Color.Black;
			this.label21.Location = new System.Drawing.Point(488, 68);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(28, 20);
			this.label21.TabIndex = 7;
			this.label21.Text = "100";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label22
			// 
			this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label22.BackColor = System.Drawing.Color.Gray;
			this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label22.ForeColor = System.Drawing.Color.Black;
			this.label22.Location = new System.Drawing.Point(372, 44);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(108, 20);
			this.label22.TabIndex = 7;
			this.label22.Text = "Reservation decay: ";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// trackReservation
			// 
			this.trackReservation.LargeChange = 20;
			this.trackReservation.Location = new System.Drawing.Point(372, 64);
			this.trackReservation.Maximum = 100;
			this.trackReservation.Name = "trackReservation";
			this.trackReservation.Size = new System.Drawing.Size(120, 42);
			this.trackReservation.SmallChange = 5;
			this.trackReservation.TabIndex = 6;
			this.trackReservation.TickFrequency = 10;
			this.trackReservation.Scroll += new System.EventHandler(this.trackReservation_Scroll);
			// 
			// label23
			// 
			this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label23.BackColor = System.Drawing.Color.Gray;
			this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label23.ForeColor = System.Drawing.Color.Black;
			this.label23.Location = new System.Drawing.Point(364, 68);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(8, 20);
			this.label23.TabIndex = 7;
			this.label23.Text = "0";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblDecay
			// 
			this.lblDecay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblDecay.BackColor = System.Drawing.Color.Gray;
			this.lblDecay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblDecay.ForeColor = System.Drawing.Color.Black;
			this.lblDecay.Location = new System.Drawing.Point(480, 44);
			this.lblDecay.Name = "lblDecay";
			this.lblDecay.Size = new System.Drawing.Size(60, 20);
			this.lblDecay.TabIndex = 7;
			this.lblDecay.Text = "3%";
			this.lblDecay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// trackDiscouragement
			// 
			this.trackDiscouragement.LargeChange = 4;
			this.trackDiscouragement.Location = new System.Drawing.Point(548, 64);
			this.trackDiscouragement.Maximum = 24;
			this.trackDiscouragement.Name = "trackDiscouragement";
			this.trackDiscouragement.Size = new System.Drawing.Size(120, 42);
			this.trackDiscouragement.TabIndex = 6;
			this.trackDiscouragement.TickFrequency = 2;
			this.trackDiscouragement.Scroll += new System.EventHandler(this.trackDiscouragement_Scroll);
			// 
			// label25
			// 
			this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label25.BackColor = System.Drawing.Color.Gray;
			this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label25.ForeColor = System.Drawing.Color.Black;
			this.label25.Location = new System.Drawing.Point(540, 44);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(108, 20);
			this.label25.TabIndex = 7;
			this.label25.Text = "Discouragement:";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label28
			// 
			this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label28.BackColor = System.Drawing.Color.Gray;
			this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label28.ForeColor = System.Drawing.Color.Black;
			this.label28.Location = new System.Drawing.Point(540, 68);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(8, 20);
			this.label28.TabIndex = 7;
			this.label28.Text = "1";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(884, 36);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(112, 28);
			this.button2.TabIndex = 8;
			this.button2.Text = "Exit";
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// cmdPause
			// 
			this.cmdPause.Location = new System.Drawing.Point(760, 72);
			this.cmdPause.Name = "cmdPause";
			this.cmdPause.Size = new System.Drawing.Size(112, 28);
			this.cmdPause.TabIndex = 8;
			this.cmdPause.Text = "Pause";
			this.cmdPause.Click += new System.EventHandler(this.cmdPause_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(884, 72);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(112, 28);
			this.button4.TabIndex = 8;
			this.button4.Text = "Restart";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.panel2.BackColor = System.Drawing.Color.Gray;
			this.panel2.Controls.Add(this.panel3);
			this.panel2.DockPadding.All = 1;
			this.panel2.ForeColor = System.Drawing.Color.Gray;
			this.panel2.Location = new System.Drawing.Point(0, 16);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(12, 584);
			this.panel2.TabIndex = 6;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.Black;
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.ForeColor = System.Drawing.Color.Gray;
			this.panel3.Location = new System.Drawing.Point(1, 1);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(10, 582);
			this.panel3.TabIndex = 7;
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.panel4.BackColor = System.Drawing.Color.Gray;
			this.panel4.DockPadding.All = 1;
			this.panel4.ForeColor = System.Drawing.Color.Gray;
			this.panel4.Location = new System.Drawing.Point(264, 16);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(1, 584);
			this.panel4.TabIndex = 7;
			// 
			// panel6
			// 
			this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.panel6.BackColor = System.Drawing.Color.Gray;
			this.panel6.Controls.Add(this.panel7);
			this.panel6.DockPadding.All = 1;
			this.panel6.ForeColor = System.Drawing.Color.Gray;
			this.panel6.Location = new System.Drawing.Point(624, 16);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(12, 584);
			this.panel6.TabIndex = 7;
			// 
			// panel7
			// 
			this.panel7.BackColor = System.Drawing.Color.Black;
			this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel7.ForeColor = System.Drawing.Color.Gray;
			this.panel7.Location = new System.Drawing.Point(1, 1);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(10, 582);
			this.panel7.TabIndex = 7;
			// 
			// panel5
			// 
			this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.panel5.BackColor = System.Drawing.Color.Gray;
			this.panel5.DockPadding.All = 1;
			this.panel5.ForeColor = System.Drawing.Color.Gray;
			this.panel5.Location = new System.Drawing.Point(284, 16);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(1, 584);
			this.panel5.TabIndex = 7;
			// 
			// panel8
			// 
			this.panel8.BackColor = System.Drawing.Color.Gray;
			this.panel8.DockPadding.All = 1;
			this.panel8.ForeColor = System.Drawing.Color.Gray;
			this.panel8.Location = new System.Drawing.Point(264, 16);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(20, 1);
			this.panel8.TabIndex = 8;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(652, 492);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(11, 89);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 9;
			this.pictureBox1.TabStop = false;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Gray;
			this.label6.Location = new System.Drawing.Point(636, 452);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(372, 24);
			this.label6.TabIndex = 4;
			this.label6.Text = "Legend";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.BackColor = System.Drawing.Color.Black;
			this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.Gainsboro;
			this.label7.Location = new System.Drawing.Point(668, 492);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(300, 20);
			this.label7.TabIndex = 4;
			this.label7.Text = "Household income per capita above 10 credits.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label8.BackColor = System.Drawing.Color.Black;
			this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.ForeColor = System.Drawing.Color.Gainsboro;
			this.label8.Location = new System.Drawing.Point(668, 512);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(336, 20);
			this.label8.TabIndex = 4;
			this.label8.Text = "Household income per capita between 5 and 10 credits.";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label9.BackColor = System.Drawing.Color.Black;
			this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.Gainsboro;
			this.label9.Location = new System.Drawing.Point(668, 528);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(280, 20);
			this.label9.TabIndex = 4;
			this.label9.Text = "Household income per capita below 5 credits.";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label10.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.ForeColor = System.Drawing.Color.Gray;
			this.label10.Location = new System.Drawing.Point(636, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(372, 24);
			this.label10.TabIndex = 4;
			this.label10.Text = "Aggregated evolution";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label11
			// 
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label11.BackColor = System.Drawing.Color.Black;
			this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label11.ForeColor = System.Drawing.Color.Gainsboro;
			this.label11.Location = new System.Drawing.Point(668, 547);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(280, 20);
			this.label11.TabIndex = 4;
			this.label11.Text = "Open companies";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label12.BackColor = System.Drawing.Color.Black;
			this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label12.ForeColor = System.Drawing.Color.Gainsboro;
			this.label12.Location = new System.Drawing.Point(668, 567);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(280, 20);
			this.label12.TabIndex = 4;
			this.label12.Text = "Bankrupcy";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pocketActivity
			// 
			this.pocketActivity.BackColor = System.Drawing.Color.Gray;
			this.pocketActivity.DockPadding.All = 1;
			this.pocketActivity.Location = new System.Drawing.Point(652, 60);
			this.pocketActivity.Name = "pocketActivity";
			this.pocketActivity.Size = new System.Drawing.Size(170, 106);
			this.pocketActivity.TabIndex = 10;
			// 
			// pocketUnemployment
			// 
			this.pocketUnemployment.BackColor = System.Drawing.Color.Gray;
			this.pocketUnemployment.DockPadding.All = 1;
			this.pocketUnemployment.Location = new System.Drawing.Point(836, 60);
			this.pocketUnemployment.Name = "pocketUnemployment";
			this.pocketUnemployment.Size = new System.Drawing.Size(170, 106);
			this.pocketUnemployment.TabIndex = 12;
			// 
			// pocketCompanies
			// 
			this.pocketCompanies.BackColor = System.Drawing.Color.Gray;
			this.pocketCompanies.DockPadding.All = 1;
			this.pocketCompanies.Location = new System.Drawing.Point(652, 192);
			this.pocketCompanies.Name = "pocketCompanies";
			this.pocketCompanies.Size = new System.Drawing.Size(170, 106);
			this.pocketCompanies.TabIndex = 13;
			// 
			// pocketSalary
			// 
			this.pocketSalary.BackColor = System.Drawing.Color.Gray;
			this.pocketSalary.DockPadding.All = 1;
			this.pocketSalary.Location = new System.Drawing.Point(652, 320);
			this.pocketSalary.Name = "pocketSalary";
			this.pocketSalary.Size = new System.Drawing.Size(170, 106);
			this.pocketSalary.TabIndex = 14;
			// 
			// pocketIncome
			// 
			this.pocketIncome.BackColor = System.Drawing.Color.Gray;
			this.pocketIncome.DockPadding.All = 1;
			this.pocketIncome.Location = new System.Drawing.Point(836, 320);
			this.pocketIncome.Name = "pocketIncome";
			this.pocketIncome.Size = new System.Drawing.Size(170, 106);
			this.pocketIncome.TabIndex = 15;
			// 
			// pocketProfit
			// 
			this.pocketProfit.BackColor = System.Drawing.Color.Gray;
			this.pocketProfit.DockPadding.All = 1;
			this.pocketProfit.Location = new System.Drawing.Point(836, 192);
			this.pocketProfit.Name = "pocketProfit";
			this.pocketProfit.Size = new System.Drawing.Size(170, 106);
			this.pocketProfit.TabIndex = 16;
			// 
			// lblActivity
			// 
			this.lblActivity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblActivity.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.lblActivity.Location = new System.Drawing.Point(656, 168);
			this.lblActivity.Name = "lblActivity";
			this.lblActivity.Size = new System.Drawing.Size(164, 20);
			this.lblActivity.TabIndex = 17;
			this.lblActivity.Text = "Participation Rate";
			this.lblActivity.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label19.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.label19.Location = new System.Drawing.Point(840, 168);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(164, 20);
			this.label19.TabIndex = 17;
			this.label19.Text = "Unemployment Rate";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblCompaniesCount
			// 
			this.lblCompaniesCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCompaniesCount.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.lblCompaniesCount.Location = new System.Drawing.Point(656, 300);
			this.lblCompaniesCount.Name = "lblCompaniesCount";
			this.lblCompaniesCount.Size = new System.Drawing.Size(164, 20);
			this.lblCompaniesCount.TabIndex = 17;
			this.lblCompaniesCount.Text = "# of Companies";
			this.lblCompaniesCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblProfitChart
			// 
			this.lblProfitChart.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProfitChart.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.lblProfitChart.Location = new System.Drawing.Point(840, 300);
			this.lblProfitChart.Name = "lblProfitChart";
			this.lblProfitChart.Size = new System.Drawing.Size(164, 20);
			this.lblProfitChart.TabIndex = 17;
			this.lblProfitChart.Text = "Total Profit / Product";
			this.lblProfitChart.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.label13.Location = new System.Drawing.Point(656, 428);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(164, 20);
			this.label13.TabIndex = 17;
			this.label13.Text = "Average Salary";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label24.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.label24.Location = new System.Drawing.Point(840, 428);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(164, 20);
			this.label24.TabIndex = 17;
			this.label24.Text = "Average Income per Capita";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// ViewManager
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1008, 709);
			this.Controls.Add(this.lblActivity);
			this.Controls.Add(this.pocketProfit);
			this.Controls.Add(this.pocketIncome);
			this.Controls.Add(this.pocketSalary);
			this.Controls.Add(this.pocketCompanies);
			this.Controls.Add(this.pocketUnemployment);
			this.Controls.Add(this.pocketActivity);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.panel8);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.panel6);
			this.Controls.Add(this.panel5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.lblCompaniesCount);
			this.Controls.Add(this.lblProfitChart);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label24);
			this.Name = "ViewManager";
			this.Text = "CASARES LABOR MARKET FRAMEWORK";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackProfit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackDismissals)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackReservation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackDiscouragement)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new ViewManager());
		}
		ArrayList l = new ArrayList();
		
		public CompanyView GetCompanyById(int id)
		{
			foreach(CompanyView cv in this.companyViews)
				if (cv.Id == id)
					return cv;
			return null;
		}
		private void CreatePersons()
		{
			personViews = new ArrayList();
			for (int n = 0; n < mainModel.Casares.Persons.Count; n++)
			{
				CasaresModel.Person person = mainModel.Casares.Persons[n];
				if (person.State != 1)
				{   //1=Occupied, 2=Unemployed, 3=Inactive
					Container container;
					PersonView c = new PersonView(mainModel.Casares.Persons[n]);
					personViews.Add(c);
					switch (person.State)
					{
						case 2:
							container = Unemployed.GetContainerWithSpace();
							container.AddPerson(c);
							break;
						case 3:
							container = Inactive.GetContainerWithSpace();
							container.AddPerson(c);
							break;
					}
				}
			}
		}

		private void Regen()
		{
			OnPaint(new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle));
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}
		int paintEventReference;
		protected override void OnPaint(PaintEventArgs e)
		{
			if (paintEventReference == Int32.MaxValue)
				paintEventReference = 0;
			paintEventReference++;
			
			foreach(Space s in Spaces)
			{
				s.Draw(e, paintEventReference);
			}
		}

		private void RefreshAnimation()
		{
			// Tries to update all moving objects
			foreach(Space s in Spaces)
			{
				s.ClearMovingObjects();
			}
			foreach(Space s in Spaces)
			{
				s.UpdateMovingObjects();
			}
			foreach(Space s in Spaces)
			{
				s.RedrawMovingObjects();
			}
			foreach(Space s in Spaces)
			{
				s.FinalRedrawMovingObjects();
			}
			
		}

		bool bDrawing = false;
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			if (bDrawing == true)
				return;
			bDrawing = true;
			RefreshAnimation();
			bDrawing = false;
		}

		private void timer2_Tick(object sender, System.EventArgs e)
		{
			this.mainModel.Casares.Evolve();
			label1.Text = "Elapsed: " + mainModel.Casares.TimeElapsed + " months";
			// Update charts
			pocketActivity.AddValue((int) (mainModel.Casares.Stats.ActivityRate));
			pocketUnemployment.AddValue((int) (mainModel.Casares.Stats.UnemploymentRate));
			pocketCompanies.AddValue((int) (mainModel.Casares.Stats.Companies));
			pocketProfit.AddValue((int) (mainModel.Casares.Stats.Profit));

			pocketIncome.AddValue((int) (mainModel.Casares.Stats.Income));
			pocketSalary.AddValue((int) (mainModel.Casares.Stats.Salary));

		}

		private void trackProfit_Scroll(object sender, System.EventArgs e)
		{
			UpdateParameters();
		}

		private void trackDismissals_Scroll(object sender, System.EventArgs e)
		{
			UpdateParameters();
		}

		private void trackReservation_Scroll(object sender, System.EventArgs e)
		{
			UpdateParameters();
		}

		private void trackDiscouragement_Scroll(object sender, System.EventArgs e)
		{
			UpdateParameters();
		}
		private void UpdateParameters()
		{
			// Update labels
			lblDismissals.Text = trackDismissals.Value.ToString() + "%";
			lblDecay.Text = trackReservation.Value.ToString() + "%";
			string plural = (trackDiscouragement.Value > 1 ? "s" : "");
			lblDiscouragement.Text = trackDiscouragement.Value.ToString() + " month" + plural;
			lblProfit.Text = trackProfit.Value.ToString() + "%";
			// Done
			if (updating == false)
			{
				mainModel.Casares.SetParameters(
					((double) trackReservation.Value) / 100,
					trackDiscouragement.Value, ((double) trackProfit.Value) / 100,
					((double)trackDismissals.Value) / 100);
			}

		}

		private void cmdPause_Click(object sender, System.EventArgs e)
		{
			if (timer2.Enabled == false)
			{
				timer2.Enabled = true;
				cmdPause.Text = "Pause";
			}
			else
			{
				timer2.Enabled = false;
				cmdPause.Text = "Resume";
			}
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			this.Restart();
			if (timer2.Enabled == false)
				cmdPause_Click(null, null);
			this.Invalidate();
		}

		private void button2_Click_1(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
		
		}


	}
}
