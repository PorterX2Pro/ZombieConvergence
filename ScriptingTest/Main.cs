using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptingTest
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using (StreamWriter writeFile = new StreamWriter("test.gsc"))
            {
                writeFile.Write(scriptBox.Text);
            }
            ZC.Scripting.GSCScript script = ZC.Scripting.Load.LoadScript("test.gsc");
        }
    }
}
