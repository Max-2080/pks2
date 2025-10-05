using System;

class NumberGuesser
{
    static void Main()
    {
        Console.WriteLine(" Игра 'Угадай число'!");
        Console.WriteLine("Загадайте число от 0 до 63. Я попробую его угадать.");
        Console.WriteLine("Отвечайте '1' (да) или '0' (нет) на мои вопросы.");
        Console.WriteLine("----------------------------------------");

        try
        {
            // Начальные границы диапазона поиска
            int lowerBound = 0;
            int upperBound = 63;
            
            // Массив битовых масок для проверки каждого бита числа
            // Каждая маска соответствует степени двойки это позволяет использовать двоичный поиск
            int[] bitMasks = { 32, 16, 8, 4, 2, 1 };
            int guessedNumber = 0; // Здесь будет накоплен результат
            
            int questionCount = 1;
            
            // Проходим по всем битовым маскам для проверки каждого бита числа
            foreach (int currentMask in bitMasks)
            {
                Console.Write($"{questionCount}. Ваше число больше или равно {lowerBound + currentMask}? (1/0): ");
                string userAnswer = Console.ReadLine();
                
                // Проверяем корректность ответа пользователя
                if (userAnswer != "1" && userAnswer != "0")
                {
                    Console.WriteLine("Ошибка: введите только '1' (да) или '0' (нет)");
                    questionCount--;
                    continue;
                }
                
                if (userAnswer == "1")
                {
                    // Если ответ "да", устанавливаем соответствующий бит в 1
                    // Операция OR устанавливает бит в позиции маски
                    guessedNumber |= currentMask;
                    
                    // Сдвигаем нижнюю границу поиска вверх
                    lowerBound += currentMask;
                }
                else
                {
                    // Если ответ "нет", устанавливаем верхнюю границу
                    // Число меньше, чем текущая нижняя граница + маска
                    upperBound = lowerBound + currentMask - 1;
                }
                
                questionCount++;
                
                // Выводим текущий диапазон поиска для наглядности
                Console.WriteLine($"   Текущий диапазон: {lowerBound} - {upperBound}");
            }
            
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($" Я думаю, вы загадали число: {guessedNumber}");
            Console.WriteLine("Я угадал? (нажмите любую клавишу для выхода)");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
            Console.WriteLine("Пожалуйста, перезапустите программу.");
        }
    }
}