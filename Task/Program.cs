using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
// https://habr.com/ru/articles/204600/ прочитай
namespace Task
{
    internal class Program
    {
        /// <summary>
        /// Решает поставленные в лабе задачи
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            StartWork();
            DoWork();
            FinishWork();
        }

        /// <summary>
        /// Время выполнения программы
        /// </summary>
        private static Stopwatch stopwatch = new();

        /// <summary>
        /// Здоровается, начинает работу и уведомляет об этом
        /// </summary>
        private static void StartWork()
        {
            Console.Beep();
            Console.WriteLine("Здравствуйте!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Работа начата");
            Console.ResetColor();
            stopwatch.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void DoWork()
        {
            PrintMenu();

            // TODO: Придумать способ выбора действия и повтор 

            // TODO: Продумать взаимодействие методов с менюшкой 
        }

       /// <summary>
       /// Проверяет корректность выбора действия из меню
       /// </summary>
       /// <param name="menu">Массив возможных действий</param>
       /// <param name="action">Номер выбранного действия</param>       
        private static void ChooseAction(string[] menu, out uint action)
        {
            bool isNumber = false;
            bool isInRange = false;
            bool isCorrectComand = isNumber && isInRange;
            action = 0;
            do
            {
                Console.Write("Введите номер выбранного действия: ");
                isNumber = uint.TryParse(Console.ReadLine(), out action);
                
                if (action <= menu.Length && action != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка: Нераспознанная команда! Проверьте корректность ввода");
                    Console.ResetColor();
                    isInRange = true;
                }
                isCorrectComand = isNumber && isInRange;

            } while (!isCorrectComand);
        }
        
        
        // TODO: Сделать менюшку
        private static uint PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Программа реализует следующую функциональность: ");
            Console.ResetColor();

            string[] menu = {
                "Создание массива",
                "Печать массива",
                "Сортировка массива",
                "Удаление чётных элементов из массива",
                "Нахождение первого чётного элемента в массиве",
                "Поиск элемента в массиве",
                "Добавление элементов в начало массива",
                "Перестановка чётных элементов в начало массива"
                            };

                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine($"  {i + 1} " + menu[i]);
                }                
                ChooseAction(menu, out uint action);
            
            return action;
        }

        /// <summary>
        /// Уведомляет о завершении, времени выполнения и прощается
        /// </summary>
        private static void FinishWork()
        {
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Работа завершена");
            Console.ResetColor();
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine("До свидания!");
        }

        /// <summary>
        /// Датчик случайных чисел
        /// </summary>
        private static Random random = new();

        /// <summary>
        /// Читает целое число и сообщает об ошибках ввода оного
        /// </summary>
        /// <param name="message">Приглашение к нужному вводу</param>
        /// <param name="error">Уведомление об ошибочном вводе</param>
        /// <returns></returns>
        private static int ReadInteger(string message = "Введите количество элементов массива:", string error = "Ошибка: Вы не ввели целое число в разрешённом дипазоне!")
        {
            bool isNumber;
            int number;
            do
            {
                Console.WriteLine(message);

                isNumber = int.TryParse(Console.ReadLine(), out number);
                if (!isNumber)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(error);
                    Console.ResetColor();
                }

            } while (!isNumber);

            return number;
        }

        /// <summary>
        /// Печатает массив целых чисел
        /// </summary>
        /// <param name="integerArray">Массив, который необходимо распечатать</param>
        private static void PrintArray(int[] integerArray)
        {
            if (integerArray == null)
                Console.Write("Массив пустой");
            else
                foreach (int p in integerArray)
                {
                    Console.Write(p + " ");
                }
            Console.WriteLine();
        }

        /// <summary>
        /// Читает массив с клавиатуры
        /// </summary>
        /// <param name="keyboardArray">Получившийся массив</param>
        private static void ReadArray(out int[]? keyboardArray)
        {
            CheckArraySize(out int length);

            keyboardArray = new int[length];
            for (int q = 0; q < length; q++)
            {
                keyboardArray[q] = ReadInteger("Введите элемент массива:");
            }
        }

        /// <summary>
        /// Создаёт массив целых чисел датчиком случайных чисел
        /// </summary>
        /// <param name="randomArray"></param>
        private static void MakeRandomArray(out int[]? randomArray)
        {
            CheckArraySize(out int length);

            randomArray = new int[length];
            for (int q = 0; q < length; q++)
            {
                randomArray[q] = random.Next(int.MinValue, int.MaxValue);
            }
        }

        /// <summary>
        /// Проверяет переполнение памяти массивом целых чисел и неположительное количество элементов
        /// </summary>
        /// <param name="length">Длина массива</param>
        private static void CheckArraySize(out int length)
        {
            bool isCorrectArraySize;
            do
            {
                length = ReadInteger();
                if (length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка: Массив не может иметь нулевую длину!");
                    Console.ResetColor(); // Сброс к стандартному цвету
                    isCorrectArraySize = false;
                }
                else if (length < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка: Массив не может иметь отрицательную длину!");
                    Console.ResetColor();
                    isCorrectArraySize = false;
                }
                else
                {
                    try
                    {
                        int[] array = new int[length];
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ввод корректен");
                        Console.ResetColor();
                        isCorrectArraySize = true;
                    }
                    catch (OutOfMemoryException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошика: Переполнение памяти слишком большим массивом!");
                        Console.ResetColor();
                        isCorrectArraySize = false;
                    }
                }

            } while (!isCorrectArraySize);
        }

        /// <summary>
        /// Находит первый чётный элемент в массиве
        /// </summary>
        /// <param name="array">Массив для поиска</param>
        /// <returns></returns>
        private static int? FindFirstEven(int[] integerArray)
        {
            for (uint i = 0; i < integerArray.Length; i++)
            {
                if (integerArray[i] % 2 == 0)
                {
                    Console.WriteLine($"Первый чётный элемент: {integerArray[i]}. Количество сравнений: {i + 1}");
                    return integerArray[i];
                }
            }

            Console.WriteLine("Нет чётных элементов!");
            return null;
        }

        /// <summary>
        /// Быстро ищет элемент в массиве
        /// </summary>
        /// <param name="integerArray">Массив для поиска элемента в нём</param>
        private static void BinarySearch(int[] sortedIntegerArray)
        {
            int target = ReadInteger("Введите целое число, которое вы хотите найти в массиве:", "Ошибка: Вы ввели не целое число!");
            int left = 0;
            int right = sortedIntegerArray.Length - 1;
            int mid = left + (right - left) / 2;
            int steps = 1;
            while (left <= right)
            {
                if (target == sortedIntegerArray[mid])
                {
                    Console.WriteLine($"Элемент есть в массиве. Его индекс: {mid + 1}. Количество сравнений: {steps}");
                    return;
                }
                else if (target > sortedIntegerArray[mid])
                    left = mid + 1;
                else
                    right = mid - 1;

                mid = left + (right - left) / 2;
                steps++;
            }
            Console.WriteLine("Элемента нет в массиве!");
        }

        /// <summary>
        /// Сортирует массив выбором
        /// </summary>
        /// <param name="array">Сортируемый массив</param>
        private static void SelectionSort(ref int[] array)
        {
            for (uint q = 0; q < array.Length - 1; q++) // идём до предпоследнего. он один останется иначе он бы был минимумом на другом шаге 
            {
                int min = array[q]; // чтобы самым первым минимальным
                uint index = q; // не заполнить всё
                for (uint p = q + 1; p < array.Length; p++) // идём до последнего чтобы на каждом шаге проверять всё на минимум
                {
                    if (min > array[p] && p != q)
                    {
                        min = array[p]; // запомнили минимальный
                        index = p; // место откуда его взяли
                    }
                }

                int temp = array[q];
                array[q] = min;
                array[index] = temp;
            }
        }

        /// <summary>
        /// Считает число чётных чисел в массиве
        /// </summary>
        /// <param name="integerArray"></param>
        private static uint CountEvens(int[] integerArray)
        {
            uint count = 0;
            foreach (int p in integerArray)
            {
                if (p % 2 == 0)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Удаляет все чётные элементы из массива
        /// </summary>
        /// <param name="integerArray">Массив, из которого нужно удалить</param>
        /// <returns></returns>
        private static void DeleteEvens(ref int[]? integerArray)
        {
            uint evensCount = CountEvens(integerArray);
            if (evensCount == 0)
                return;
            else if (evensCount == integerArray.Length)
            {
                Console.WriteLine("После удаления массив стал пустым!");
                integerArray = null;
            }
            else
            {
                int[] newArray = new int[integerArray.Length - evensCount];
                uint index = 0;
                foreach (int p in integerArray)
                {
                    if (p % 2 != 0)
                    {
                        newArray[index] = p;
                        index++;
                    }
                }
                integerArray = newArray;
            }  
        }

        // TODO: добавить К элементов в начало массива

        // TODO: чётные элементы переставить в начало, а нечётные в конец
    }
} 