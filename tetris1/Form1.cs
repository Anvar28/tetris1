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
            private const int _width = 10;
            private const int _height = 20;
            private const int _blockSize = 28;
            private const int _blockSizeWithBorder = 30;
            private const int _border = 5;

            private const int _figureSize = 3;
            private const int _figureSizeArray = _figureSize+1;
            private sBlock[,] _figure = new sBlock[_figureSizeArray, _figureSizeArray];
            private Point _figureXY;
            private int _score;
            public int score { get { return _score; } }
            private bool _gameOver;
            public bool gameOver { get { return _gameOver; } }

            private sBlock[,] blocks = new sBlock[_width, _height];

            public tGlass()
            {
                init();
            }

            public void init()
            {
                _figureXY.X = 0;
                _figureXY.Y = 0;
                _gameOver = false;

                for (int i = 0; i < _width; i++)
                    for (int j = 0; j < _height; j++)
                        blocks[i, j].init();

                newFigure();
            }

            /// <summary>
            /// Прорисовка блока
            /// </summary>
            /// <param name="x">Координаты блока не более ширины стакана</param>
            /// <param name="y">Координаты блока на более высоты стакана</param>
            /// <param name="empty">Пустой блок, фон</param>
            private void drawBlock(int x, int y, Color color, Graphics canvas, bool empty, bool k = false)
            {
                if (x < _width && y < _height)
                {
                    Pen pen = new Pen(Color.Red, 1);
                    Brush br = empty ? Brushes.Silver : new SolidBrush(color);
                    Rectangle rect = new Rectangle(_border + _blockSizeWithBorder * x, _border + _blockSizeWithBorder * y, _blockSize, _blockSize);

                    canvas.FillRectangle(br, rect);
                    if (k)
                        canvas.DrawRectangle(pen, rect);
                }
            }

            /// <summary>
            /// Прорисовка всего стакана
            /// </summary>
            public void draw(Graphics canvas)
            {
                Pen penEmpty = new Pen(Color.Gray, 1);

                // Прорисовка блоков стакана
                for (int i = 0; i < _width; i++)
                    for (int j = 0; j < _height; j++)
                        drawBlock(i, j, blocks[i, j].color, canvas, blocks[i, j].empty);

                // Прорисовка текущей фигуры
                for (int i = 0; i <= _figureSize; i++)
                    for (int j = 0; j <= _figureSize; j++)
                        if (!_figure[i, j].empty)
                            drawBlock(_figureXY.X + i, _figureXY.Y + j, _figure[i, j].color, canvas, _figure[i, j].empty);

            }

            /// <summary>
            /// Поворот фигуры против часовой стрелки
            /// </summary>             
            public void turnL()
            {
                // Поворот фигуры во временном массиве
                sBlock[,] figureTemp = new sBlock[_figureSizeArray, _figureSizeArray];
                for (int i = 0; i <= _figureSize; i++)
                    for (int j = 0; j <= _figureSize; j++)
                        figureTemp[i, j] = _figure[_figureSize-j, i];

                // Проверка не накладываются ли блоки фигуры на блоки в стакане, если нет, то переносим
                // фигуру из временного массива в основной
                if (!checkCollision(figureTemp, _figureXY))
                {
                    for (int i = 0; i <= _figureSize; i++)
                        for (int j = 0; j <= _figureSize; j++)
                            _figure[i, j] = figureTemp[i, j];
                }
            }

            /// <summary>
            ///  Проверка не накладываются ли блоки фигуры на блоки в стакане
            /// </summary>
            public bool checkCollision(sBlock[,] figureTemp, Point figureXYTemp)
            {
                bool result = false;
                // Проверка не накладываются ли блоки фигуры на блоки в стакане
                for (int i = 0; i <= _figureSize; i++)
                    for (int j = 0; j <= _figureSize; j++)
                        if (!figureTemp[i, j].empty)
                            if (!blocks[figureXYTemp.X + i, figureXYTemp.Y + j].empty)
                            {
                                result = true;
                                break;
                            }


                return result;
            }

            /// <summary>
            /// Следущий шаг игры, проверка нет ли под фигурой других блоков, если есть
            /// то фиксация фигуры в стакане и проверка нет ли полных строк которые можно удалить из стакана
            /// сдвиг фигуры на одну строку вниз
            /// </summary>
            public void step() {

                // Проверка нет ли под фигурой других блоков в стакане
                bool wayFree = true;
                for (int i = 0; i <= _figureSize; i++)
                {
                    int j;
                    // Есть ли у фигуры в колонке не пустой блок
                    for (j = _figureSize; j >= 0; j--)
                        if (!_figure[i, j].empty)
                            break;

                    if (j > -1)
                    {
                        if (_figureXY.Y + j + 1 >= _height) // уперлись в дно стакана
                        {
                            wayFree = false;
                            break;
                        }
                        if (!blocks[_figureXY.X + i, _figureXY.Y + j + 1].empty) // ниже лежит блок в стакане
                        {
                            wayFree = false;
                            break;
                        }
                    }
                }
                
                if (wayFree)
                {
                    // Сдвиг фигуры на одну строку вниз
                    _figureXY.Y++;
                }
                else
                {
                    // Фиксация фигуры в стакане
                    int j;
                    for (int i = 0; i <= _figureSize; i++)
                        for (j = 0; j <= _figureSize; j++)
                            if ((!_figure[i, j].empty) && (_figureXY.X + i < _width) && (_figureXY.Y + j < _height))
                                blocks[_figureXY.X + i, _figureXY.Y + j] = _figure[i, j];

                    // Проверка нет ли полных строк в стакане
                    j = _height;
                    while (j > 0)
                    {
                        j--;

                        bool stringFull = true;
                        int i = 0;
                        while (i < _width && stringFull)
                        {
                            stringFull = !blocks[i, j].empty;
                            i++;
                        }
                        if (stringFull)
                        {
                            // смещаем все вышележащие строки вниз
                            for (int j2 = j - 1; j2 > 0; j2--)
                                for (int i2 = 0; i2 < _width; i2++)
                                    blocks[i2, j2+1] = blocks[i2, j2];

                            // т.к. вышележащая строка смещается вниз, а она тоже может быть полной
                            // то нужно текущую строку проверить еще раз, увеличим счетчик
                            j++;

                            // Счетчик очков
                            _score++;
                        }
                    }

                    newFigure();
                }
            }

            /// <summary>
            /// Генерация новой фигуры
            /// </summary>
            void newFigure()
            {
                _figureXY.X = 4;
                _figureXY.Y = 0;

                byte colorCount = 6;
                Color[] colorArray = new Color[6] { Color.Red, Color.White, Color.Black, Color.Azure, Color.Blue, Color.Green };

                Random rnd = new Random();
                Color color = colorArray[rnd.Next(0, colorCount)];

                // Очистка массива
                for (int i = 0; i <= _figureSize; i++)
                    for (int j = 0; j <= _figureSize; j++)
                        _figure[i, j].init();

                switch (rnd.Next(0, 7))
                {
                    case 0:  // L
                        _figure[1, 0].empty = false;
                        _figure[1, 0].color = color;
                        _figure[1, 1].empty = false;
                        _figure[1, 1].color = color;
                        _figure[1, 2].empty = false;
                        _figure[1, 2].color = color;
                        _figure[2, 2].empty = false;
                        _figure[2, 2].color = color;
                        break;

                    case 1:  // J
                        _figure[2, 0].empty = false;
                        _figure[2, 0].color = color;
                        _figure[2, 1].empty = false;
                        _figure[2, 1].color = color;
                        _figure[2, 2].empty = false;
                        _figure[2, 2].color = color;
                        _figure[1, 2].empty = false;
                        _figure[1, 2].color = color;
                        break;

                    case 2:  // T
                        _figure[2, 0].empty = false;
                        _figure[2, 0].color = color;
                        _figure[2, 1].empty = false;
                        _figure[2, 1].color = color;
                        _figure[2, 2].empty = false;
                        _figure[2, 2].color = color;
                        _figure[1, 1].empty = false;
                        _figure[1, 1].color = color;
                        break;

                    case 3:  // I
                        _figure[1, 0].empty = false;
                        _figure[1, 0].color = color;
                        _figure[1, 1].empty = false;
                        _figure[1, 1].color = color;
                        _figure[1, 2].empty = false;
                        _figure[1, 2].color = color;
                        _figure[1, 3].empty = false;
                        _figure[1, 3].color = color;
                        break;

                    case 4:  // o
                        _figure[1, 1].empty = false;
                        _figure[1, 1].color = color;
                        _figure[2, 1].empty = false;
                        _figure[2, 1].color = color;
                        _figure[2, 2].empty = false;
                        _figure[2, 2].color = color;
                        _figure[1, 2].empty = false;
                        _figure[1, 2].color = color;
                        break;

                    case 5:  // s
                        _figure[0, 2].empty = false;
                        _figure[0, 2].color = color;
                        _figure[1, 1].empty = false;
                        _figure[1, 1].color = color;
                        _figure[1, 2].empty = false;
                        _figure[1, 2].color = color;
                        _figure[2, 1].empty = false;
                        _figure[2, 1].color = color;
                        break;

                    case 6:  // z
                        _figure[0, 1].empty = false;
                        _figure[0, 1].color = color;
                        _figure[1, 1].empty = false;
                        _figure[1, 1].color = color;
                        _figure[1, 2].empty = false;
                        _figure[1, 2].color = color;
                        _figure[2, 2].empty = false;
                        _figure[2, 2].color = color;
                        break;

                    default:
                        break;
                }

                // Проверка не наложится ли фигура на другие блоки в стакане
                if (checkCollision(_figure, _figureXY))
                {
                    _gameOver = true;
                }
            }

            /// <summary>
            /// Передвижение фигуры влево с проверкой
            /// </summary>
            public void left()
            {
                Point newFigureXY = _figureXY;
                newFigureXY.X--;
                if (checkFigureMove(newFigureXY))
                    if (!checkCollision(_figure, newFigureXY))
                        _figureXY = newFigureXY;
            }

            /// <summary>
            /// Передвижение фигуры вправо с проверкой
            /// </summary>
            public void right()
            {
                Point newFigureXY = _figureXY;
                newFigureXY.X++;
                if (checkFigureMove(newFigureXY))
                    if (!checkCollision(_figure, newFigureXY))
                        _figureXY = newFigureXY;
            }

            /// <summary>
            /// Возвращает минимальную и максимальную колоки где есть не пустые блоки в фигуре
            /// </summary>
            /// <returns></returns>
            (int min, int max) getFigureLimit()
            {
                int maxI = 0, minI = _figureSize;

                for (int i = 0; i <= _figureSize; i++)
                    for (int j = 0; j <= _figureSize; j++)
                        if (!_figure[i, j].empty)
                        {
                            maxI = maxI < i ? i : maxI;
                            minI = minI > i ? i : minI;
                        }
                return (min: minI, max: maxI);
            }

            /// <summary>
            /// Проверка не выходит ли фигура за границы
            /// </summary>
            bool checkFigureMove(Point newFigureXY)
            {
                bool result = true;
                (int min, int max) figureLimit = getFigureLimit();

                if (newFigureXY.X + figureLimit.min < 0)
                    result = false;

                if (newFigureXY.X + figureLimit.max >= _width)
                    result = false;

                return result;
            }

        }

        tGlass glass;
        int oldInterval;
        const int maxSpeed = 25;
        

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
            lScore.Text = glass.score.ToString();
            if (glass.gameOver)
            {
                timer1.Enabled = false;
                lGameOver.Visible = true;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            glass.draw(e.Graphics);
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    glass.left();
                    break;
                case Keys.Right:
                    glass.right();
                    break;
                case Keys.Up:
                    glass.turnL();
                    break;
                case Keys.Down:
                    if (timer1.Interval != maxSpeed)
                    {
                        oldInterval = timer1.Interval;
                        timer1.Interval = maxSpeed;
                    }
                    break;
            }
            pictureBox1.Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                timer1.Interval = oldInterval;
            }
        }

        private void lNewGame_MouseHover(object sender, EventArgs e)
        {
            (sender as Label).BackColor = Color.Silver;
        }

        private void lNewGame_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).BackColor = SystemColors.Control;
        }

        private void lNewGame_Click(object sender, EventArgs e)
        {
            glass.init();
            timer1.Enabled = true;
            BackColor = SystemColors.Control;
            lGameOver.Visible = false;
        }

        private void lPausa_Click(object sender, EventArgs e)
        {
            if (!glass.gameOver)
            {
                timer1.Enabled = !timer1.Enabled;
                BackColor = timer1.Enabled ? SystemColors.Control : Color.Silver;
            }
        }

    }
}
