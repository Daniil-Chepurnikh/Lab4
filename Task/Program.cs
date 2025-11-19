using System;
using System.Diagnostics;

namespace Task
{
    internal class Program
    {
        /// <summary>
        /// Решает поставленные в лабе задачи
        /// </summary>
        /// <param name="args">Чтобы было</param>
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
        /// Здоровается, начинает работу
        /// </summary>
        private static void StartWork()
        {
            Console.WriteLine("Здравствуйте!");
            PrintMessage("Работа начата", ConsoleColor.Cyan);
            stopwatch.Start();
        }

        /// <summary>
        /// Выполняет все требования пользователя
        /// </summary>
        private static void DoWork()
        {
            string[] mainMenu =
            [
                    "Создание массива",
                    "Печать массива",
                    "Сортировка массива",
                    "Удаление чётных элементов из массива",
                    "Нахождение первого чётного элемента в массиве",
                    "Поиск элемента в массиве",
                    "Добавление элементов в начало массива",
                    "Перестановка чётных элементов в начало массива",
                    "Завершить работу"
            ];

            string end = "Нет";
            int[] array = [];
            do
            {
                switch (PrintMenu(mainMenu))
                {
                    case 1:
                        {
                            array = CreateArray();
                            break;
                        }
                    case 2:
                        {
                            PrintArray(array);
                            break;
                        }
                    case 3:
                        {
                            Sort(array);
                            break;
                        }
                    case 4:
                        {
                            DeleteEvens(ref array);
                            break;
                        }
                    case 5:
                        {
                            FindFirstEven(array);
                            break;
                        }
                    case 6:
                        {
                            BinarySearch(array);
                            break;
                        }
                    case 7:
                        {
                            array = AddElements(array);
                            break;
                        }
                    case 8:
                        {
                            array = EvenOddSort(array);
                            break;
                        }
                    case 9:
                        {
                            end = "Да";
                            break;
                        }    
                }
            } while (string.Equals(end, "Нет", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Печатает меню и принимает выбор пользователя
        /// </summary>
        /// <param name="menu">Массив возможных действий</param>
        /// <returns>Выбранное действие</returns>
        private static uint PrintMenu(string[] menu, string message = "Программа реализует следующую функциональность: ")
        {
            uint action;
            string? choice;
            do
            {
                bool isCorrectAction;
                do
                {
                    PrintMessage(message);
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

                Console.WriteLine("Вы выбрали дейстиве: " + menu[action - 1]);
                Console.WriteLine("Вы уверены в своём выборе? Если уверены, напишите Да(в любом регистре), любой другой ввод будет воспринят как нет");
                choice = Console.ReadLine();

            } while (!string.Equals(choice, "Да", StringComparison.OrdinalIgnoreCase)); // подсказал интернет

            PrintMessage("Приступаю к выполнению команды");

            return action;
        }

        /// <summary>
        /// Создаёт массив выбранным способом
        /// </summary>
        /// <returns>Созданный массив</returns>
        private static int[] CreateArray()
        {
            string[] arrayMenu =
            [
                "Создать массив самостоятельно",
                "Создать массив случайно"
            ];

            int[] array = [];
            bool isCreated = true;
            do
            {
                switch (PrintMenu(arrayMenu, "Выберете способ создания массива:"))
                {
                    case 1:
                        {
                            array = ReadArray();
                            break;
                        }
                    case 2:
                        {
                            array = MakeRandomArray();
                            break;
                        }
                }
            } while (!isCreated);
            return array;
        }

        /// <summary>
        /// Уведомляет о завершении, времени выполнения и прощается
        /// </summary>
        private static void FinishWork()
        {
            PrintMessage("Работа закончена", ConsoleColor.Cyan);
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
            {
                PrintMessage("Массив пустой", ConsoleColor.Cyan);
            }
            else
            {
                foreach (int p in integerArray)
                {
                    Console.Write(p + " ");
                }
                Console.WriteLine();
            }        
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
        private static int[] ReadArray()
        {
            uint length = GetArraySize();

            int[] keyboardArray = new int[length];
            for (int q = 0; q < length; q++)
            {
                keyboardArray[q] = ReadInteger("Введите элемент массива: ");
            }
            return keyboardArray;
        }

        /// <summary>
        /// Создаёт массив целых чисел датчиком случайных чисел
        /// </summary>
        /// <param name="randomArray"></param>
        private static int[] MakeRandomArray()
        {
            uint length = GetArraySize();

            int[] randomArray = new int[length];
            for (int q = 0; q < length; q++)
            {
                randomArray[q] = random.Next(-100, 100);
            }
            return randomArray;
        }

        /// <summary>
        /// Проверяет переполнение памяти массивом целых чисел и неположительное количество элементов
        /// </summary>
        /// <param name="length">Длина массива</param>
        private static uint GetArraySize()
        {
            bool isCorrectArraySize;
            uint length;
            do
            {
                length = (uint)ReadInteger();
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
                        PrintMessage();
                        isCorrectArraySize = true;
                    }
                    catch (OutOfMemoryException)
                    {
                        PrintError("Переполнение памяти слишком большим массивом!");
                        isCorrectArraySize = false;
                    }
                }
            } while (!isCorrectArraySize);
            return length;
        }

        /// <summary>
        /// Находит первый чётный элемент в массиве
        /// </summary>
        /// <param name="array">Массив для поиска</param>
        /// <returns></returns>
        private static int FindFirstEven(int[] integerArray)
        {
            if (CheckEmpty(integerArray))
            {
                PrintError("Невозможно найти элемент в пустом массиве!");
                return -1;
            }

            for (int i = 0; i < integerArray.Length; i++)
            {
                if (integerArray[i] % 2 == 0)
                {
                    Console.WriteLine($"Первый чётный элемент: {integerArray[i]}. Количество сравнений: {i + 1}");
                    return i + 1;
                }
            }
            Console.WriteLine("Нет чётных элементов!");
            return -1;
        }

        /// <summary>
        /// Быстро ищет элемент в массиве
        /// </summary>
        /// <param name="integerArray">Массив для поиска элемента в нём</param>
        private static int BinarySearch(int[] sortedIntegerArray)
        {
            if (CheckEmpty(sortedIntegerArray))
            {
                PrintError("Невозможно найти элемент в пустом массиве!");
                return -1;
            }
            if (CheckSort(sortedIntegerArray)) // ищем только если отсортирован
            {
                int target = ReadInteger("Введите целое число, которое вы хотите найти в массиве:", "Ошибка: Вы ввели не целое число!");
                int left = 0;
                int right = sortedIntegerArray.Length - 1;
                int steps = 1;
                while (left <= right)
                {
                    int mid = left + (right - left) / 2;

                    if (target == sortedIntegerArray[mid])
                    {
                        Console.WriteLine($"Элемент есть в массиве. Его индекс: {mid + 1}. Количество сравнений: {steps}");
                        return mid + 1;
                    }
                    else if (target > sortedIntegerArray[mid])
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                    steps++;
                }
            }
            else
            {
                PrintError("Невозможно использовать бинарный поиск в неотсортированном массиве!");
                return -1;
            }
            PrintMessage("Элемента нет в массиве", ConsoleColor.Cyan);
            return -1;
        }

        /// <summary>
        /// Проверяет массив на отсортированность
        /// </summary>
        /// <param name="array">Проверяемый массив</param>
        /// <returns>false если массив не отсортирован</returns>
        private static bool CheckSort(int[] array)
        {
            uint index = 0;
            while (index  < array.Length - 1)
            {
                if (array[index] > array[++index])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Сортирует массив выбором
        /// </summary>
        /// <param name="array">Сортируемый массив</param>
        private static void SelectionSort(int[] integerArray)
        {
            if (CheckEmpty(integerArray))
            {
                PrintError("Невозможно отсортировать пустой массив!");
                return;
            }
            for (int q = 0; q < integerArray.Length - 1; q++) // идём до предпоследнего. он один останется иначе он бы был минимумом на другом шаге 
            {
                int min = integerArray[q]; // чтобы самым первым минимальным
                int indexMin = q; // не заполнить всё
                for (int p = q + 1; p < integerArray.Length; p++) // идём до последнего чтобы на каждом шаге проверять всё на минимум
                {
                    if (min > integerArray[p])
                    {
                        min = integerArray[p]; // запомнили минимальный
                        indexMin = p; // место откуда его взяли
                    }
                }
                if (indexMin == q)
                {
                    continue;
                }
                Swap(integerArray, indexMin, q);
            }
        }

        /// <summary>
        /// Мненяет местами два элемента в массиве
        /// </summary>
        /// <param name="integerArray">Массив, в котором нужно произвести обмен</param>
        /// <param name="indexFirst">Номер первого элемента</param>
        /// <param name="indexSecond">Номер второго элемента</param>
        private static void Swap(int[] integerArray, int indexFirst, int indexSecond)
        {
            int temp = integerArray[indexSecond];
            integerArray[indexSecond] = integerArray[indexFirst];
            integerArray[indexFirst] = temp;
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
                {
                    count++;
                }
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
            {
                PrintError("Невозможно удалить элементы в пустом массиве!");
                return;
            }

            uint evensCount = CountEvens(integerArray);
            if (evensCount == 0) // нечего удалять
            { 
                return; 
            }
            else if (evensCount == integerArray.Length) // проще сразу отдать пустоту
            {
                Console.WriteLine("После удаления массив стал пустым!");
                integerArray = [];
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
            return integerArray.Length == 0;
        }

        /// <summary>
        /// Добавляет К элементов в начало массива
        /// </summary>
        /// <param name="integerArray">Массив, в который надо добавить элемент</param>
        private static int[] AddElements(int[] integerArray)
        {
            string[] addMenu =
            [
                "Добавить элементы самостоятельно",
                "Добавить элементы случайно"
            ];
            int newElementsCount = ReadInteger("Введите количство добавляемых элементов");
            int[] newArray;

            try
            {
                newArray = new int[newElementsCount + integerArray.Length];
            }
            catch (OutOfMemoryException)
            {
                PrintError("После добавления массив стал слишком большим!");
                return integerArray; // вернём, что дали
            }

            switch (PrintMenu(addMenu, "Выберете способ добавления элементов:"))
            {
                case 1:
                    {
                        for (int p = 0; p < newElementsCount; p++)
                        {
                            newArray[p] = ReadInteger("Введите элемент массива: ");
                        }
                        break;
                    }

                case 2:
                    {
                        for (int p = 0; p < newElementsCount; p++)
                        {
                            newArray[p] = random.Next(-100, 100);
                        }
                        break;
                    }
            }

            for (int q = newElementsCount; q < newArray.Length; q++)
            {
                newArray[q] = integerArray[q - newElementsCount];
            }
            return newArray;
        }

        /// <summary>
        /// Перставляет чётные в начало
        /// </summary>
        /// <param name="integerArray">Массив для перестановки</param>
        private static int[] EvenOddSort(int[] integerArray)
        {
            if (CheckEmpty(integerArray))
            {
                PrintError("Невозможно переставлить элементы в пустом массиве!");
                return integerArray;
            }

            uint countEvens = CountEvens(integerArray);
            if (countEvens == 0 || countEvens == integerArray.Length)
            {
                return integerArray; // не делай лишнего
            }

            int[] sortedArray = new int[integerArray.Length];
            uint counter = 0;
            for (uint p = 0; p < integerArray.Length; p++)
            {
                if (integerArray[p] % 2 == 0)
                {
                    sortedArray[counter++] = integerArray[p];
                }
                else
                {
                    sortedArray[countEvens++] = integerArray[p];
                }
            }
            return sortedArray;
        }

        /// <summary>
        /// Печатаем красивые сообщения пользователю
        /// </summary>
        /// <param name="message">Сообщение на печать</param>
        /// <param name="color">Цвет печать</param>
        public static void PrintMessage(string message = "Ввод корректен", ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Сортирует массив выбранным способом
        /// </summary>
        /// <param name="array">Сортируемый массив</param>
        private static void Sort(int[] array)
        {
            if (CheckSort(array))
            {
                PrintMessage("Массив уже отсортирован");
                return;
            }
            
            string[] sortMenu =
            [
                "Сортировка простым выбором",
                "Сортировка Хоара",
                "Сортировка слиянием"

            ];

            bool isSorted = false;
            do
            {
                switch (PrintMenu(sortMenu, "Выберете способ сортировки массива:"))
                {
                    case 1:
                        {
                            SelectionSort(array);
                            isSorted = true;
                            break;
                        }
                    case 2:
                        {
                            HoareSort(array, 0, array.Length - 1);
                            isSorted = true;
                            break;
                        }
                    case 3:
                        {
                            MergeSort(array, 0, array.Length - 1);
                            isSorted = true;
                            break;
                        }
                }
            } while (!isSorted);
        }

        /// <summary>
        /// Сортировка Хоара
        /// </summary>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="left">Левая граница массива</param>
        /// <param name="right">Правая граница массива</param>
        private static void HoareSort(int[] array, int left, int right)
        {            
            if (left < right) // если равно то один элемент в подмассиве и его сортировать не надо
            {
                int pivotIndex = Partition(array, left, right); // получаем новый опорный индекс
                HoareSort(array, left, pivotIndex - 1); // сортируем те которые оказались меньше
                HoareSort(array, pivotIndex + 1, right); // сортируем те которые оказались больше или равны
            }
        }

        /// <summary>
        /// Разделение массива для сортировки Хоара
        /// </summary>
        /// <param name="array">Массив который делим</param>
        /// <param name="left">Левая граница массива</param>
        /// <param name="right">Правая граница массив</param>
        /// <returns>Индекс, на котором оказался опорный элемент</returns>
        private static int Partition(int[] array, int left, int right)
        {
            int pivot = array[left]; // выбрали первый элемент как опорный
            int low = left + 1; // начало мальньких
            int high = right; // конец больших

            // чтобы не гулять по чужим подмассивам(маленьким по большим и наоборот)
            while (high >= low)
            {
                while (high >= low && array[low] < pivot) // встретили большой элемент среди маленьких и ушли
                {
                    low++; // сдвигаемся к провому концу массива
                }
                
                while (high >= low && array[high] >= pivot) // встретили маленький элемент среди больших и ушли
                {
                    high--; // сдвигаемся к левому концу массива
                }
                
                if (high < low) // совсем плохо когда правая граница меньше левой
                {
                    break;
                }
                Swap(array, low, high); // меняем местами попаданцев не в свой подмассив
            }

            // по последнему большому сдвинульсь так что большой указывает на последенего маленького
            // О М М М М М Б Б Б Б Б Б Б Б
            // поэтому по индексу большого и опорного меняемся
            // М М М М М О Б Б Б Б Б Б Б Б 
            // получили что маленькие до большие после
            Swap(array, left, high);
            return high; // вернули индекс О
        }

        /// <summary>
        /// Сортировка слиянием
        /// </summary>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="left">Левая граница сортируемого массива(не обязательно нуль)</param>
        /// <param name="right">Правая граница сортируемого массива(не обязательно длина без единицы)</param>
        private static void MergeSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2; // хотя бы середину стандартно считаем
                MergeSort(array, left, mid); // сортируем левый подмассив
                MergeSort(array, mid + 1, right); // сортируем правый подмассив
                Merge(array, left, mid, right); // сливаем два отсортированных подмассива
            }
        }

        /// <summary>
        /// Сливает отсортированные массивы
        /// </summary>
        /// <param name="array">Массив в котором были сливаемые подмассивы</param>
        /// <param name="left">Левая граница(не всегда нуль)</param>
        /// <param name="mid">Середина массива от лефт до райт</param>
        /// <param name="right">Правая граница(не всегда настоящая правая)</param>
        private static void Merge(int[] array, int left, int mid, int right)
        {
            int leftCounter = left; // счётчик по левому подмассиву
            int rightCounter = mid + 1; // счётчик по правому подмассиву
            int[] sortedArray = new int[right - left + 1]; // массив в который будем вписывать элементы в нужном порядке, его длина это буквально сколько между лефт и райт элементов
            int sortedArrayCounter = 0; // счётчик по сортированному массиву из которого потом будем в основной вписывать

            while (leftCounter <= mid && rightCounter <= right) // чтобы чужим индексом не влезть в не свой подмассив
            {
                if (array[leftCounter] <= array[rightCounter])
                {
                    sortedArray[sortedArrayCounter++] = array[leftCounter++];
                }
                else
                {
                    sortedArray[sortedArrayCounter++] = array[rightCounter++];
                }
            }

            while (leftCounter <= mid) // оставшиеся большие в левом подмассиве вписываем последними
            {
                sortedArray[sortedArrayCounter++] = array[leftCounter++]; // вписка левых
            }
            
            while (rightCounter <= right) // оставшиеся большие в правом подмассиве вписываем последеними
            {
                sortedArray[sortedArrayCounter++] = array[rightCounter++]; // вписка правых
            }
            
            for (uint i = 0; i < sortedArray.Length; i++) // записываем в правильном порядке в нужное место исходного массива(left + i, как раз из-за того что left не всегда будет нулём)
            {
                array[left + i] = sortedArray[i]; // вписка в исходный массив
            }
        }
    }
} 