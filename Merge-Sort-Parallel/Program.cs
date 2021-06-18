using System;
using System.Threading;

namespace Merge_Sort_Parallel
{
    class Program
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

                int[] arr1 = new int[numSize];
                for (int i = 0; i < arr1.Length; i++)
                {
                    arr1[i] = random.Next(100);
                }

                int[] arr2 = new int[numSize];
                Array.Copy(arr1, arr2, arr1.Length);
                Console.Write("\nCzy chcesz wydrukować wygenerowane tablice? [y/n]: \n>");
                String generatedArrChoice = Console.ReadLine();

                if (generatedArrChoice.ToLower().Equals("y"))
                {
                    Console.WriteLine("\nWygenerowana tablica:");
                    printArray(arr1);
                }

                // Sortowanie asynchroniczne ( równoległe )
                var watchParallel = System.Diagnostics.Stopwatch.StartNew(); // Timer dla rónoległego
                MergeSort.parallelSort(arr1, 4);
                watchParallel.Stop();
                var elapsedTimeParallel = watchParallel.ElapsedMilliseconds;

                // Sortowanie synchroniczne
                var watchSync = System.Diagnostics.Stopwatch.StartNew(); // Timer dla synchronicznego
                MergeSort.sort(arr2, 0, arr2.Length - 1);
                watchSync.Stop();
                var elapsedTimeSync = watchSync.ElapsedMilliseconds;

                Console.Write("\nCzy chcesz wyświetlić posortowaną tablice? [y/n]: \n>");
                String sortedArrChoice = Console.ReadLine();

                if (sortedArrChoice.ToLower().Equals("y"))
                {
                    Console.WriteLine("\nPosortowana tablica:");
                    printArray(arr1);
                }

                Console.WriteLine($"\nCzas sortowania Rónoległego: {elapsedTimeParallel} ms");
                Console.WriteLine($"Czas sortowania Synchronicznego: {elapsedTimeSync} ms");

                Console.Write("\nCzy chcesz powtórzyć sortowanie? [y/n]: \n> ");
                exit = Console.ReadLine();
            } while (!exit.Equals("n"));
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
