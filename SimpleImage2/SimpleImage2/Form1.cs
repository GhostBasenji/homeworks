// Простейший вывод изображения в форму 
namespace SimpleImage2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // В свойствах формы щелкаем на значке молнии и в появившемся списке всех событий
            // для объета Form1 выбираем Paint.
            // Событие Paint - это событие рисования формы.
            this.Text = "Рисунок";

            // Создаем объект для работы с изображением:
            var image = Image.FromFile(@"D:\logo.png");
            // или var image = new Bitmap(@"D:\logo.png");

            // Выводим изображение на форму:
            e.Graphics.DrawImage(image, 0, 0);
        }
    }
}
