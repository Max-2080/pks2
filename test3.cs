using System;

class Program
{
    static void Main()
    {
        try
        {
            // Ввод числителя
            Console.Write("Введите числитель M: ");
            int numerator = int.Parse(Console.ReadLine());

            // Ввод знаменателя
            Console.Write("Введите знаменатель N: ");
            int denominator = int.Parse(Console.ReadLine());

            // Проверка на нулевой знаменатель
            if (denominator == 0)
            {
                Console.WriteLine("Ошибка: знаменатель не может быть равен нулю");
                return;
            }

            // Проверка на отрицательные числа
            if (numerator < 0 || denominator < 0)
            {
                Console.WriteLine("Внимание: работа с отрицательными числами");
            }

            // Находим наибольший общий делитель (НОД)
            int greatestCommonDivisor = FindGCD(numerator, denominator);

            // Сокращаем дробь, деля числитель и знаменатель на НОД
            int simplifiedNumerator = numerator / greatestCommonDivisor;
            int simplifiedDenominator = denominator / greatestCommonDivisor;

            // Выводим исходную дробь для сравнения
            Console.WriteLine($"Исходная дробь: {numerator}/{denominator}");

            // Выводим результат сокращения
            Console.WriteLine($"Сокращенная дробь: {simplifiedNumerator}/{simplifiedDenominator}");

            // Дополнительная информация
            Console.WriteLine($"Найденный НОД: {greatestCommonDivisor}");

            // Если дробь уже была несократимой
            if (greatestCommonDivisor == 1)
            {
                Console.WriteLine("Дробь уже была несократимой");
            }
        }
        catch (FormatException)// Обрабатываем ошибки, возникающие при работе пользователя
        {
            Console.WriteLine("Ошибка: введите целые числа в правильном формате");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Ошибка: введенное число слишком большое");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Ошибка: деление на ноль");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
        }
    }

    // Метод для нахождения наибольшего общего делителя (НОД) по алгоритму Евклида
    static int FindGCD(int firstNumber, int secondNumber)
    {
        // Используем абсолютные значения для работы с отрицательными числами
        int a = Math.Abs(firstNumber);
        int b = Math.Abs(secondNumber);

        // Алгоритм Евклида в цикле
        while (b != 0)
        {
            // Сохраняем текущее значение b
            int temporary = b;
            
            // Обновляем b как остаток от деления a на b
            b = a % b;
            
            // Обновляем a как предыдущее значение b
            a = temporary;
        }

        // Когда b становится 0, a содержит НОД
        return a;
    }
}