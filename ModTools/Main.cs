using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace ModTools
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ModToolUtil.InitDirectories();
            modList.Items.AddRange(ModToolUtil.GetMods().ToArray());
        }

        private void modButton_Click(object sender, EventArgs e)
        {
            string Result = Interaction.InputBox("Enter in a name for your mod:", "Zombie Convergence Mod Creator");
            if (Result != string.Empty)
            {

            }
        }

        private void modList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modList.SelectedIndex != -1)
            {
                modBuildButton.Enabled = true;
                radiantButton.Enabled = true;
            }
        }
    }
}
