namespace GraphingData
{
    partial class Form1
    {
        private OxyPlot.WindowsForms.PlotView plotView;
        private System.Windows.Forms.CheckedListBox checkedListBoxStates;
        private System.Windows.Forms.TextBox textBoxHorizontalScale;
        private System.Windows.Forms.TextBox textBoxVerticalScale;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonRedraw;
        private System.Windows.Forms.Button buttonUndo;


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
            plotView = new OxyPlot.WindowsForms.PlotView();
            checkedListBoxStates = new CheckedListBox();
            buttonRedraw = new Button();
            buttonSave = new Button();
            textBoxHorizontalScale = new TextBox();
            textBoxVerticalScale = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            label2 = new Label();
            groupBox2 = new GroupBox();
            buttonUndo = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // plotView
            // 
            plotView.Location = new Point(14, 14);
            plotView.Margin = new Padding(4, 3, 4, 3);
            plotView.Name = "plotView";
            plotView.PanCursor = Cursors.Hand;
            plotView.Size = new Size(1078, 497);
            plotView.TabIndex = 0;
            plotView.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // checkedListBoxStates
            // 
            checkedListBoxStates.FormattingEnabled = true;
            checkedListBoxStates.Location = new Point(13, 16);
            checkedListBoxStates.Margin = new Padding(4, 3, 4, 3);
            checkedListBoxStates.Name = "checkedListBoxStates";
            checkedListBoxStates.Size = new Size(170, 166);
            checkedListBoxStates.TabIndex = 1;
            checkedListBoxStates.CheckOnClick = true;
            // 
            // buttonRedraw
            // 
            buttonRedraw.Location = new Point(679, 674);
            buttonRedraw.Margin = new Padding(4, 3, 4, 3);
            buttonRedraw.Name = "buttonRedraw";
            buttonRedraw.Size = new Size(233, 31);
            buttonRedraw.TabIndex = 2;
            buttonRedraw.Text = "Redraw Graph";
            buttonRedraw.UseVisualStyleBackColor = true;
            buttonRedraw.Click += buttonRedraw_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(992, 674);
            buttonSave.Margin = new Padding(4, 3, 4, 3);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(100, 31);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save Graph";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // textBoxHorizontalScale
            // 
            textBoxHorizontalScale.Location = new Point(14, 46);
            textBoxHorizontalScale.Name = "textBoxHorizontalScale";
            textBoxHorizontalScale.Size = new Size(100, 23);
            textBoxHorizontalScale.TabIndex = 3;
            // 
            // textBoxVerticalScale
            // 
            textBoxVerticalScale.Location = new Point(14, 108);
            textBoxVerticalScale.Name = "textBoxVerticalScale";
            textBoxVerticalScale.Size = new Size(100, 23);
            textBoxVerticalScale.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 28);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 4;
            label1.Text = "Year Scaler";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBoxVerticalScale);
            groupBox1.Controls.Add(textBoxHorizontalScale);
            groupBox1.Location = new Point(208, 523);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(187, 203);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Axes Scaler";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 90);
            label2.Name = "label2";
            label2.Size = new Size(124, 15);
            label2.TabIndex = 5;
            label2.Text = "Unemployment Scaler";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkedListBoxStates);
            groupBox2.Location = new Point(1, 523);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(195, 203);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Select States";
            // 
            // UndoButton
            // 
            buttonUndo.Location = new Point(537, 674);
            buttonUndo.Name = "buttonUndo";
            buttonUndo.Size = new Size(117, 31);
            buttonUndo.TabIndex = 7;
            buttonUndo.Text = "Undo";
            buttonUndo.UseVisualStyleBackColor = true;
            buttonUndo.Click += buttonUndo_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1114, 727);
            Controls.Add(buttonUndo);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(buttonSave);
            Controls.Add(buttonRedraw);
            Controls.Add(plotView);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Unemployment Data Graph";
            FormClosing += programClosingSave;
            Load += programOpenLoad;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label label2;
        private GroupBox groupBox2;
    }
}
