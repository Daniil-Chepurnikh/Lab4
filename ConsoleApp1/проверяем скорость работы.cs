using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int[] A = new int[10001];
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
                A[i] = random.Next(-10000,10000);

            HoareSort(A, 0, A.Length - 1);
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");



        }


        private static void HoareSort(int[] array, int left, int right)
        {
            if (left < right) // если равно то один элемент в подмассиве и его сортировать не надо
            {
                int pivotIndex = HoarePartition(array, left, right); // получаем новый опорный индекс
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
        private static int HoarePartition(int[] array, int left, int right)
        {
            int pivot = array[left]; // выбрали первый элемент как опорный
            int low = left + 1; // начало мальньких
            int high = right; // конец больших

            while (high > low) // чтобы не гулять по чужим подмассивам(маленьким по большим и наоборот)
            {
                while (high >= low && array[low] < pivot) // встретили большой элемент среди маленьких и ушли
                    low++; // сдвигаемся к провому концу массива

                while (high >= low && array[high] >= pivot) // встретили маленький элемент среди больших и ушли
                    high--; // сдвигаемся к левому концу массива

                if (high < low) // совсем плохо когда правая граница меньше левой
                    break;
                Swap(array, low, high); // меняем местами попаданцев не в свой подмассив
            }

            // по последнему обольшому сдвинульсь так что большой указывает на последенего маленького
            // О М М М М М Б Б Б Б Б Б Б Б
            // поэтому по индексу большого и попрного меняемся
            // М М М М М О Б Б Б Б Б Б Б Б 
            // получили что маленькие до большие после
            Swap(array, left, high);

            return high; // вернули индекс О
        }

        private static void Swap(int[] integerArray, int indexFirst, int indexSecond)
        {
            int temp = integerArray[indexSecond];
            integerArray[indexSecond] = integerArray[indexFirst];
            integerArray[indexFirst] = temp;
        }


        private static void SelectionSort(int[] integerArray)
        {
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
                    continue;

                Swap(integerArray, indexMin, q);
            }
        }
    }
}
