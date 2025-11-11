using System;
using System.Diagnostics;

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
        /// Выполняет все требования пользователя
        /// </summary>
        private static void DoWork()
        {
            string[] mainMenu =
            {
                    "Создание массива",
                    "Печать массива",
                    "Сортировка массива",
                    "Удаление чётных элементов из массива",
                    "Нахождение первого чётного элемента в массиве",
                    "Поиск элемента в массиве",
                    "Добавление элементов в начало массива",
                    "Перестановка чётных элементов в начало массива",
                    "Завершить работу"
            };

            string end = "Нет";
            int[] array = [];
            do
            {
                uint action = PrintMenu(mainMenu);
                switch (action)
                {
                    case 1:
                        CreateArray(ref array);
                        break;
                    case 2:
                        PrintArray(array);
                        break;
                    case 3:
                        SelectionSort(ref array);
                        break;
                    case 4:
                        DeleteEvens(ref array);
                        break;
                    case 5:
                        FindFirstEven(array);
                        break;
                    case 6:
                        BinarySearch(array);
                        break;
                    case 7:
                        AddElements(ref array);
                        break;
                    case 8:
                        EvenOddSort(ref array);
                        break;
                    case 9:
                        end = "Да";
                        break;
                }
            } while (end != "Да");
        }
        
        /// <summary>
        /// Печатает меню и принимает выбор пользователя
        /// </summary>
        /// <param name="menu">Массив возможных действий</param>
        /// <returns>Выбранное действие</returns>
        private static uint PrintMenu(string[] menu, string message = "Программа реализует следующую функциональность: ")
        {
            string? choice;
            uint action;
            do
            {
                bool isCorrectAction;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(message);
                    Console.ResetColor();
                    for (int i = 0; i < menu.Length; i++)
                    {
                        Console.WriteLine($"  {i + 1} " + menu[i]);
                    }

                    Console.Write("Введите номер выбранного действия: ");
                    isCorrectAction = uint.TryParse(Console.ReadLine(), out action);

                    if (action > menu.Length || action == 0)
                    {
                        PrintError();
                        isCorrectAction = false;
                    }
                } while (!isCorrectAction);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ввод корректен");
                Console.ResetColor();

                Console.WriteLine("Вы уверены в своём выборе? Если уверены, напишите Да, любой другой ввод будет воспринят как нет");
                choice = Console.ReadLine();


            } while (choice != "Да");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Приступаю к выполнению команды");
            Console.ResetColor();

            return action;
        }

        /// <summary>
        /// Создаёт массив выбранным способом
        /// </summary>
        /// <returns>Созданный массив</returns>
        private static void CreateArray(ref int[] array)
        {
            string[] arrayMenu =
            {
                "Самостоятельно",
                "Случайно"
            };

            array = [];
            bool isCreated = true;
            do
            {
                switch (PrintMenu(arrayMenu, "Выберете способ создания массива:"))
                {
                    case 1:
                        ReadArray(out array);
                        break;
                    case 2:
                        MakeRandomArray(out array);
                        break;
                    default:
                        isCreated = false;
                        break;
                }
            } while (!isCreated);
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
        private static int ReadInteger(string message = "Введите количество элементов массива:", string error = "Вы не ввели целое число в разрешённом дипазоне!")
        {
            bool isNumber;
            int number;
            do
            {
                Console.WriteLine(message);

                isNumber = int.TryParse(Console.ReadLine(), out number);
                if (!isNumber)
                {
                    PrintError(error);
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
            if (CheckEmpty(integerArray))
                Console.Write("Массив пустой");
            else
                foreach (int p in integerArray)
                {
                    Console.Write(p + " ");
                }
            Console.WriteLine();
        }

        /// <summary>
        /// Сообщает об ошибках
        /// </summary>
        /// <param name="error">Печатаемая ошибка</param>
        private static void PrintError(string error = "Нераспознанная команда! Проверьте корректность ввода")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка: " + error);
            Console.ResetColor();
        }

        /// <summary>
        /// Читает массив с клавиатуры
        /// </summary>
        /// <param name="keyboardArray">Получившийся массив</param>
        private static void ReadArray(out int[] keyboardArray)
        {
            CheckArraySize(out int length);

            keyboardArray = new int[length];
            for (int q = 0; q < length; q++)
            {
                keyboardArray[q] = ReadInteger("Введите элемент массива: ");
            }
        }

        /// <summary>
        /// Создаёт массив целых чисел датчиком случайных чисел
        /// </summary>
        /// <param name="randomArray"></param>
        private static void MakeRandomArray(out int[] randomArray)
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
                if (length < 0)
                {
                    PrintError("Массив не может иметь отрицательную длину!");
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
                        PrintError("Переполнение памяти слишком большим массивом!");
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
        private static void FindFirstEven(int[] integerArray)
        {
            if (CheckEmpty(integerArray))
                return;

            for (uint i = 0; i < integerArray.Length; i++)
            {
                if (integerArray[i] % 2 == 0)
                {
                    Console.WriteLine($"Первый чётный элемент: {integerArray[i]}. Количество сравнений: {i + 1}");
                    return;
                }
            }

            Console.WriteLine("Нет чётных элементов!");
            return;
        }

        /// <summary>
        /// Быстро ищет элемент в массиве
        /// </summary>
        /// <param name="integerArray">Массив для поиска элемента в нём</param>
        private static void BinarySearch(int[] sortedIntegerArray)
        {
            if (CheckEmpty(sortedIntegerArray))
                return;

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
        private static void SelectionSort(ref int[] integerArray)
        {
            if (CheckEmpty(integerArray))
                return;

            
            for (uint q = 0; q < integerArray.Length - 1; q++) // идём до предпоследнего. он один останется иначе он бы был минимумом на другом шаге 
            {
                int min = integerArray[q]; // чтобы самым первым минимальным
                uint index = q; // не заполнить всё
                for (uint p = q + 1; p < integerArray.Length; p++) // идём до последнего чтобы на каждом шаге проверять всё на минимум
                {
                    if (min > integerArray[p] && p != q)
                    {
                        min = integerArray[p]; // запомнили минимальный
                        index = p; // место откуда его взяли
                    }
                }

                int temp = integerArray[q];
                integerArray[q] = min;
                integerArray[index] = temp;
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
        private static void DeleteEvens(ref int[] integerArray)
        {
            if (CheckEmpty(integerArray))
                return;

            uint evensCount = CountEvens(integerArray);
            if (evensCount == 0)
                return;
            else if (evensCount == integerArray.Length)
            {
                Console.WriteLine("После удаления массив стал пустым!");
                integerArray = new int[0];
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

        /// <summary>
        /// Проверяет массив на пустоту
        /// </summary>
        /// <param name="integerArray">Проверяемый массив</param>
        /// <returns>Логическое значение true если пустой</returns>
        private static bool CheckEmpty(int[] integerArray)
        {
            if (integerArray.Length == 0)
            {
                PrintError("Невозможно выполнить выбранное действие в пустом массиве!");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Добавляет К элементов в начало массива
        /// </summary>
        /// <param name="integerArray">Массив, в который надо добавить элемент</param>
        private static void AddElements(ref int[] integerArray)
        {
            int newElementsCount = ReadInteger("Введите количство добавляемых элементов");
            int[] newArray = new int[newElementsCount + integerArray.Length];

            for (int p = 0; p < newElementsCount; p++)
            {
                newArray[p] = ReadInteger("Введите элемент массива");
            }
            for (int q = newElementsCount; q < newArray.Length; q++)
            {
                newArray[q] = integerArray[q - newElementsCount];
            }

            integerArray = newArray;
        }
        
        /// <summary>
        /// Перставляет чётные в начало, а нечётные в конец
        /// </summary>
        /// <param name="integerArray">Массив для перестановки</param>
        private static void EvenOddSort(ref int[] integerArray)
        {
            if (CheckEmpty(integerArray))
            {
                PrintError("Невозможно переставлить элементы в пустом массиве!");
                return; 
            }

            uint countEvens = CountEvens(integerArray);
            int[] sortedArray = new int[integerArray.Length];
            uint counter = countEvens - 1;
            for (uint p = 0; p < integerArray.Length; p++)
            {
                if (integerArray[p] % 2 == 0)
                    sortedArray[counter--] = integerArray[p];
                else
                    sortedArray[countEvens++] = integerArray[p];
            }

            integerArray = sortedArray;
        }
    }
} 