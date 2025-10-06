using System;

namespace MatrixOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Программа для работы с матрицами");
            Console.WriteLine("=================================");
            
            // Объявление переменных для хранения матриц
            double[,] matrix1 = null;
            double[,] matrix2 = null;
            
            // Главный цикл программы
            while (true)
            {
                Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1. Создание матриц");
                Console.WriteLine("2. Заполнение матриц");
                Console.WriteLine("3. Операции с матрицами");
                Console.WriteLine("4. Выход");
                Console.Write("Выберите пункт меню: ");
                
                string choice = Console.ReadLine();
                
                // Обработка выбора пользователя в главном меню
                switch (choice)
                {
                    case "1":
                        // Создание двух матриц
                        matrix1 = CreateMatrix("первой");
                        matrix2 = CreateMatrix("второй");
                        break;
                    case "2":
                        // Проверка, что матрицы созданы перед заполнением
                        if (matrix1 == null || matrix2 == null)
                        {
                            Console.WriteLine("Сначала создайте матрицы!");
                            break;
                        }
                        FillMatrices(ref matrix1, ref matrix2);
                        break;
                    case "3":
                        // Проверка, что матрицы созданы и заполнены перед операциями
                        if (matrix1 == null || matrix2 == null)
                        {
                            Console.WriteLine("Сначала создайте и заполните матрицы!");
                            break;
                        }
                        MatrixOperationsMenu(matrix1, matrix2);
                        break;
                    case "4":
                        Console.WriteLine("Выход из программы...");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }
        
        // 1) Создание матрицы с заданными размерами
        static double[,] CreateMatrix(string name)
        {
            Console.WriteLine($"\nСоздание {name} матрицы:");
            // Получение количества строк и столбцов от пользователя
            int n = GetPositiveInteger("Введите количество строк (n): ");
            int m = GetPositiveInteger("Введите количество столбцов (m): ");
            
            // Создание новой матрицы заданного размера
            double[,] matrix = new double[n, m];
            Console.WriteLine($"{name} матрица размером {n}x{m} создана.");
            return matrix;
        }
        
        // 2) Заполнение матриц значениями
        static void FillMatrices(ref double[,] matrix1, ref double[,] matrix2)
        {
            Console.WriteLine("\n=== ЗАПОЛНЕНИЕ МАТРИЦ ===");
            Console.WriteLine("1. Заполнить вручную");
            Console.WriteLine("2. Заполнить случайными числами");
            Console.Write("Выберите способ заполнения: ");
            
            string choice = Console.ReadLine();
            
            // Обработка выбора способа заполнения
            switch (choice)
            {
                case "1":
                    // Ручное заполнение обеих матриц
                    Console.WriteLine("\nЗаполнение первой матрицы:");
                    FillMatrixManually(ref matrix1);
                    Console.WriteLine("\nЗаполнение второй матрицы:");
                    FillMatrixManually(ref matrix2);
                    break;
                case "2":
                    // Заполнение случайными числами в заданном диапазоне
                    Console.WriteLine("\nЗаполнение случайными числами:");
                    double a = GetDouble("Введите нижнюю границу a: ");
                    double b = GetDouble("Введите верхнюю границу b: ");
                    
                    // Проверка корректности диапазона
                    if (a > b)
                    {
                        Console.WriteLine("Ошибка: a должно быть меньше или равно b!");
                        return;
                    }
                    
                    // Заполнение обеих матриц случайными числами
                    FillMatrixRandomly(ref matrix1, a, b);
                    FillMatrixRandomly(ref matrix2, a, b);
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }
            
            // Вывод заполненных матриц на экран
            Console.WriteLine("\nПервая матрица:");
            PrintMatrix(matrix1);
            Console.WriteLine("\nВторая матрица:");
            PrintMatrix(matrix2);
        }
        
        // Заполнение матрицы вручную с клавиатуры
        static void FillMatrixManually(ref double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            
            // Поэлементный ввод значений матрицы
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"Элемент [{i},{j}]: ");
                    // Цикл до тех пор, пока не будет введено корректное число
                    while (!double.TryParse(Console.ReadLine(), out matrix[i, j]))
                    {
                        Console.Write("Неверный формат! Введите число: ");
                    }
                }
            }
        }
        
        // 3) Заполнение матрицы случайными числами в диапазоне [a, b]
        static void FillMatrixRandomly(ref double[,] matrix, double a, double b)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            Random rand = new Random();
            
            // Заполнение каждого элемента случайным числом
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = a + (b - a) * rand.NextDouble();
                }
            }
        }
        
        // Меню операций с матрицами
        static void MatrixOperationsMenu(double[,] matrix1, double[,] matrix2)
        {
            while (true)
            {
                Console.WriteLine("\n=== ОПЕРАЦИИ С МАТРИЦАМИ ===");
                Console.WriteLine("1. Показать матрицы");
                Console.WriteLine("2. Сложение матриц");
                Console.WriteLine("3. Умножение матриц");
                Console.WriteLine("4. Детерминант матрицы");
                Console.WriteLine("5. Обратная матрица");
                Console.WriteLine("6. Транспонирование матрицы");
                Console.WriteLine("7. Решение системы уравнений");
                Console.WriteLine("8. Назад в главное меню");
                Console.Write("Выберите операцию: ");
                
                string choice = Console.ReadLine();
                
                // Обработка выбора операции с матрицами
                switch (choice)
                {
                    case "1":
                        // Отображение текущих матриц
                        Console.WriteLine("\nПервая матрица:");
                        PrintMatrix(matrix1);
                        Console.WriteLine("\nВторая матрица:");
                        PrintMatrix(matrix2);
                        break;
                    case "2":
                        // Сложение двух матриц
                        AddMatrices(matrix1, matrix2);
                        break;
                    case "3":
                        // Умножение двух матриц
                        MultiplyMatrices(matrix1, matrix2);
                        break;
                    case "4":
                        // Вычисление детерминанта
                        DeterminantMenu(matrix1, matrix2);
                        break;
                    case "5":
                        // Нахождение обратной матрицы
                        InverseMatrixMenu(matrix1, matrix2);
                        break;
                    case "6":
                        // Транспонирование матрицы
                        TransposeMenu(matrix1, matrix2);
                        break;
                    case "7":
                        // Решение системы линейных уравнений
                        SolveEquationsSystem(matrix1);
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }
        
        // 4) Сложение двух матриц
        static void AddMatrices(double[,] matrix1, double[,] matrix2)
        {
            Console.WriteLine("\n=== СЛОЖЕНИЕ МАТРИЦ ===");
            
            // Проверка возможности сложения (матрицы должны быть одного размера)
            if (matrix1.GetLength(0) != matrix2.GetLength(0) || 
                matrix1.GetLength(1) != matrix2.GetLength(1))
            {
                Console.WriteLine("ОШИБКА: Невозможно сложить матрицы - размерности не совпадают!");
                Console.WriteLine($"Размер первой матрицы: {matrix1.GetLength(0)}x{matrix1.GetLength(1)}");
                Console.WriteLine($"Размер второй матрицы: {matrix2.GetLength(0)}x{matrix2.GetLength(1)}");
                return;
            }
            
            // Создание результирующей матрицы
            double[,] result = new double[matrix1.GetLength(0), matrix1.GetLength(1)];
            
            // Поэлементное сложение матриц
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            
            Console.WriteLine("Результат сложения:");
            PrintMatrix(result);
        }
        
        // 5) Умножение двух матриц
        static void MultiplyMatrices(double[,] matrix1, double[,] matrix2)
        {
            Console.WriteLine("\n=== УМНОЖЕНИЕ МАТРИЦ ===");
            
            // Проверка возможности умножения (число столбцов первой должно равняться числу строк второй)
            if (matrix1.GetLength(1) != matrix2.GetLength(0))
            {
                Console.WriteLine("ОШИБКА: Невозможно умножить матрицы - несовместимые размерности!");
                Console.WriteLine($"Количество столбцов первой матрицы: {matrix1.GetLength(1)}");
                Console.WriteLine($"Количество строк второй матрицы: {matrix2.GetLength(0)}");
                return;
            }
            
            // Определение размеров результирующей матрицы
            int rows = matrix1.GetLength(0);
            int cols = matrix2.GetLength(1);
            int common = matrix1.GetLength(1);
            double[,] result = new double[rows, cols];
            
            // Вычисление произведения матриц
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = 0;
                    // Сумма произведений элементов i-й строки первой матрицы на j-й столбец второй
                    for (int k = 0; k < common; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            
            Console.WriteLine("Результат умножения:");
            PrintMatrix(result);
        }
        
        // 6) Меню для вычисления детерминанта матрицы
        static void DeterminantMenu(double[,] matrix1, double[,] matrix2)
        {
            Console.WriteLine("\n=== ДЕТЕРМИНАНТ МАТРИЦЫ ===");
            Console.WriteLine("1. Детерминант первой матрицы");
            Console.WriteLine("2. Детерминант второй матрицы");
            Console.Write("Выберите матрицу: ");
            
            string choice = Console.ReadLine();
            double[,] selectedMatrix = choice == "1" ? matrix1 : matrix2;
            string matrixName = choice == "1" ? "первой" : "второй";
            
            // Проверка, что матрица квадратная (n x n)
            if (selectedMatrix.GetLength(0) != selectedMatrix.GetLength(1))
            {
                Console.WriteLine($"ОШИБКА: Невозможно найти детерминант {matrixName} матрицы - матрица не квадратная!");
                Console.WriteLine($"Размер матрицы: {selectedMatrix.GetLength(0)}x{selectedMatrix.GetLength(1)}");
                return;
            }
            
            // Вычисление детерминанта
            double determinant = CalculateDeterminant(selectedMatrix);
            Console.WriteLine($"Детерминант {matrixName} матрицы: {determinant:F4}");
        }
        
        // Рекурсивная функция вычисления детерминанта
        static double CalculateDeterminant(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            
            // Базовые случаи рекурсии
            if (n == 1) return matrix[0, 0];
            if (n == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            
            double determinant = 0;
            
            // Разложение по первой строке (метод Лапласа)
            for (int j = 0; j < n; j++)
            {
                // Получение минора для элемента [0,j]
                double[,] minor = GetMinor(matrix, 0, j);
                // Рекурсивный вычет для минора с учетом знака
                determinant += (j % 2 == 0 ? 1 : -1) * matrix[0, j] * CalculateDeterminant(minor);
            }
            
            return determinant;
        }
        
        // Получение минора матрицы (удаление строки row и столбца col)
        static double[,] GetMinor(double[,] matrix, int row, int col)
        {
            int n = matrix.GetLength(0);
            double[,] minor = new double[n - 1, n - 1];
            
            int minorRow = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == row) continue;
                
                int minorCol = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == col) continue;
                    
                    minor[minorRow, minorCol] = matrix[i, j];
                    minorCol++;
                }
                minorRow++;
            }
            
            return minor;
        }
        
        // 7) Меню для нахождения обратной матрицы
        static void InverseMatrixMenu(double[,] matrix1, double[,] matrix2)
        {
            Console.WriteLine("\n=== ОБРАТНАЯ МАТРИЦА ===");
            Console.WriteLine("1. Обратная матрица для первой матрицы");
            Console.WriteLine("2. Обратная матрица для второй матрицы");
            Console.Write("Выберите матрицу: ");
            
            string choice = Console.ReadLine();
            double[,] selectedMatrix = choice == "1" ? matrix1 : matrix2;
            string matrixName = choice == "1" ? "первой" : "второй";
            
            // Проверка, что матрица квадратная
            if (selectedMatrix.GetLength(0) != selectedMatrix.GetLength(1))
            {
                Console.WriteLine($"ОШИБКА: Невозможно найти обратную матрицу для {matrixName} матрицы - матрица не квадратная!");
                return;
            }
            
            // Вычисление детерминанта для проверки обратимости
            double determinant = CalculateDeterminant(selectedMatrix);
            // Проверка, что детерминант не равен нулю (матрица не вырождена)
            if (Math.Abs(determinant) < 1e-10)
            {
                Console.WriteLine($"ОШИБКА: Невозможно найти обратную матрицу для {matrixName} матрицы - детерминант равен нулю!");
                Console.WriteLine($"Детерминант: {determinant:F4}");
                return;
            }
            
            // Вычисление обратной матрицы
            double[,] inverse = CalculateInverseMatrix(selectedMatrix, determinant);
            Console.WriteLine($"Обратная матрица для {matrixName} матрицы:");
            PrintMatrix(inverse);
        }
        
        // Вычисление обратной матрицы методом алгебраических дополнений
        static double[,] CalculateInverseMatrix(double[,] matrix, double determinant)
        {
            int n = matrix.GetLength(0);
            double[,] inverse = new double[n, n];
            
            // Вычисление каждого элемента обратной матрицы
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Получение минора для элемента [i,j]
                    double[,] minor = GetMinor(matrix, i, j);
                    // Вычисление алгебраического дополнения
                    double cofactor = ((i + j) % 2 == 0 ? 1 : -1) * CalculateDeterminant(minor);
                    // Элемент обратной матрицы - транспонированное алгебраическое дополнение, деленное на детерминант
                    inverse[j, i] = cofactor / determinant; // транспонирование
                }
            }
            
            return inverse;
        }
        
        // 8) Меню для транспонирования матрицы
        static void TransposeMenu(double[,] matrix1, double[,] matrix2)
        {
            Console.WriteLine("\n=== ТРАНСПОНИРОВАНИЕ МАТРИЦЫ ===");
            Console.WriteLine("1. Транспонировать первую матрицу");
            Console.WriteLine("2. Транспонировать вторую матрицу");
            Console.Write("Выберите матрицу: ");
            
            string choice = Console.ReadLine();
            double[,] selectedMatrix = choice == "1" ? matrix1 : matrix2;
            string matrixName = choice == "1" ? "первой" : "второй";
            
            // Транспонирование выбранной матрицы
            double[,] transposed = TransposeMatrix(selectedMatrix);
            Console.WriteLine($"Транспонированная {matrixName} матрица:");
            PrintMatrix(transposed);
        }
        
        // Транспонирование матрицы (замена строк на столбцы)
        static double[,] TransposeMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] transposed = new double[cols, rows];
            
            // Замена элементов: [i,j] -> [j,i]
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    transposed[j, i] = matrix[i, j];
                }
            }
            
            return transposed;
        }
        
        // 9) Решение системы линейных уравнений методом Гаусса
        static void SolveEquationsSystem(double[,] matrix)
        {
            Console.WriteLine("\n=== РЕШЕНИЕ СИСТЕМЫ УРАВНЕНИЙ ===");
            Console.WriteLine("Предполагается, что матрица представляет коэффициенты системы Ax = b");
            Console.WriteLine("и последний столбец - вектор свободных членов b");
            
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            
            // Проверка корректности размерности матрицы для системы уравнений
            if (cols != rows + 1)
            {
                Console.WriteLine("ОШИБКА: Невозможно решить систему уравнений!");
                Console.WriteLine("Для системы n уравнений с n неизвестными матрица должна иметь размер n x (n+1)");
                Console.WriteLine($"Текущий размер: {rows}x{cols}");
                return;
            }
            
            // Создание расширенной матрицы для метода Гаусса
            double[,] augmented = (double[,])matrix.Clone();
            
            // Прямой ход метода Гаусса (приведение к треугольному виду)
            for (int i = 0; i < rows; i++)
            {
                // Поиск главного элемента в столбце (для устойчивости метода)
                int maxRow = i;
                for (int k = i + 1; k < rows; k++)
                {
                    if (Math.Abs(augmented[k, i]) > Math.Abs(augmented[maxRow, i]))
                    {
                        maxRow = k;
                    }
                }
                
                // Перестановка строк для размещения главного элемента на диагонали
                if (maxRow != i)
                {
                    for (int k = 0; k <= rows; k++)
                    {
                        double temp = augmented[i, k];
                        augmented[i, k] = augmented[maxRow, k];
                        augmented[maxRow, k] = temp;
                    }
                }
                
                // Проверка на вырожденность системы
                if (Math.Abs(augmented[i, i]) < 1e-10)
                {
                    Console.WriteLine("ОШИБКА: Система уравнений не имеет единственного решения!");
                    Console.WriteLine("Матрица коэффициентов вырождена или близка к вырожденной.");
                    return;
                }
                
                // Обнуление элементов ниже главной диагонали
                for (int k = i + 1; k < rows; k++)
                {
                    double factor = augmented[k, i] / augmented[i, i];
                    for (int j = i; j <= rows; j++)
                    {
                        augmented[k, j] -= factor * augmented[i, j];
                    }
                }
            }
            
            // Обратный ход метода Гаусса (нахождение решения)
            double[] solution = new double[rows];
            for (int i = rows - 1; i >= 0; i--)
            {
                solution[i] = augmented[i, rows];
                for (int j = i + 1; j < rows; j++)
                {
                    solution[i] -= augmented[i, j] * solution[j];
                }
                solution[i] /= augmented[i, i];
            }
            
            // Вывод решения системы уравнений
            Console.WriteLine("Решение системы уравнений:");
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine($"x{i + 1} = {solution[i]:F4}");
            }
        }
        
        // Вспомогательный метод для вывода матрицы на экран
        static void PrintMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            
            // Форматированный вывод каждого элемента матрицы
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j],10:F4} ");
                }
                Console.WriteLine();
            }
        }
        
        // Вспомогательный метод для получения положительного целого числа от пользователя
        static int GetPositiveInteger(string message)
        {
            int value;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value) && value > 0)
                {
                    return value;
                }
                Console.WriteLine("Неверный формат! Введите положительное целое число.");
            }
        }
        
        // Вспомогательный метод для получения числа с плавающей точкой от пользователя
        static double GetDouble(string message)
        {
            double value;
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                Console.WriteLine("Неверный формат! Введите число.");
            }
        }
    }
}