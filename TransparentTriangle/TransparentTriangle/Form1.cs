// Программируем форму, в которой размещен прозрачный треугольник

namespace TransparentTriangle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.ClientSize = new Size(240, 200);

            // Задаем вершины треугольника
            var p1 = new Point(20, 20);
            var p2 = new Point(225, 66);
            var p3 = new Point(80, 185);

            // Инициализируем массив точек
            Point[] points = { p1, p2, p3 };

            // Закрашиваем треугольник цветом ControlDark
            e.Graphics.FillPolygon(new SolidBrush(SystemColors.ControlDark), points);
            // Цвет ControlDark задаем прозрачным
            this.TransparencyKey = SystemColors.ControlDark;
        }
    }
}


// При обработке события перерисовки формы назначим три вершины треугольника, инициализируя массив точек.
// Объект Graphics получим из аргумента события e. Воспользуемся методом FillPolygon для рисования закрашенного
// многоугольника. Цвет закрашивания может быть любым, однако этот же цвет необходимо назначить в качестве
// прозрачного в свойстве формы TransparencyKey. 