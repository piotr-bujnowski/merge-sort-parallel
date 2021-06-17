using System;

namespace Merge_Sort_Parallel
{
    class MergeSort
    {
        public static void Main(String[] args)
        {
            Random random = new Random();
            string exit;
            int numSize;

            do
            {
                Console.Write($"\nPodaj ilość liczb do wygenerowania losowo: \n> ");
                do
                {
                    while (!int.TryParse(Console.ReadLine(), out numSize))
                    {
                        Console.WriteLine("Podana wartość nie jest liczbą!");
                    }

                    if (numSize <= 0)
                    {
                        Console.WriteLine("Podana wartość nie może być <= 0!");
                    }
                }
                while (numSize <= 0);

                int[] arr = new int[numSize];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = random.Next(100);
                }

                Console.WriteLine("\nWygenerowane tablica:");
                printArray(arr);

                MergeSort ob = new MergeSort();
                ob.sort(arr, 0, arr.Length - 1);

                Console.WriteLine("\nPosortowane tablica: ");
                printArray(arr);

                Console.Write("\nCzy chcesz powtórzyć sortowanie? [y/n]: \n> ");
                exit = Console.ReadLine();
            } while (!exit.Equals("n"));
        }

        public void merge(int[] array, int left, int middle, int right)
        {
            // Znajdz wielkość dwóch tablic do scalenia
            int n1 = middle - left + 1; // Wielkość I tablicy
            int n2 = right - middle; // Wielkość II tablicy

            // Tymczasowe tablice
            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];
            int i, j;

            // Kopiowanie liczb do tablic
            for (i = 0; i < n1; ++i)
            {
                leftArr[i] = array[left + i];
            }
            for (j = 0; j < n2; ++j)
            {
                rightArr[j] = array[middle + 1 + j];
            }

            // Początkowe indeksy tablic
            i = 0;
            j = 0;

            // Indeks początku do przejrzenia tablic
            int k = left;
            while (i < n1 && j < n2)
            {
                if (leftArr[i] <= rightArr[j])
                {
                    array[k] = leftArr[i];
                    i++;
                }
                else
                {
                    array[k] = rightArr[j];
                    j++;
                }
                k++;
            }

            // Skopiuj pozostałe liczby lewej tablicy jeśli jakieś pozostały
            while (i < n1)
            {
                array[k] = leftArr[i];
                i++;
                k++;
            }

            // Skopiuj pozostałe liczby prawej tablicy jeśli jakieś pozostały
            while (j < n2)
            {
                array[k] = rightArr[j];
                j++;
                k++;
            }
        }

        // Sortowanie ( rekurencja )
        public void sort(int[] array, int left, int right)
        {
            if (left < right) // Ogon rekurencji
            {
                // Znajdź środkowy indeks
                int middle = left + (right - left) / 2;

                // Posortuj pierwszą i drugą połowe tablicy ( podziel )
                sort(array, left, middle);
                sort(array, middle + 1, right);

                // Scal posortowane tablice
                merge(array, left, middle, right);
            }
        }

        // Drukuj tablice
        static void printArray(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
    }
}
