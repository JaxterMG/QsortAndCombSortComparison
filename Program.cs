using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASD_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();


            int sortAmount = 100000;
            int maxArr = 1000;
            int step = 10;
            double[] time1 = new double [maxArr];
            double[] time2=new double[maxArr];
            int k = 0;
            for (int i = 10; i <= maxArr; i += step)
            {

                for (int j = 0; j <= sortAmount; j++)
                {
                    double[] arr = GetRandomArr(i);
                    double[] arr2 = new double[i];
                    arr.CopyTo(arr2,0);
                    myStopwatch.Start(); //запуск
                    quicksort(arr, 0, i - 1);
                    myStopwatch.Stop(); //остановить
                    time1[k] += myStopwatch.Elapsed.TotalSeconds;
                    myStopwatch.Reset();

                    myStopwatch.Start(); //запуск
                    combSort(arr2);
                    myStopwatch.Stop(); //остановить

                    time2[k] += myStopwatch.Elapsed.TotalSeconds;
                    myStopwatch.Reset();
                }
                Console.WriteLine("Iteration " + (k + 1));
                Console.WriteLine("RunTime quicksort " + time1[k] / sortAmount);
                Console.WriteLine("RunTime combsort " + time2[k] / sortAmount);
                ++k;


            }
            
            using (StreamWriter sw = new StreamWriter(File.OpenWrite("Data.txt")))
            {
                
                for (k = 0; k < (maxArr / step); k++)
                {
                    sw.WriteLine("{0}\t{1}\t{2}", 10 * k + 10, time1[k] / sortAmount, time2[k] / sortAmount);
                }
            }

            Console.WriteLine("File has been filled ");
            Console.ReadLine();
        }
        private static double[] GetRandomArr(int maxArray)

        {
            double[] arr = new double[maxArray];
            Random rand = new Random();
            for (int i = 0; i < maxArray; i++)
            {
                arr[i] = rand.Next(0, maxArray);
            }
            return arr;
        }

       private static int partition(double[] array, int start, int end)
        {
            double temp;//swap helper
            int marker = start;//divides left and right subarrays
            for (int i = start; i <= end; i++)
            {
                if (array[i] < array[end]) //array[end] is pivot
                {
                    temp = array[marker]; // swap
                    array[marker] = array[i];
                    array[i] = temp;
                    marker += 1;
                } 
            }
            
            temp = array[marker];
            array[marker] = array[end];
            array[end] = temp;
            return marker;
        }

       private static void quicksort(double[] array, int start, int end)
       {
            if (start >= end)
            {
                return;
            }
            int pivot = partition(array, start, end);
            quicksort(array, start, pivot - 1);
            quicksort(array, pivot + 1, end);
       }
        public static double[] combSort(double[] input)
        {
            double gap = input.Length;
            bool swaps = true;
            while (gap > 1 || swaps)
            {
                gap /= 1.247330950103979;
                if (gap < 1) 
                {
                    gap = 1;
                }
                int i = 0;
                swaps = false;
                while (i + gap < input.Length)
                {
                    int igap = i + (int)gap;
                    if (input[i] > input[igap])
                    {
                        double swap = input[i];
                        input[i] = input[igap];
                        input[igap] = swap;
                        swaps = true;
                    }
                    i++;
                }
            }
            return input;
        }

    
    }

}
