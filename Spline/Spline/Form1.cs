// Программа строит сплайн Безье по двум узловым точкам, а две контрольные (управляющие) точки совмещены в одну. Эта одна управляющая точка
// отображается в форме в виде красного прямоугольника. Перемещая указателем мыши управляющую точку, мы регулируем форму сплайна (кривой)

namespace Spline
{
    public partial class Form1 : Form
    {
        PointF[] MassivTochek;
        Boolean Manage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Manage = false;
            this.Text = "Управление сплайном Безье";

            // Начальная узловая точка:
            var p0 = new PointF(50, 50);

            // Две контрольные точки, мы их совместим в одну:
            var p1 = new PointF(125, 125);
            var p2 = new PointF(125, 125);

            // Конечная узловая точка:
            var p3 = new PointF(200.0F, 200.0F);

            MassivTochek = new PointF[] { p0, p1, p2, p3 };
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Задаем поверхность для рисования из аргумента события е:
            var Grafika = e.Graphics;
            var Pero = new Pen(Color.Blue, 3);

            // Рисуем начальную и конечную узловые точки диаметром 4 пиксела:
            Grafika.DrawEllipse(Pero, MassivTochek[0].X - 2, MassivTochek[0].Y - 2, 4.0F, 4.04F);
            Grafika.DrawEllipse(Pero, MassivTochek[3].X - 2, MassivTochek[3].Y - 2, 4.0F, 4.04F);

            // Одна управляющая точка в виде прямоугольника красного цвета:
            Pero.Color = Color.Red;
            Grafika.DrawRectangle(Pero, MassivTochek[1].X - 2, MassivTochek[1].Y - 2, 4.0F, 4.0F);
            Pero.Color = Color.Blue;

            // Рисуем сплайн Безье:
            Grafika.DrawBeziers(Pero, MassivTochek);

            // Освобождаем ресурсы:
            Grafika.Dispose();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Событие перемещения курсора мыши в области формы.
            // Если курсор мыши расположен над управляющей точкой
            if (Math.Abs(e.X - MassivTochek[1].X) < 4.0F &&
                Math.Abs(e.Y - MassivTochek[1].Y) < 4.0F &&
                Manage == true)
            {
                // и при этом нажата кнопка мыши,
                // то меняем координаты управляющей точки:
                MassivTochek[1].X = e.X;
                MassivTochek[1].Y = e.Y;
                MassivTochek[2].X = e.X;
                MassivTochek[2].Y = e.Y;
                // и обновляем (перерисовываем) форму
                this.Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // Если кнопку мыши отпустили, то запрещаем управлять формой кривой:
            Manage = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Если нажата кнопка мыши, то разрешаем управлять формой кривой:
            Manage = true;
        }
    }
}
