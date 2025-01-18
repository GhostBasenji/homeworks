// Программа, позволяющая выбрать текст из двух вариантов, задать цвет
// и размер шрифта для этого текста на трех вкладках TabControl с
// использованием переключателей RadioButton

namespace Tabs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Создаем третью вкладку "программно":
            var tabPage3 = new TabPage();
            tabPage3.Text = "Third Tab";
            tabPage3.UseVisualStyleBackColor = true;
            // Добавляем третью вкладку в существующий набор вкладок tabControl1:
            tabControl1.TabPages.Add(tabPage3);

            // Создаем переключатели 5 и 6:
            RadioButton radioButton5 = new RadioButton();
            RadioButton radioButton6 = new RadioButton();
            radioButton5.Location = new Point(20, 15);
            radioButton6.Location = new Point(20, 58);

            radioButton5.CheckedChanged += radioButton5_CheckedChanged;
            radioButton6.CheckedChanged += radioButton6_CheckedChanged;


            // Добавляем переключателей (RadioButton) 5 и 6 на третью вкладку:
            tabPage3.Controls.Add(radioButton5);
            tabPage3.Controls.Add(radioButton6);
            this.Text = "Какая улыбка Вам ближе";

            // Задаем названия вкладок:
            tabControl1.TabPages[0].Text = "Текст";
            tabControl1.TabPages[1].Text = "Цвет";
            tabControl1.TabPages[2].Text = "Размер";

            // Эта пара переключателей изменяет текст:
            radioButton1.Text = "Восхищенная, сочувственная, " + "\n" + "скромно-смущенная";
            radioButton2.Text = "Нежная улыбка, ехидная, бесстыжая, " + "\n" + "подленькая, снисходительная";
            // или
            // radioButton2.Text = "Нежная улыбка, бесстыжая," + Environment.NewLine + "подленькая, снисходительная"

            // Эта пара переключателей изменяет цвет текста:
            radioButton3.Text = "Красный";
            radioButton4.Text = "Синий";

            // Эта пара переключателей изменяет размер шрифта:
            radioButton5.Text = "11 пунктов";
            radioButton6.Text = "13 пунктов";
            label1.Text = radioButton1.Text;
        }

        // Ниже обрабатываем события изменения состояния шести переключателей
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = radioButton1.Text;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = radioButton2.Text;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Blue;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            label1.Font = new Font("Arial", 11);
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            label1.Font = new Font("Arial", 13);
        }
    }
}
