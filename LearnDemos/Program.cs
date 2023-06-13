// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

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
SearchNum(nums, 7);