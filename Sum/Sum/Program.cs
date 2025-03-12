// Программа организует ввод двух числе, их сложение и вывод суммы на консоль
Console.Title = "Складываю два числа:";
Console.BackgroundColor = ConsoleColor.Cyan; // Цвет фона
Console.ForegroundColor = ConsoleColor.Black; // Цвет текста
Console.Clear(); // Очистка консоли

// Ввод первого слагаемого
Console.WriteLine("Введите первое слагаемое:");
var Stroka = Console.ReadLine();
// Преобразование строковой переменной в число:
var X = Single.Parse(Stroka);

// Ввод второго слагаемого
Console.WriteLine("Введите второе слагаемое:");
Stroka = Console.ReadLine();
var Y = Single.Parse(Stroka);
var Z = X + Y;
Console.WriteLine("Сумма = {0} + {1} = {2}", X, Y, Z);

// Звуковой сигнал частотой 1000 Гц и длительностью 0.5 секунды (500 мс):
Console.Beep(1000, 500);

// Приостановливаем выполнение программы до нажатия какой-нибудь клавиши:
Console.ReadKey();