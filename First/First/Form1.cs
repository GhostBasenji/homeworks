// В нашей первой программе мы создаем форму с надписью, например, "Microsoft Visual C#".
// Кроме нее в форме будет расположена кнопка с надписью "Нажми меня".
// При нажатии кнопки откроется диалоговое окно с сообщением "Всем привет!"

namespace First
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Всем привет!");
        }
    }
}
