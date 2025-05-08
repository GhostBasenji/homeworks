// Программа позволяет при нажатой левой или правой кнопке мыши рисовать в форме

namespace DrawMouse
{
    public partial class Form1 : Form
    {
        // Булева переменная Should_I_draw дает разрешение на рисование:
        Boolean Should_I_draw;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Рисую мышью в форме";
            button1.Text = "Стереть";
            Should_I_draw = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Если нажата кнопка мыши - MouseDown, то рисуем:
            Should_I_draw = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // Если кнопку мыши отпустили, то не рисовать:
            Should_I_draw = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Рисуем прямоугольник, если нажата кнопка мыши:
            if (Should_I_draw == true)
            {
                // Рисуем прямоугольник в точке (e.X, e.Y)
                var Grafika = CreateGraphics();
                Grafika.FillRectangle(new SolidBrush(Color.Blue), e.X, e.Y, 10, 10);
                // 10*10 пикселов - размер сплошного прямоугольника
                // e.X, e.Y - координаты указателя мыши
                Grafika.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Методы очистки формы:
            var Grafika = CreateGraphics();
            Grafika.Clear(this.BackColor);
            // this.Refresh(); - этот метод также перерисовывает форму
        }
    }
}


// Здесь в начале программы объявлена переменная Should_I_draw логического типа (Boolean)со значением False. Эта переменная либо позволяет (Should_I_draw = true)
// рисовать в форме при перемещении мыши (событие MouseMove), либо не разрешает делать это (Should_I_draw = false). Область действия переменной Should_I_draw —
// весь класс Form1, т. е. изменить или выяснить ее значение можно в любой процедуре этого класса. 
// Значение переменной Should_I_draw может изменить либо событие MouseUp (кнопку мыши отпустили, рисовать нельзя, Should_I_draw = false), либо событие MouseDown
// (кнопку мыши нажали, рисовать можно, Should_I_draw = frue). При перемещении мыши с нажатой кнопкой программа создает графический объект Graphics пространства
// имен System.Drawing, используя метод CreateGraphics(), и рисует прямоугольник FillRectangle() размером 10*10 пикселов, заполненный красным цветом. e.X, e.Y —
// координаты указателя мыши, которые так же являются координатами левого верхнего угла прямоугольника. 

// Заметим, что можно было бы очистить область рисования более короткой командой Clear(Color.White), т.е. закрасить форму белым цветом (White), либо выбрать другой
// цвет из списка 146 цветов после ввода оператора "точка" (.) за словом Color. Однако ни один из 146 цветов не является первоначальным цветом формы (BackColor).
// Поэтому задаем этот цвет через другие константы цвета, представленные в перечислении Color.FromKnownColor. Также можно задать цвет как Color.FromName("Control").
// Можно использовать функцию перевода шестнадцатеричного кода цвета ColorTranslator.FromHtml(). Очистить форму от всего нарисованного на ней можно также, свернув ее,
// а затем восстановив. Рисовать в форме можно как левой, так и правой кнопками мыши.