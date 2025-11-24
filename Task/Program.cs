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
            int[] integerArray = [];
            do
            {
                switch (PrintMenu(mainMenu))
                {
                    case 1:
                        {
                            integerArray = CreateArray();
                            break;
                        }
                    case 2:
                        {
                            PrintArray(integerArray);
                            break;
                        }
                    case 3:
                        {
                            Sort(integerArray);
                            break;
                        }
                    case 4:
                        {
                            DeleteEvens(ref integerArray);
                            break;
                        }
                    case 5:
                        {
                            FindFirstEven(integerArray);
                            break;
                        }
                    case 6:
                        {
                            BinarySearch(integerArray);
                            break;
                        }
                    case 7:
                        {
                            integerArray = AddElements(integerArray);
                            break;
                        }
                    case 8:
                        {
                            integerArray = EvenOddSort(integerArray);
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

            int[] integerArray = [];
            bool isCreated = true;
            do
            {
                switch (PrintMenu(arrayMenu, "Выберете способ создания массива:"))
                {
                    case 1:
                        {
                            integerArray = ReadArray();
                            break;
                        }
                    case 2:
                        {
                            integerArray = MakeRandomArray();
                            break;
                        }
                }
            } while (!isCreated);
            return integerArray;
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
        /// <returns>Прочитанное число</returns>
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
        /// <param name="integerArray">Печатаемый массив</param>
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
        /// Читает массив целых чисел с клавиатуры
        /// </summary>
        /// <returns>Прочитанный массив</returns>
        private static int[] ReadArray()
        {
            int length = GetArraySize();

            int[] keyboardArray = new int[length];
            for (int q = 0; q < length; q++)
            {
                keyboardArray[q] = ReadInteger("Введите элемент массива: ");
            }
            return keyboardArray;
        }

        /// <summary>
        /// Создаёт массив датчиком случайных чисел
        /// </summary>
        /// <returns>Созданный массив</returns>
        private static int[] MakeRandomArray()
        {
            int length = GetArraySize();

            int[] randomArray = new int[length];
            for (int q = 0; q < length; q++)
            {
                randomArray[q] = random.Next(-100, 100);
            }
            return randomArray;
        }

        /// <summary>
        /// Получает длину массива, проверяет переполнение памяти массивом целых чисел и неположительное количество элементов
        /// </summary>
        /// <param name="length">Длина массива</param>
        private static int GetArraySize()
        {
            bool isCorrectArraySize;
            int length;
            do
            {
                length = ReadInteger();
                if (length < 0)
                {
                    PrintError("Массив не может иметь отрицательную длину!");
                    isCorrectArraySize = false;
                }
                else if (length == 0)
                {
                    PrintError("Сейчас бессмысленно создавать массив длины нуль!");
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
        /// <param name="integerArray">Массив для поиска</param>
        /// <returns>Номер первого чётного, считая с единицы</returns>
        private static int FindFirstEven(int[] integerArray)
        {
            if (CheckEmpty(integerArray))
            {
                PrintError("Невозможно найти элемент в пустом массиве!");
                return -1;
            }

            if (CountEvens(integerArray) == 0)
            {
                Console.WriteLine("Нет чётных элементов!");
                return -1;
            }
            else
            {
                int index = 0;
                for (; index < integerArray.Length; index++)
                {
                    if (integerArray[index] % 2 == 0)
                    {
                        Console.WriteLine($"Первый чётный элемент: {integerArray[index]}. Количество сравнений: {index + 1}");
                        break;
                    }
                }
                return index + 1;
            }
        }

        /// <summary>
        /// Быстро ищет элементы в массиве
        /// </summary>
        /// <param name="sortedIntegerArray">Массив для поиска</param>
        /// <returns></returns>
        private static int BinarySearch(int[] sortedIntegerArray)
        {
            if (CheckEmpty(sortedIntegerArray))
            {
                PrintError("Невозможно найти элемент в пустом массиве!");
                return -1;
            }
            int steps = 1;
            if (CheckSort(sortedIntegerArray)) // ищем только если отсортирован
            {
                int target = ReadInteger("Введите целое число, которое вы хотите найти в массиве:", "Ошибка: Вы ввели не целое число!");
                int left = 0;
                int right = sortedIntegerArray.Length - 1;
                while (left <= right)
                {
                    int mid = left + (right - left) / 2;

                    if (target == sortedIntegerArray[mid])
                    {
                        Console.WriteLine($"Элемент есть в массиве. Его индекс: {mid + 1}. Количество сравнений: {steps}");
                        return mid + 1; // нашли
                    }
                    else if (target > sortedIntegerArray[mid])
                    {
                        left = mid + 1; // сдвинулись в правую половину
                    }
                    else
                    {
                        right = mid - 1; // сдвинулись в левую половину
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
            Console.WriteLine($"Количество сравнений: {steps}");
            return -1;
        }

        /// <summary>
        /// Проверяет массив на отсортированность
        /// </summary>
        /// <param name="integerArray">Проверяемый массив</param>
        /// <returns>false если массив не отсортирован</returns>
        private static bool CheckSort(int[] integerArray)
        {
            for (uint p = 0; p < integerArray.Length - 1;)
            {
                if (integerArray[p] > integerArray[++p])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Сортирует массив простым выбором
        /// </summary>
        /// <param name="integerArray">Сортируемый массив</param>
        private static void SelectionSort(int[] integerArray)
        {
            if (CheckEmpty(integerArray))
            {
                PrintError("Невозможно отсортировать пустой массив!");
                return;
            }
            for (int first = 0; first < integerArray.Length - 1; first++) // идём до предпоследнего. он один останется иначе он бы был минимумом на другом шаге 
            {
                int min = integerArray[first]; // чтобы самым первым минимальным
                int indexMin = first; // не заполнить всё
                for (int p = first + 1; p < integerArray.Length; p++) // идём до последнего чтобы на каждом шаге проверять всё на минимум
                {
                    if (min > integerArray[p])
                    {
                        min = integerArray[p]; // запомнили минимальный
                        indexMin = p; // место откуда его взяли
                    }
                }
                if (indexMin == first)
                {
                    continue;
                }
                Swap(integerArray, indexMin, first);
            }
        }

        /// <summary>
        /// Меняет местами два элемента в массиве
        /// </summary>
        /// <param name="integerArray">Массив, в котором нужно произвести обмен</param>
        /// <param name="indexFirst">Индекс первого элемента</param>
        /// <param name="indexSecond">Индекс второго элемента</param>
        private static void Swap(int[] integerArray, int indexFirst, int indexSecond)
        {
            int temp = integerArray[indexSecond];
            integerArray[indexSecond] = integerArray[indexFirst];
            integerArray[indexFirst] = temp;
        }

        /// <summary>
        /// Считает количество чётных элементов в массиве
        /// </summary>
        /// <param name="integerArray">Массив для счёта</param>
        /// <returns>Количество чётных элементов в массиве</returns>
        private static uint CountEvens(int[] integerArray)
        {
            uint countEvens = 0;
            foreach (int p in integerArray)
            {
                if (p % 2 == 0)
                {
                    countEvens++;
                }
            }
            return countEvens;
        }

        /// <summary>
        /// Удаляет все чётные элементы из массива
        /// </summary>
        /// <param name="integerArray">Массив, из которого  надо удалить</param>
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
            int newElementsCount = ReadInteger("Введите количество добавляемых элементов:");
            if (newElementsCount == 0)
            {
                PrintError("Добавлять нуль элементов бессмысленно!");
                return integerArray;
            }
            if (newElementsCount < 0)
            {
                PrintError("Невозможно добавить отрицательное число элементов!");
                return integerArray;
            }

            int[] newArray;
            try
            {
                newArray = new int[checked(newElementsCount + integerArray.Length)];
            }
            catch (OutOfMemoryException)
            {
                PrintError("После добавления массив стал слишком большим!");
                return integerArray; // вернём, что дали
            }
            catch (OverflowException)
            {
                PrintError("Невозможно вычислить сумму для количества элементов в заданном типе!");
                return integerArray; // вернём, что дали
            }

            string[] addMenu =
            [
                "Добавить элементы самостоятельно",
                "Добавить элементы случайно"
            ];

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
                PrintError("Невозможно переставить элементы в пустом массиве!");
                return integerArray;
            }

            uint countEvens = CountEvens(integerArray);
            if (countEvens == 0 || countEvens == integerArray.Length)
            {
                Console.WriteLine("Массив не изменился");
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
        /// <param name="integerArray">Сортируемый массив</param>
        private static void Sort(int[] integerArray)
        {
            if (CheckEmpty(integerArray))
            {
                PrintError("Невозможно отсортировать пустой массив!");
                return;
            }
            if (CheckSort(integerArray))
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
                            SelectionSort(integerArray);
                            isSorted = true;
                            break;
                        }
                    case 2:
                        {
                            HoareSort(integerArray, 0, integerArray.Length - 1);
                            isSorted = true;
                            break;
                        }
                    case 3:
                        {
                            MergeSort(integerArray, 0, integerArray.Length - 1);
                            isSorted = true;
                            break;
                        }
                }
            } while (!isSorted);
        }

        /// <summary>
        /// Сортировка Хоара
        /// </summary>
        /// <param name="integerArray">Сортируемый массив</param>
        /// <param name="left">Левая граница массива</param>
        /// <param name="right">Правая граница массива</param>
        private static void HoareSort(int[] integerArray, int left, int right)
        {
            if (left < right) // если равно то один элемент в подмассиве и его сортировать не надо
            {
                int pivotIndex = Partition(integerArray, left, right); // получаем новый опорный индекс
                HoareSort(integerArray, left, pivotIndex - 1); // сортируем те которые оказались меньше
                HoareSort(integerArray, pivotIndex + 1, right); // сортируем те которые оказались больше или равны
            }
        }

        /// <summary>
        /// Разделение массива для сортировки Хоара
        /// </summary>
        /// <param name="integerArray">Массив который делим</param>
        /// <param name="left">Левая граница массива</param>
        /// <param name="right">Правая граница массив</param>
        /// <returns>Индекс, на котором оказался опорный элемент</returns>
        private static int Partition(int[] integerArray, int left, int right)
        {
            int pivot = integerArray[left]; // выбрали первый элемент как опорный
            int low = left + 1; // начало мальньких
            int high = right; // конец больших

            // чтобы не гулять по чужим подмассивам(маленьким по большим и наоборот)
            while (high >= low)
            {
                while (high >= low && integerArray[low] < pivot) // встретили большой элемент среди маленьких и ушли
                {
                    low++; // сдвигаемся к провому концу массива
                }

                while (high >= low && integerArray[high] >= pivot) // встретили маленький элемент среди больших и ушли
                {
                    high--; // сдвигаемся к левому концу массива
                }

                if (high < low) // совсем плохо когда правая граница меньше левой
                {
                    break;
                }
                Swap(integerArray, low, high); // меняем местами попаданцев не в свой подмассив
            }

            // по последнему обольшому сдвинульсь так что большой указывает на последенего маленького
            // О М М М М М Б Б Б Б Б Б Б Б
            // поэтому по индексу большого и опорного меняемся
            // М М М М М О Б Б Б Б Б Б Б Б 
            // получили что маленькие до большие после
            Swap(integerArray, left, high);

            return high; // вернули индекс О
        }

        /// <summary>
        /// Сортировка слиянием
        /// </summary>
        /// <param name="integerArray">Сортируемый массив</param>
        /// <param name="left">Левая граница сортируемого массива(не обязательно нуль)</param>
        /// <param name="right">Правая граница сортируемого массива(не обязательно длина без единицы)</param>
        private static void MergeSort(int[] integerArray, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2; // хотя бы середину стандартно считаем
                MergeSort(integerArray, left, mid); // сортируем левый подмассив
                MergeSort(integerArray, mid + 1, right); // сортируем правый подмассив
                Merge(integerArray, left, mid, right); // сливаем два отсортированных подмассива
            }
        }

        /// <summary>
        /// Сливает отсортированные массивы
        /// </summary>
        /// <param name="integerArray">Массив в котором были сливаемые подмассивы</param>
        /// <param name="left">Левая граница(не всегда нуль)</param>
        /// <param name="mid">Середина массива от лефт до райт</param>
        /// <param name="right">Правая граница(не всегда настоящая правая)</param>
        private static void Merge(int[] integerArray, int left, int mid, int right)
        {
            int leftCounter = left; // счётчик по левому подмассиву
            int rightCounter = mid + 1; // счётчик по правому подмассиву
            int[] sortedArray = new int[right - left + 1]; // массив в который будем вписывать элементы в нужном порядке, его длина это буквально сколько между лефт и райт элементов
            int sortedArrayCounter = 0;

            while (leftCounter <= mid && rightCounter <= right) // чтобы чужим индексом не влезть в не свой подмассив
            {
                if (integerArray[leftCounter] <= integerArray[rightCounter])
                {
                    sortedArray[sortedArrayCounter++] = integerArray[leftCounter++];
                }
                else
                {
                    sortedArray[sortedArrayCounter++] = integerArray[rightCounter++];
                }
            }

            while (leftCounter <= mid) // оставшиеся большие в левом подмассиве вписываем последними
            {
                sortedArray[sortedArrayCounter++] = integerArray[leftCounter++];
            }
            while (rightCounter <= right) // оставшиеся большие в правом подмассиве вписываем последеними
            {
                sortedArray[sortedArrayCounter++] = integerArray[rightCounter++];
            }
            for (uint i = 0; i < sortedArray.Length; i++) // записываем в правильном порядке в нужное место исходного массива(left + i, как раз из-за того что left не всегда будет нулём)
            {
                integerArray[left + i] = sortedArray[i];
            }
        }
    }
}