using System.Drawing.Drawing2D;
using System.Runtime.ConstrainedExecution;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ProjetoWagner3BIM
{
    public partial class Form1 : Form
    {
        //int[] xs = { 667, 771, 771, 667, 563, 563 };
        //int[] ys = { 226, 286, 406, 466, 406, 286 };
        //int[] centro = { 667, 346 };

        bool translationX = false;
        bool translationY = false;
        bool rotation = false;
        bool scale = false;

        int xOrigem = 667;
        int yOrigem = 346;

        int[] hx0 = { 667, 771, 771, 667, 563, 563 };
        int[] hy0 = { 226, 286, 406, 466, 406, 286 };

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

        int txState = 0, tyState = 0;   // os mesmos inteiros que seus métodos usam
        int angState = 0;               // graus
        int sxState = 1, syState = 1;   // fatores inteiros (igual sua escala)

        // BUFFERS DO ESTADO (pontos prontos para desenhar)
        int[] hxState, hyState;
        int[][] xsState, ysState;

        public Form1()
        {
            InitializeComponent();
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

        public void translacaoX(PaintEventArgs e, int[] x, int[] y, Color cor, Pen corlinha, int tx)
        {
            tx = tx - panel1.Width / 2;
            int[] novoX = new int[x.Length];

            for (int c = 0; c < x.Length; c++)
            {
                novoX[c] = x[c] + tx;
            }

            desenhaPoligono(e, novoX, y, cor, corlinha);
            translationX = false;
        }

        public void translacaoY(PaintEventArgs e, int[] x, int[] y, Color cor, Pen corlinha, int ty)
        {
            ty = ty - panel1.Height / 2;
            int[] novoY = new int[y.Length];

            for (int c = 0; c < x.Length; c++)
            {
                novoY[c] = y[c] + (ty * -1);
            }

            desenhaPoligono(e, x, novoY, cor, corlinha);
            translationY = false;
        }

        public void rotacao(PaintEventArgs e, int[] x, int[] y, Color cor, Pen corlinha, int angulo)
        {
            int n = x.Length;
            int[] novoX = new int[n];
            int[] novoY = new int[n];

            double t = angulo * Math.PI / 180.0;
            double cos = Math.Cos(t);
            double sin = Math.Sin(t);

            for (int i = 0; i < n; i++)
            {
                double dx = x[i] - xOrigem;
                double dy = y[i] - yOrigem;

                double xr = dx * cos - dy * sin;
                double yr = dx * sin + dy * cos;

                novoX[i] = (int)Math.Round(xOrigem + xr);
                novoY[i] = (int)Math.Round(yOrigem + yr);
            }

            desenhaPoligono(e, novoX, novoY, cor, corlinha);
            rotation = false;
        }

        public void escala(PaintEventArgs e, int[] x, int[] y, Color cor, Pen corlinha, int escalaX, int escalaY)
        {
            int[] novoX = new int[x.Length];
            int[] novoY = new int[y.Length];

            for (int c = 0; c < x.Length; c++)
            {
                double dx = x[c] - xOrigem;
                double dy = y[c] - yOrigem;

                double xs = dx * escalaX;
                double ys = dy * escalaY;

                novoX[c] = (int)(xOrigem + xs);
                novoY[c] = (int)(yOrigem + ys);
            }

            desenhaPoligono(e, novoX, novoY, cor, corlinha);
            scale = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using var pen = caneta_estilo(2, Color.Black);

            if (translationX)
            {
                translacaoX(e, hxState, hyState, Color.Transparent, pen, panel1.Width / 2);
                for (int i = 0; i < xsState.Length; i++)
                    translacaoX(e, xsState[i], ysState[i], Color.Transparent, pen, panel1.Width / 2);
                translationX = false;
            }
            else if (translationY)
            {
                translacaoY(e, hxState, hyState, Color.Transparent, pen, panel1.Height / 2);
                for (int i = 0; i < xsState.Length; i++)
                    translacaoY(e, xsState[i], ysState[i], Color.Transparent, pen, panel1.Height / 2);
                translationY = false;
            }
            else if (rotation)
            {
                rotacao(e, hxState, hyState, Color.Transparent, pen, 0);
                for (int i = 0; i < xsState.Length; i++)
                    rotacao(e, xsState[i], ysState[i], Color.Transparent, pen, 0);
                rotation = false;
            }
            else if (scale)
            {
                escala(e, hxState, hyState, Color.Transparent, pen, 1, 1);
                for (int i = 0; i < xsState.Length; i++)
                    escala(e, xsState[i], ysState[i], Color.Transparent, pen, 1, 1);
                scale = false;
            }
            else
            {
                // Sem flag: escolha qualquer identidade (ex.: rotacao 0)
                rotacao(e, hxState, hyState, Color.Transparent, pen, 0);
                for (int i = 0; i < xsState.Length; i++)
                    rotacao(e, xsState[i], ysState[i], Color.Transparent, pen, 0);
            }
        }

        private void trackBarX_ValueChanged(object sender, EventArgs e)
        {
            txState = trackBarX.Value;
            translationX = true;
            RecalcularEstado();
            panel1.Invalidate();
        }

        private void trackBarY_ValueChanged(object sender, EventArgs e)
        {
            tyState = trackBarY.Value;
            translationY = true;
            RecalcularEstado();
            panel1.Invalidate();
        }

        private void trackBarRotacao_ValueChanged(object sender, EventArgs e)
        {
            angState = trackBarRotacao.Value;
            rotation = true;
            RecalcularEstado();
            panel1.Invalidate();
        }

        private void trackBarEscala_ValueChanged(object sender, EventArgs e)
        {
            sxState = Math.Max(1, trackBarEscala.Value);
            syState = sxState;
            scale = true;
            RecalcularEstado();
            panel1.Invalidate();
        }

        private (int[] nx, int[] ny) CalcTranslacaoX(int[] x, int[] y, int tx)
        {
            int txEff = tx - panel1.Width / 2;
            int[] nx = new int[x.Length];
            for (int i = 0; i < x.Length; i++) nx[i] = x[i] + txEff;
            return (nx, (int[])y.Clone());
        }

        private (int[] nx, int[] ny) CalcTranslacaoY(int[] x, int[] y, int ty)
        {
            int tyEff = ty - panel1.Height / 2;
            int[] ny = new int[y.Length];
            for (int i = 0; i < y.Length; i++) ny[i] = y[i] + (tyEff * -1);
            return ((int[])x.Clone(), ny);
        }

        private (int[] nx, int[] ny) CalcRotacao(int[] x, int[] y, int angulo)
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

        private (int[] nx, int[] ny) CalcEscala(int[] x, int[] y, int escalaX, int escalaY)
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
            // Hexágono
            var (xH, yH) = ((int[])hx0.Clone(), (int[])hy0.Clone());

            // 1) ROTACIONA em torno de (xOrigem, yOrigem)
            (xH, yH) = CalcRotacao(xH, yH, angState);

            // 2) ESCALA em torno de (xOrigem, yOrigem)
            (xH, yH) = CalcEscala(xH, yH, sxState, syState);

            // 3) TRANSLADA (usa seus mesmos cálculos com panel.Width/Height)
            (xH, yH) = CalcTranslacaoX(xH, yH, txState);
            (xH, yH) = CalcTranslacaoY(xH, yH, tyState);

            hxState = xH; hyState = yH;

            // Triângulos (mesma ordem)
            xsState = new int[xs0.Length][];
            ysState = new int[ys0.Length][];
            for (int i = 0; i < xs0.Length; i++)
            {
                var (xT, yT) = ((int[])xs0[i].Clone(), (int[])ys0[i].Clone());
                (xT, yT) = CalcRotacao(xT, yT, angState);
                (xT, yT) = CalcEscala(xT, yT, sxState, syState);
                (xT, yT) = CalcTranslacaoX(xT, yT, txState);
                (xT, yT) = CalcTranslacaoY(xT, yT, tyState);
                xsState[i] = xT; ysState[i] = yT;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            RecalcularEstado();
        }
    }
}
