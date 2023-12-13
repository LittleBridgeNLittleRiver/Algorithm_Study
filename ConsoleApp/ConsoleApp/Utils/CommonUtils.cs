using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Utils {
	public static class CommonUtils {
		/// <summary>
		/// 交换位置
		/// </summary>
		/// <param name="_arr"></param>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static void Swap(int[] _arr, int a, int b) {
			if (a == b)
				return;
			_arr[a] = _arr[a] ^ _arr[b];
			_arr[b] = _arr[a] ^ _arr[b];
			_arr[a] = _arr[a] ^ _arr[b];
		}

		/// <summary>
		/// 获取数组中最大的值
		/// </summary>
		/// <param name="_arr"></param>
		/// <returns></returns>
		public static int GetMax(int[] _arr) {
			if (_arr == null || _arr.Length == 0) {
				return Int32.MinValue;
			}
			return GetMax(_arr, 0, _arr.Length - 1);
		}

		private static int GetMax(int[] _arr, int _L, int _R) {
			if (_L == _R) {
				return _arr[_L];
			}
			int mid = _L + ((_R - _L) >> 1);    // 中点位置【重点】
			int leftMax = GetMax(_arr, _L, mid);
			int rightMax = GetMax(_arr, mid + 1, _R);
			return Math.Max(leftMax, rightMax);
		}

		/// <summary>
		/// 小和算法
		/// </summary>
		/// <param name="_arr"></param>
		/// <returns></returns>
		public static int SmallSum(int[] _arr) {
			if (_arr == null || _arr.Length == 0) {
				return 0;
			}
			return SmallSum(_arr, 0, _arr.Length - 1);
		}

		private static int SmallSum(int[] _arr, int _L, int _R) {
			if (_L == _R) {
				return 0;
			}
			int mid = _L + ((_R - _L) >> 1);
			return SmallSum(_arr, _L, mid) + SmallSum(_arr, mid + 1, _R) + SmallSumMerge(_arr, _L, mid, _R);
		}

		private static int SmallSumMerge(int[] _arr, int _L, int _M, int _R) {
			int[] help = new int[_R - _L + 1];
			int i = 0;
			int p1 = _L;
			int p2 = _M + 1;
			int sum = 0;
			while (p1 <= _M && p2 <= _R) {
				// 【注意】遇到左右侧相等的数时，右侧的数优先进入数组
				// 【注意】无法保留相同值的前后顺序
				bool condition = _arr[p1] < _arr[p2];
				sum += condition ? _arr[p1] * (_R - p2 + 1) : 0;
				help[i++] = condition ? _arr[p1++] : _arr[p2++];
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
			return sum;
		}

		/// <summary>
		/// 打印逆序对
		/// </summary>
		/// <param name="_arr"></param>
		public static void PrintReversePair(int[] _arr) {
			if (_arr == null || _arr.Length == 0) return;
			PrintReversePair(_arr, 0, _arr.Length - 1);
		}

		private static void PrintReversePair(int[] _arr, int _L, int _R) {
			if (_L == _R) return;
			int mid = _L + ((_R - _L) >> 1);
			PrintReversePair(_arr, _L, mid);
			PrintReversePair(_arr, mid + 1, _R);
			PrintReversePairMerge(_arr, _L, mid, _R);
		}

		private static void PrintReversePairMerge(int[] _arr, int _L, int _M, int _R) {
			int[] help = new int[_R - _L + 1];
			int i = 0;
			int p1 = _L;
			int p2 = _M + 1;
			while (p1 <= _M && p2 <= _R) {
				// 【注意】需保留相同值的前后顺序
				bool condition = _arr[p1] <= _arr[p2];
				if (condition == false) {
					for (int j = p1; j <= _M; j++) {
						Console.WriteLine("({0}, {1})", _arr[j], _arr[p2]);
					}
				}
				help[i++] = condition ? _arr[p1++] : _arr[p2++];
			}
			while (p1 <= _M) {
				help[i++] = _arr[p1++];
			}
			while (p2 <= _R) {
				help[i++] = _arr[p2++];
			}
			for (i = 0; i < help.Length; i++) {
				_arr[_L + i] = help[i];
			}
		}

		// 荷兰国旗问题

		/// <summary>
		/// 以目标值，将数组中【小于等于】和【大于】的部分，由左往右划分
		/// </summary>
		/// <param name="_arr"></param>
		/// <param name="_targetValue"></param>
		public static void SortingLessGreater(int[] _arr, int _targetValue) {
			if (_arr == null || _arr.Length < 2) return;
			int lessIndex = 0;
			for (int i = 0; i < _arr.Length; i++) {
				int curValue = _arr[i];
				if (curValue <= _targetValue) {
					Swap(_arr, i, lessIndex++);
				}
			}
		}

		/// <summary>
		/// 以目标值，将数组中【小于】、【等于】和【大于】的部分，由左往右划分
		/// </summary>
		/// <param name="_arr"></param>
		/// <param name="_targetValue"></param>
		public static void SortingLessEqualGreater(int[] _arr, int _targetValue) {
			if (_arr == null || _arr.Length < 2) {
				return;
			}
			int lessIndex = -1;
			int greaterIndex = _arr.Length;
			int i = 0;
			while (i < _arr.Length && i < greaterIndex) {
				int curValue = _arr[i];
				if (curValue < _targetValue) {
					Swap(_arr, i++, ++lessIndex);
				} else if (curValue > _targetValue) {
					Swap(_arr, i, --greaterIndex);
				} else {
					i++;
				}
			}
		}

		/// <summary>
		/// 获取位数
		/// </summary>
		/// <param name="num"></param>
		/// <param name="system">进制系统/多少进制</param>
		/// <returns></returns>
		public static int GetNumOfDigits(int num, int system) {
			int digits = 0;
			while (num > 0) {
				digits++;
				num /= system;
			}
			return digits;
		}


		/// <summary>
		/// 获取某一位数的值
		/// </summary>
		/// <param name="num"></param>
		/// <param name="system">进制系统/多少进制</param>
		/// <param name="dight">第几位</param>
		/// <returns></returns>
		public static int GetDigit(int num, int dight, int system = 10) {
			for (int i = 1; i < dight; i++) {
				num /= system;
			}
			num %= system;
			return num;
		}
	}
}
// 【重点】Master公式
// 【要素】子问题规模要一致。eg：可以是T(2N/3)&T(2N/3)，但不可以是T(N/3)&T(2N/3)
// 【公式】T(N) = a * T(N/b) + O(N^d)
// 【判断条件】C = log(b, a); D = d
// ①C>D => 复杂度为O(N^log(b,a));
// ②C=D => 复杂度为O(N^d * logN);
// ③C<D => 复杂度为O(N^d)
// 【判断条件】
