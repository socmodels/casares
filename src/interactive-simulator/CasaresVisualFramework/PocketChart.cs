using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace WindowsApplication5
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class PocketChart : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel6 ;
		private System.Windows.Forms.Panel panel7 ;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Panel panDraw;
			
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public int [] Values;
		private System.Windows.Forms.Label lbl100;
		private System.Windows.Forms.Label lbl75;
		private System.Windows.Forms.Label lbl50;
		private System.Windows.Forms.Label lbl25;
		private System.Windows.Forms.Label lbl0;
		int currentValue = 0;
		int _scale = 100;

		public void SetMaxScale(int scale)
		{
			_scale = scale;
			lbl100.Text = scale.ToString();
			lbl75.Text = ((int) (scale * .75F)).ToString();
			lbl50.Text = ((int) (scale * .50F)).ToString();
			lbl25.Text = ((int) (scale * .25F)).ToString();
		}
		public void ResetChart()
		{
			for (int n = 0; n < Values.Length; n++)
				Values[n] = 0;
			currentValue = 0;
			panDraw.Invalidate();
		}
		public void SetBackColor(Color c)
		{
			panel1.BackColor = c;
		}
		public void AddValue(int val)
		{
			Values[currentValue] = (int) ((double) val / _scale * 79F);
			currentValue++;
			if (currentValue >= Values.Length)
				currentValue = 0;
			panDraw.Invalidate();
		}
		public PocketChart()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			Values = new int[142];
			
			panDraw.Paint += new PaintEventHandler(panDraw_Paint);	
			this.Size = new Size(170, 106);
		}
		

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.panel8 = new System.Windows.Forms.Panel();
			this.lbl100 = new System.Windows.Forms.Label();
			this.lbl75 = new System.Windows.Forms.Label();
			this.lbl50 = new System.Windows.Forms.Label();
			this.lbl25 = new System.Windows.Forms.Label();
			this.lbl0 = new System.Windows.Forms.Label();
			this.panDraw = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(224)), ((System.Byte)(192)));
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel4);
			this.panel1.Controls.Add(this.panel5);
			this.panel1.Controls.Add(this.panel6);
			this.panel1.Controls.Add(this.panel7);
			this.panel1.Controls.Add(this.panel8);
			this.panel1.Controls.Add(this.lbl100);
			this.panel1.Controls.Add(this.lbl75);
			this.panel1.Controls.Add(this.lbl50);
			this.panel1.Controls.Add(this.lbl25);
			this.panel1.Controls.Add(this.lbl0);
			this.panel1.Controls.Add(this.panDraw);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(1, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(168, 104);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Silver;
			this.panel2.Location = new System.Drawing.Point(14, 12);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(144, 1);
			this.panel2.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.Gray;
			this.panel3.Location = new System.Drawing.Point(14, 92);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(144, 1);
			this.panel3.TabIndex = 0;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.Silver;
			this.panel4.Location = new System.Drawing.Point(14, 72);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(4, 1);
			this.panel4.TabIndex = 0;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.Silver;
			this.panel5.Location = new System.Drawing.Point(14, 52);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(4, 1);
			this.panel5.TabIndex = 0;
			this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.Color.Silver;
			this.panel6.Location = new System.Drawing.Point(14, 32);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(4, 1);
			this.panel6.TabIndex = 0;
			this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
			// 
			// panel7
			// 
			this.panel7.BackColor = System.Drawing.Color.Silver;
			this.panel7.Location = new System.Drawing.Point(156, 12);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(1, 80);
			this.panel7.TabIndex = 0;
			// 
			// panel8
			// 
			this.panel8.BackColor = System.Drawing.Color.Gray;
			this.panel8.Location = new System.Drawing.Point(16, 12);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(1, 80);
			this.panel8.TabIndex = 0;
			// 
			// lbl100
			// 
			this.lbl100.Font = new System.Drawing.Font("Tahoma", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbl100.ForeColor = System.Drawing.Color.Black;
			this.lbl100.Location = new System.Drawing.Point(-2, 10);
			this.lbl100.Name = "lbl100";
			this.lbl100.Size = new System.Drawing.Size(16, 12);
			this.lbl100.TabIndex = 1;
			this.lbl100.Text = "100";
			this.lbl100.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbl75
			// 
			this.lbl75.Font = new System.Drawing.Font("Tahoma", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbl75.ForeColor = System.Drawing.Color.Black;
			this.lbl75.Location = new System.Drawing.Point(-2, 28);
			this.lbl75.Name = "lbl75";
			this.lbl75.Size = new System.Drawing.Size(16, 12);
			this.lbl75.TabIndex = 1;
			this.lbl75.Text = "75";
			this.lbl75.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbl50
			// 
			this.lbl50.Font = new System.Drawing.Font("Tahoma", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbl50.ForeColor = System.Drawing.Color.Black;
			this.lbl50.Location = new System.Drawing.Point(-2, 48);
			this.lbl50.Name = "lbl50";
			this.lbl50.Size = new System.Drawing.Size(16, 12);
			this.lbl50.TabIndex = 1;
			this.lbl50.Text = "50";
			this.lbl50.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbl25
			// 
			this.lbl25.Font = new System.Drawing.Font("Tahoma", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbl25.ForeColor = System.Drawing.Color.Black;
			this.lbl25.Location = new System.Drawing.Point(-2, 68);
			this.lbl25.Name = "lbl25";
			this.lbl25.Size = new System.Drawing.Size(16, 12);
			this.lbl25.TabIndex = 1;
			this.lbl25.Text = "25";
			this.lbl25.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbl0
			// 
			this.lbl0.Font = new System.Drawing.Font("Tahoma", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbl0.ForeColor = System.Drawing.Color.Black;
			this.lbl0.Location = new System.Drawing.Point(-2, 88);
			this.lbl0.Name = "lbl0";
			this.lbl0.Size = new System.Drawing.Size(16, 12);
			this.lbl0.TabIndex = 1;
			this.lbl0.Text = "0";
			this.lbl0.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// panDraw
			// 
			this.panDraw.Location = new System.Drawing.Point(16, 12);
			this.panDraw.Name = "panDraw";
			this.panDraw.Size = new System.Drawing.Size(140, 80);
			this.panDraw.TabIndex = 2;
			// 
			// UserControl1
			// 
			this.BackColor = System.Drawing.Color.Gray;
			this.Controls.Add(this.panel1);
			this.DockPadding.All = 1;
			this.Name = "UserControl1";
			this.Size = new System.Drawing.Size(170, 106);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void panel5_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void panel6_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void panDraw_Paint(object sender, PaintEventArgs e)
		{
			// Draw all data
			int desde = currentValue + 1;
			if (desde >= Values.Length) desde = 0;
			Point prev = new Point(0, panDraw.Height - Values[desde]);
			for (int x = 0; x < Values.Length; x++)
			{
				if (desde >= Values.Length) desde = 0;
				
				if (desde % 24 == 0)
					e.Graphics.DrawLine(Pens.LightGray,
						x, 0, x, 80);
				
				Point actual = new Point(x, panDraw.Height - Values[desde]);
				e.Graphics.DrawLine(Pens.Red, prev,
					actual);
				prev = actual;
				desde++;
			}
		}
	}
}
