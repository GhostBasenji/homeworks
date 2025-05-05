// Программа формирует изображение методами класса Graphics, записывает
// его на диск в формате JPG-файла и выводит его отображение в форму

namespace CreateJPG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Показать дату";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Создаем точечное изображение размером 250*40 точек с глубиной цвета 24
            var Risunok = new Bitmap(250, 40, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // Создаем новый объект класса Graphics из изображения РАСТР
            var Grafika = Graphics.FromImage(Risunok);

            // Теперь становятся доступными метода класса Graphics.

            // Заливаем поверхность цветом формы:
            Grafika.Clear(Color.FromName("Control"));

            // Выводим в строку полной даты:
            var Data = String.Format("Сегодня {0:D}", DateTime.Now);

            // Разворачиваем графику на 356 градусов по часовой стрелке:
            Grafika.RotateTransform(356.0F);

            // Выводим на изображение текстовую строку Data,
            // x = 5, y = 15 - координаты левого верхнего угла строки
            Grafika.DrawString(Data, new Font("Times New Roman", 14, FontStyle.Regular), Brushes.Blue, 5, 15);

            // Сохраняем изображение в файле risunok.jpg:
            Risunok.Save("risunok.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            // Задаем стиль границ рисунка:
            pictureBox1.BorderStyle = BorderStyle.None;

            // Загружаем рисунок из файла:
            pictureBox1.Image = Image.FromFile("risunok.jpg");

            // Освобождаем ресурсы:
            Risunok.Dispose(); Grafika.Dispose();
        }
    }
}
