// Программа Калькулятор с кнопками цифр. Управление калькулятором возможно 
// только мышью. Данный калькулятор выполняет лишь арифметические операции

namespace Calc
{
    public partial class Form1 : Form
    {
        // Объявляем внешние переменные, видимые из всех процедур класса Form1:
        String Znak = String.Empty; // Знак арифметической операции
        // Признак того, что пользователь вводит новое число:
        Boolean Start_Vvod = true;
        // Первое и второе числа, вводимые пользователем:
        Double Number1, Number2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Калькулятор";
            button1.Text = "1"; button2.Text = "2"; button3.Text = "3";
            button4.Text = "4"; button5.Text = "5"; button6.Text = "6";
            button7.Text = "7"; button8.Text = "8"; button9.Text = "9";
            button10.Text = "0"; button11.Text = "="; button12.Text = "+";
            button13.Text = "-"; button14.Text = "*"; button15.Text = "/";
            button16.Text = "Очистить";
            textBox1.Text = "0"; textBox1.TextAlign = HorizontalAlignment.Right;

            // Связываем все события "щелчок на кнопках-цифрах" c обработчиком NUMBER_Click и OPERATIONS_Click:
            this.button1.Click += new System.EventHandler(this.NUMBER_Click);
            this.button2.Click += new System.EventHandler(this.NUMBER_Click);
            this.button3.Click += new System.EventHandler(this.NUMBER_Click);
            this.button4.Click += new System.EventHandler(this.NUMBER_Click);
            this.button5.Click += new System.EventHandler(this.NUMBER_Click);
            this.button6.Click += new System.EventHandler(this.NUMBER_Click);
            this.button7.Click += new System.EventHandler(this.NUMBER_Click);
            this.button8.Click += new System.EventHandler(this.NUMBER_Click);
            this.button9.Click += new System.EventHandler(this.NUMBER_Click);
            this.button10.Click += new System.EventHandler(this.NUMBER_Click);

            this.button12.Click += new System.EventHandler(this.OPERATIONS_Click);
            this.button13.Click += new System.EventHandler(this.OPERATIONS_Click);
            this.button14.Click += new System.EventHandler(this.OPERATIONS_Click);
            this.button15.Click += new System.EventHandler(this.OPERATIONS_Click);

            this.button11.Click += new System.EventHandler(this.RAVNO);
            this.button16.Click += new System.EventHandler(this.CLEAR);
        }

        private void NUMBER_Click(object sender, EventArgs e)
        {
            // Обработка события нажатия кнопки-цифры.
            // Получить текст, отображаемый на кнопке можно так:
            Button Knopka = (Button)sender;
            String Digit = Knopka.Text;

            if (Start_Vvod == true)
            {
                // Ввод первой цифры числа:
                textBox1.Text = Digit;
                Start_Vvod = false; return;
            }
            // "Сцепляем" полученные цифры в новое число:
            if (Start_Vvod == false)
                textBox1.Text = textBox1.Text + Digit;
        }

        private void OPERATIONS_Click(object sender, EventArgs e)
        {
            // Обработка события нажатия кнопки арифметической операции:
            Number1 = Double.Parse(textBox1.Text);
            // Получить текст, отображаемый на кнопке можно так:
            Button Knopka = (Button)sender;
            Znak = Knopka.Text;
            Number1 = Convert.ToDouble(textBox1.Text);
            Start_Vvod = true;
        }

        private void RAVNO(object sender, EventArgs e)
        {
            // Обработка события нажатия кнопки "равно"
            double Resultat = 0;
            Number2 = Double.Parse(textBox1.Text);
            if (Znak == "+") Resultat = Number1 + Number2;
            if (Znak == "-") Resultat = Number1 - Number2;
            if (Znak == "*") Resultat = Number1 * Number2;
            if (Znak == "/") Resultat = Number1 / Number2;
            Znak = null;

            // Отображаем результат в текстовом поле:
            textBox1.Text = Resultat.ToString();
            Number1 = Resultat; Start_Vvod = true;
        }

        private void CLEAR(object sender, EventArgs e)
        {
            // Обработка события нажатия кнопки "Очистить"
            textBox1.Text = "0"; Znak = null; Start_Vvod = true;
        }
    }
}


// В этом коде мы отказались от задания свойств элементов управления и формы в окне Properties, чтобы не потеряться в огромном количестве этих свойств
// и не забывать, какое свойство мы изменили, а какое нет. Исходя из этих соображений, мы задали все свойства объектов в процедуре обработки события загрузки
// формы Form1_Load. Именно здесь заданы надписи на кнопках, нуль в текстовом поле, причем этот нуль прижат к правому краю поля:
// textBox1.TextAlign = HorizontalAlignment.Right.

// Далее связываем все события Click от кнопок-цифр с одной процедурой обработки этих событий NUMBER_Click. Аналогично все события Click
// от кнопок арифметических операций связываем с одной процедурой OPERATIONS_Click. 

// В процедуре обработки события "щелчок на любой из кнопок-цифр" NUMBER_Click в строковую переменную Digit копируем цифру, изображенную на кнопке из свойства Text так, как 
// мы это делали в предыдущем примере, когда отлавливали нажатие пользователем одной из двух кнопок.

// Теперь необходимо значение Digit присвоить свойству TextBox1.Text, но здесь изначально записан нуль. Если пользователь вводит первую цифру, то вместо нуля нужно 
// записать эту цифру, а если пользователь вводит последующие цифры, то их надо "сцепить" вместе. Для управления такой ситуацией мы ввели булеву (логическую) переменную Start_Vvoda.
// Поскольку мы ввели ее в начале программы, область действия этой переменной — весь класс Form1, т. е. она "видна" в процедурах обработки всех событий. 
// Это означает, что мы различаем начало ввода числа Start_Vvoda = True, когда нуль следует менять на вводимую цифру, и последующий ввод Start_Vvoda = False, когда
// очередную цифру следует добавлять справа. Таким образом, если это уже не первая нажатая пользователем кнопка-цифра (Start_Vvoda = False), то "сцепляем" полученную
// цифру с предыдущими введенными цифрами, иначе — просто запоминаем первую цифру в текстовом поле TextBox1.

// При обработке событий "щелчок указателем мыши по кнопкам" арифметических операций +, -, *, / в процедуре OPERATIONS_Click преобразуем первое введенное пользователем число
// из текстового поля в переменную Число1 типа Double. Строковой переменной Znak присваивается символьное представление арифметической операции. Поскольку пользователь
// нажал кнопку арифметической операции, ожидаем, что следующим действием пользователя будет ввод очередного числа, поэтому присваиваем булевой переменной Start_Vvoda значение true.
// Заметьте, что обрабатывая два других события: нажатие кнопки "равно" и нажатие кнопки Очистить, мы также устанавливаем логическую переменную Start_Vvoda в состояние true (т. е. начинаем ввод числа). 

// В процедуре обработки события нажатия кнопки "равно" преобразуем второе введенное пользователем число в переменную типа Double. Теперь, поскольку знак арифметической операции нам известен и известны
// также оба числа, мы можем выполнить непосредственно арифметическую операцию. После того как пользователь получит результат, например Результат = Число1 + Число2, возможно, он захочет с этим результатом 
// выполнить еще какое-либо действие, поэтому этот результат записываем в первую переменную Число1.