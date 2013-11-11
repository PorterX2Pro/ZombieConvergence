namespace ModTools
{
    partial class Main
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radiantButton = new System.Windows.Forms.Button();
            this.modButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.modList = new System.Windows.Forms.ListBox();
            this.modBuildButton = new System.Windows.Forms.Button();
            this.consoleBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mod Tools";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Creative Tools";
            // 
            // radiantButton
            // 
            this.radiantButton.Enabled = false;
            this.radiantButton.Location = new System.Drawing.Point(17, 173);
            this.radiantButton.Name = "radiantButton";
            this.radiantButton.Size = new System.Drawing.Size(115, 25);
            this.radiantButton.TabIndex = 2;
            this.radiantButton.Text = "Map Tool";
            this.radiantButton.UseVisualStyleBackColor = true;
            // 
            // modButton
            // 
            this.modButton.Location = new System.Drawing.Point(17, 142);
            this.modButton.Name = "modButton";
            this.modButton.Size = new System.Drawing.Size(115, 25);
            this.modButton.TabIndex = 3;
            this.modButton.Text = "Create Mod";
            this.modButton.UseVisualStyleBackColor = true;
            this.modButton.Click += new System.EventHandler(this.modButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(264, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Mod Builder";
            // 
            // modList
            // 
            this.modList.FormattingEnabled = true;
            this.modList.Location = new System.Drawing.Point(268, 142);
            this.modList.Name = "modList";
            this.modList.Size = new System.Drawing.Size(234, 95);
            this.modList.TabIndex = 5;
            this.modList.SelectedIndexChanged += new System.EventHandler(this.modList_SelectedIndexChanged);
            // 
            // modBuildButton
            // 
            this.modBuildButton.Enabled = false;
            this.modBuildButton.Location = new System.Drawing.Point(397, 243);
            this.modBuildButton.Name = "modBuildButton";
            this.modBuildButton.Size = new System.Drawing.Size(105, 28);
            this.modBuildButton.TabIndex = 6;
            this.modBuildButton.Text = "Build Mod";
            this.modBuildButton.UseVisualStyleBackColor = true;
            // 
            // consoleBox
            // 
            this.consoleBox.Location = new System.Drawing.Point(17, 282);
            this.consoleBox.Multiline = true;
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consoleBox.Size = new System.Drawing.Size(358, 110);
            this.consoleBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 22);
            this.label4.TabIndex = 8;
            this.label4.Text = "Console";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 404);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.consoleBox);
            this.Controls.Add(this.modBuildButton);
            this.Controls.Add(this.modList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.modButton);
            this.Controls.Add(this.radiantButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zombie Convergence - ModTools";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button radiantButton;
        private System.Windows.Forms.Button modButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox modList;
        private System.Windows.Forms.Button modBuildButton;
        private System.Windows.Forms.TextBox consoleBox;
        private System.Windows.Forms.Label label4;
    }
}

