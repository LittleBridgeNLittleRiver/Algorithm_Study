using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Utils {
	public static class SortingUtils {
		/// <summary>
		/// 选择排序
		/// </summary>
		/// <param name="_arr"></param>
		/// <param name="_accending">是否为升序</param>
		public static void SelectionSort(int[] _arr, bool _accending = true) {
			if (_arr == null || _arr.Length < 2) {
				return;
			}
			for (int i = 0; i < _arr.Length; i++) {
				int targetIndex = i;
				for (int j = i + 1; j < _arr.Length; j++) {
					bool swap = _accending ? (_arr[targetIndex] > _arr[j]) : (_arr[targetIndex] < _arr[j]);
					if (swap) {
						targetIndex = j;
					}
				}
				CommonUtils.Swap(_arr, i, targetIndex);
			}
		}

		/// <summary>
		/// 冒泡排序
		/// </summary>
		/// <param name="_arr"></param>
		/// <param name="_accending"></param>
		public static void BubbleSort(int[] _arr, bool _accending = true) {
			if (_arr == null || _arr.Length < 2) {
				return;
			}
			for (int i = _arr.Length - 1; i >= 0; i--) {
				for (int j = 0; j < i; j++) {
					bool swap = _accending ? (_arr[j] > _arr[j + 1]) : (_arr[j] < _arr[j + 1]);
					if (swap) {
						CommonUtils.Swap(_arr, j, j + 1);
					}
				}
			}
		}

		/// <summary>
		/// 插入排序
		/// </summary>
		/// <param name="_arr"></param>
		/// <param name="_accending"></param>
		public static void InsertSort(int[] _arr, bool _accending = true) {
			for (int i = 1; i < _arr.Length; i++) {
				int insertPos = i;
				for (int j = i - 1; j >= 0; j--) {
					bool accend = _arr[j] > _arr[j + 1];
					bool swap = _accending ? accend : !accend;
					if (swap)
						CommonUtils.Swap(_arr, j, j + 1);
					else
						break;
				}
			}
		}

		private static void Merge(int[] _arr, int _L, int _M, int _R) {
			int[] help = new int[_R - _L + 1];
			int i = 1;
			int p1 = _L;
			int p2 = _M + 1;
			while(p1 <= _M && p2 <= _R) {
				help[i++] = _arr[p1] <= _arr[p2] ? _arr[p1++] : _arr[p2++];
			}
			while (p1 <= _M) {
				help[i++] = _arr[p1++];
			}
			while (p2 <= _R) {
				help[i++] = _arr[p2++];
			}
			// 结果返回
			for (i = 0; i < help.Length; i++) {
				_arr[_L + i] = help[i];
			}
		}

		private static void MergeSort(int[] _arr, int _L, int _R) {
			// 中止条件
			if (_L == _R) {
				return;
			}
			int mid = _L + ((_R - _L) >>  1);
			MergeSort(_arr, _L, mid);
			MergeSort(_arr, mid + 1, _R);
			Merge(_arr, _L, mid, _R);
		}

		/// <summary>
		/// 归并排序
		/// </summary>
		/// <param name="_arr"></param>
		public static void MergeSort(int[] _arr) {
			if (_arr == null || _arr.Length < 2)
				return;
			MergeSort(_arr, 0, _arr.Length - 1);
		}

		/// <summary>
		/// 快排2.0（荷兰国旗）
		/// </summary>
		/// <param name="_arr"></param>
		public static void QuickSort_2(int[] _arr) {
			if (_arr == null || _arr.Length < 2) {
				return;
			}
			QuickSort_2(_arr, 0, _arr.Length - 1);
		}

		private static void QuickSort_2(int[] _arr, int _L, int _R) {
			if (_L <= _R) {
				(int, int) boundary = QuickSort_Three_Partition(_arr, _L, _R);
				QuickSort_2(_arr, _L, boundary.Item1);
				QuickSort_2(_arr, boundary.Item2, _R);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_arr"></param>
		/// <param name="_L"></param>
		/// <param name="_R"></param>
		/// <returns>（小于区的右边界Index, 大于区的左边界Index）</returns>
		private static (int, int) QuickSort_Three_Partition(int[] _arr, int _L, int _R) {
			// 以最右侧的数为目标书，完成荷兰国旗（小于、等于、大于）问题
			int targetValue = _arr[_R];
			int lessIndex = _L - 1;
			int greaterIndex = _R;
			while (_L < greaterIndex) {
				int curValue = _arr[_L];
				if (curValue < targetValue) {
					CommonUtils.Swap(_arr, _L++, ++lessIndex);
				} else if (curValue > targetValue) {
					CommonUtils.Swap(_arr, _L, --greaterIndex);
				} else {
					_L++;
				}
			}
			CommonUtils.Swap(_arr, _R, greaterIndex);
			return (lessIndex, greaterIndex + 1);
		}

		/// <summary>
		/// 随即快排（快排3.0）
		/// </summary>
		/// <param name="_arr"></param>
		public static void RandomizedQuickSort(int[] _arr) {
			if (_arr == null || _arr.Length < 2) {
				return;
			}
			var random = new Random();
			RandomizedQuickSort(random, _arr, 0, _arr.Length - 1);
		}

		private static void RandomizedQuickSort(Random random, int[] _arr, int _L, int _R) {
			if (_L <= _R) {
				CommonUtils.Swap(_arr, random.Next(_L, _R), _R);
				(int, int) boundary = QuickSort_Three_Partition(_arr, _L, _R);
				QuickSort_2(_arr, _L, boundary.Item1);
				QuickSort_2(_arr, boundary.Item2, _R);
			}
		}

		/// <summary>
		/// 计数排序
		/// 适合小范围计数，且为自然数
		/// </summary>
		/// <param name="_arr"></param>
		/// <param name="_L"></param>
		/// <param name="_R"></param>
		public static void CountingSort(int[] _arr, int _L, int _R) {
			// 找出最大数
			int max = int.MinValue;
			for (int i = _L; i < _R; i++) {
				if (max > _arr[i]) max = _arr[i];
			}
			// 空间换时间
			int[] counter = new int[max + 1];
			for (int i = _L; i < _R; i++) {
				counter[_arr[i]]++;
			}
			// 再从计数器中倒出
			int index = _L;
			for (int cIndex = 0; cIndex < counter.Length; cIndex++) {
				// 有多少哥桶
				for (int i = 0; i < counter[cIndex]; i++) {
					// 每个桶里有多少个数
					_arr[index++] = cIndex;
				}
			}
		}
	}
}
