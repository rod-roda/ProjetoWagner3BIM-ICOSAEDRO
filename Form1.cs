using System.Drawing.Drawing2D;
using System.Runtime.ConstrainedExecution;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ProjetoWagner3BIM
{
    public partial class Form1 : Form
    {

        int xOrigem = 667;
        int yOrigem = 346;

        int[] hx0 = { 667, 771, 771, 667, 563, 563 };
        int[] hy0 = { 226, 286, 406, 466, 406, 286 };
        Color[] hcolor = { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) };

        int[][] xs0 = new int[][]
        {
            new[] { 667, 615, 719  }, // triangulo meio
            new[] { 667, 615, 563 }, // triangulo meio-esquerda
            new[] { 667, 719, 771 }, // triangulo meio‑direita
            new[] { 615, 719, 667 }, // triangulo topo
            new[] { 667, 667, 563 }, // triangulo baixo-esquerda
            new[] { 667, 667, 771 }, // triangulo baixo-direita

            new[] { 563, 563, 615 }, // triangulo meio-esquerda-esquerda
            new[] { 771, 771, 719 },  // triangulo meio-direita-direita
            new[] { 615, 563, 667 }, // triangulo topo-esquerda-esquerda
            new[] { 719, 771, 667 } // triangulo topo-direita-direita
        };

        int[][] ys0 = new int[][]
        {
            new[] { 406, 316, 316 }, // triangulo meio
            new[] { 406, 316, 406 }, // triangulo meio-esquerda
            new[] { 406, 316, 406 }, // triangulo meio‑direita
            new[] { 316, 316, 226 }, // triangulo topo
            new[] { 466, 406, 406 }, // triangulo baixo-esquerda
            new[] { 466, 406, 406 }, // triangulo baixo-direita

            new[] { 406, 286, 316 }, // triangulo meio-esquerda-esquerda
            new[] { 406, 286, 316 },  // triangulo meio-direita-direita
            new[] { 316, 286, 226 }, // triangulo topo-esquerda-esquerda
            new[] { 316, 286, 226 } // triangulo topo-direita-direita

        };

        Color[][] tcolor = new Color[][]
        {
            new[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) }, // triangulo meio
            new[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) }, // triangulo meio-esquerda
            new[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) }, // triangulo meio-direita
            new[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) }, // triangulo topo
            new[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) }, // triangulo baixo-esquerda
            new[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) }, // triangulo baixo-direita

            new[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) }, // triangulo meio-esquerda-esquerda
            new[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) }, // triangulo meio-direita-direita
            new[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) }, // triangulo topo-esquerda-esquerda
            new[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) }  // triangulo topo-direita-direita
        };

        int txState = 667, tyState = 346;
        int angState = 0;               // graus ou passos (0..3) se modo 90°
        int sxState = 1, syState = 1;

        // BUFFERS DO ESTADO (pontos prontos para desenhar)
        int[] hxState, hyState;
        int[][] xsState, ysState;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        public Form1()
        {
            InitializeComponent();
            timer.Interval = 500; // 1 segundo
            timer.Tick += Timer_Tick;

            checkBoxRotacaoTransposta.Checked = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            RecalcularEstado();
            IniciarComboBox();
            timer.Enabled = false;
        }

        void IniciarComboBox()
        {
            cbbox_cor.Items.Add("Preto");
            cbbox_cor.Items.Add("Vermelho");
            cbbox_cor.Items.Add("Laranja");
            cbbox_cor.Items.Add("Amarelo");
            cbbox_cor.Items.Add("Verde");
            cbbox_cor.Items.Add("Azul");
            cbbox_cor.Items.Add("Anil");
            cbbox_cor.Items.Add("Violeta");
            cbbox_cor.Items.Add("Branco");

            cbbox_tipo_aplicacao.Items.Add("Contorno");
            cbbox_tipo_aplicacao.Items.Add("Preenchimento");

            cbbox_onde_aplicar.Items.Add("Icosaedro");
            cbbox_onde_aplicar.Items.Add("Triângulo Meio");
            cbbox_onde_aplicar.Items.Add("Triângulo Meio-Esquerda");
            cbbox_onde_aplicar.Items.Add("Triângulo Meio-Direita");
            cbbox_onde_aplicar.Items.Add("Triângulo Topo");
            cbbox_onde_aplicar.Items.Add("Triângulo Baixo-Esquerda");
            cbbox_onde_aplicar.Items.Add("Triângulo Baixo-Direita");
            cbbox_onde_aplicar.Items.Add("Triângulo Meio-Esquerda-Esquerda");
            cbbox_onde_aplicar.Items.Add("Triângulo Meio-Direita-Direita");
            cbbox_onde_aplicar.Items.Add("Triângulo Topo-Esquerda-Esquerda");
            cbbox_onde_aplicar.Items.Add("Triângulo Topo-Direita-Direita");

            cbbox_cor.SelectedIndex = 0;
            cbbox_tipo_aplicacao.SelectedIndex = 0;
            cbbox_onde_aplicar.SelectedIndex = 0;

        }

        Color Cor_primitiva(int red = 0, int green = 0, int blue = 0)
        {
            return Color.FromArgb(red, green, blue);
        }

        Pen caneta_estilo(int espessura, Color cor, float[] estilo = null)
        {
            Pen caneta = new Pen(cor, espessura);
            if (estilo != null) caneta.DashPattern = estilo;
            else caneta.DashStyle = DashStyle.Solid;
            return caneta;
        }
        public void preencherPoligono(PaintEventArgs e, Point[] pontos, Color corPreenchimento)
        {
            SolidBrush fundo = new SolidBrush(corPreenchimento);
            e.Graphics.FillPolygon(fundo, pontos);
        }
        public void preencherPoligono(PaintEventArgs e, PointF[] pontos, Color corPreenchimento)
        {
            SolidBrush fundo = new SolidBrush(corPreenchimento);
            e.Graphics.FillPolygon(fundo, pontos);
        }

        public void desenhaPoligono(PaintEventArgs e, int[] x, int[] y, Color corPreenchimento, Pen caneta)
        {
            if (x.Length == y.Length)
            {
                if (x.Length > 2 && y.Length > 2)
                {
                    Point[] pontos = new Point[x.Length];
                    for (int i = 0; i < x.Length; i++)
                    {

                        Point point1 = new Point(x[i], y[i]);
                        pontos[i] = point1;

                    }

                    e.Graphics.DrawPolygon(caneta, pontos);
                    if (corPreenchimento != Color.Transparent) preencherPoligono(e, pontos, corPreenchimento);
                }
                else
                {
                    MessageBox.Show("É necessário no mínimo três pontos para formar o polígono");
                }
            }
            else
            {
                MessageBox.Show("Vetores de tamanhos diferentes! Favor corrigir");
            }

        }

        public void desenhaPoligono(PaintEventArgs e, double[] x, double[] y, Color corPreenchimento, Pen caneta)
        {
            if (x.Length == y.Length)
            {
                if (x.Length > 2 && y.Length > 2)
                {
                    PointF[] pontos = new PointF[x.Length];
                    for (int i = 0; i < x.Length; i++)
                    {

                        PointF point1 = new PointF((float)x[i], (float)y[i]);
                        pontos[i] = point1;

                    }

                    e.Graphics.DrawPolygon(caneta, pontos);
                    if (corPreenchimento != Color.Transparent) preencherPoligono(e, pontos, corPreenchimento);
                }
                else
                {
                    MessageBox.Show("É necessário no mínimo três pontos para formar o polígono");
                }
            }
            else
            {
                MessageBox.Show("Vetores de tamanhos diferentes! Favor corrigir");
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using var pen = caneta_estilo(2, hcolor[0]);

            RecalcularEstado();

            desenhaPoligono(e, hxState, hyState, hcolor[1], pen);
            for (int i = 0; i < xsState.Length; i++)
                desenhaPoligono(e, xsState[i], ysState[i], tcolor[i][1], caneta_estilo(2, tcolor[i][0]));
        }

        private void trackBarX_ValueChanged(object sender, EventArgs e)
        {
            txState = trackBarX.Value;
            panel1.Invalidate();
        }

        private void trackBarY_ValueChanged(object sender, EventArgs e)
        {
            tyState = trackBarY.Value;
            panel1.Invalidate();
        }

        private void trackBarRotacao_ValueChanged(object sender, EventArgs e)
        {
            angState = trackBarRotacao.Value;
            panel1.Invalidate();
        }

        private void trackBarEscala_ValueChanged(object sender, EventArgs e)
        {
            sxState = Math.Max(1, trackBarEscala.Value);
            syState = sxState;
            panel1.Invalidate();
        }

        private (int[] nx, int[] ny) translacaoX(int[] x, int[] y, int tx)
        {
            int txEff = tx - panel1.Width / 2;
            int[] nx = new int[x.Length];
            for (int i = 0; i < x.Length; i++) nx[i] = x[i] + txEff;
            return (nx, (int[])y.Clone());
        }

        private (int[] nx, int[] ny) translacaoY(int[] x, int[] y, int ty)
        {
            int tyEff = ty - panel1.Height / 2;
            int[] ny = new int[y.Length];
            for (int i = 0; i < y.Length; i++) ny[i] = y[i] + (tyEff * -1);
            return ((int[])x.Clone(), ny);
        }

        private (int[] nx, int[] ny) rotacao(int[] x, int[] y, int angulo)
        {
            int n = x.Length;
            int[] nx = new int[n], ny = new int[n];
            double t = angulo * Math.PI / 180.0;
            double cos = Math.Cos(t), sin = Math.Sin(t);

            for (int i = 0; i < n; i++)
            {
                double dx = x[i] - xOrigem;
                double dy = y[i] - yOrigem;
                double xr = dx * cos - dy * sin;
                double yr = dx * sin + dy * cos;
                nx[i] = (int)Math.Round(xOrigem + xr);
                ny[i] = (int)Math.Round(yOrigem + yr);
            }
            return (nx, ny);
        }

        private (int[] nx, int[] ny) rotacaoTransposicaoMatriz(int[] x, int[] y, int passos)
        {
            passos = ((passos % 4) + 4) % 4;

            if (passos == 0) return ((int[])x.Clone(), (int[])y.Clone());

            int[] coordX = new int[x.Length];
            int[] coordY = new int[y.Length];

            for (int i = 0; i < x.Length; i++)
            {
                coordX[i] = x[i] - xOrigem;
                coordY[i] = y[i] - yOrigem;
            }

            int[,] matrizCoordenadas = new int[2, x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                matrizCoordenadas[0, i] = coordX[i];
                matrizCoordenadas[1, i] = coordY[i];
            }

            for (int passo = 0; passo < passos; passo++)
            {
                matrizCoordenadas = AplicarRotacao90Transposta(matrizCoordenadas);
            }

            int[] resultX = new int[x.Length];
            int[] resultY = new int[y.Length];

            for (int i = 0; i < x.Length; i++)
            {
                resultX[i] = matrizCoordenadas[0, i] + xOrigem;
                resultY[i] = matrizCoordenadas[1, i] + yOrigem;
            }

            return (resultX, resultY);
        }

        private int[,] AplicarRotacao90Transposta(int[,] matriz)
        {
            int linhas = matriz.GetLength(0);
            int colunas = matriz.GetLength(1);

            int[,] matrizTransposta = new int[colunas, linhas];
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    matrizTransposta[j, i] = matriz[i, j];
                }
            }

            int[,] matrizRotacionada = new int[linhas, colunas];
            for (int j = 0; j < colunas; j++)
            {
                matrizRotacionada[0, j] = -matrizTransposta[j, 1];
                matrizRotacionada[1, j] = matrizTransposta[j, 0];
            }

            return matrizRotacionada;
        }

        private (int[] nx, int[] ny) escala(int[] x, int[] y, int escalaX, int escalaY)
        {
            int[] nx = new int[x.Length], ny = new int[y.Length];
            for (int i = 0; i < x.Length; i++)
            {
                double dx = x[i] - xOrigem;
                double dy = y[i] - yOrigem;
                double xs = dx * escalaX;
                double ys = dy * escalaY;
                nx[i] = (int)(xOrigem + xs);
                ny[i] = (int)(yOrigem + ys);
            }
            return (nx, ny);
        }

        private void RecalcularEstado()
        {
            var (xH, yH) = ((int[])hx0.Clone(), (int[])hy0.Clone());

            if (checkBoxRotacaoTransposta.Checked)
            {
                (xH, yH) = rotacaoTransposicaoMatriz(xH, yH, angState);
            }
            else
            {
                (xH, yH) = rotacao(xH, yH, angState);
            }

            (xH, yH) = escala(xH, yH, sxState, syState);
            (xH, yH) = translacaoX(xH, yH, txState);
            (xH, yH) = translacaoY(xH, yH, tyState);
            hxState = xH; hyState = yH;

            xsState = new int[xs0.Length][]; ysState = new int[ys0.Length][];
            for (int i = 0; i < xs0.Length; i++)
            {
                var (xT, yT) = ((int[])xs0[i].Clone(), (int[])ys0[i].Clone());
                if (checkBoxRotacaoTransposta.Checked)
                    (xT, yT) = rotacaoTransposicaoMatriz(xT, yT, angState);
                else
                    (xT, yT) = rotacao(xT, yT, angState);
                (xT, yT) = escala(xT, yT, sxState, syState);
                (xT, yT) = translacaoX(xT, yT, txState);
                (xT, yT) = translacaoY(xT, yT, tyState);
                xsState[i] = xT; ysState[i] = yT;
            }
        }

        private void btnColorir_Click(object sender, EventArgs e)
        {
            if (cbbox_tipo_aplicacao.SelectedIndex == 0)
            {
                int index = cbbox_onde_aplicar.SelectedIndex; Color cor = getColor();
                if (index == 0) { for (int i = 0; i < tcolor.Length; i++) tcolor[i][0] = cor; }
                else { tcolor[index - 1][0] = cor; }
                panel1.Invalidate();
            }
            else if (cbbox_tipo_aplicacao.SelectedIndex == 1)
            {
                int index = cbbox_onde_aplicar.SelectedIndex; Color cor = getColor();
                if (index == 0) { for (int i = 0; i < tcolor.Length; i++) tcolor[i][1] = cor; }
                else { tcolor[index - 1][1] = cor; }
                panel1.Invalidate();
            }
        }

        Color getColor()
        {
            switch (cbbox_cor.SelectedIndex)
            {
                case 0: return Cor_primitiva(0, 0, 0);
                case 1: return Cor_primitiva(255, 0, 0);
                case 2: return Cor_primitiva(255, 127, 0);
                case 3: return Cor_primitiva(255, 255, 0);
                case 4: return Cor_primitiva(0, 255, 0);
                case 5: return Cor_primitiva(0, 0, 255);
                case 6: return Cor_primitiva(75, 0, 130);
                case 7: return Cor_primitiva(148, 0, 211);
                case 8: return Cor_primitiva(255, 255, 255);
                default: return Cor_primitiva(0, 0, 0);
            }
        }

        Color getColor(int index)
        {
            switch (index)
            {
                case 0: return Cor_primitiva(0, 0, 0);
                case 1: return Cor_primitiva(255, 0, 0);
                case 2: return Cor_primitiva(255, 127, 0);
                case 3: return Cor_primitiva(255, 255, 0);
                case 4: return Cor_primitiva(0, 255, 0);
                case 5: return Cor_primitiva(0, 0, 255);
                case 6: return Cor_primitiva(75, 0, 130);
                case 7: return Cor_primitiva(148, 0, 211);
                case 8: return Cor_primitiva(255, 255, 255);
                default: return Cor_primitiva(0, 0, 0);
            }
        }

        private void btnAleatorio_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            if (cbbox_tipo_aplicacao.SelectedIndex == 0)
            {
                for (int i = 0; i < tcolor.Length; i++)
                {
                    int randomIndex = random.Next(0, 9);
                    tcolor[i][0] = getColor(randomIndex);
                }

                panel1.Invalidate();
            }
            else if (cbbox_tipo_aplicacao.SelectedIndex == 1)
            {
                int index = cbbox_onde_aplicar.SelectedIndex;
                if (index == 0)
                {
                    for (int i = 0; i < tcolor.Length; i++)
                    {
                        int randomIndex = random.Next(0, 9);
                        tcolor[i][1] = getColor(randomIndex);
                    }
                }
                else
                {
                    int randomIndex = random.Next(0, 9);
                    tcolor[index - 1][1] = getColor(randomIndex);
                }
                panel1.Invalidate();
            }
        }

        private void btnRedefinir_Click(object sender, EventArgs e)
        {
            cbbox_cor.SelectedIndex = 0;
            cbbox_tipo_aplicacao.SelectedIndex = 0;
            cbbox_onde_aplicar.SelectedIndex = 0;
            trackBarX.Value = 667;
            trackBarY.Value = 346;
            trackBarRotacao.Value = 0;
            trackBarEscala.Value = 1;
            angState = 0;
            for (int i = 0; i < tcolor.Length; i++) { tcolor[i][0] = Color.FromArgb(0, 0, 0); tcolor[i][1] = Color.FromArgb(255, 255, 255); }
            panel1.Invalidate();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (checkBoxRotacaoTransposta.Checked)
                angState = (angState + 1) % 4;
            else
                angState = (angState + 10) % 360;
            trackBarRotacao.Value = angState;
            panel1.Invalidate();
        }

        private void btnRotacionar_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }

        private void checkBoxRotacaoTransposta_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRotacaoTransposta.Checked)
            {
                trackBarRotacao.Minimum = 0;
                trackBarRotacao.Maximum = 3;
                angState = 0;
                trackBarRotacao.Value = 0;
            }
            else
            {
                trackBarRotacao.Minimum = 0;
                trackBarRotacao.Maximum = 360;
                angState = 0;
                trackBarRotacao.Value = 0;
            }
            panel1.Invalidate();
        }

        private bool PontoDentroTriangulo(Point p, Point a, Point b, Point c)
        {
            float Area(Point p1, Point p2, Point p3)
            {
                return Math.Abs((p1.X * (p2.Y - p3.Y) +
                                 p2.X * (p3.Y - p1.Y) +
                                 p3.X * (p1.Y - p2.Y)) / 2f);
            }

            float areaABC = Area(a, b, c);
            float areaPAB = Area(p, a, b);
            float areaPBC = Area(p, b, c);
            float areaPCA = Area(p, c, a);

            return Math.Abs((areaPAB + areaPBC + areaPCA) - areaABC) < 0.5f;
        }


        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < xsState.Length; i++)
            {
                Point a = new Point(xsState[i][0], ysState[i][0]);
                Point b = new Point(xsState[i][1], ysState[i][1]);
                Point c = new Point(xsState[i][2], ysState[i][2]);

                if (PontoDentroTriangulo(e.Location, a, b, c))
                {
                    Color cor = getColor(cbbox_cor.SelectedIndex);

                    if (cbbox_tipo_aplicacao.SelectedIndex == 0)
                        tcolor[i][0] = cor;
                    else
                        tcolor[i][1] = cor;

                    panel1.Invalidate();
                    return;
                }
            }
        }
    }
}
