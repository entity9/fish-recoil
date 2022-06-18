using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using static Fatal.Variables.Settings;
using static Fatal.Variables.Menu;
using static Fatal.Variables.Weapon;
using static Fatal.Weapons;
using System.Threading;
using System.Windows.Input;

namespace Fatal
{
    public partial class Menu : MetroFramework.Forms.MetroForm
    {
        #region vars
        public static bool activ = false;
        public static bool minmax = false;
        public static bool test1 = false;

        public static int min = 0;
        public static int max = 1;
        public static int smooth = 1;
        public static double sense = 1.0;
        public static int fov = 90;

        public static string weapon;
        public static string attachment;
        public static string scope;

        #endregion
        public Menu()
        {
			Thread KB2 = new Thread(KB);
			KB2.SetApartmentState(ApartmentState.STA);
			CheckForIllegalCrossThreadCalls = false;
			KB2.Start();
			InitializeComponent();
		}

		public string kobe = "no";
		public string hidden = "no";
		public bool hidestatus = false;
		void KB()
		{
			while (true)
			{
				Thread.Sleep(40);
				if ((Keyboard.GetKeyStates(Key.RightShift) & KeyStates.Down) > 0)
				{
					kobe = "yes";
				}
				else
				{
					if (kobe == "yes")
					{
						metroCheckBox1.Checked = !metroCheckBox1.Checked;
						kobe = "no";
					}
				}

				if ((Keyboard.GetKeyStates(Key.Insert) & KeyStates.Down) > 0)
				{
					hidden = "yes";
				}
				else
				{
					if (hidden == "yes")
					{
						Thread.Sleep(10);
						if (hidestatus)
						{
							this.Show();
							hidestatus = !hidestatus;
						}
						else
						{
							this.Hide();
							var cp = base.CreateParams;
							cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
							hidestatus = !hidestatus;
						}

						//this.ShowInTaskbar = !this.ShowInTaskbar;
						hidden = "no";
					}
				}

			}
		}

		#region bool
		private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(metroCheckBox1.Checked)
            {
                setEnabled(true);
				metroCheckBox1.Text = "Enabled";
			}
            else
            {
				setEnabled(false);
				metroCheckBox1.Text = "Disabled";
			}
        }

        #endregion
        #region int

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
			setSensitivity(Convert.ToDouble(numericUpDown1.Value));
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
			setFOV(Convert.ToInt32(numericUpDown2.Value));
        }
        #endregion
        #region string
        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			if ((string)metroComboBox1.SelectedItem == "Assault Rifle")
			{
				setVariables(1);
			}
			else if((string)metroComboBox1.SelectedItem == "LR-300 Assault Rifle")
			{
				setVariables(2);
			}
			else if ((string)metroComboBox1.SelectedItem == "Semi Assault Rifle")
			{
				setVariables(3);
			}
			else if ((string)metroComboBox1.SelectedItem == "Custom SMG")
			{
				setVariables(4);
			}
			else if ((string)metroComboBox1.SelectedItem == "MP5A4")
			{
				setVariables(5);
			}
			else if ((string)metroComboBox1.SelectedItem == "Thompson")
			{
				setVariables(6);
			}
			else if ((string)metroComboBox1.SelectedItem == "M92")
			{
				setVariables(7);
			}
			else if ((string)metroComboBox1.SelectedItem == "M39")
			{
				setVariables(8);
			}
			else if ((string)metroComboBox1.SelectedItem == "M249")
			{
				setVariables(9);
			};
		}

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
			if ((string)metroComboBox2.SelectedItem == "None")
			{
				changeBarrel(0);
			}
			else if ((string)metroComboBox2.SelectedItem == "Silencer")
			{
				changeBarrel(1);
			}
			else if ((string)metroComboBox2.SelectedItem == "Muzzle Boost")
			{
				changeBarrel(2);
			}
			else if ((string)metroComboBox2.SelectedItem == "Muzzle Brake")
			{
				changeBarrel(3);
			};
		}

        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
			if ((string)metroComboBox3.SelectedItem == "None")
			{
				changeScope(0);
			}
			else if ((string)metroComboBox3.SelectedItem == "Simple")
			{
				changeScope(1);
			}
			else if ((string)metroComboBox3.SelectedItem == "Holosight")
			{
				changeScope(2);
			}
			else if ((string)metroComboBox3.SelectedItem == "8x")
			{
				changeScope(3);
			}
			else if ((string)metroComboBox3.SelectedItem == "16")
			{
				changeScope(4);
			};
        }
        #endregion

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

		private void metroTrackBar3_ValueChanged(object sender, EventArgs e)
		{
			setRandomness(metroTrackBar3.Value);
		}

		private void metroCheckBox2_CheckStateChanged(object sender, EventArgs e)
		{
			setHumanizer(metroCheckBox2.Checked);
		}

		private void metroPanel3_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
