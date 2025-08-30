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
            label5 = new Label();
            cbbox_cor = new ComboBox();
            label6 = new Label();
            cbbox_tipo_aplicacao = new ComboBox();
            label7 = new Label();
            cbbox_onde_aplicar = new ComboBox();
            btnColorir = new Button();
            btnAleatorio = new Button();
            btnRedefinir = new Button();
            btnRotacionar = new Button();
            checkBoxRotacaoTransposta = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)trackBarY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRotacao).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarEscala).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonFace;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1334, 692);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            panel1.MouseDoubleClick += panel1_MouseDoubleClick;
            // 
            // trackBarY
            // 
            trackBarY.Location = new Point(1361, 173);
            trackBarY.Maximum = 692;
            trackBarY.Minimum = 1;
            trackBarY.Name = "trackBarY";
            trackBarY.Size = new Size(418, 45);
            trackBarY.TabIndex = 1;
            trackBarY.Value = 346;
            trackBarY.ValueChanged += trackBarY_ValueChanged;
            // 
            // trackBarX
            // 
            trackBarX.Location = new Point(1361, 92);
            trackBarX.Maximum = 1334;
            trackBarX.Name = "trackBarX";
            trackBarX.Size = new Size(418, 45);
            trackBarX.TabIndex = 2;
            trackBarX.Value = 667;
            trackBarX.ValueChanged += trackBarX_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(1361, 50);
            label1.Name = "label1";
            label1.Size = new Size(133, 30);
            label1.TabIndex = 3;
            label1.Text = "Translação X:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(1361, 140);
            label2.Name = "label2";
            label2.Size = new Size(133, 30);
            label2.TabIndex = 4;
            label2.Text = "Translação Y:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(1361, 221);
            label3.Name = "label3";
            label3.Size = new Size(93, 30);
            label3.TabIndex = 5;
            label3.Text = "Rotação:";
            // 
            // trackBarRotacao
            // 
            trackBarRotacao.Location = new Point(1361, 254);
            trackBarRotacao.Maximum = 360;
            trackBarRotacao.Name = "trackBarRotacao";
            trackBarRotacao.Size = new Size(418, 45);
            trackBarRotacao.TabIndex = 6;
            trackBarRotacao.ValueChanged += trackBarRotacao_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(1361, 366);
            label4.Name = "label4";
            label4.Size = new Size(75, 30);
            label4.TabIndex = 7;
            label4.Text = "Escala:";
            // 
            // trackBarEscala
            // 
            trackBarEscala.Location = new Point(1361, 399);
            trackBarEscala.Maximum = 5;
            trackBarEscala.Minimum = 1;
            trackBarEscala.Name = "trackBarEscala";
            trackBarEscala.Size = new Size(418, 45);
            trackBarEscala.TabIndex = 8;
            trackBarEscala.Value = 1;
            trackBarEscala.ValueChanged += trackBarEscala_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(17, 725);
            label5.Name = "label5";
            label5.Size = new Size(50, 30);
            label5.TabIndex = 9;
            label5.Text = "Cor:";
            // 
            // cbbox_cor
            // 
            cbbox_cor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbox_cor.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbbox_cor.FormattingEnabled = true;
            cbbox_cor.Location = new Point(17, 758);
            cbbox_cor.Name = "cbbox_cor";
            cbbox_cor.Size = new Size(330, 38);
            cbbox_cor.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(396, 725);
            label6.Name = "label6";
            label6.Size = new Size(183, 30);
            label6.TabIndex = 11;
            label6.Text = "Tipo de Aplicação:";
            // 
            // cbbox_tipo_aplicacao
            // 
            cbbox_tipo_aplicacao.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbox_tipo_aplicacao.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbbox_tipo_aplicacao.FormattingEnabled = true;
            cbbox_tipo_aplicacao.Location = new Point(396, 758);
            cbbox_tipo_aplicacao.Name = "cbbox_tipo_aplicacao";
            cbbox_tipo_aplicacao.Size = new Size(350, 38);
            cbbox_tipo_aplicacao.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(814, 725);
            label7.Name = "label7";
            label7.Size = new Size(139, 30);
            label7.TabIndex = 13;
            label7.Text = "Onde Aplicar:";
            // 
            // cbbox_onde_aplicar
            // 
            cbbox_onde_aplicar.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbox_onde_aplicar.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbbox_onde_aplicar.FormattingEnabled = true;
            cbbox_onde_aplicar.Location = new Point(814, 758);
            cbbox_onde_aplicar.Name = "cbbox_onde_aplicar";
            cbbox_onde_aplicar.Size = new Size(312, 38);
            cbbox_onde_aplicar.TabIndex = 14;
            // 
            // btnColorir
            // 
            btnColorir.Location = new Point(17, 802);
            btnColorir.Name = "btnColorir";
            btnColorir.Size = new Size(75, 23);
            btnColorir.TabIndex = 15;
            btnColorir.Text = "Colorir";
            btnColorir.UseVisualStyleBackColor = true;
            btnColorir.Click += btnColorir_Click;
            // 
            // btnAleatorio
            // 
            btnAleatorio.Location = new Point(98, 802);
            btnAleatorio.Name = "btnAleatorio";
            btnAleatorio.Size = new Size(75, 23);
            btnAleatorio.TabIndex = 16;
            btnAleatorio.Text = "Aleatorizar";
            btnAleatorio.UseVisualStyleBackColor = true;
            btnAleatorio.Click += btnAleatorio_Click;
            // 
            // btnRedefinir
            // 
            btnRedefinir.Font = new Font("Segoe UI", 16F);
            btnRedefinir.Location = new Point(1150, 758);
            btnRedefinir.Name = "btnRedefinir";
            btnRedefinir.Size = new Size(138, 42);
            btnRedefinir.TabIndex = 17;
            btnRedefinir.Text = "Redefinir";
            btnRedefinir.UseVisualStyleBackColor = true;
            btnRedefinir.Click += btnRedefinir_Click;
            // 
            // btnRotacionar
            // 
            btnRotacionar.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRotacionar.Location = new Point(1361, 305);
            btnRotacionar.Name = "btnRotacionar";
            btnRotacionar.Size = new Size(182, 30);
            btnRotacionar.TabIndex = 18;
            btnRotacionar.Text = "Rotação Automática";
            btnRotacionar.UseVisualStyleBackColor = true;
            btnRotacionar.Click += btnRotacionar_Click;
            // 
            // checkBoxRotacaoTransposta
            // 
            checkBoxRotacaoTransposta.AutoSize = true;
            checkBoxRotacaoTransposta.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxRotacaoTransposta.Location = new Point(1586, 305);
            checkBoxRotacaoTransposta.Name = "checkBoxRotacaoTransposta";
            checkBoxRotacaoTransposta.Size = new Size(193, 29);
            checkBoxRotacaoTransposta.TabIndex = 19;
            checkBoxRotacaoTransposta.Text = "Rotação Transposta";
            checkBoxRotacaoTransposta.UseVisualStyleBackColor = true;
            checkBoxRotacaoTransposta.CheckedChanged += checkBoxRotacaoTransposta_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(1793, 837);
            Controls.Add(checkBoxRotacaoTransposta);
            Controls.Add(btnRotacionar);
            Controls.Add(btnRedefinir);
            Controls.Add(btnAleatorio);
            Controls.Add(btnColorir);
            Controls.Add(cbbox_onde_aplicar);
            Controls.Add(label7);
            Controls.Add(cbbox_tipo_aplicacao);
            Controls.Add(label6);
            Controls.Add(cbbox_cor);
            Controls.Add(label5);
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
        private Label label5;
        private ComboBox cbbox_cor;
        private Label label6;
        private ComboBox cbbox_tipo_aplicacao;
        private Label label7;
        private ComboBox cbbox_onde_aplicar;
        private Button btnColorir;
        private Button btnAleatorio;
        private Button btnRedefinir;
        private Button btnRotacionar;
        private CheckBox checkBoxRotacaoTransposta;
    }
}
