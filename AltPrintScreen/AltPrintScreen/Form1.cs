// Программная имитация нажатия клавиш <Alt>+<PrintScreen> методом Send класса SendKeys

namespace AltPrintScreen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Имитируем нажатие <Alt>+<PrintScreen>";
            button1.Text = "методом Send класса SendKeys";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Метод SendKeys.Send посылает сообщение активному приложению о нажатии клавиш <Alt>+<PrintScreen>
            SendKeys.Send("%{PRTSC}");

            // Также можно получить символьное представление клавиши:
            // var S = Keys.PrintScreen.ToString();
        }
    }
}


// В этой программе при обработке события "щелчок на кнопке" метод Send класса SendKeys посылает сообщение активному приложению о нажатии комбинации клавиш <Alt>+<PrintScreen>.
// Нажатие клавиши <PrintScreen> генерирует код "{PRTSC}". Чтобы указать сочетание клавиш <Alt><PrintScreen>, следует в этот код добавить символ процента: "%{PRTSC}". 