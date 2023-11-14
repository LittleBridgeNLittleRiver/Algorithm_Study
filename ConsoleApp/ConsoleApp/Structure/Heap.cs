using ConsoleApp.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Structure {
	/// <summary>
	/// 大根堆
	/// ①heap[0]不存放值
	/// ②对于index，父index>>1，左index<<1，右(index<<1)|1
	/// </summary>
	public class Heap {
		private int[] _heap;
		private int _heapSize;
		private int _heapLimit;

		public Heap(int size) {
			_heap = new int[size + 1];
			_heapLimit = size;
			_heapSize = 0;
		}

		public Heap(int[] heap) : this(heap.Length) {
			for (int i = 0; i < heap.Length; i++) {
				Push(heap[i]);
			}
		}

		public Heap(Heap heap) {
			_heap = heap._heap;
			_heapSize = heap._heapSize;
			_heapLimit = heap._heapLimit;
		}

		public int? Pop() {
			if (IsEmpty()) {
				Debug.WriteLine("Heap is empty.");
				return null;
			}
			int result = _heap[1];
			CommonUtils.Swap(_heap, 1, _heapSize--);
			Heapify(1);
			return result;
		}

		public void Push(int value) {
			if (IsFull()) {
				throw new Exception("Heap is full.", new Exception("堆溢出"));
			}
			_heap[++_heapSize] = value;
			HeapInsert(_heapSize);
		}

		public void Change(int index, int value)
		{
			index++;
			if (IsValidIndex(index) == false) { 
			}
			// 赋值
			int lastValue = _heap[index];
			_heap[index] = value;
			// 判断数值是变大了，还是变小了
			if (value > lastValue) {
				HeapInsert(index);
			} else if (value < lastValue) {
				Heapify(index);
			}
		}

		private void HeapInsert(int index) {
			int[] arr = _heap;
			int parent = index >> 1;
			while ((arr[index] > arr[parent]) && (index != 1)) {
				CommonUtils.Swap(arr, parent, index);
				index = parent;
				parent = index >> 1;
			}
		}

		private void Heapify(int index) {
			if (IsValidIndex(index) == false) {
				return;
			}
			int[] arr = _heap;
			int heapSize = _heapSize;
			// 左孩子有效，则说明仍然有孩子
			int left = index << 1;
			while (IsValidIndex(left)) {
				int right = left | 1;
				int largest = (IsValidIndex(right) && arr[right] > arr[left]) ? right : left;
				largest = arr[largest] >= arr[index] ? largest : index;
				if (largest == index) {
					break;
				}
				CommonUtils.Swap(arr, index, largest);
				index = largest;
				left = index << 1;
			}
		}

		/// <summary>
		/// 堆排序（返回数组）
		/// </summary>
		/// <returns></returns>
		public int[]? GetSortingArray() {
			if (_heapSize <= 0) {
				return null;
			}
			// 复制堆
			int heapSize = _heapSize;
			Heap clone = Clone();
			// 遍历堆
			int[] arr = new int[heapSize];
			int i = 0;
			while (clone.IsEmpty() == false) {
				arr[i++] = clone.Pop().GetValueOrDefault();
			}
			return arr;
		}

		public void HeapSort() {

		}

		public Heap Clone() {
			return new Heap(this);
		}

		/// <summary>
		/// 索引值是否合理
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private bool IsValidIndex(int index) {
			return index <= _heapSize;
		}

		private bool IsFull() {
			return _heapSize == _heapLimit;
		}

		private bool IsEmpty() {
			return _heapSize == 0;
		}
	}
}
