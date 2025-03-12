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

// Используя метод String.Format, инициализировали свойство Text метки label1. Различные шестнадцатеричные номера соответствуют греческим буквам
// и арифметической операции "умножить", в инициализации строки участвует также константа Pi = 3,14. Ее более точное значение получаем из Math.PI.
// Escape-последовательность "\n" используем для переноса текста на новую строку. Т.н. перевод каретки можно осуществить также с помощью строки
// NewLine из перечисления Environment.
// Обрабатывая событие button1_Click (щелчок на кнопке), мы проверяем с помощью метода TryParse, число ли введено в текстовое поле.
// Если пользователь ввел число (true), то метод TryParse возвращает значение радиуса R. При вычислении длины окружности beta приводим
// значение константы Math.PI из типа Double к типу Single посредством неявного преобразования.
// После вычисления длины окружности beta выводим ее значение вместе с греческой буквой  — Convert.ToChar(0x3B2) в диалоговое окно MessageBox.
// Здесь используем метод String.Format. Выражение "{0:F4}" означает, что значение переменной beta следует выводить по фиксированному формату
// с четырьмя знаками после запятой.
// Данная программа будет корректно отображать греческие буквы, даже если открыть файл Form1.cs текстовым редактором Блокнот и сохранить его в кодировке ANSI.