using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgos
{
    public class Sorts<T> where T : IComparable<T>
    {
        public static T[] BubbleSort(T[] values)
        {
            while (!IsSorted(values))
            {
                for (int i = 0; i < values.Length - 1; i++)
                {
                    if (values[i].CompareTo(values[i + 1]) > 0) (values[i], values[i + 1]) = (values[i + 1], values[i]);
                }
            }
            
            return values;
        }

        public static T[] SelectionSort(T[] values)
        {
            int index = 0;
            for (int i = 0; index < values.Length; i++)
            {
                (values[index], values[GetSmallest(values, index)]) = (values[GetSmallest(values, index)], values[index]);
                index++;
            }
            return values;
        }

        private static int GetSmallest(T[] values, int lowerBound)
        {
            int result = lowerBound;
            for(int i = lowerBound; i < values.Length; i++)
            {
                if (values[i].CompareTo(values[result]) < 0) result = i;
            }
            return result;
        }

        public static bool IsSorted(T[] values)
        {
            for(int i = 0; i < values.Length - 1; i++)
            {
                if (values[i].CompareTo(values[i + 1]) > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static T[] MergeSort(T[] values)
        {
            if (values.Length < 2)
            {
                return values;
            }

            int middle = values.Length / 2;
            T[] left = new T[values.Length - middle];
            T[] right = new T[middle];
            for (int i = 0; i < values.Length; i++)
            {
                if (i < left.Length) left[i] = values[i];
                else right[i - left.Length] = values[i];
            }

            return Merge(MergeSort(left), MergeSort(right));
        }

        private static T[] Merge(T[] left, T[] right)
        {
            T[] output = new T[left.Length + right.Length];
            int firstIndex = 0;
            int secondIndex = 0;
            int outputIndex = 0;

            while (firstIndex < left.Length && secondIndex < right.Length)
            {
                if (left[firstIndex].CompareTo(right[secondIndex]) < 0)
                {
                    output[outputIndex] = left[firstIndex];
                    firstIndex++;
                }
                else
                {
                    output[outputIndex] = right[secondIndex];
                    secondIndex++;
                }

                outputIndex++;
            }

            while (firstIndex < left.Length)
            {
                output[outputIndex] = left[firstIndex];
                outputIndex++;
                firstIndex++;
            }

            while (secondIndex < right.Length)
            {
                output[outputIndex] = right[secondIndex];
                outputIndex++;
                secondIndex++;
            }

            return output;
        }

        public static T[] QuickSortLomuto(T[] values)
        {
            QuickSortLomuto(values, 0, values.Length - 1);

            return values;
        }

        private static void QuickSortLomuto(T[] values, int left, int right)
        {
            if (left < right)
            {
                int pivot = LomutoPartition(values, left, right);
                QuickSortLomuto(values, left, pivot - 1);
                QuickSortLomuto(values, pivot + 1, right);
            }
        }

        private static int LomutoPartition(T[] items, int leftBound, int rightBound)
        {
            int pivot = rightBound;
            int wall = leftBound - 1;

            for (int i = leftBound; i < rightBound; i++)
            {
                if (items[i].CompareTo(items[pivot]) < 0)
                {
                    wall++;
                    (items[i], items[wall]) = (items[wall], items[i]);
                }
            }

            (items[pivot], items[wall + 1]) = (items[wall + 1], items[pivot]);

            return wall + 1;
        }

        public static T[] QuickSortHoare(T[] values)
        {
            QuickSortHoare(values, 0, values.Length - 1);

            return values;
        }

        private static void QuickSortHoare(T[] values, int left, int right)
        {
            if (left < right)
            {
                int pivot = HoarePartition(values, left, right);
                QuickSortHoare(values, left, pivot);
                QuickSortHoare(values, pivot + 1, right);
            }
        }

        private static int HoarePartition(T[] values, int leftBound, int rightBound)
        {
            T pivot = values[leftBound];
            int left = leftBound - 1;
            int right = rightBound + 1;

            while (left < right)
            {
                left++;
                while (values[left].CompareTo(pivot) < 0) left++;

                right--;
                while (values[right].CompareTo(pivot) > 0) right--;

                if (left < right) (values[left], values[right]) = (values[right], values[left]);
            }
            return right;
        }
    }
}
