// Программа демонстрирует возможность вывода в текстовую метку, а также 
// в диалоговое окно MessageBox греческих букв. Программа приглашает 
// пользователя ввести радиус R, чтобы вычислить длину окружности 

namespace Unico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Font = new Font("Times New Roman", 12.0F);
            this.Text = "Греческие буквы"; button1.Text = "Вычислить";
            // бета = 2 * Пи * R
            label1.Text = String.Format("Найдем длину окружности: \n" + " {0} = 2{1}{2}{1}R, \n" + "где {2} = {3}",
                Convert.ToChar(0x3B2), Convert.ToChar(0x2219),
                Convert.ToChar(0x3C0), Math.PI);
            // Здесь: 0 = бета, 1 - точка, 2 - Пи, 3 - число Пи
            label2.Text = "Введите радиус R:";
            textBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, число ли введено:
            Single R; // - радиус окружности
            var isNumeric = Single.TryParse(textBox1.Text,
                System.Globalization.NumberStyles.Number,
                System.Globalization.NumberFormatInfo.CurrentInfo,
                out R);

            if (isNumeric == false)
            {
                MessageBox.Show("Следует вводить числа!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var beta = 2 * Math.PI * R;
            // 0x3B2 — греческая буква бета 
            MessageBox.Show(String.Format("Длина окружности {0} = {1:F4}",
                           Convert.ToChar(0x3B2), beta), "Греческая буква");
        }
    }
}
