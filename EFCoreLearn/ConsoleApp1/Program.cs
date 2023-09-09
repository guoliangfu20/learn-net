// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


/*
 	var num1 = new int[]{1,2,3};
	var num2 = new int[]{8,9};
	var result = Sum(num1, num2);		
	Console.WriteLine(string.Join(",", result)); // 2,1,2

    入参
	[1, 2 , 3] = 123
	[8, 9 ]    = 89
	
	逻辑
	123 + 89 = 212
	
	出参
	[2, 1, 2]
 
 */
using System.Collections.Generic;
using System.Text;

static int[] Sum(int[] num1, int[] num2)
{
    string str1 = "";
    num1.ToList().ForEach(a => { str1 += a.ToString(); });

    string str2 = "";
    num2.ToList().ForEach(a => { str2 += a.ToString(); });

    int sum = Convert.ToInt32(str1) + Convert.ToInt32(str2);
    string str3 = sum.ToString();

    int[] result = new int[str3.Length];
    for (int i = 0; i < str3.Length; i++)
    {
        result[i] = Convert.ToInt32(str3[i].ToString());
    }
    return result;
}

var r = Sum(new int[] { 1, 2, 3 }, new int[] { 8, 9 });

var d = Sum2(new int[] { 5, 2, 929 }, new int[] { 9, 18, 19 });
static int[] Sum2(int[] num1, int[] num2)
{
    string str1 = string.Concat(num1);
    string str2 = string.Concat(num2);

    int maxLength = Math.Max(str1.Length, str2.Length);

    str1 = str1.PadLeft(maxLength, '0');
    str2 = str2.PadLeft(maxLength, '0');

    int carry = 0;
    string result = "";
    for (int i = maxLength - 1; i >= 0; i--)
    {
        int d1 = int.Parse(str1[i].ToString());
        int d2 = int.Parse(str2[i].ToString());

        int sum = d1 + d2 + carry;
        int digitSum = sum % 10; // 求和的个位数
        carry = sum / 10;  // 往上进1
        result = digitSum.ToString() + result;
    }
    if (carry > 0)
    {
        result = result + carry.ToString();
    }
    return result.Select(s => int.Parse(s.ToString())).ToArray();
}


List<int> student = new List<int> { };


student.AddRange(Enumerable.Range(1, 6));

//int count = student.Count;

//Random rnd = new Random();
//rnd.Next(1, count);

/**/
Console.WriteLine(string.Join(",", student));

Console.WriteLine("------------");
List<int> result = new();

int countIndex = student.Count % 2 == 1 ? student.Count - 1 : student.Count - 1;

for (int i = 0; i < countIndex; i++)
{
    if (result.Contains(student[i])) continue;

    int d1 = student[i];
    int d2 = student[countIndex - i];

    result.Add(student[i]);
    if (i != countIndex - i)
    {
        result.Add(student[countIndex - i]);
    }
}

Console.WriteLine(string.Join(",", result));


/*
Random random = new Random();
List<int> result = new List<int>();
for (int i = 0; i < 3; i++)
{
    result.Add(GetOne(result, student, random, i));
}

Console.WriteLine(string.Join(",", result));

int GetOne(List<int> result, List<int> list, Random random, int i)
{
    int _index = random.Next(0, list.Count);

    int o = list[_index];
    if (result.Contains(o)) o = GetOne(result, list, random, i);
    return o;
}
*/



Console.ReadKey();