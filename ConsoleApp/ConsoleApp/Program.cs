// See https://aka.ms/new-console-template for more information
using ConsoleApp.Sort;
using ConsoleApp.Structure;
using ConsoleApp.Utils;
using System.Collections.Generic;
 
Console.WriteLine("Hello, World!");

//int[] arr = new int[] { 4, 2, 13, 5, 1, 0, 10, 8, 7, 7, 7 };

//Utils.PrintOddTimesNumFromArrayThereIsOne(1, 2, 2, 2, 2, 1, 1, 3, 3, 4, 4, 1, 5, 1, 5);
//Utils.PrintOddTimesNumFromArrayThereAreTwo(2, 3, 4, 5, 6, 2, 3, 5, 7, 7, 8, 8);

//SortingUtils.InsertSort(arr);
//for (int i = 0; i < arr.Length; i++) {
//	Console.Write(arr[i] + ",");
//	Console.WriteLine();
//}
//bool getIt = Utils.FindNumInSortedArrayWithBisection(arr, 9);
//int getIt = Utils.FindLeftMostIndexBiggerThanNumInSortedArrayWithBisection(arr, 6);

int[] arr = new int[] { 5,1,2,6,3,1,4,5,7,1,2,1,4 };
//int smallSum = CommonUtils.SmallSum(arr);
//CommonUtils.PrintReversePair(arr);
//CommonUtils.SortingLessEqualGreater(arr, 4);
//SortingUtils.QuickSort_2(arr);
//SortingUtils.RandomizedQuickSort(arr);
//int a = 1;
//arr[++a] = 3;

Heap heap = new Heap(arr);
//heap.Change(5, 8);
//int[] b = new int[arr.Length];
//arr.CopyTo(b, 0);
int[]? a = heap.GetSortingArray();

Console.WriteLine();

//Console.WriteLine(arr.ToString());	