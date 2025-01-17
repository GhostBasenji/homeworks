// Программа управляет стилем шрифта текста, выведенного на метку Label,
// посредством двух флажков CheckBox. Программа использует побитовый
// оператор (^) Xor — исключающее ИЛИ

namespace CheckBox2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Флажок CheckBox";
            checkBox1.Text = "Полужирный"; checkBox1.Focus();
            checkBox2.Text = "Наклонный";
            label1.Text = "Выбери стиль шрифта";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Font = new Font("Courier New", 14.0F);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Font = new Font("Courier New", 14.0F, label1.Font.Style ^ FontStyle.Bold);
            // Здесь каждый раз при изменении состояния флажка значение параметра 
            // Label1.Font.Style сравнивается с одним и тем же значением FontStyle.Bold. Поскольку
            // между ними стоит побитовый оператор(^) (исключающее ИЛИ), он будет назначать
            // Bold, если текущее состояние Label1.Font.Style "не Bold".А если Label1.Font.Style
            // пребывает в состоянии "Bold", то оператор(^) будет назначать состояние "не Bold".
            // Этот оператор еще называют логическим XOR
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Font = new Font("Courier New", 14.0F, label1.Font.Style ^ FontStyle.Italic);
        }
    }
}


