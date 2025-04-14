// Программа тестирует студента по какому-либо предмету обучения

namespace SmallTest
{
    public partial class Form1 : Form
    {
        // Внешние переменные:
        int QuestionCount;  // Счет вопросов
        int RightAnswers;   // Количество правильных ответов
        int WrongAnswers;   // Количество неправильных ответов

        // Массив вопросов, на которые даны неправильные ответы:
        String[] NotRightAnswers;  // Размерность этого массива зададим позже
        int NumberOfRightAnswers;  // Номер правильного ответа
        int ChosenAnswer;          // Номер ответа, выбранного студентом

        System.IO.StreamReader Читатель;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Следующий вопрос";
            button2.Text = "Выход";

            // Подписываемся на событие "изменение состояния переключателей" RadioButton:
            radioButton1.CheckedChanged += new EventHandler(IzmenenieSostoyaniyaRadioButton);
            radioButton2.CheckedChanged += new EventHandler(IzmenenieSostoyaniyaRadioButton);
            radioButton3.CheckedChanged += new EventHandler(IzmenenieSostoyaniyaRadioButton);
            BeginOfTest();
        }

        void BeginOfTest()
        {
            var Кодировка = System.Text.Encoding.UTF8;
            try
            {
                // Создаем экземпляр класса StreamReader для чтения из файла:
                Читатель = new System.IO.StreamReader(System.IO.Directory.GetCurrentDirectory() +
                                              @"\test.txt", Кодировка);
                this.Text = Читатель.ReadLine(); // Название предмета

                // Обнуляем все счетчики:
                QuestionCount = 0; RightAnswers = 0; WrongAnswers = 0;

                // Задаем размер массива для неправильных ответов:
                NotRightAnswers = new String[100];
            }
            catch (Exception Situation)
            {
                MessageBox.Show(Situation.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            ReadNextQuestion();
        }

        void ReadNextQuestion()
        {
            label1.Text = Читатель.ReadLine();
            // Считываем варианты ответов:
            radioButton1.Text = Читатель.ReadLine();
            radioButton2.Text = Читатель.ReadLine();
            radioButton3.Text = Читатель.ReadLine();

            // Выясняем, какой ответ - правильный:
            NumberOfRightAnswers = int.Parse(Читатель.ReadLine());

            // Переводим все переключатели в состояние "выключено":
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            // Первую кнопку задаем неактивной, пока студент не выберет вариант ответа:
            button1.Enabled = false;

            QuestionCount = QuestionCount + 1;

            // Проверяем, закончились ли вопросы:
            if (Читатель.EndOfStream == true) button1.Text = "Завершить";
        }

        void IzmenenieSostoyaniyaRadioButton(object sender, EventArgs e)
        {
            // Кнопка "Следующий вопрос" становится активной, и ей передаем фокус:
            button1.Enabled = true; button1.Focus();

            RadioButton Perekluchatel = (RadioButton)sender;
            var tmp = Perekluchatel.Name;

            // Выясняем номер ответа, выбранного студентом:
            ChosenAnswer = int.Parse(tmp.Substring(11));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // "Следующий вопрос/Завершить/Начать тестирование сначала" 
            // Счет правильных ответов:
            if (ChosenAnswer == NumberOfRightAnswers) RightAnswers = RightAnswers + 1;
            if (ChosenAnswer != NumberOfRightAnswers)
            {
                // Счет неправильных ответов:
                WrongAnswers = WrongAnswers + 1;
                // Запоминаем вопросы с неправильными ответами:
                NotRightAnswers[WrongAnswers] = label1.Text;
            }

            if (button1.Text == "Начать тестирование сначала")
            {
                button1.Text = "Следующий вопрос";

                // Переключатели становятся видимыми, доступными для выбора:
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;

                // Переходим к началу файла:
                BeginOfTest();
                return;
            }
            else if (button1.Text == "Завершить")
            {
                // Закрываем файл:
                Читатель.Close();
                // Переключатели становятся невидимыми, недоступными для выбора:
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;

                // Формируем оценку за тест:
                label1.Text = String.Format("Тестирование завершено.\n" +
                    "Правильных ответов: {0} из {1}.\n" +
                    "Оценка в пятибалльной системе: {2:F2}.", RightAnswers,
                    QuestionCount, (RightAnswers * 5.0F) / QuestionCount);    // 5F — это максимальная оценка

                button1.Text = "Начать тестирование сначала";
                // Вывод вопросов, на которые даны неправильные ответы:
                if (WrongAnswers != 0)
                {
                    var Str = "СПИСОК ВОПРОСОВ, НА КОТОРЫЕ ВЫ ДАЛИ " + "НЕПРАВИЛЬНЫЙ ОТВЕТ:\n\n";
                    for (int i = 1; i <= WrongAnswers; i++)
                    {
                        Str = Str + NotRightAnswers[i] + "\n";
                    }
                    MessageBox.Show(Str, "Тестирование завершено");
                }
            }
            else if (button1.Text == "Следующий вопрос")
            {
                ReadNextQuestion();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

// В программе есть несколько переменных, которые объявлены в начале вне всех процедур, чтобы они были "видны" из всех процедур класса Form1. В процедуре обработки
// загрузки формы организуем подписку на событие "изменение состояния переключателей" RadioButton одной процедурой IzmenenieSostoyaniyaRadioButton. В этой программе изменение состояния
// любого из трех переключателей будем обрабатывать одной процедурой IzmenenieSostoyaniyaRadioButton.
// Далее в процедуре BeginOfTest открываем файл test.txt, в котором содержится непосредственно тест, и читаем первую строку с названием предмета или темы, подлежащей тестированию. При этом
// обнуляем счетчик всех вопросов и счетчики вопросов, на которые студент дал правильные и неправильные ответы. Затем вызываем процедуру ReadNextQuestion, которая читает очередной вопрос,
// варианты ответов на него и номер варианта правильного ответа. Тут же проверяем, не достигнут ли конец читаемого программой файла. Если достигнут, то меняем надпись на первой кнопке на "Завершить".
// В нашей программе надпись на первой кнопке является как бы флагом, который указывает, по какой ветви в программе следует передавать управление.
// При выборе студентом того или иного варианта испытуемый может сколь угодно раз щелкать на разных переключателях, пока не выберет окончательно вариант ответа. Программа зафиксирует выбранный вариант
// только после щелчка на кнопке Следующий вопрос. В процедуре обработки события "изменение состояния переключателей" выясняем, какой из вариантов ответа выбрал студент, но делаем вывод, правильно ли
// ответил студент или нет, только при обработке события "щелчок на первой кнопке".

// В процедуре обработки события "щелчок на первой кнопке" ведем счет правильных и неправильных ответов, а также запоминаем в строковый массив вопросы, на которые студент дал неверный ответ. Если достигнут
// конец файла, и надпись на кнопке стала "Завершить", то закрываем текстовый файл, все переключатели делаем невидимыми (уже выбирать нечего) и формируем оценку за прохождение теста, а также через MessageBox
// выводим список вопросов, на которые испытуемый дал ошибочный ответ. 

// Можно совершенствовать эту программу. Например, добавить элемент управления Timer, чтобы ограничить время сдачи теста. Или в качестве исходных данных для программы использовать не текстовый файл, который
// может обеспечить только текстовое представление, а файл HTML. Такой файл будет содержать не только текстовые вопросы, но и графические, включающие изображения. 