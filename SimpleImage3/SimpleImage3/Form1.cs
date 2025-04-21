// Простейший вывод изображения в форму 

namespace SimpleImage3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Рисунок";
            button1.Text = "Показать рисунок";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var img = new Bitmap(@"D:\logo.png");
            
            // Создаем графический объект
            var graphics = this.CreateGraphics();
            // или var graphics = CreateGraphics();
            graphics.DrawImage(img, 0, 0);
        }
    }
}
