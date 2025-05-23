// Рассматриваемая программа выводит в поле элемента управления PictureBox изображение из растрового файла (например, PNG).
// При этом изображение записывается в БО. Пользователь может убедиться в этом, например, запустив Paint — стандартный
// графический редактор ОС Windows. Далее пользователь может поместить в БО любое изображение с помощью какого-нибудь
// графического редактора, например того же Paint. Затем, нажав кнопку Извлечь из БО нашей программы, получить в форме содержимое БО.


namespace ClipboardBMP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Содержимое БО:";
            button1.Text = "Извлечь из БО";

            // Записываем в PictureBox изображение из файла:
            try
            {
                pictureBox1.Image = Image.FromFile(
                    System.IO.Directory.GetCurrentDirectory() + @"\logo.png");
            }
            catch (System.IO.FileNotFoundException Situation)
            {
                // Обработка исключительной ситуации:
                MessageBox.Show(
                    "Нет такого файла \n" + Situation.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                button1.Enabled = false;
                return;
            }
            // Записываем в БО изображение из графического окна формы
            Clipboard.SetDataObject(pictureBox1.Image);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Recipient = Clipboard.GetDataObject();
            Bitmap Rastr;

            // Если данные в БО представлены в формате Bitmap...
            if (Recipient.GetDataPresent(DataFormats.Bitmap) == true)
            {
                // то записываем эти данные из БО в переменную Rastr в формате Bitmap:
                Rastr = (Bitmap)Recipient.GetData(DataFormats.Bitmap);
                pictureBox1.Image = Rastr;
            }
        }
    }
}


// Как видно из кода программы, при обработке события загрузки формы в графическое поле pictureBox1 записываем какой-нибудь файл, например logo.png. Функция
// GetCurrentDirectory() возвращает полный путь текущей папки. Далее по команде Clipboard.SetDataObject(PictureBox1.Image) происходит запись содержимого
// графического поля в буфер обмена.
// Теперь пользователь может проверить содержимое БО с помощью какого-либо графического редактора, например Paint. Далее он может записать что-либо в буфер обмена,
// опять же используя какой-нибудь графический редактор. Нажатие кнопки "Извлечь из БО" нашей программы в форме приведет к появлению изображения, находящегося в
// буфере обмена.
// В коде при обработке события "щелчок на кнопке Извлечь из БО" объявляем объектную переменную Recipient. Это объект, с помощью которого будем получать изображение,
// записанное в буфер обмена. Далее следует проверка — записаны ли данные, представленные в БО, в формате растровой графики Bitmap. Если да, то записать данные из БО
// в переменную Rastr в формате Bitmap с помощью объектной переменной Recipient, используя преобразование в переменную типа Bitmap. Далее, чтобы изображение появилось
// в элементе управления pictureBox1 в форме, присваиваем свойству Image значение переменной Rastr. 
