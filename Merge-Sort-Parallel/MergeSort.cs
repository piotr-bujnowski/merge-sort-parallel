using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Merge_Sort_Parallel
{
    class MergeSort
    {
        // Sortowanie równoległe
        public static void parallelSort(int[] array, int maxNumOfThreads)
        {
            int length = array.Length;
            int maxLimit;
            /*
             * Praca ( sortwanie ) na wątek -> elementy / wątek
             * Jeśli cała elementów idzie do wszystkich dostepnych elementów -> podziel pracę równo na wątki
             * Jeśli została reszta, wtedy przypuść, że jest 1 dostępny wątek
             * przypisz wtedy pozostałe elementy do pozostałego wątku
             */
            bool exact = length % maxNumOfThreads == 0;
            if (exact)
            {
                maxLimit = length / maxNumOfThreads;
            }
            else
            {
                maxLimit = length / (maxNumOfThreads - 1);
            }

            // Jeśli praca nie wymaga nie więcej niż 1 wątek, wtedy przypisz całą pracę do 1 wątku
            maxLimit = maxLimit < maxNumOfThreads ? maxNumOfThreads : maxLimit;

            // Lista wątków
            ArrayList threadsList = new ArrayList();

            /* Rozpocznij każdy wątek i podziel prace ( na kawałki elementów )
             * np. watek1 = 0-7, watek2 = 8-15, watek3 = 16-23, watek4 = 24-31
             */
            for (int i = 0; i < length; i += maxLimit)
            {
                int left = i;
                int remain = (length) - i; // Pozostałe
                int right = remain < maxLimit ? i + (remain - 1) : i + (maxLimit - 1);
                Thread thread = new Thread(() =>
                {
                    sort(array, left, right);
                });
                thread.Start();
                threadsList.Add(thread);
            }

            //Zakończenie wszystkich wątków - wymagane do scalenia
            foreach (Thread thread in threadsList)
            {
                thread.Join();
            }

            /* Merge bierze 2 tablice na raz i scala w jedną,
             * wtedy bierze wynik i scala nastepne
             * 
             * dla maxLimit = 2 -> 2 elementy na wątek, gdzie ilość wątków = 4, wszystkie elementy 4 * 8 = 8 elementów
             */
            for (int i = 0; i < length; i += maxLimit)
            {
                int mid = i == 0 ? 0 : i - 1;
                int remain = (length) - i;
                int end = remain < maxLimit ? i + (remain - 1) : i + (maxLimit - 1);
                merge(array, 0, mid, end);
            }
        }

        public static void merge(int[] array, int left, int middle, int right)
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
        public static void sort(int[] array, int left, int right)
        {
            if (left < right) // Ogon rekurencji
            {
                // Znajdź środkowy indeks
                int middle = left + (right - left) / 2;

                // Posortuj tablice ( podziel )
                sort(array, left, middle);
                sort(array, middle + 1, right);

                // Scal posortowane tablice
                merge(array, left, middle, right);
            }
        }
    }
}
