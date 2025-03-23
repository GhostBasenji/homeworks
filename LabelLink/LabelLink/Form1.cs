// Программа обеспечивает ссылку для посещения почтового сервера 
// www.google.com, ссылку для просмотра папки C:\Windows\ и ссылку для запуска 
// текстового редактора Блокнот с помощью элемента управления LinkLabel

namespace LabelLink
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            base.Text = "Щелкните по ссылке:";
            linkLabel1.Text = "www.google.com";
            linkLabel2.Text = @"Папка C:\Windows\";
            linkLabel3.Text = "Блокнот";
            base.Font = new Font("Consolas", 12.0F);

            // Подписка на события: все три события обрабатываются одной процедурой:
            linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LINK);
            linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LINK);
            linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LINK);
        }

        private void LINK(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var link = (LinkLabel)sender;
            link.LinkVisited = true; // Отмечаем ссылку посещенной

            switch (link.Name)
            {
                case "linkLabel1":
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "http://www.google.com",
                        UseShellExecute = true
                    });
                    break;
                case "linkLabel2":
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "explorer",
                        Arguments = @"C:\Windows\",
                        UseShellExecute = true
                    });
                    break;
                case "linkLabel3":
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "notepad",
                        UseShellExecute = true
                    });
                    break;
            }
        }
    }
}


// Как видно кода, в свойстве Text каждой из ссылки LinkLabel задаем текст, из которого пользователь поймет назначение этих ссылок.
// В задании свойства Text ссылки LinkLabel3 для того, чтобы слово "Блокнот" было в двойных кавычках, кавычки используем дважды.
// Для большей выразительности задаем шрифт Consolas, 12 пунктов. Это шрифт моноширинный. Поскольку свойство LinkVisited = True,
// то соответствующая ссылка отображается как уже посещавшаяся (изменяется цвет).

// Организуем обработку всех трех событий Click по каждой из ссылок одной процедурой обработки LINK. В этой процедуре в зависимости от имени
// объекта (ссылки), создающего события (linkLabel1, linkLabel2, linkLabel3), мы вызываем одну из трех программ: либо Chrome, либо Windows Explorer,
// либо Блокнот. Информация об объекте, создающем событие Click, записана в объектную переменную sender. 
// Она позволяет распознавать объекты (ссылки), создающие события. Чтобы "вытащить" эту информацию из sender, объявим переменную link типа LinkLabel и с помощью 
// обычного присваивания выполним конвертирование параметра sender в экземпляр класса LinkLabel. В этом случае переменная link будет содержать все свойства
// объекта-источника события, в том числе свойство Name, с помощью которого мы сможем распознавать выбранную ссылку. Идентифицируя по свойству Name каждую
// из ссылок, с помощью метода Start вызываем либо Chrome Explorer, либо Windows Explorer, либо Блокнот. Вторым параметром метода Start является имя ресурса,
// подлежащее открытию. Именем ресурса может быть или название веб-страницы, или имя текстового файла. 