using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListTest.SingleLinkedListTest
{
    internal class SingleLinkedList<T> : ISingleLinkedListFunc<T>
    {
        private SingleLinkedListNode<T> first = null;
        public SingleLinkedListNode<T> First
        {
            get { return first; }
        }

        private SingleLinkedListNode<T> last = null;
        public SingleLinkedListNode<T> Last
        {
            get { return last; }
        }

        private int count;
        public int Count
        {
            get
            {
                count = GetCount();
                return count;
            }
        }

        private bool isEmpty;
        public bool IsEmpty
        {
            get
            {
                isEmpty = GetIsEmpty();
                return isEmpty;
            }
        }


        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    return default;
                }
                SingleLinkedListNode<T> tempNode = first;
                for (int i = 0; i < index; i++)
                {
                    tempNode = tempNode.Next;
                }
                return tempNode.Value;
            }
        }

        public SingleLinkedListNode<T> AddFirst(T value)
        {
            SingleLinkedListNode<T> tempNode = new SingleLinkedListNode<T>();
            tempNode.Value = value;
            if (first == null)
            {
                first = tempNode;
                last = tempNode;
            }
            else
            {
                // 跟旧的first互换
                tempNode.Next = first;
                first = tempNode;
            }
            return tempNode;
        }

        /// <summary>
        /// 添加节点到首节点
        /// (此时储存的并不是传递过来的数据地址,而是新建节点,赋值传过来节点的数据)
        /// </summary>
        /// <param name="node">数据节点</param>
        /// <returns>真正添加的节点</returns>
        public SingleLinkedListNode<T> AddFirst(SingleLinkedListNode<T> node)
        {
            SingleLinkedListNode<T> tempNode = CopyToFrom(node);
            //tempNode.Next = null;//  因为是添加首节点，此处需要清空它的next
            //tempNode.Next = first;
            if (first == null)
            {
                first = tempNode;
                last = tempNode;
            }
            else
            {
                tempNode.Next = first;
                first = tempNode;
            }
            return tempNode;
        }

        /// <summary>
        /// 添加数据到末尾节点
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SingleLinkedListNode<T> AddLast(T value)
        {
            SingleLinkedListNode<T> tempNode = new SingleLinkedListNode<T>();
            tempNode.Value = value;
            if (first == null)
            {
                first = tempNode;
                last = tempNode;
            }
            else
            {
                last.Next = tempNode;
                last = tempNode;
            }
            return tempNode;
        }

        public SingleLinkedListNode<T> AddLast(SingleLinkedListNode<T> node)
        {
            SingleLinkedListNode<T> tempNode = CopyToFrom(node);
            tempNode.Next = null;
            if (first == null)
            {
                first = tempNode;
                last = tempNode;
            }
            else
            {
                last.Next = tempNode;
                last = tempNode;
            }
            return tempNode;
        }

        public void Clear()
        {
            first = null;
            last = null;
        }

        public bool Contains(T value)
        {
            if (first == null)
            {
                return false;
            }
            SingleLinkedListNode<T> tempNode = first;
            if (tempNode.Value.Equals(value))
            {
                return true;
            }
            while (true)  // 循环链表
            {
                if (tempNode.Next != null)
                {
                    if (tempNode.Value.Equals(value))
                    {
                        return true;
                    }
                    tempNode = tempNode.Next;
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        public bool Delete(T value)
        {
            if (!Contains(value))
            {
                return false;
            }
            SingleLinkedListNode<T> tempNode = first;
            if (tempNode.Value.Equals(value))
            {
                first = first.Next;
                return true;
            }

            // 需要寻找出前一个
            SingleLinkedListNode<T> preNode = null;
            while (true)
            {
                if (tempNode.Next != null)
                {
                    preNode = tempNode;
                    tempNode = tempNode.Next;
                    if (tempNode.Value.Equals(value))
                    {
                        preNode.Next = tempNode.Next;
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        public bool Delete(SingleLinkedListNode<T> node)
        {
            if (first == null)
            {
                return false;
            }
            else
            {
                SingleLinkedListNode<T> tempNode = first;
                if (tempNode.Equals(node))
                {
                    first = first.Next;
                    //first = tempNode.Next;
                    return true;
                }
                SingleLinkedListNode<T> tempPre = null;
                while (true)
                {
                    if (tempNode.Next != null)
                    {
                        tempPre = tempNode;
                        tempNode = tempNode.Next;
                        if (tempNode.Equals(node))
                        {
                            tempPre.Next = tempNode.Next;
                            return true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                return false;
            }



        }

        public bool DeleteAt(int index)
        {
            if (index >= Count)
            {
                return false;
            }
            if (index == 0)
            {
                first = first.Next;
                return true;
            }
            else
            {
                SingleLinkedListNode<T> tempNode = first;
                for (int i = 0; i < index - 1; i++) { }
                {
                    tempNode = tempNode.Next;
                }
                tempNode.Next = tempNode.Next.Next;
            }
            return true;
        }

        public bool DeleteFirst()
        {
            if (first == null)
            {
                return false;
            }
            else
            {
                first = first.Next;
                return true;
            }
        }

        public bool DeleteLast()
        {
            if (first == null)
            {
                return false;
            }
            else
            {
                if (first == last)
                {
                    last = null;
                    first = null;
                }
                else
                {
                    SingleLinkedListNode<T> tempNode = first;
                    SingleLinkedListNode<T> tempPre = null;
                    while (true)
                    {
                        if (tempNode.Next != null)
                        {
                            tempPre = tempNode;
                            tempNode = tempPre.Next;
                        }
                        else
                        {
                            tempPre.Next = null;
                            last = tempPre;
                            return true;
                        }

                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 查找数据所属节点
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SingleLinkedListNode<T> Find(T value)
        {
            if (Contains(value))
            {
                SingleLinkedListNode<T> tempNode = first;
                if (tempNode.Value.Equals(value))
                {
                    return tempNode;
                }
                while (true)
                {
                    if (tempNode.Next != null)
                    {
                        if (tempNode.Next.Value.Equals(value))
                        {
                            return tempNode.Next;
                        }
                        tempNode = tempNode.Next;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 查找数据上一个节点
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SingleLinkedListNode<T> FindPrevious(T value)
        {
            if (first == null)
            {
                return null;
            }
            SingleLinkedListNode<T> tempNode = first;
            if (tempNode.Value.Equals(value))
            {
                return null;
            }
            SingleLinkedListNode<T> tempPre = null;
            while (true)
            {
                if (tempNode.Next != null)
                {
                    tempPre = tempNode;
                    tempNode = tempPre.Next;
                    if (tempNode.Value.Equals(value))
                    {
                        return tempPre;
                    }
                }
                else
                {
                    break;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取下标的数据域
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetElement(int index)
        {
            if (index >= Count)
            {
                return default;
            }
            else
            {
                if (index == 0)
                {
                    return first.Value;
                }
                else
                {
                    SingleLinkedListNode<T> tempNode = first;
                    for (int i = 0; i < index; i++)
                    {
                        tempNode = tempNode.Next;
                    }
                    return tempNode.Value;
                }
            }
        }

        /// <summary>
        /// 查找数据下标
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(T value)
        {
            if (Contains(value))
            {
                int tempIndex = 0;
                SingleLinkedListNode<T> tempNode = first;
                if (tempNode.Value.Equals(value))
                {
                    return tempIndex;
                }
                while (true)
                {
                    if (tempNode.Next != null)
                    {
                        if (tempNode.Value.Equals(value))
                        {
                            return tempIndex;
                        }
                        tempNode = tempNode.Next;
                        tempIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 插入数据到Index下标处
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public SingleLinkedListNode<T> Insert(T value, int index)
        {
            if (index > Count || index < 0)
            {
                return null;
            }
            if (index == 0)
            {
                return AddFirst(value);
            }
            else if (index == Count)
            {
                return AddLast(value);
            }
            else
            {
                if (first == null)
                {
                    return null;
                }
                SingleLinkedListNode<T> tempNode = first;
                for (int i = 0; i < index - 1; i++)
                {
                    tempNode = tempNode.Next;
                }
                SingleLinkedListNode<T> tempNew = new SingleLinkedListNode<T>();
                tempNew.Value = value;
                tempNew.Next = tempNode.Next;

                tempNode.Next = tempNew;

                return tempNew;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public SingleLinkedListNode<T> Insert(SingleLinkedListNode<T> node, int index)
        {
            if (index > Count || index < 0)
            {
                return null;
            }
            if (index == 0)
            {
                return AddFirst(node);
            }
            else if (index == Count)
            {
                return AddLast(node);
            }
            else
            {
                if (first == null) return null;

                SingleLinkedListNode<T> tempNode = first;
                for (int i = 0; i < index - 1; i++)
                {
                    tempNode = tempNode.Next;
                }
                SingleLinkedListNode<T> newNode = CopyToFrom(node);
                newNode.Next = tempNode.Next;
                tempNode.Next = newNode;
                return newNode;
            }
        }

        private bool GetIsEmpty()
        {
            return first == null;
        }

        private int GetCount()
        {
            if (first == null)
            {
                return 0;
            }

            SingleLinkedListNode<T> tempNode = first;
            int countNum = 1;
            while (true)
            {
                if (tempNode.Next != null)
                {
                    countNum++;
                    tempNode = tempNode.Next;
                }
                else
                {
                    break;
                }
            }
            return countNum;
        }

        private SingleLinkedListNode<T> CopyToFrom(SingleLinkedListNode<T> target)
        {
            SingleLinkedListNode<T> tempNode = new SingleLinkedListNode<T>();
            tempNode.Value = target.Value;
            tempNode.Next = target.Next;
            return tempNode;
        }
    }
}
