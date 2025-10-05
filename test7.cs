using System;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("  Расчет максимальной толщины защиты модулей");
            Console.WriteLine("==============================================");
            
            // Ввод количества модулей
            Console.Write("Введите количество модулей (n): ");
            int moduleCount = int.Parse(Console.ReadLine());
            
            if (moduleCount <= 0)
            {
                Console.WriteLine(" Ошибка: количество модулей должно быть положительным");
                return;
            }

            // Ввод размеров одного модуля
            Console.Write("Введите размеры модуля (ширина высота): ");
            string[] moduleInput = Console.ReadLine().Split();
            if (moduleInput.Length < 2)
            {
                Console.WriteLine(" Ошибка: введите два числа через пробел");
                return;
            }
            int moduleWidth = int.Parse(moduleInput[0]);
            int moduleHeight = int.Parse(moduleInput[1]);
            
            if (moduleWidth <= 0 || moduleHeight <= 0)
            {
                Console.WriteLine(" Ошибка: размеры модуля должны быть положительными");
                return;
            }

            // Ввод размеров поля размещения
            Console.Write("Введите размеры поля (высота ширина): ");
            string[] fieldInput = Console.ReadLine().Split();
            if (fieldInput.Length < 2)
            {
                Console.WriteLine(" Ошибка: введите два числа через пробел");
                return;
            }
            int fieldHeight = int.Parse(fieldInput[0]);
            int fieldWidth = int.Parse(fieldInput[1]);
            
            if (fieldHeight <= 0 || fieldWidth <= 0)
            {
                Console.WriteLine(" Ошибка: размеры поля должны быть положительными");
                return;
            }

            // Вычисление максимальной толщины защиты
            int maxProtectionThickness = CalculateMaxProtectionThickness(
                moduleCount, moduleWidth, moduleHeight, fieldHeight, fieldWidth);
            
            // Вывод результата
            Console.WriteLine("\n Результаты расчета:");
            Console.WriteLine($"Количество модулей: {moduleCount}");
            Console.WriteLine($"Размер модуля: {moduleWidth} × {moduleHeight}");
            Console.WriteLine($"Размер поля: {fieldHeight} × {fieldWidth}");
            
            if (maxProtectionThickness == -1)
            {
                Console.WriteLine(" Невозможно разместить модули даже без защиты");
            }
            else
            {
                Console.WriteLine($" Максимальная толщина защиты: {maxProtectionThickness}");
                Console.WriteLine($"Размер модуля с защитой: {moduleWidth + 2 * maxProtectionThickness} × {moduleHeight + 2 * maxProtectionThickness}");
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
    }

    /// <summary>
    /// Вычисляет максимальную толщину защиты для модулей
    /// </summary>
    /// <param name="moduleCount">Количество модулей для размещения</param>
    /// <param name="moduleWidth">Ширина одного модуля</param>
    /// <param name="moduleHeight">Высота одного модуля</param>
    /// <param name="fieldHeight">Высота поля размещения</param>
    /// <param name="fieldWidth">Ширина поля размещения</param>
    /// <returns>Максимальная толщина защиты или -1 если размещение невозможно</returns>
    static int CalculateMaxProtectionThickness(int moduleCount, int moduleWidth, int moduleHeight, 
                                             int fieldHeight, int fieldWidth)
    {
        // Проверяем возможность размещения без защиты
        if (!CanPlaceModulesWithProtection(moduleCount, moduleWidth, moduleHeight, 
                                         fieldHeight, fieldWidth, 0))
        {
            return -1; // Размещение невозможно даже без защиты
        }

        // Бинарный поиск максимальной толщины защиты
        // Правая граница - минимальный размер поля, деленный на 2
        int leftBound = 0;
        int rightBound = Math.Min(fieldHeight, fieldWidth) / 2;
        int maxPossibleThickness = 0;

        // Бинарный поиск для нахождения максимальной толщины
        while (leftBound <= rightBound)
        {
            int middleThickness = (leftBound + rightBound) / 2;
            
            if (CanPlaceModulesWithProtection(moduleCount, moduleWidth, moduleHeight, 
                                            fieldHeight, fieldWidth, middleThickness))
            {
                // Если можем разместить с текущей толщиной, пробуем увеличить
                maxPossibleThickness = middleThickness;
                leftBound = middleThickness + 1;
            }
            else
            {
                // Если не можем разместить, уменьшаем толщину
                rightBound = middleThickness - 1;
            }
        }

        return maxPossibleThickness;
    }

    /// <summary>
    /// Проверяет возможность размещения модулей с заданной толщиной защиты
    /// </summary>
    /// <param name="moduleCount">Количество модулей</param>
    /// <param name="moduleWidth">Ширина модуля</param>
    /// <param name="moduleHeight">Высота модуля</param>
    /// <param name="fieldHeight">Высота поля</param>
    /// <param name="fieldWidth">Ширина поля</param>
    /// <param name="protectionThickness">Толщина защиты</param>
    /// <returns>True если модули можно разместить, иначе False</returns>
    static bool CanPlaceModulesWithProtection(int moduleCount, int moduleWidth, int moduleHeight,
                                            int fieldHeight, int fieldWidth, int protectionThickness)
    {
        // Размеры модуля с учетом защиты (защита добавляется со всех сторон)
        int protectedModuleWidth = moduleWidth + 2 * protectionThickness;
        int protectedModuleHeight = moduleHeight + 2 * protectionThickness;

        // Проверяем оба варианта ориентации модулей на поле
        bool orientation1 = CanArrangeModules(moduleCount, protectedModuleWidth, protectedModuleHeight, 
                                            fieldHeight, fieldWidth);
        bool orientation2 = CanArrangeModules(moduleCount, protectedModuleHeight, protectedModuleWidth, 
                                            fieldHeight, fieldWidth);

        return orientation1 || orientation2;
    }

    /// <summary>
    /// Проверяет возможность размещения модулей в заданной ориентации
    /// </summary>
    /// <param name="moduleCount">Количество модулей</param>
    /// <param name="moduleWidth">Ширина модуля в текущей ориентации</param>
    /// <param name="moduleHeight">Высота модуля в текущей ориентации</param>
    /// <param name="fieldHeight">Высота поля</param>
    /// <param name="fieldWidth">Ширина поля</param>
    /// <returns>True если модули помещаются, иначе False</returns>
    static bool CanArrangeModules(int moduleCount, int moduleWidth, int moduleHeight,
                                int fieldHeight, int fieldWidth)
    {
        // Проверяем, что модуль вообще помещается в поле
        if (moduleWidth > fieldWidth || moduleHeight > fieldHeight)
            return false;

        // Вычисляем максимальное количество модулей по ширине и высоте
        int modulesPerRow = fieldWidth / moduleWidth;     // Количество модулей в строке
        int modulesPerColumn = fieldHeight / moduleHeight; // Количество модулей в столбце

        // Общее количество модулей, которое можно разместить
        int totalModulesPossible = modulesPerRow * modulesPerColumn;

        return totalModulesPossible >= moduleCount;
    }
}