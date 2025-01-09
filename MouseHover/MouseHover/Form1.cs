// ДЗ 002. Событие MouseHover
namespace MouseHover
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Приветствие";
            // можно base.Text = "Приветствие";
            label1.Text = "Microsoft Visual C#";
            button1.Text = "Нажми меня";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Привет всем!");
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            MessageBox.Show("Событие MouseHover");
        }
    }
}
