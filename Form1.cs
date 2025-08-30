/*
 * 1. Para a definição dos vértices, foi utilizado os pontos de um triângulo central como base, dele partindo os pontos do hexágono.
 * Com os pontos do triângulo central e do hexágono feitos, os outros 9 triângulos foram formados a partir da ligação do triângulo central
 * com os vértices do hexágono, formando assim o Icosaedro da figura. Os vértices foram definidos de maneira absoluta.
 * 
 * 2. A translação, rotação e escala foram feitas conforme o método passado em sala de aula. Porém, algumas particularidades apareceram na aplicação
 * prática. A necessidade de alinhar todos os processos com o centro da figura (xOrigem e yOrigem), além de saber realizar a ordem correta da aplicação
 * de cada transformação na hora de formar todos os pontos (acontecimento que ocorre na função RecalcularEstado()) foram os pontos mais difíceis de se 
 * trabalhar. Além disso, na parte da rotação, quisemos acrescentar uma forma a mais, utilizando a conta geométrica padrão ao invés do conceito de matriz composta,
 * permitindo que o usuário também rotacione por completo a figura. Aplicar esse método adicional também foi uma dificuldade. Entretanto, usamos dos conceitos mate-
 * máticos do próprio ensino médio e da ajuda das IAs Generativas para encontrar a forma e fórmula correta a ser aplicada e, dessa maneira, conseguimos solucionar todos
 * esses pontos de dificuldade apresentados.
 * 
 * 3. No nosso programa, os triângulos tem seus pontos inseridos dentro de um arrayJagged, que nesse contexto atua como uma matriz, mapeando logo de início a posição de
 * cada triângulo, possibilitando tratá-los como formas separadas. Essa separação foi essencial na aplicação do mosaico de cores, já que, graças a ela, conseguimos aplicar
 * a coloração simplesmente através da primitiva já construída anteriormente de "preencherPolígono".
 * 
 * 4. As primitivas já implementadas antes foram as de cor, caneta, preenchimento e desenho do polígono. As novas, foram as destinadas a translação (X e Y), rotação
(tanto no modo tradicional com trigonometria quando no modo por matriz transposta) e escala.
 */

using System.Drawing.Drawing2D;
using System.Runtime.ConstrainedExecution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProjetoWagner3BIM
{
    public partial class Form1 : Form
    {
        // =================== CONFIGURAÇÕES DE GEOMETRIA ===================
        
        /// Ponto de origem para transformações de rotação e escala
        /// Todas as rotações e escalas são aplicadas em relação a este ponto central
        int xOrigem = 667;
        int yOrigem = 346;

        // =================== GEOMETRIA BASE DO HEXÁGONO ===================
        
        /// Coordenadas X originais do hexágono (estado inicial imutável)
        /// Define os 6 vértices do hexágono central do icosaedro
        int[] hx0 = { 667, 771, 771, 667, 563, 563 };
        
        /// Coordenadas Y originais do hexágono (estado inicial imutável)
        /// Define os 6 vértices do hexágono central do icosaedro
        int[] hy0 = { 226, 286, 406, 466, 406, 286 };
        
        /// Cores do hexágono [0] = cor do contorno, [1] = cor de preenchimento
        /// Inicialmente: contorno preto, preenchimento branco
        Color[] hcolor = { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) };

        // =================== GEOMETRIA BASE DOS TRIÂNGULOS ===================
        
        /// Coordenadas X originais dos 10 triângulos que compõem o icosaedro
        /// Cada sub-array contém 3 pontos X de um triângulo específico
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

        /// Coordenadas Y originais dos 10 triângulos que compõem o icosaedro
        /// Cada sub-array contém 3 pontos Y de um triângulo específico
        /// Mesma ordem que xs0 para manter correspondência
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

        /// Matriz de cores para cada triângulo
        /// Para cada triângulo: [0] = cor do contorno, [1] = cor de preenchimento
        /// Inicialmente todos com contorno preto e preenchimento branco
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

        // =================== ESTADO ATUAL DAS TRANSFORMAÇÕES ===================
        
        /// Posição atual de translação (txState, tyState)
        int txState = 667, tyState = 346;
        
        /// Ângulo atual de rotação
        int angState = 0;
        
        /// Fatores de escala atual (X e Y)
        int sxState = 1, syState = 1;

        // =================== BUFFERS DE ESTADO TRANSFORMADO ===================
        
        /// Coordenadas do hexágono após aplicar todas as transformações
        /// Estes arrays conterão os pontos prontos para desenho
        int[] hxState, hyState;
        
        /// Coordenadas dos triângulos após aplicar todas as transformações
        /// Cada elemento é um array de 3 pontos (vértices do triângulo)
        int[][] xsState, ysState;

        // =================== CONTROLE DE ANIMAÇÃO ===================
        
        /// Timer para animação automática de rotação
        /// Incrementa o ângulo periodicamente quando ativado
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent(); // Inicializa os componentes do designer
            timer.Interval = 500; // Define intervalo de 500ms entre cada tick do timer
            timer.Tick += Timer_Tick; // Associa o evento do timer

            // Inicia com modo de rotação por transposição de matriz ativado
            checkBoxRotacaoTransposta.Checked = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RecalcularEstado(); // Calcula o estado inicial das figuras
            IniciarComboBox(); // Popula os ComboBoxes com as opções
            timer.Enabled = false; // Timer inicia desabilitado
        }

        // =================== INICIALIZAÇÃO DA INTERFACE ===================

        /// Popula os ComboBoxes com as opções disponíveis
        /// Configura as cores, tipos de aplicação e elementos do icosaedro>
        void IniciarComboBox()
        {
            // Adiciona as opções de cores disponíveis
            cbbox_cor.Items.Add("Preto");
            cbbox_cor.Items.Add("Vermelho");
            cbbox_cor.Items.Add("Laranja");
            cbbox_cor.Items.Add("Amarelo");
            cbbox_cor.Items.Add("Verde");
            cbbox_cor.Items.Add("Azul");
            cbbox_cor.Items.Add("Anil");
            cbbox_cor.Items.Add("Violeta");
            cbbox_cor.Items.Add("Branco");

            // Adiciona os tipos de aplicação de cor
            cbbox_tipo_aplicacao.Items.Add("Contorno"); // Cor da borda
            cbbox_tipo_aplicacao.Items.Add("Preenchimento"); // Cor interna

            // Adiciona os elementos que podem ser coloridos
            cbbox_onde_aplicar.Items.Add("Icosaedro"); // Todos os elementos
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

            // Define seleções padrão
            cbbox_cor.SelectedIndex = 0; // Preto
            cbbox_tipo_aplicacao.SelectedIndex = 0; // Contorno
            cbbox_onde_aplicar.SelectedIndex = 0; // Icosaedro
        }

        // =================== PRIMITIVAS GRÁFICAS AUXILIARES ===================

        /// Primitiva de Cor
        Color Cor_primitiva(int red = 0, int green = 0, int blue = 0)
        {
            return Color.FromArgb(red, green, blue);
        }

        /// Primitiva do Objeto Caneta
        Pen caneta_estilo(int espessura, Color cor, float[] estilo = null)
        {
            Pen caneta = new Pen(cor, espessura);
            if (estilo != null) caneta.DashPattern = estilo; // Aplica padrão tracejado
            else caneta.DashStyle = DashStyle.Solid; // Linha sólida
            return caneta;
        }

        /// Primitiva de Preenchimento do Polígono
        public void preencherPoligono(PaintEventArgs e, Point[] pontos, Color corPreenchimento)
        {
            SolidBrush fundo = new SolidBrush(corPreenchimento);
            e.Graphics.FillPolygon(fundo, pontos);
        }

        /// Primitiva de Desenho do Polígono, base para o programa
        public void desenhaPoligono(PaintEventArgs e, int[] x, int[] y, Color corPreenchimento, Pen caneta)
        {
            // Validação: arrays devem ter o mesmo tamanho
            if (x.Length == y.Length)
            {
                // Validação: precisa de pelo menos 3 pontos para formar um polígono
                if (x.Length > 2 && y.Length > 2)
                {
                    // Converte arrays de coordenadas para array de Points
                    Point[] pontos = new Point[x.Length];
                    for (int i = 0; i < x.Length; i++)
                    {
                        Point point1 = new Point(x[i], y[i]);
                        pontos[i] = point1;
                    }

                    // Desenha o contorno do polígono
                    e.Graphics.DrawPolygon(caneta, pontos);

                    // Preenche o polígono com a cor de preenchimento enviada
                    preencherPoligono(e, pontos, corPreenchimento);
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
            // Cria caneta para o hexágono usando a cor de contorno
            using var pen = caneta_estilo(2, hcolor[0]);

            // Recalcula todas as transformações antes de desenhar
            RecalcularEstado();

            // Desenha o hexágono central com sua cor de preenchimento e contorno
            desenhaPoligono(e, hxState, hyState, hcolor[1], pen);
            
            // Desenha todos os triângulos com suas respectivas cores
            for (int i = 0; i < xsState.Length; i++)
                desenhaPoligono(e, xsState[i], ysState[i], tcolor[i][1], caneta_estilo(2, tcolor[i][0]));
        }

        // =================== EVENTOS DOS CONTROLES DE TRANSFORMAÇÃO ===================

        /// Evento do TrackBar de translação X - move a figura horizontalmente
        private void trackBarX_ValueChanged(object sender, EventArgs e)
        {
            txState = trackBarX.Value; // Atualiza estado de translação X
            panel1.Invalidate(); // Força redesenho do painel
        }

        /// Evento do TrackBar de translação Y - move a figura verticalmente
        private void trackBarY_ValueChanged(object sender, EventArgs e)
        {
            tyState = trackBarY.Value; // Atualiza estado de translação Y
            panel1.Invalidate(); // Força redesenho do painel
        }

        /// Evento do TrackBar de rotação - gira a figura
        private void trackBarRotacao_ValueChanged(object sender, EventArgs e)
        {
            angState = trackBarRotacao.Value; // Atualiza ângulo de rotação
            panel1.Invalidate(); // Força redesenho do painel
        }

        /// Evento do TrackBar de escala - redimensiona a figura
        private void trackBarEscala_ValueChanged(object sender, EventArgs e)
        {
            sxState = Math.Max(1, trackBarEscala.Value); // Garante escala mínima de 1
            syState = sxState; // Mantém proporção (escala uniforme)
            panel1.Invalidate(); // Força redesenho do painel
        }

        // =================== PRIMITIVAS DE TRANSFORMAÇÃO GEOMÉTRICA ===================
        // =================== O CORE DO PROGRAMA ESTÁ AQUI ===================

        /// Aplica translação no eixo X (movimento horizontal)
        private (int[] nx, int[] ny) translacaoX(int[] x, int[] y, int tx)
        {
            int txEff = tx - xOrigem; // Calcula deslocamento efetivo em relação à origem
            int[] nx = new int[x.Length];
            
            // Aplica translação a cada ponto
            for (int i = 0; i < x.Length; i++) 
                nx[i] = x[i] + txEff;
            
            return (nx, (int[])y.Clone()); // Retorna X transformado e Y inalterado
        }

        /// Aplica translação no eixo Y (movimento vertical)
        private (int[] nx, int[] ny) translacaoY(int[] x, int[] y, int ty)
        {
            int tyEff = ty - yOrigem; // Calcula deslocamento efetivo em relação à origem
            int[] ny = new int[y.Length];
            
            // Aplica translação a cada ponto (invertendo sinal para sistema de coordenadas da tela)
            for (int i = 0; i < y.Length; i++) 
                ny[i] = y[i] + (tyEff * -1);
            
            return ((int[])x.Clone(), ny); // Retorna X inalterado e Y transformado
        }

        /// Aplica rotação usando trigonometria tradicional
        /// Rotaciona todos os pontos em torno do ponto de origem
        private (int[] nx, int[] ny) rotacao(int[] x, int[] y, int angulo)
        {
            int n = x.Length;
            int[] nx = new int[n], ny = new int[n];
            
            // Converte graus para radianos
            double t = angulo * Math.PI / 180.0;
            double cos = Math.Cos(t), sin = Math.Sin(t);

            // Aplica matriz de rotação a cada ponto
            for (int i = 0; i < n; i++)
            {
                // Translada para origem
                double dx = x[i] - xOrigem;
                double dy = y[i] - yOrigem;
                
                // Aplica rotação: [cos -sin][dx]
                //                [sin  cos][dy]
                double xr = dx * cos - dy * sin;
                double yr = dx * sin + dy * cos;
                
                // Translada de volta e arredonda para inteiro
                nx[i] = (int)Math.Round(xOrigem + xr);
                ny[i] = (int)Math.Round(yOrigem + yr);
            }
            return (nx, ny);
        }

        // =================== ROTAÇÃO POR TRANSPOSIÇÃO DE MATRIZ ===================

        /// Aplica rotação em passos de 90° usando transposição de matriz
        private (int[] nx, int[] ny) rotacaoTransposicaoMatriz(int[] x, int[] y, int passos)
        {
            // Normaliza passos para o intervalo 0-3 (0°, 90°, 180°, 270°)
            passos = ((passos % 4) + 4) % 4;

            // Se não há rotação, retorna cópia dos arrays originais
            if (passos == 0) return ((int[])x.Clone(), (int[])y.Clone());

            // Translada pontos para origem (facilita a rotação)
            int[] coordX = new int[x.Length];
            int[] coordY = new int[y.Length];

            for (int i = 0; i < x.Length; i++)
            {
                coordX[i] = x[i] - xOrigem; // Vai para o ponto 0
                coordY[i] = y[i] - yOrigem; // Vai para o ponto 0
            }

            // Cria matriz de coordenadas [2 x n]
            // Linha 0: coordenadas X, Linha 1: coordenadas Y
            int[,] matrizCoordenadas = new int[2, x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                matrizCoordenadas[0, i] = coordX[i]; // Linha X
                matrizCoordenadas[1, i] = coordY[i]; // Linha Y
            }

            // Aplica rotações de 90° sucessivas usando transposição
            for (int passo = 0; passo < passos; passo++)
            {
                matrizCoordenadas = AplicarRotacao90Transposta(matrizCoordenadas);
            }

            // Extrai resultados e usa a transposição para voltar a posição original
            int[] resultX = new int[x.Length];
            int[] resultY = new int[y.Length];

            for (int i = 0; i < x.Length; i++)
            {
                resultX[i] = matrizCoordenadas[0, i] + xOrigem; // Restaura o X removido inicialmente
                resultY[i] = matrizCoordenadas[1, i] + yOrigem; // Restaura o Y removido inicialmente
            }

            return (resultX, resultY);
        }

        /// Aplica uma rotação de 90° anti-horário usando transposição de matriz
        /// 1. Transpõe a matriz (troca linhas por colunas)
        /// 2. Aplica transformação (x,y) → (-y,x) para rotação 90° anti-horário
        private int[,] AplicarRotacao90Transposta(int[,] matriz)
        {
            int linhas = matriz.GetLength(0); // 2 (linhas X e Y)
            int colunas = matriz.GetLength(1); // n (número de pontos)

            // PASSO 1: Transposição da matriz [2 x n] → [n x 2]
            int[,] matrizTransposta = new int[colunas, linhas];
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    matrizTransposta[j, i] = matriz[i, j]; // Troca índices i,j por j,i
                }
            }

            // PASSO 2: Aplicação da rotação 90° anti-horário
            // Transformação: (x,y) → (-y,x)
            int[,] matrizRotacionada = new int[linhas, colunas];
            for (int j = 0; j < colunas; j++)
            {
                // Nova linha X recebe -Y da transposta (rotação)
                matrizRotacionada[0, j] = -matrizTransposta[j, 1];
                // Nova linha Y recebe X da transposta
                matrizRotacionada[1, j] = matrizTransposta[j, 0];
            }

            return matrizRotacionada;
        }

        /// Aplica transformação de escala (redimensionamento)
        private (int[] nx, int[] ny) escala(int[] x, int[] y, int escalaX, int escalaY)
        {
            int[] nx = new int[x.Length], ny = new int[y.Length];
            
            for (int i = 0; i < x.Length; i++)
            {
                // Calcula distância do ponto ao centro de escala
                double dx = x[i] - xOrigem;
                double dy = y[i] - yOrigem;
                
                // Aplica fatores de escala
                double xs = dx * escalaX;
                double ys = dy * escalaY;
                
                // Reposiciona ponto escalado
                nx[i] = (int)(xOrigem + xs);
                ny[i] = (int)(yOrigem + ys);
            }
            return (nx, ny);
        }

        /// <summary>
        /// Recalcula o estado de todas as figuras aplicando todas as transformações feitas até o momento + a atual
        /// ORDEM: Rotação → Escala → Translação X → Translação Y
        private void RecalcularEstado()
        {
            // ====== PROCESSAMENTO DO HEXÁGONO ======
            
            // Clona coordenadas originais (preserva dados base)
            var (xH, yH) = ((int[])hx0.Clone(), (int[])hy0.Clone());

            // Aplica rotação (modo selecionado pelo usuário)
            if (checkBoxRotacaoTransposta.Checked)
            {
                // rotação por transposição de matriz
                (xH, yH) = rotacaoTransposicaoMatriz(xH, yH, angState);
            }
            else
            {
                // rotação trigonométrica
                (xH, yH) = rotacao(xH, yH, angState);
            }

            // Aplica transformações subsequentes
            (xH, yH) = escala(xH, yH, sxState, syState);     // Escala
            (xH, yH) = translacaoX(xH, yH, txState);         // Translação X
            (xH, yH) = translacaoY(xH, yH, tyState);         // Translação Y
            
            // Armazena resultado final no buffer de estado
            hxState = xH; hyState = yH;

            // ====== PROCESSAMENTO DOS TRIÂNGULOS ======
            
            // Inicializa arrays de estado para todos os triângulos
            xsState = new int[xs0.Length][]; 
            ysState = new int[ys0.Length][];
            
            // Processa cada triângulo individualmente
            for (int i = 0; i < xs0.Length; i++)
            {
                // Clona coordenadas originais do triângulo i
                var (xT, yT) = ((int[])xs0[i].Clone(), (int[])ys0[i].Clone());
                
                // Aplica as mesmas transformações
                if (checkBoxRotacaoTransposta.Checked)
                    (xT, yT) = rotacaoTransposicaoMatriz(xT, yT, angState);
                else
                    (xT, yT) = rotacao(xT, yT, angState);
                
                (xT, yT) = escala(xT, yT, sxState, syState);
                (xT, yT) = translacaoX(xT, yT, txState);
                (xT, yT) = translacaoY(xT, yT, tyState);
                
                // Armazena resultado no buffer de estado
                xsState[i] = xT; ysState[i] = yT;
            }
        }

        // =================== SISTEMA DE COLORAÇÃO ===================

        /// Evento do botão Colorir - aplica cor selecionada ao elemento escolhido
        private void btnColorir_Click(object sender, EventArgs e)
        {
            // Verifica se é aplicação de cor no contorno
            if (cbbox_tipo_aplicacao.SelectedIndex == 0)
            {
                int index = cbbox_onde_aplicar.SelectedIndex; 
                Color cor = getColor();
                
                if (index == 0) 
                { 
                    // Aplica a todos os triângulos (Icosaedro completo)
                    for (int i = 0; i < tcolor.Length; i++) 
                        tcolor[i][0] = cor; 
                }
                else 
                { 
                    // Aplica a triângulo específico
                    tcolor[index - 1][0] = cor; 
                }
                panel1.Invalidate(); // Força redesenho
            }
            // Aplicação de cor no preenchimento
            else if (cbbox_tipo_aplicacao.SelectedIndex == 1)
            {
                int index = cbbox_onde_aplicar.SelectedIndex; Color cor = getColor();
                if (index == 0) { for (int i = 0; i < tcolor.Length; i++) tcolor[i][1] = cor; }
                else { tcolor[index - 1][1] = cor; }
                panel1.Invalidate();
            }
        }

        /// Converte seleção do ComboBox de cor para objeto Color
        Color getColor()
        {
            switch (cbbox_cor.SelectedIndex)
            {
                case 0: return Cor_primitiva(0, 0, 0);         // Preto
                case 1: return Cor_primitiva(255, 0, 0);       // Vermelho
                case 2: return Cor_primitiva(255, 127, 0);     // Laranja
                case 3: return Cor_primitiva(255, 255, 0);     // Amarelo
                case 4: return Cor_primitiva(0, 255, 0);       // Verde
                case 5: return Cor_primitiva(0, 0, 255);       // Azul
                case 6: return Cor_primitiva(75, 0, 130);      // Anil (Índigo)
                case 7: return Cor_primitiva(148, 0, 211);     // Violeta
                case 8: return Cor_primitiva(255, 255, 255);   // Branco
                default: return Cor_primitiva(0, 0, 0);        // Preto (fallback)
            }
        }

        /// Versão sobrecarregada de getColor que aceita índice direto
        /// Usada para coloração aleatória
        Color getColor(int index)
        {
            switch (index)
            {
                case 0: return Cor_primitiva(0, 0, 0);         // Preto
                case 1: return Cor_primitiva(255, 0, 0);       // Vermelho
                case 2: return Cor_primitiva(255, 127, 0);     // Laranja
                case 3: return Cor_primitiva(255, 255, 0);     // Amarelo
                case 4: return Cor_primitiva(0, 255, 0);       // Verde
                case 5: return Cor_primitiva(0, 0, 255);       // Azul
                case 6: return Cor_primitiva(75, 0, 130);      // Anil
                case 7: return Cor_primitiva(148, 0, 211);     // Violeta
                case 8: return Cor_primitiva(255, 255, 255);   // Branco
                default: return Cor_primitiva(0, 0, 0);        // Preto (fallback)
            }
        }

        /// Evento do botão Aleatório - aplica cores aleatórias aos elementos
        private void btnAleatorio_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            
            // Coloração aleatória para contorno
            if (cbbox_tipo_aplicacao.SelectedIndex == 0)
            {
                // Aplica cor aleatória a todos os triângulos
                for (int i = 0; i < tcolor.Length; i++)
                {
                    int randomIndex = random.Next(0, 9); // Escolhe cor aleatória (0-8)
                    tcolor[i][0] = getColor(randomIndex);
                }
                panel1.Invalidate();
            }
            // Coloração aleatória para preenchimento
            else if (cbbox_tipo_aplicacao.SelectedIndex == 1)
            {
                int index = cbbox_onde_aplicar.SelectedIndex;
                
                if (index == 0)
                {
                    // Aplica cores aleatórias a todos os triângulos
                    for (int i = 0; i < tcolor.Length; i++)
                    {
                        int randomIndex = random.Next(0, 9);
                        tcolor[i][1] = getColor(randomIndex);
                    }
                }
                else
                {
                    // Aplica cor aleatória a triângulo específico
                    int randomIndex = random.Next(0, 9);
                    tcolor[index - 1][1] = getColor(randomIndex);
                }
                panel1.Invalidate();
            }
        }

        /// Evento do botão Redefinir - restaura todas as configurações para o estado inicial
        private void btnRedefinir_Click(object sender, EventArgs e)
        {
            // Restaura seleções dos ComboBoxes
            cbbox_cor.SelectedIndex = 0;
            cbbox_tipo_aplicacao.SelectedIndex = 0;
            cbbox_onde_aplicar.SelectedIndex = 0;
            
            // Restaura valores dos TrackBars
            trackBarX.Value = 667;         // Posição X original
            trackBarY.Value = 346;         // Posição Y original
            trackBarRotacao.Value = 0;     // Sem rotação
            trackBarEscala.Value = 1;      // Escala original
            
            // Restaura estado interno
            angState = 0;
            
            // Restaura cores de todos os triângulos
            for (int i = 0; i < tcolor.Length; i++) 
            { 
                tcolor[i][0] = Color.FromArgb(0, 0, 0);     // Contorno preto
                tcolor[i][1] = Color.FromArgb(255, 255, 255); // Preenchimento branco
            }
            
            panel1.Invalidate(); // Força redesenho
        }

        /// Evento do Timer - executa animação automática de rotação
        /// Incrementa o ângulo periodicamente criando efeito de rotação contínua
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (checkBoxRotacaoTransposta.Checked)
                // Modo transposição: avança 1 passo (90°) por vez
                angState = (angState + 1) % 4;
            else
                // Modo tradicional: avança 10° por vez
                angState = (angState + 10) % 360;
            
            trackBarRotacao.Value = angState; // Atualiza interface
            panel1.Invalidate(); // Força redesenho
        }

        /// Evento do botão Rotacionar - liga/desliga a animação automática
        private void btnRotacionar_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled; // Alterna estado do timer
        }

        /// Evento do CheckBox de rotação transposta - alterna modo de rotação
        /// Controla se usa rotação trigonométrica ou transposição de matriz
        private void checkBoxRotacaoTransposta_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRotacaoTransposta.Checked)
            {
                // MODO TRANSPOSIÇÃO DE MATRIZ (acadêmico)
                trackBarRotacao.Minimum = 0;
                trackBarRotacao.Maximum = 3;    // 4 passos (0°, 90°, 180°, 270°)
                angState = 0;
                trackBarRotacao.Value = 0;
            }
            else
            {
                // MODO TRIGONOMÉTRICO (tradicional)
                trackBarRotacao.Minimum = 0;
                trackBarRotacao.Maximum = 360;  // 360 graus
                angState = 0;
                trackBarRotacao.Value = 0;
            }
            panel1.Invalidate(); // Força redesenho
        }

        // =================== SISTEMA DE INTERAÇÃO POR CLIQUE ===================

        /// Verifica se um ponto está dentro de um triângulo usando cálculo de áreas
        /// ALGORITMO: Um ponto P está dentro do triângulo ABC se:
        /// Área(ABC) = Área(PAB) + Área(PBC) + Área(PCA)
        private bool PontoDentroTriangulo(Point p, Point a, Point b, Point c)
        {
            // Função local para calcular área de um triângulo
            float Area(Point p1, Point p2, Point p3)
            {
                return Math.Abs((p1.X * (p2.Y - p3.Y) +
                                 p2.X * (p3.Y - p1.Y) +
                                 p3.X * (p1.Y - p2.Y)) / 2f);
            }

            // Calcula área do triângulo original
            float areaABC = Area(a, b, c);
            
            // Calcula áreas dos sub-triângulos formados pelo ponto P
            float areaPAB = Area(p, a, b);
            float areaPBC = Area(p, b, c);
            float areaPCA = Area(p, c, a);

            // Se soma das sub-áreas ≈ área original, ponto está dentro
            return Math.Abs((areaPAB + areaPBC + areaPCA) - areaABC) < 0.5f;
        }

        /// Evento de duplo clique no painel - permite colorir triângulos individualmente
        /// Detecta qual triângulo foi clicado e aplica a cor selecionada
        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Testa cada triângulo para ver se o clique foi dentro dele
            for (int i = 0; i < xsState.Length; i++)
            {
                // Extrai os 3 vértices do triângulo i
                Point a = new Point(xsState[i][0], ysState[i][0]);
                Point b = new Point(xsState[i][1], ysState[i][1]);
                Point c = new Point(xsState[i][2], ysState[i][2]);

                // Verifica se o clique foi dentro deste triângulo
                if (PontoDentroTriangulo(e.Location, a, b, c))
                {
                    // Obtém cor selecionada no ComboBox
                    Color cor = getColor(cbbox_cor.SelectedIndex);

                    // Aplica cor conforme tipo selecionado
                    if (cbbox_tipo_aplicacao.SelectedIndex == 0)
                        tcolor[i][0] = cor; // Contorno
                    else
                        tcolor[i][1] = cor; // Preenchimento

                    panel1.Invalidate(); // Força redesenho
                    return; // Sai após encontrar o primeiro triângulo
                }
            }
        }
    }
}
