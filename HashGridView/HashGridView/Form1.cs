// В данной программе используется структура данных - хэш-таблица. С ее помощью программа ставит в соответствие 
// государствам их столицы. При этом в качестве ключей указываем названия государств, а в качестве значений — их столицы.
// Далее, используя элемент управления DataGridView, программа выводит эту хэш-таблицу в форму 


using System.Collections;
using System.Data;
using System.Security.Policy;

namespace HashGridView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Пример хэш-таблицы";

            // Создаем новую хэш-таблицу
            var Hash = new Hashtable();

            // Заполняем хэш-таблицу
            // Можно добавлять записи "ключ - значение" таким образом:
            Hash["Россия"] = "Москва";
            // А можно добавлять так:
            Hash.Add("Беларусь", "Минск");
            // Здесь государство - это ключ, а столица - это значение
            Hash.Add("Украина", "Киев");
            
            // Создаем обычную таблицу (не хэш):
            var Table = new DataTable();

            // Задаем схему таблицы, заказывая две колонки:
            Table.Columns.Add("ГОСУДАРСТВА");
            Table.Columns.Add("СТОЛИЦЫ");

            // В цикле заполняем обычную таблицу парами из хэш-таблицы по рядам:
            foreach (DictionaryEntry OneCouple in Hash)
                // Здесь структура DictionaryEntry определяем пару "ключ - значение"
                Table.Rows.Add(OneCouple.Key, OneCouple.Value);
            // Указываем источник данных для DataGridView:
            dataGridView1.DataSource = Table;
        }
    }
}



// Здесь при обработке события загрузки формы создается объект класса Hashtable. Хэш-таблица заполняется тремя парами "код — значение",
// причем, как показано в коде, допустимы обе формы записи: через присваивание и посредством метода Add. Далее создается вспомогательный
// объект класса DataTable, который следует заполнить данными из хэш-таблицы. Хэш-таблица имеет структуру типа DictionaryEntry, которая
// позволяет перемещаться по рядам в цикле и таким образом получить все пары из хэш-таблицы. В этом цикле происходит заполнение объекта класса
// DataTable. Далее для dataGridView1 указываем в качестве источника данных заполненный объект DataTable.

// В заключение отметим, что хэш-таблицу называют ассоциативным массивом, но в этом "массиве" роль индекса играет ключ. Для реализации хэш-таблицы
// можно было бы использовать обычный одномерный массив, в котором элементы с четным индексом являются ключами, а с нечетным — значениями. Однако
// для реализации трех основных операций с хэш-таблицей: добавления новой пары, операции поиска и операции удаления пары по ключу потребовалось бы
// отлаживать довольно-таки много строчек кода.