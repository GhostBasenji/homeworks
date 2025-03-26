// Программа, информирующая пользователя о тех клавишах и комбинациях клавиш, которые он нажал 

namespace KeyboardEvents
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Font = new Font(FontFamily.GenericMonospace, 14.0F);
            // Поскольку мы задали этот шрифт увеличенным (от 8 по умолчанию до 14),
            // форма окажется пропорционально увеличенной
            this.Text = "Какие клавиши нажаты сейчас:";
            label1.Text = String.Empty; label2.Text = String.Empty;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Здесь обрабатываем событие нажатия клавиши. При удержании клавиши событие генерируется непрерывно.
            label1.Text = "Нажата клавиша: " + e.KeyChar;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Здесь обрабатываем мгновенное событие первоначального нажатия клавиши
            label2.Text = String.Empty;
            // Если нажата клавиша <Alt>
            if (e.Alt == true) label2.Text += "Alt: Yes\n";
            else label2.Text += "Alt: No\n";
            // Если нажата клавиша <Shift>
            if (e.Shift == true) label2.Text += "Shift: Yes\n";
            else label2.Text += "Shift: No\n";
            // Если нажата клавиша <Ctrl>
            if (e.Control == true) label2.Text += "Ctrl: Yes\n";
            else label2.Text += "Ctrl: No\n";
            label2.Text += String.Format("Код клавиши: {0} \nKeyData: {1} \nKeyValue: {2}", e.KeyCode, e.KeyData, e.KeyValue);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // Очищаем метки при отпускании клавиши
            label1.Text = String.Empty; label2.Text = String.Empty;
        }
    }
}


// События клавиатуры(клавишные события) создаются в момент нажатия или отпускания ее клавиш. Различают событие KeyPress, которое генерируется в момент нажатия
// клавиши. При удержании клавиши в нажатом состоянии оно генерируется непрерывно с некоторой частотой. С помощью этого события можно распознать нажатую клавишу,
// если только она не является так называемой модифицирующей, т. е. <Alt>, <Shift> и <Ctrl>. А вот для того чтобы распознать, нажата ли модифицирующая клавиша <Alt>,
// <Shift> или <Ctrl>, следует обработать либо событие KeyDown, либо событие KeyUp. Событие KeyDown генерируется в первоначальный момент нажатия клавиши, а событие KeyUp — в момент ее отпускания. 

// В первую текстовую метку записываем сведения о нажатой обычной (т. е. не модифицирующей и не функциональной) клавише при обработке события KeyPress.
// Во вторую метку из аргумента события e (e.Alt, e.Shift и e.Control) получаем сведения, была ли нажата какая-либо модифицирующая клавиша (либо их комбинация).
// Обработчик события KeyUp очищает обе метки при освобождении клавиш. 