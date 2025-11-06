/*
 Создать массив целых чисел: датчиком случайных чисел и вводом с клавиатуры; распечатать его;
 удалить из него все чётные элементы; добавить К элементов в начало массива(что это за элементы?);
 чётные элементы переставить в начало а нечётные в конец; найти первый чётный элемент; 
 бинарный поиск элемента; отсортировать массив простым выбором(???)
*/
using System;

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
            ReadArray(out int[] keyboardArray);
            PrintArray(keyboardArray);
            Console.WriteLine(FindFirstEven(keyboardArray));

            MakeRandomArray(out int[] randomArray);
            PrintArray(randomArray);
            Console.WriteLine(FindFirstEven(randomArray));
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
        private static int ReadInteger(string message = "n ?", string error = "Ошибка ввода n!")
        {
            bool isNumber;
            int number;
            do
            {
                Console.WriteLine(message);

                isNumber = int.TryParse(Console.ReadLine(), out number);

                if (!isNumber)
                    Console.WriteLine(error);
            } while (!isNumber);

            return number;
        }

        /// <summary>
        /// Печатает массив целых чисел
        /// </summary>
        /// <param name="integerArray">Массив, который необходимо распечатать</param>
        private static void PrintArray(int[] integerArray)
        {
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
        private static void ReadArray(out int[] keyboardArray)
        {
            CheckArray(out int n);

            keyboardArray = new int[n];
            for (int q = 0; q < n; q++)
            {
                keyboardArray[q] = ReadInteger("Элемент ?", "Ошибка ввода элемента массива!");
            }
        }

        /// <summary>
        /// Создаёт массив целых чисел датчиком случайных чисел
        /// </summary>
        /// <param name="randomArray"></param>
        private static void MakeRandomArray(out int[] randomArray)
        {
            CheckArray(out int n);

            randomArray = new int[n];
            for (int q = 0; q < n; q++)
            {
                randomArray[q] = random.Next(int.MinValue, int.MaxValue);
            }
        }

        /// <summary>
        /// Проверяет переполнение памяти массивом целых чисел
        /// </summary>
        /// <param name="length">Длина массива</param>
        private static void CheckArray(out int length)
        {
            bool isCorrectArraySize;
            do
            {
                length = ReadInteger();
                if (length == 0)
                {
                    Console.WriteLine("Массив не может иметь нулевой размер!");
                    isCorrectArraySize = false;
                }
                else
                {
                    try
                    {
                        int[] array = new int[length];
                        isCorrectArraySize = true;
                    }
                    catch (OutOfMemoryException)
                    {
                        Console.WriteLine("Переполнение памяти слишком большим массивом");
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
        private static string FindFirstEven(int[] integerArray)
        {
            for (uint i = 0; i < integerArray.Length; i++)
            {
                if (integerArray[i] % 2 == 0)
                    return "Первый чётный элемент: " + $"{integerArray[i]}";
            }

            return "Нет чётных элементов!";
        }

        /// <summary>
        /// Ищет элемент в массиве алгоритмом бинарного поиска
        /// </summary>
        /// <param name="integerArray">Массив для поиска элемента в нём</param>
        private static void BinarySearch(int[] sortedIntegerArray)
        {
            int target = ReadInteger("Введите целое число, которое вы хотите найти в массиве", "Ошибка ввода целого числа!");
            int left = 0;
            int right = sortedIntegerArray.Length - 1;
            int mid = (left + right) / 2;
            int steps = 1;
            do
            {
                if (target == sortedIntegerArray[mid])
                {
                    Console.WriteLine("Элемент существует в массиве. Его индекс: " + (mid + 1));
                    return;
                }
                else if (target > sortedIntegerArray[mid])
                    left = mid + 1;
                else
                    right = mid - 1;

                mid = (left + right) / 2;
                steps++;

            } while (left <= right);

            Console.WriteLine("Элемента не существует в массиве");
        }
    }
}