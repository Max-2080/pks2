using System;

class Program
{
    static void Main()
    {
        try
        {
            // Получаем номер билета от пользователя
            Console.Write("Введите шестизначный номер билета: ");
            string ticket = Console.ReadLine();

            // Проверяем, что строка не пустая
            if (string.IsNullOrWhiteSpace(ticket))
            {
                Console.WriteLine("Ошибка: номер билета не может быть пустым");
                return;
            }

            // Удаляем возможные пробелы
            ticket = ticket.Trim();

            // Проверяем корректность ввода: длина и что все символы - цифры
            if (ticket.Length != 6)
            {
                Console.WriteLine("Ошибка: номер билета должен состоять из 6 цифр");
                return;
            }

            if (!IsNumber(ticket))
            {
                Console.WriteLine("Ошибка: номер билета должен содержать только цифры");
                return;
            }

            // Преобразуем строку в массив цифр
            int[] digits = new int[6];
            for (int i = 0; i < 6; i++)
            {
                // Каждый символ строки преобразуем в цифру
                digits[i] = int.Parse(ticket[i].ToString());
            }

            // Вычисляем сумму первых трех цифр
            int sumFirst = digits[0] + digits[1] + digits[2];
            
            // Вычисляем сумму последних трех цифр
            int sumLast = digits[3] + digits[4] + digits[5];

            // Выводим промежуточные суммы для наглядности
            Console.WriteLine($"Сумма первых трех цифр: {sumFirst}");
            Console.WriteLine($"Сумма последних трех цифр: {sumLast}");

            // Проверяем, является ли билет счастливым
            if (sumFirst == sumLast)
            {
                Console.WriteLine("Билет счастливый! 🎉");
            }
            else
            {
                Console.WriteLine("Билет обычный.");
            }
        }
        catch (FormatException)
        {
            // Обрабатываем ошибки преобразования типов
            Console.WriteLine("Ошибка: неверный формат данных при преобразовании");
        }
        catch (OverflowException)
        {
            // Обрабатываем переполнение (хотя маловероятно для 6 цифр)
            Console.WriteLine("Ошибка: число слишком большое");
        }
        catch (Exception ex)
        {
            // Обрабатываем все остальные непредвиденные ошибки
            Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
        }
        finally
        {
            // Этот блок выполняется всегда, даже если была ошибка
            Console.WriteLine("\nПрограмма завершена. Спасибо за использование!");
        }
    }

    // Вспомогательный метод для проверки, является ли строка числом
    // Проверяет, что каждый символ в строке - цифра от 0 до 9
    static bool IsNumber(string str)
    {
        // Проверяем каждый символ в строке
        foreach (char c in str)
        {
            // Если символ не находится в диапазоне цифр '0'-'9'
            if (c < '0' || c > '9')
                return false; // Найден нецифровой символ
        }
        return true; // Все символы - цифры
    }
}