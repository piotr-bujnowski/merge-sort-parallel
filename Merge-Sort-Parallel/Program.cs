using System;

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
