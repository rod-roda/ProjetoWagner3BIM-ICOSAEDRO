namespace ProjetoWagner3BIM
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
            panel1 = new Panel();
            trackBarY = new TrackBar();
            trackBarX = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            trackBarRotacao = new TrackBar();
            label4 = new Label();
            trackBarEscala = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)trackBarY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRotacao).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarEscala).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1334, 692);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // trackBarY
            // 
            trackBarY.Location = new Point(151, 788);
            trackBarY.Maximum = 692;
            trackBarY.Minimum = 1;
            trackBarY.Name = "trackBarY";
            trackBarY.Size = new Size(273, 45);
            trackBarY.TabIndex = 1;
            trackBarY.Value = 346;
            trackBarY.ValueChanged += trackBarY_ValueChanged;
            // 
            // trackBarX
            // 
            trackBarX.Location = new Point(151, 737);
            trackBarX.Maximum = 1334;
            trackBarX.Name = "trackBarX";
            trackBarX.Size = new Size(273, 45);
            trackBarX.TabIndex = 2;
            trackBarX.Value = 667;
            trackBarX.ValueChanged += trackBarX_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 737);
            label1.Name = "label1";
            label1.Size = new Size(133, 30);
            label1.TabIndex = 3;
            label1.Text = "Translação X:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 788);
            label2.Name = "label2";
            label2.Size = new Size(133, 30);
            label2.TabIndex = 4;
            label2.Text = "Translação Y:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(479, 737);
            label3.Name = "label3";
            label3.Size = new Size(93, 30);
            label3.TabIndex = 5;
            label3.Text = "Rotação:";
            // 
            // trackBarRotacao
            // 
            trackBarRotacao.Location = new Point(578, 737);
            trackBarRotacao.Maximum = 360;
            trackBarRotacao.Name = "trackBarRotacao";
            trackBarRotacao.Size = new Size(273, 45);
            trackBarRotacao.TabIndex = 6;
            trackBarRotacao.ValueChanged += trackBarRotacao_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(479, 788);
            label4.Name = "label4";
            label4.Size = new Size(75, 30);
            label4.TabIndex = 7;
            label4.Text = "Escala:";
            // 
            // trackBarEscala
            // 
            trackBarEscala.Location = new Point(578, 788);
            trackBarEscala.Maximum = 5;
            trackBarEscala.Minimum = 1;
            trackBarEscala.Name = "trackBarEscala";
            trackBarEscala.Size = new Size(273, 45);
            trackBarEscala.TabIndex = 8;
            trackBarEscala.Value = 1;
            trackBarEscala.ValueChanged += trackBarEscala_ValueChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1358, 845);
            Controls.Add(trackBarEscala);
            Controls.Add(label4);
            Controls.Add(trackBarRotacao);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(trackBarX);
            Controls.Add(trackBarY);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackBarY).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarX).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRotacao).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarEscala).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TrackBar trackBarY;
        private TrackBar trackBarX;
        private Label label1;
        private Label label2;
        private Label label3;
        private TrackBar trackBarRotacao;
        private Label label4;
        private TrackBar trackBarEscala;
    }
}
