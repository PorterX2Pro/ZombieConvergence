namespace ScriptingTest
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
            this.label2 = new System.Windows.Forms.Label();
            this.scriptBox = new System.Windows.Forms.TextBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.debugButton = new System.Windows.Forms.Button();
            this.runBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.variableList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Script";
            // 
            // scriptBox
            // 
            this.scriptBox.Location = new System.Drawing.Point(16, 45);
            this.scriptBox.Multiline = true;
            this.scriptBox.Name = "scriptBox";
            this.scriptBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scriptBox.Size = new System.Drawing.Size(357, 358);
            this.scriptBox.TabIndex = 3;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(457, 181);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(89, 23);
            this.loadButton.TabIndex = 4;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(552, 181);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(91, 23);
            this.runButton.TabIndex = 5;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // debugButton
            // 
            this.debugButton.Location = new System.Drawing.Point(649, 181);
            this.debugButton.Name = "debugButton";
            this.debugButton.Size = new System.Drawing.Size(90, 23);
            this.debugButton.TabIndex = 6;
            this.debugButton.Text = "Debug Step";
            this.debugButton.UseVisualStyleBackColor = true;
            // 
            // runBox
            // 
            this.runBox.Location = new System.Drawing.Point(379, 210);
            this.runBox.Multiline = true;
            this.runBox.Name = "runBox";
            this.runBox.Size = new System.Drawing.Size(417, 193);
            this.runBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(379, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 22);
            this.label1.TabIndex = 8;
            this.label1.Text = "Run";
            // 
            // variableList
            // 
            this.variableList.FormattingEnabled = true;
            this.variableList.Location = new System.Drawing.Point(379, 51);
            this.variableList.Name = "variableList";
            this.variableList.Size = new System.Drawing.Size(413, 108);
            this.variableList.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(379, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 22);
            this.label3.TabIndex = 10;
            this.label3.Text = "Variables";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 415);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.variableList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.runBox);
            this.Controls.Add(this.debugButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.scriptBox);
            this.Controls.Add(this.label2);
            this.Name = "Main";
            this.Text = "Scripting Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox scriptBox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button debugButton;
        private System.Windows.Forms.TextBox runBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox variableList;
        private System.Windows.Forms.Label label3;
    }
}

