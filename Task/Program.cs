/*
 Создать массив целых чисел: датчиком случайных чисел и вводом с клавиатуры; СДЕЛАНО
 распечатать его; СДЕЛАНО
 удалить из него все чётные элементы;
 добавить К элементов в начало массива(что это за элементы???);
 чётные элементы переставить в начало, а нечётные в конец; 
 найти первый чётный элемент; СДЕЛАНО
 бинарный поиск элемента; СДЕЛАНО
 отсортировать массив простым выбором СДЕЛАНО
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
            // TODO: сделать двухуровневое текстовое меню

            ReadArray(out int[] keyboardArray);
            PrintArray(keyboardArray);
            FindFirstEven(keyboardArray);
            SelectionSort(keyboardArray);
            BinarySearch(keyboardArray);
            PrintArray(DeleteEvens(keyboardArray));
            
            Console.WriteLine("===================================");
            
            MakeRandomArray(out int[] randomArray);
            PrintArray(randomArray);
            FindFirstEven(randomArray);
            SelectionSort(randomArray);
            BinarySearch(randomArray);
            PrintArray(DeleteEvens(randomArray));
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
            CheckArraySize(out int length);

            keyboardArray = new int[length];
            for (int q = 0; q < length; q++)
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
            CheckArraySize(out int length);

            randomArray = new int[length];
            for (int q = 0; q < length; q++)
            {
                randomArray[q] = random.Next(-100, 100);
            }
        }

        /// <summary>
        /// Проверяет переполнение памяти массивом целых чисел
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
                    Console.WriteLine("Массив не может иметь нулевую длину!");
                    isCorrectArraySize = false;
                }
                else if (length < 0)
                {
                    Console.WriteLine("Массив не может иметь отрицательную длину!");
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
                        Console.WriteLine("Переполнение памяти слишком большим массивом!");
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
                    Console.WriteLine($"Первый чётный элемент: {integerArray[i]}. Его индекс: {i + 1}");
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
            int target = ReadInteger("Введите целое число, которое вы хотите найти в массиве:", "Ошибка ввода целого числа!");
            int left = 0;
            int right = sortedIntegerArray.Length - 1;
            int mid = left + (right - left) / 2;
            int steps = 1;
            while (left <= right)
            {
                if (target == sortedIntegerArray[mid])
                {
                    Console.WriteLine("Элемент есть в массиве. Его индекс: " + (mid + 1) + "." + " Количество сравнений: " + steps);
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

        private static int[] SelectionSort(int[] array)
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

            PrintArray(array);
            return array;
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

        // TODO: удалить из массива все чётные элементы
        /// <summary>
        /// Удаляет все чётные элементы из массива
        /// </summary>
        /// <param name="integerArray">Массив, из которого нужно удалить элементы</param>
        /// <returns></returns>
        private static int[]? DeleteEvens(int[] integerArray)
        {
            uint evens = CountEvens(integerArray);
            if (evens == 0)
                return integerArray;
            else if (evens == integerArray.Length)
            {
                // TODO: узнать что делать в этом случае
                Console.WriteLine("После удаления массив стал пустым!");
                int[] empty = {0};
                return empty;
            }
            else
            {
                int[] newArray = new int[integerArray.Length - evens];
                uint index = 0;
                foreach (int p in integerArray)
                {
                    if (p % 2 != 0)
                        newArray[index] = p;
                    index++;
                }
                return newArray;
            }  
        }


        // TODO: добавить К элементов в начало массива

        // TODO: чётные элементы переставить в начало, а нечётные в конец
    }
} 