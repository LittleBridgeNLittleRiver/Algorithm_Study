using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Sort {
	public static class Utils {
		/// <summary>
		/// 获取位运算中最右侧的一
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		private static int GetTheRightMostOne(int num) {
			return num & (~num + 1);	// or (num & -num)
		}

		/// <summary>
		/// 获得数组中奇数个的项（数组中仅有一种数是奇数个）
		/// </summary>
		/// <param name="_arr"></param>
		/// <returns></returns>
		public static int GetOddTimesNumFromArrayThereIsOne(int[] _arr) {
			int eor = 0;
			for (int i = 0; i < _arr.Length; i++) {
				eor ^= _arr[i];
			}
			return eor;
		}

		/// <summary>
		/// 获得数组中奇数个的项（数组中仅有两种不同的数是奇数个）
		/// </summary>
		/// <param name="_arr"></param>
		/// <param name="_numOne"></param>
		/// <param name="_numTwo"></param>
		public static void GetOddTimesNumFromArrayThereAreTwo(int[] _arr, out int _numOne, out int _numTwo) {
			// 求所有数的异或值eor，其值为两个奇数个的项的异或值
			int eor = 0;
			for (int i = 0; i < _arr.Length; i++) {
				eor ^= _arr[i];
			}
			// 两个不同的数，其异或值一定存在一位，不等于0
			// 获得只包含eor最右侧的1的数，这个位置的1，代表着两个奇数个的项，在该位置一定存在差异
			int rightMostOne = GetTheRightMostOne(eor);
			// 求出差异位上为1的奇数个的项
			_numOne = 0;	// 需要赋默认值
			for (int i = 0; i < _arr.Length; i++) {
				int temp = _arr[i];
				if ((temp & rightMostOne) != 0) {
					_numOne ^= temp;
				}
			}   
			// 求出另一个奇数个的项
			_numTwo = eor ^ _numOne;
		}

		public static void PrintOddTimesNumFromArrayThereIsOne(params int[] _arr) {
			int a = GetOddTimesNumFromArrayThereIsOne(_arr);
			Console.WriteLine("odd = {0}", a.ToString());
		}

		public static void PrintOddTimesNumFromArrayThereAreTwo(params int[] _arr) {
			int a, b;
			GetOddTimesNumFromArrayThereAreTwo(_arr, out a, out b);
			Console.WriteLine("one = {0}, two = {1}", a.ToString(), b.ToString());
		}

		private static bool FindNumInSortedArrayWithBisection(int[] _arr, int _target, int _left, int _right) {
			int middle = (_right + _left) / 2;
			int middleValue = _arr[middle];
			Console.Write(middleValue + ",");
			// 结束条件
			if (_right == _left && middleValue != _target)
				return false;
			// 判断条件
			if (middleValue == _target)
				return true;
			else if (middleValue < _target)
				return FindNumInSortedArrayWithBisection(_arr, _target, middle + 1, _right);
			else
				return FindNumInSortedArrayWithBisection(_arr, _target, _left, middle);
		}

		public static bool FindNumInSortedArrayWithBisection(int[] _arr, int _target) {
			return FindNumInSortedArrayWithBisection(_arr, _target, 0,  _arr.Length - 1);
		}

		private static int FindLeftMostIndexBiggerThanNumInSortedArrayWithBisection(int[] _arr, int _comparison, int _left, int _right) {
			int middle = (_right + _left) / 2;
			int middleValue = _arr[middle];
			Console.Write(middleValue + ",");
			// 结束条件
			if (_left == _right) {
				return middle;
			}
			// 判断条件
			if (middleValue > _comparison)
				return FindLeftMostIndexBiggerThanNumInSortedArrayWithBisection(_arr, _comparison, _left, middle);
			else
				return FindLeftMostIndexBiggerThanNumInSortedArrayWithBisection(_arr, _comparison, middle + 1, _right);
		}

		public static int FindLeftMostIndexBiggerThanNumInSortedArrayWithBisection(int[] _arr, int _comparison) {
			return FindLeftMostIndexBiggerThanNumInSortedArrayWithBisection(_arr, _comparison, 0, _arr.Length);
		}

		private static int FindA_MinimumIndex(int[] _arr, int _left, int _right) {
			Console.WriteLine("LeftIndex = {0}. RightIndex = {1}", _left, _right);
			int target = -1;
			// 判斷條件
			int middle = (_right + _left) / 2;
			if (middle == 0) {
				if (_arr[middle] < _arr[middle + 1])
					target = middle;
			} else if (middle == _arr.Length - 1) {
				if (_arr[middle] < _arr[middle - 1])
					target = middle;
			} else if (_arr[middle] < _arr[middle - 1] && _arr[middle] < _arr[middle + 1]) {
				target = middle;
			}
			// 結束條件
			if (target == -1) {
				if (_left != _right) {
					target = FindA_MinimumIndex(_arr, _left, middle);
					if (target == -1)
						target = FindA_MinimumIndex(_arr, middle + 1, _right);
				}
			}
			return target;
		}

		public static int FindA_MinimumIndex(int[] _arr) {
			if (_arr == null || _arr.Length == 0)
				return -1;
			if (_arr.Length == 1)
				return 0;
			return FindA_MinimumIndex(_arr, 0, _arr.Length - 1);
		}
	}
}
