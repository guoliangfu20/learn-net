// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using LearnDemos;

int[] nums = { 1, 2, 2, 3, 5, 5, 6, 7, 7, 7, 8 };

/*
Dictionary<int, int> dics = new Dictionary<int, int>();
foreach (int num in nums)
{
    //Dictionary<int, int> tmp = new Dictionary<int, int>();

    if (dics.ContainsKey(num))
    {
        int tmp = dics[num] + 1;
        dics.Remove(num);
        dics.Add(num, tmp);
    }
    else
    {
        dics.Add(num, 1);
    }
}

foreach (int n in dics.Keys)
{
    Console.WriteLine($"{n}-- {dics[n]}");
}

*/


static void SearchNum(int[] arrs, int target)
{
    if (arrs == null || arrs.Length == 0)
    {
        return;
    }

    int times = 0;
    foreach (var item in arrs)
    {
        if (item == target)
        {
            times++;
        }
    }
    Console.WriteLine($"数字 {target}, 出现了 {times} 次");
}
//SearchNum(nums, 7);

#region record test

var r1 = new RecordModel(1, "张三");
var r2 = new RecordModel(2, "李四");
var r3 = new RecordModel(2, "李四");
var r4 = new RecordModel(3, "王五");

Console.WriteLine($"r2.Equel(r3), 结果： {r2.Equals(r3)}");  // True
Console.WriteLine($"r2==r3, 结果： {r2 == r3}");   // True
Console.WriteLine($"r2！=r3, 结果： {r2 != r3}");   // False
Console.WriteLine($"r1==r3, 结果： {r1 == r3}");   // False

RecordModel[] rs = (new RecordModel[] { r1, r2, r3, r4 }).Distinct().ToArray();
Console.WriteLine($"{string.Join(",", rs.Select(s => $"[{s.Id},{s.UserName}]"))}");  // 去重效果


#endregion
