using System;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine(" Моделирование воздействия антибиотика на бактерии");
            Console.WriteLine("===================================================");
            
            // Ввод начальных данных с проверкой корректности
            Console.Write("Введите начальное количество бактерий (N): ");
            int initialBacteriaCount = int.Parse(Console.ReadLine());
            
            Console.Write("Введите количество капель антибиотика (X): ");
            int antibioticDrops = int.Parse(Console.ReadLine());
            
            // Проверка на положительные значения
            if (initialBacteriaCount <= 0 || antibioticDrops <= 0)
            {
                Console.WriteLine("Ошибка: значения должны быть положительными числами");
                return;
            }

            // Инициализация переменных для симуляции
            int currentBacteriaCount = initialBacteriaCount; // текущее количество бактерий
            int elapsedHours = 0;                            // прошедшее время в часах
            int currentAntibioticPower = antibioticDrops * 10; // начальная мощность антибиотика
            
            Console.WriteLine("\n Динамика изменения количества бактерий:");
            Console.WriteLine("Час | Бактерии | Мощность антибиотика");
            Console.WriteLine("----|----------|---------------------");

            // Основной цикл симуляции - продолжается пока антибиотик активен
            while (currentAntibioticPower > 0 && currentBacteriaCount > 0)
            {
                elapsedHours++;
                
                // Фаза размножения: бактерии удваиваются каждый час
                currentBacteriaCount *= 2;
                Console.WriteLine($"{elapsedHours,3} | {currentBacteriaCount,8} | (после размножения)");
                
                // Фаза воздействия антибиотика: убиваем бактерии
                int bacteriaKilled = Math.Min(currentBacteriaCount, currentAntibioticPower);
                currentBacteriaCount -= bacteriaKilled;
                
                // Уменьшение мощности антибиотика с каждой дозой
                currentAntibioticPower -= antibioticDrops;
                
                // Вывод результатов текущего часа
                Console.WriteLine($"{elapsedHours,3} | {currentBacteriaCount,8} | {currentAntibioticPower + antibioticDrops,19}");
                Console.WriteLine($"     Убито бактерий: {bacteriaKilled}");
                
                // Защита от бесконечного цикла (на случай быстрого роста бактерий)
                if (elapsedHours > 100)
                {
                    Console.WriteLine("  Прервано: превышено максимальное время симуляции");
                    break;
                }
            }

            // Вывод итоговых результатов
            Console.WriteLine("\n===================================================");
            Console.WriteLine(" Результаты симуляции:");
            Console.WriteLine($"Общее время: {elapsedHours} часов");
            Console.WriteLine($"Начальное количество бактерий: {initialBacteriaCount}");
            Console.WriteLine($"Конечное количество бактерий: {currentBacteriaCount}");
            Console.WriteLine($"Использовано капель антибиотика: {antibioticDrops}");
            
            // Анализ результата
            if (currentBacteriaCount == 0)
            {
                Console.WriteLine(" Антибиотик победил! Все бактерии уничтожены.");
            }
            else
            {
                Console.WriteLine(" Бактерии выжили! Антибиотик не справился.");
                Console.WriteLine($"Оставшиеся бактерии: {currentBacteriaCount}");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine(" Ошибка: введите корректные целые числа");
        }
        catch (OverflowException)
        {
            Console.WriteLine(" Ошибка: введенные числа слишком большие");
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Неожиданная ошибка: {ex.Message}");
        }
        
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}