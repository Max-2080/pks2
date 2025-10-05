using System;

class CoffeeMachine
{
    // Константы для рецептов напитков (в миллилитрах)
    private const int AMERICANO_WATER_REQUIRED = 300;  // воды для американо
    private const int LATTE_WATER_REQUIRED = 30;       // воды для латте
    private const int LATTE_MILK_REQUIRED = 270;       // молока для латте
    
    // Цены напитков (в рублях)
    private const int AMERICANO_PRICE = 150;
    private const int LATTE_PRICE = 170;
    
    // Текущие запасы ингредиентов
    private int currentWaterSupply;    // текущий запас воды в мл
    private int currentMilkSupply;     // текущий запас молока в мл

    // Основной метод запуска кофемашины
    public void Start()
    {
        try
        {
            Console.WriteLine(" Добро пожаловать в кофемашину!");
            Console.WriteLine("=================================");
            
            // Запрашиваем начальный запас ингредиентов у оператора
            Console.Write("Введите начальное количество воды (мл): ");
            currentWaterSupply = int.Parse(Console.ReadLine());
            
            Console.Write("Введите начальное количество молока (мл): ");
            currentMilkSupply = int.Parse(Console.ReadLine());
            
            // Проверяем, что запасы не отрицательные
            if (currentWaterSupply < 0 || currentMilkSupply < 0)
            {
                Console.WriteLine("Ошибка: запасы не могут быть отрицательными");
                return;
            }
            
            Console.WriteLine($"\nИнициализация завершена. Запасы: {currentWaterSupply} мл воды, {currentMilkSupply} мл молока");
            
            // Запускаем обработку заказов
            ProcessCustomerOrder();
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: введите корректные числовые значения");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
        }
    }

    // Метод для обработки заказа клиента
    private void ProcessCustomerOrder()
    {
        Console.WriteLine("\n=================================");
        Console.WriteLine("Меню напитков:");
        Console.WriteLine("1 - Американо (требуется воды: 300 мл, цена: 150 руб)");
        Console.WriteLine("2 - Латте (требуется воды: 30 мл, молока: 270 мл, цена: 170 руб)");
        Console.Write("\nВыберите напиток (1 или 2): ");
        
        string userChoice = Console.ReadLine();
        
        switch (userChoice)
        {
            case "1":
                PrepareAmericano();
                break;
            case "2":
                PrepareLatte();
                break;
            default:
                Console.WriteLine(" Неверный выбор. Пожалуйста, выберите 1 или 2");
                // Предлагаем повторить выбор при ошибке
                ProcessCustomerOrder();
                break;
        }
    }

    // Метод для приготовления американо
    private void PrepareAmericano()
    {
        Console.WriteLine("\n--- Приготовление Американо ---");
        
        // Проверяем достаточно ли воды для американо
        if (currentWaterSupply >= AMERICANO_WATER_REQUIRED)
        {
            // Уменьшаем запас воды
            currentWaterSupply -= AMERICANO_WATER_REQUIRED;
            Console.WriteLine($" Американо готовится... Стоимость: {AMERICANO_PRICE} руб.");
            DisplayRemainingSupplies();
        }
        else
        {
            Console.WriteLine(" Недостаточно воды для приготовления Американо");
            Console.WriteLine($"Требуется: {AMERICANO_WATER_REQUIRED} мл, доступно: {currentWaterSupply} мл");
        }
        
        OfferNewOrder();
    }

    // Метод для приготовления латте
    private void PrepareLatte()
    {
        Console.WriteLine("\n--- Приготовление Латте ---");
        
        // Проверяем достаточно ли и воды и молока для латте
        bool hasEnoughWater = currentWaterSupply >= LATTE_WATER_REQUIRED;
        bool hasEnoughMilk = currentMilkSupply >= LATTE_MILK_REQUIRED;
        
        if (hasEnoughWater && hasEnoughMilk)
        {
            // Уменьшаем запасы ингредиентов
            currentWaterSupply -= LATTE_WATER_REQUIRED;
            currentMilkSupply -= LATTE_MILK_REQUIRED;
            Console.WriteLine($" Латте готовится... Стоимость: {LATTE_PRICE} руб.");
            DisplayRemainingSupplies();
        }
        else
        {
            Console.WriteLine(" Недостаточно ингредиентов для приготовления Латте");
            if (!hasEnoughWater)
                Console.WriteLine($"Требуется воды: {LATTE_WATER_REQUIRED} мл, доступно: {currentWaterSupply} мл");
            if (!hasEnoughMilk)
                Console.WriteLine($"Требуется молока: {LATTE_MILK_REQUIRED} мл, доступно: {currentMilkSupply} мл");
        }
        
        OfferNewOrder();
    }

    // Метод для отображения оставшихся запасов
    private void DisplayRemainingSupplies()
    {
        Console.WriteLine("\nТекущие запасы:");
        Console.WriteLine($" Вода: {currentWaterSupply} мл");
        Console.WriteLine($" Молоко: {currentMilkSupply} мл");
        Console.WriteLine($" Заполненность: {CalculateSupplyPercentage():F1}%");
    }

    // Метод для расчета процента заполненности 
    private double CalculateSupplyPercentage()
    {
        // Условный расчет - можно адаптировать под реальные емкости
        double waterPercentage = (currentWaterSupply / 1000.0) * 100; // предполагаем емкость 1000 мл
        double milkPercentage = (currentMilkSupply / 1000.0) * 100;   // предполагаем емкость 1000 мл
        return (waterPercentage + milkPercentage) / 2;
    }

    // Метод для предложения нового заказа
    private void OfferNewOrder()
    {
        Console.Write("\nЖелаете сделать еще один заказ? (да/нет): ");
        string response = Console.ReadLine()?.ToLower();
        
        if (response == "да" || response == "д" || response == "yes" || response == "y")
        {
            ProcessCustomerOrder();
        }
        else
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("Спасибо за использование кофемашины! До свидания! ");
        }
    }

    static void Main()
    {
        CoffeeMachine coffeeMachine = new CoffeeMachine();
        coffeeMachine.Start();
    }
}