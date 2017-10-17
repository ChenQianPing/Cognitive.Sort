using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognitive.Sort
{
    public static class SortHelper<T> where T : IComparable
    {
        #region 插入类排序-直接插入排序
        public static void StraightInsertSort(T[] arr)
        {
            int i;

            for (i = 1; i < arr.Length; i++)
            {
                var j = i - 1;
                var temp = arr[i];

                while (j >= 0 && temp.CompareTo(arr[j]) < 0)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = temp;
            }
        }
        #endregion
        
        #region 插入类排序-希尔排序
        public static void ShellSort(T[] arr)
        {
            int i, j, d;
            T temp;

            for (d = arr.Length / 2; d >= 1; d = d / 2)
            {
                for (i = d; i < arr.Length; i++)
                {
                    j = i - d;
                    temp = arr[i];

                    while (j >= 0 && temp.CompareTo(arr[j]) < 0)
                    {
                        arr[j + d] = arr[j];
                        j = j - d;
                    }

                    arr[j + d] = temp;
                }
            }
        }
        #endregion

        #region 交换类排序-冒泡排序
        public static void BubbleSort(T[] arr)
        {
            int i, j;
            T temp;
            var isExchanged = true;

            for (j = 1; j < arr.Length && isExchanged; j++)
            {
                isExchanged = false;
                for (i = 0; i < arr.Length - j; i++)
                {
                    if (arr[i].CompareTo(arr[i + 1]) > 0)
                    {
                        // 核心操作：交换两个元素
                        temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        // 附加操作：改变标志
                        isExchanged = true;
                    }
                }
            }
        }
        #endregion

        #region 交换类排序-快速排序，小数据量中最快方法
        public static void QuickSort(T[] arr)
        {
            // 查看数组是否为空
            if (arr.Length > 0)
            {
                QuickSort(arr, 0, arr.Length - 1);
            }
        }

        public static void QuickSort(T[] arr, int low, int high)
        {
            if (low < high)
            {
                var index = Partition(arr, low, high);
                // 对左区间递归排序
                QuickSort(arr, low, index - 1);
                // 对右区间递归排序
                QuickSort(arr, index + 1, high);
            }
        }

        /// <summary>
        /// 获取基准值的实际存储位置
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        private static int Partition(T[] arr, int low, int high)
        {
            int i = low, j = high;
            T temp = arr[i]; // 确定第一个元素作为"基准值"

            while (i < j)
            {
                // Stage1:从右向左扫描直到找到比基准值小的元素
                while (i < j && arr[j].CompareTo(temp) >= 0)
                {
                    j--;
                }
                // 将比基准值小的元素移动到基准值的左端
                arr[i] = arr[j];

                // Stage2:从左向右扫描直到找到比基准值大的元素
                while (i < j && arr[i].CompareTo(temp) <= 0)
                {
                    i++;
                }
                // 将比基准值大的元素移动到基准值的右端
                arr[j] = arr[i];
            }

            // 记录归位
            arr[i] = temp;

            return i;
        }
        #endregion

        #region 选择类排序-简单选择排序
        public static void SimpleSelectSort(T[] arr)
        {
            int i, j, k;
            T temp;

            for (i = 0; i < arr.Length - 1; i++)
            {
                k = i; // k用于记录每一趟排序中最小元素的索引号
                for (j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j].CompareTo(arr[k]) < 0)
                    {
                        k = j;
                    }
                }

                if (k != i)
                {
                    // 交换arr[k]和arr[i]
                    temp = arr[k];
                    arr[k] = arr[i];
                    arr[i] = temp;
                }
            }
        }
        #endregion

        #region 选择类排序-堆排序
        public static void HeapSort(T[] arr)
        {
            var n = arr.Length; // 获取序列的长度

            // 构造初始堆
            for (var i = n / 2 - 1; i >= 0; i--)
            {
                Sift(arr, i, n - 1);
            }

            // 进行堆排序
            T temp;
            for (var i = n - 1; i >= 1; i--)
            {
                temp = arr[0];       // 获取堆顶元素
                arr[0] = arr[i];     // 将堆中最后一个元素移动到堆顶
                arr[i] = temp;       // 最大元素归位,下一次不会再参与计算

                Sift(arr, 0, i - 1); // 重新递归调整堆
            }
        }

        /// <summary>
        /// 核心：创建堆的过程
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        private static void Sift(T[] arr, int low, int high)
        {
            // i为欲调整子树的根节点索引号，j为这个节点的左孩子
            int i = low, j = 2 * i + 1;
            // temp记录根节点的值
            T temp = arr[i];

            while (j <= high)
            {
                // 如果左孩子小于右孩子，则将要交换的孩子节点指向右孩子
                if (j < high && arr[j].CompareTo(arr[j + 1]) < 0)
                {
                    j++;
                }
                // 如果根节点小于它的孩子节点
                if (temp.CompareTo(arr[j]) < 0)
                {
                    arr[i] = arr[j]; // 交换根节点与其孩子节点
                    i = j;           // 以交换后的孩子节点作为根节点继续调整其子树
                    j = 2 * i + 1;   // j指向交换后的孩子节点的左孩子
                }
                else
                {
                    // 调整完毕，可以直接退出
                    break;
                }
            }
            // 使最初被调整的节点存入正确的位置
            arr[i] = temp;
        }
        #endregion

        #region 归并类排序-二路归并排序
        public static void MergeSort(T[] arr)
        {
            MergeSort(arr, 0, arr.Length - 1);
        }

        /// <summary>
        /// 首先归并左边子序列，其次归并右边子序列，最后归并当前序列
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        public static void MergeSort(T[] arr, int low, int high)
        {
            if (low < high)
            {
                var mid = (low + high) / 2;
                MergeSort(arr, low, mid);       // 归并左边的子序列（递归）
                MergeSort(arr, mid + 1, high);  // 归并右边的子序列（递归）
                Merge(arr, low, mid, high);     // 归并当前前序列
            }
        }

        /// <summary>
        /// 核心：将两个有序的子序列合并成一个有序序列
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="low"></param>
        /// <param name="mid"></param>
        /// <param name="high"></param>
        private static void Merge(T[] arr, int low, int mid, int high)
        {
            // result为临时空间，用于存放合并后的序列
            T[] result = new T[high - low + 1];
            int i = low, j = mid + 1, k = 0;
            // 合并两个子序列
            while (i <= mid && j <= high)
            {
                if (arr[i].CompareTo(arr[j]) < 0)
                {
                    result[k++] = arr[i++];
                }
                else
                {
                    result[k++] = arr[j++];
                }
            }
            // 将左边子序列的剩余部分复制到合并后的序列
            while (i <= mid)
            {
                result[k++] = arr[i++];
            }
            // 将右边子序列的剩余部分复制到合并后的序列
            while (j <= high)
            {
                result[k++] = arr[j++];
            }
            // 将合并后的序列覆盖合并前的序列
            for (k = 0, i = low; i <= high; k++, i++)
            {
                arr[i] = result[k];
            }
        }

        #endregion

        #region 插入类排序-桶排序
        public static IList<int> BucketSort(IList<int> list)
        {
            /*
             * 其实这也算是，插入排序算法。
             * 只不过声明这种非常大的容器是很消耗内存的。并不是很推荐这种方法。
             */

            var max = list[0];
            var min = list[0];
            // 找集合中，最小值与最大值
            for (var i = 0; i < list.Count; i++)
            {
                if (((IComparable)list[i]).CompareTo(max) > 0)
                {
                    max = list[i];
                }

                if (((IComparable)list[i]).CompareTo(min) < 0)
                {
                    min = list[i];
                }
            }

            // 定义一个足够大的容器。因为是最大值-最小值。所以肯定是足够装下所有集合。
            // 注意事项：数组数量溢出
            var holder = new ArrayList[max - min + 1];

            // 让数组变成二维数组
            for (var i = 0; i < holder.Length; i++)
            {
                holder[i] = new ArrayList();
            }

            // 把集合的数据，付给二维数组
            for (var i = 0; i < list.Count; i++)
            {
                holder[list[i] - min].Add(list[i]);
            }

            var k = 0;
            // 循环容器
            for (var i = 0; i < holder.Length; i++)
            {
                // 判断是否有值
                if (holder[i].Count > 0)
                {
                    // 重新给list进行赋值操作
                    for (var j = 0; j < holder[i].Count; j++)
                    {
                        list[k] = (int)holder[i][j];
                        k++;
                    }
                }
            }

            return list;
        }
        #endregion

        #region 圈排序
        public static IList<int> CycleSort(IList<int> list)
        {
            /*
             * 圈排序是一种不稳定的排序算法，是一种理论上最优的比较算法。
             * 他的思想是要把数列分解为圈，可以分别旋转得到排序结果。
             * 与其他排序不同的是，元素不会被放入数组的任何位置，
             * 如果这个值在正确位置，则不动。否则只会写一次即可。
             * 
             * 首先进行从前往后的循环。获取不同位置的数据，当获取到数据以后，
             * 会循环整个数组找到其相应的位置。然后进行位置插入。
             * 
             * 看一下，具体的性能。对于非常杂乱无章的序列来讲，真的好慢。
             */

            // 循环每一个数组
            for (var cycleStart = 0; cycleStart < list.Count; cycleStart++)
            {
                var item = list[cycleStart];
                var pos = cycleStart;
                do
                {
                    var to = 0;
                    // 循环整个数组，找到其相应的位置
                    for (var i = 0; i < list.Count; i++)
                    {
                        if (i != cycleStart && ((IComparable) list[i]).CompareTo(item) < 0)
                        {
                            to++;
                        }
                    }
                    if (pos != to)
                    {
                        while (pos != to && ((IComparable) item).CompareTo(list[to]) == 0)
                        {
                            to++;
                        }
                        var temp = list[to];
                        list[to] = item;
                        item = temp;
                        pos = to;
                    }
                } while (cycleStart != pos);
            }
            return list;
        }
        #endregion

        #region 鸽巢排序，基数分类（大数据量中最快的排序方法）
        public static IList<int> PigeonHoleSort(IList<int> list)
        {
            /*
             * 鸽巢排序假设有个待排序的数组，
             * 给它建立一个空的辅助数组（俗称鸽巢）。
             * 把原始数组的每个值作为格子（鸽巢的索引），
             * 遍历原始数据，根据每个值放入辅助数组对应的格子中。
             * 
             * 顺序遍历鸽巢数组，把非空的鸽巢中的元素放回原始数组。
             * 这种排序方式适合在差值很小的范围内使用。
             */

            int min = list[0], max = list[0];
            foreach (var x in list)
            {
                if (((IComparable)min).CompareTo(x) > 0)
                {
                    min = x;
                }
                if (((IComparable)max).CompareTo(x) < 0)
                {
                    max = x;
                }
            }

            var size = max - min + 1;
            var holes = new int[size];
            foreach (var x in list)
            {
                holes[x - min]++;
            }

            var i = 0;
            for (var count = 0; count < size; count++)
            {
                while (holes[count]-- > 0)
                {
                    list[i] = count + (int)min;
                    i++;
                }
            }

            return list;
        }
        #endregion


    }
}
