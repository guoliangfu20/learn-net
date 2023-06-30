using LinkedListTest.SingleLinkedListTest;
using System;

SingleLinkedList<int> singleLinkedList = new SingleLinkedList<int>();
SingleLinkedListNode<int> singleLinkedListNode = new SingleLinkedListNode<int>();
singleLinkedListNode.Value = 6;

//singleLinkedList.AddFirst(1011);
//singleLinkedList.AddFirst(1012);
//singleLinkedList.AddFirst(1013);
//singleLinkedList.AddFirst(singleLinkedListNode);

//Console.WriteLine($"SingleLinkedList[0]: {singleLinkedList[0]}");  // 6
//Console.WriteLine($"SingleLinkedList[1]: {singleLinkedList[1]}");  // 1013
//Console.WriteLine($"SingleLinkedList[2]: {singleLinkedList[2]}");  // 1012
//Console.WriteLine($"SingleLinkedList[3]: {singleLinkedList[3]}");  // 1011


singleLinkedList.AddLast(1011);
singleLinkedList.AddLast(1012);
singleLinkedList.AddLast(1013);
singleLinkedList.AddLast(singleLinkedListNode);

//Console.WriteLine($"SingleLinkedList[0]: {singleLinkedList[0]}");  // 1011
//Console.WriteLine($"SingleLinkedList[1]: {singleLinkedList[1]}");  // 1012
//Console.WriteLine($"SingleLinkedList[2]: {singleLinkedList[2]}");  // 1013
//Console.WriteLine($"SingleLinkedList[3]: {singleLinkedList[3]}");  // 6


SingleLinkedListNode<int> singleLinkedListNode1 = new SingleLinkedListNode<int>();
singleLinkedListNode1.Value = 998;
//singleLinkedList.Insert(singleLinkedListNode1, 3);
singleLinkedList.Insert(998, 3);
Console.WriteLine($"SingleLinkedList[0]: {singleLinkedList[0]}");  // 1011
Console.WriteLine($"SingleLinkedList[1]: {singleLinkedList[1]}");  // 1012
Console.WriteLine($"SingleLinkedList[2]: {singleLinkedList[2]}");  // 1013
Console.WriteLine($"SingleLinkedList[3]: {singleLinkedList[3]}");  // 998
Console.WriteLine($"SingleLinkedList[4]: {singleLinkedList[4]}");  // 6


singleLinkedList.Delete(998);

Console.WriteLine("after delete...");

Console.WriteLine($"SingleLinkedList[0]: {singleLinkedList[0]}");  // 1011
Console.WriteLine($"SingleLinkedList[1]: {singleLinkedList[1]}");  // 1012
Console.WriteLine($"SingleLinkedList[2]: {singleLinkedList[2]}");  // 1013
Console.WriteLine($"SingleLinkedList[3]: {singleLinkedList[3]}");  // 6

Console.WriteLine("链表数量: " + singleLinkedList.Count);
Console.WriteLine("链表是否为空: " + singleLinkedList.IsEmpty);
singleLinkedList.ToString();
Console.ReadKey();
