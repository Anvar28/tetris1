using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tetris1
{
    public partial class Form1 : Form
    {

        struct sBlock
        {
            public bool empty;
            public Color color;

            public void init()
            {
                empty = true;
                color = Color.Gray;
            }
        }

        class tGlass
        {
            private const int width = 10;
            private const int height = 20;
            private const int blockSize = 28;
            private const int blockSizeWithBorder = 30;
            private const int border = 5;

            private const int figureSize = 3;
            private const int figureSizeArray = 4;
            private sBlock[,] figure = new sBlock[figureSizeArray, figureSizeArray];
            private Point figureXY; 

            private sBlock[,] blocks = new sBlock[width, height];

            public tGlass()
            {

                figureXY.X = 0;
                figureXY.Y = 16;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        blocks[i, j].init();
                    }                       
                }

                for (int i = 0; i <= figureSize; i++)
                {
                    for (int j = 0; j <= figureSize; j++)
                    {
                        figure[i, j].init();
                    }
                }

                figure[1, 0].empty = false;
                figure[1, 0].color = Color.Aqua;
                figure[1, 1].empty = false;
                figure[1, 1].color = Color.Aqua;
                figure[1, 2].empty = false;
                figure[1, 2].color = Color.Aqua;
                figure[2, 1].empty = false;
                figure[2, 1].color = Color.Aqua;
            }

            /// <summary>
            /// Прорисовка блока
            /// </summary>
            /// <param name="x">Координаты блока не более ширины стакана</param>
            /// <param name="y">Координаты блока на более высоты стакана</param>
            /// <param name="empty">Пустой блок, фон</param>
            private void drawBlock(int x, int y, Color color, Graphics canvas, bool empty)
            {
                if (x < width && y < height)
                {
                   // Pen pen = new Pen(color, 1);
                    Brush br = empty ? Brushes.Gray : new SolidBrush(color);
                    Rectangle rect = new Rectangle(border + blockSizeWithBorder * x, border + blockSizeWithBorder * y, blockSize, blockSize);

                    canvas.FillRectangle(br, rect);
                }
            }

            /// <summary>
            /// Прорисовка всего стакана
            /// </summary>
            public void draw(Graphics canvas)
            {
                Pen penEmpty = new Pen(Color.Gray, 1);

                // Прорисовка блоков стакана
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        drawBlock(i, j, blocks[i, j].color, canvas, blocks[i, j].empty);
                    }
                }

                // Прорисовка текущей фигуры
                for (int i = 0; i < figureSize; i++)
                {
                    for (int j = 0; j < figureSize; j++)
                    {
                        if (!figure[i, j].empty)
                        {
                            drawBlock(figureXY.X + i, figureXY.Y + j, figure[i, j].color, canvas, figure[i, j].empty);
                        }                        
                    }
                }

            }

            /// <summary>
            /// Поворот фигуры против часовой стрелки
            /// </summary>             
            void turnL()
            {
                sBlock[,] temp = new sBlock[figureSizeArray, figureSizeArray];
                for (int i = 0; i <= figureSize; i++)
                {
                    for (int j = 0; j <= figureSize; j++)
                    {
                        temp[j, i] = blocks[i, figureSize - j];
                    }
                }
                for (int i = 0; i <= figureSize; i++)
                {
                    for (int j = 0; j <= figureSize; j++)
                    {
                        blocks[i, j] = temp[j, i];
                    }
                }
            }

            /// <summary>
            /// Поворот фигуры по часовой стрелки
            /// </summary>
            void turnR()
            {
                sBlock[,] temp = new sBlock[figureSizeArray, figureSizeArray];
                for (int i = 0; i <= figureSize; i++)
                {
                    for (int j = 0; j <= figureSize; j++)
                    {
                        temp[j, i] = blocks[figureSize - i, j];
                    }
                }
                for (int i = 0; i <= figureSize; i++)
                {
                    for (int j = 0; j <= figureSize; j++)
                    {
                        blocks[i, j] = temp[j, i];
                    }
                }
            }

            /// <summary>
            /// Следущий шаг игры, проверка нет ли под фигурой других блоков, если есть
            /// то фиксация фигуры в стакане и проверка нет ли полных строк которые можно удалить из стакана
            /// сдвиг фигуры на одну строку вниз
            /// </summary>
            public void step() {

                // Проверка нет ли под фигурой других блоков в стакане
                bool wayFree = true;
                for (int i = 0; i <= figureSize; i++)
                {
                    int j;
                    // Есть ли у фигуры в колонке не пустой блок
                    for (j = figureSize; j >= 0; j--)
                    {
                        if (!figure[i, j].empty)
                            break;
                    }
                    if (j > -1)
                    {
                        if (figureXY.Y + j + 1 >= height) // уперлись в дно стакана
                        {
                            wayFree = false;
                            break;
                        }
                        if (!blocks[figureXY.X + i, figureXY.Y + j + 1].empty)
                        {
                            wayFree = false;
                            break;
                        }
                    }
                }
                
                if (wayFree)
                {
                    // Сдвиг фигуры на одну строку вниз
                    figureXY.Y++;
                }
                else
                {
                    // Фиксация фигуры в стакане
                    int j;
                    for (int i = 0; i <= figureSize; i++)
                    {
                        for (j = 0; j <= figureSize; j++)
                        {
                            if ((!figure[i, j].empty) && (figureXY.X + i < width) && (figureXY.Y + j < height))
                            {
                                blocks[figureXY.X + i, figureXY.Y + j] = figure[i, j];
                            }                            
                        }
                    }

                    // Проверка нет ли полных строк в стакане
                    j = height;
                    while (j > 0)
                    {
                        j--;

                        bool stringFull = true;
                        int i = 0;
                        while (i < width && stringFull)
                        {
                            stringFull = !blocks[i, j].empty;
                            i++;
                        }
                        if (stringFull)
                        {
                            // смещаем все вышележащие строки вниз
                            for (int j2 = j - 1; j2 < 0; j2--)
                            {
                                for (int i2 = 0; i2 < width; i2++)
                                {
                                    blocks[i2, j2] = blocks[i2, j2 + 1];
                                }
                            }

                            // т.к. вышележащая строка смещается вниз, а она тоже может быть полной
                            // то нужно текущую строку проверить еще раз, увеличим счетчик
                            j++;
                        }
                    }

                    newFigure();
                }
            }

            void newFigure()
            {
                figureXY.X = 0;
                figureXY.Y = 0;

                byte colorCount = 6;
                Color[] colorArray = new Color[6] { Color.Red, Color.White, Color.Black, Color.Azure, Color.Blue, Color.Green };

                Random rnd = new Random();
                Color color = colorArray[rnd.Next(0, colorCount)];

                // Очистка массива
                for (int i = 0; i <= figureSize; i++)
                {
                    for (int j = 0; j <= figureSize; j++)
                    {
                        figure[i, j].empty = true;
                    }
                }

                figure[0, 0].empty = false;
                figure[0, 0].color = color;
                figure[1, 0].empty = false;
                figure[1, 0].color = color;
                figure[1, 1].empty = false;
                figure[1, 1].color = color;
                return;

                switch (rnd.Next(0, 7))
                {
                    case 0:  // L
                        figure[1, 0].empty = false;
                        figure[1, 0].color = color;
                        figure[1, 1].empty = false;
                        figure[1, 1].color = color;
                        figure[1, 2].empty = false;
                        figure[1, 2].color = color;
                        figure[2, 2].empty = false;
                        figure[2, 2].color = color;
                        break;

                    case 1:  // J
                        figure[2, 0].empty = false;
                        figure[2, 0].color = color;
                        figure[2, 1].empty = false;
                        figure[2, 1].color = color;
                        figure[2, 2].empty = false;
                        figure[2, 2].color = color;
                        figure[1, 2].empty = false;
                        figure[1, 2].color = color;
                        break;

                    case 2:  // T
                        figure[2, 0].empty = false;
                        figure[2, 0].color = color;
                        figure[2, 1].empty = false;
                        figure[2, 1].color = color;
                        figure[2, 2].empty = false;
                        figure[2, 2].color = color;
                        figure[1, 1].empty = false;
                        figure[1, 1].color = color;
                        break;

                    case 3:  // I
                        figure[2, 0].empty = false;
                        figure[2, 0].color = color;
                        figure[2, 1].empty = false;
                        figure[2, 1].color = color;
                        figure[2, 2].empty = false;
                        figure[2, 2].color = color;
                        figure[2, 3].empty = false;
                        figure[2, 3].color = color;
                        break;

                    case 4:  // o
                        figure[1, 1].empty = false;
                        figure[1, 1].color = color;
                        figure[2, 1].empty = false;
                        figure[2, 1].color = color;
                        figure[2, 2].empty = false;
                        figure[2, 2].color = color;
                        figure[1, 2].empty = false;
                        figure[1, 2].color = color;
                        break;

                    case 5:  // s
                        figure[0, 2].empty = false;
                        figure[0, 2].color = color;
                        figure[1, 1].empty = false;
                        figure[1, 1].color = color;
                        figure[1, 2].empty = false;
                        figure[1, 2].color = color;
                        figure[2, 1].empty = false;
                        figure[2, 1].color = color;
                        break;

                    case 6:  // z
                        figure[0, 1].empty = false;
                        figure[0, 1].color = color;
                        figure[1, 1].empty = false;
                        figure[1, 1].color = color;
                        figure[1, 2].empty = false;
                        figure[1, 2].color = color;
                        figure[2, 2].empty = false;
                        figure[2, 2].color = color;
                        break;

                    default:
                        break;
                }
            }

            public void left()
            {

            }
        }

        tGlass glass;

        public Form1()
        {
            InitializeComponent();

            glass = new tGlass();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            glass.step();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            glass.draw(e.Graphics);
        }
    }
}
