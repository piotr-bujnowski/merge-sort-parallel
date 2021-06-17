using System;

namespace Merge_Sort_Parallel
{
    class MergeSort
    {
        // Driver code
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

        void merge(int[] arr, int l, int m, int r)
        {
            int n1 = m - l + 1;
            int n2 = r - m;

            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j;

            for (i = 0; i < n1; ++i)
            {
                L[i] = arr[l + i];
            }

            for (j = 0; j < n2; ++j)
            {
                R[j] = arr[m + 1 + j];
            }

            i = 0;
            j = 0;

            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        void sort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                int m = l + (r - l) / 2;

                sort(arr, l, m);
                sort(arr, m + 1, r);

                merge(arr, l, m, r);
            }
        }

        static void printArray(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
    }
}
