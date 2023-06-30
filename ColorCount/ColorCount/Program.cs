/*
  题目介绍

有一段5米的惨尺，现在需要给其染上背景色。现在有红、黄、蓝、紫四种染料，小明会随机使用一种染料，给绳子染一段不固定长度的颜色，然后从上一次染色结束的地方接着用另一种不同的颜色的染料进行染色，染色长度也不固定。小明按照这样的方式进行染色，直至5米全部染色完成。染色期间， 小明记录了每次染色开始时的刻度和该颜色染了多少厘米，如下表所示:

Begin(染色初始位对应的卷尺刻度)     Color(颜色)       Length(染色的长度，单位 cm)
0                                  Red              64
64                                 Blue             29
93                                 Red              57
150                                Yellow           143
293                                Purple           129
422                                Red              18
440                                Purple           4
444                                Yellow           56


实现需求：

现在要求实现一个方法，求某一段卷尺的每100厘米的刻度之间， 每个颜色染了多少厘米。
比如，200厘米到400厘米之间，每段100厘米中每个颜色各染了多少厘米。

 */

using ColorCount;
using System.Text.Json;

var argsLength = args.Length;

// 记录的数据文件
string filePath;

// 要计算的长度的起始位置
// 输入值可能为 0, 100, 200, 300, 400
// 单位: 厘米
int begin;

// 要计算的长度的结束位置
// 输入值可能为 100, 200, 300, 400, 500
// 单位：厘米
// 且一定大于 begin
int end;

// 计算结果保存的文件
string resultFilePath;

if (argsLength > 0)
{
    filePath = args[0];
    begin = int.Parse(args[1]);
    end = int.Parse(args[2]);
    resultFilePath = args[3];
}
else
{
    filePath = "Data.csv";
    begin = 200;
    end = 400;
    resultFilePath = "result.json";
}

// 开始计算
static IEnumerable<DataPerOneMeter> Calc(List<TapeColor> data, int begin, int end)
{
    // TODO 实现 {begin}厘米到 {end}厘米之间，每隔100厘米中每个颜色染了多少厘米
    //throw new NotImplementedException();

    List<DataPerOneMeter> rel = new List<DataPerOneMeter>();

    // 找出每段的区域
    // 每隔100厘米为一个区域
    int dataPerlenth = (end - begin) / 100;

    // 计算每个区域
    for (int i = 0; i < dataPerlenth; i++)
    {
        int tempBegin = begin * (i + 1);
        int tempEed = tempBegin + 100;

        //找出查询区域的数据
        var TapeLength = data.Where(w => w.Begin >= tempBegin && w.Begin < tempEed).ToList();
        //计算最后一个
        var lastdataint = data.LastOrDefault().Begin + data.LastOrDefault().Length;

        //计算超出部分的
        if (tempBegin > lastdataint) break;
        if (TapeLength.Count == 0)
        {
            var TapeLength2 = data.Where(w => w.Begin <= tempBegin).ToList().LastOrDefault();
            List<DataPerOneMeterItem> listMeterItem2 = new List<DataPerOneMeterItem>();
            var perLength = 0;
            if (lastdataint > tempEed)
                perLength = 100;
            else
                perLength = lastdataint - tempBegin;
            var DatsPerOne = new DataPerOneMeterItem(TapeLength2.Color, perLength); listMeterItem2.Add(DatsPerOne);

            var dataPer2 = new DataPerOneMeter(tempBegin, tempEed, listMeterItem2); rel.Add(dataPer2);
        }
        else
        {
            //找出前段超出部分
            if (TapeLength[0].Begin > tempBegin)
            {
                var tempTape = data.Where(w => w.Begin < tempBegin).LastOrDefault(); TapeLength.Add(tempTape);
            }
            TapeLength = TapeLength.OrderBy(o => o.Begin).ToList();

            List<DataPerOneMeterItem> listMeterItem = new List<DataPerOneMeterItem>(); foreach (var item in TapeLength)
            {
                //计算第一个区间的颜色
                if (item.Begin < tempBegin)
                {
                    int firstColorLength = item.Begin + item.Length - tempBegin;
                    DataPerOneMeterItem meterItem = new DataPerOneMeterItem(item.Color, firstColorLength);
                    listMeterItem.Add(meterItem);
                }
                //计算最后一个区间的颇色
                else if (item.Begin + item.Length > tempEed)
                {
                    int endColorLength = tempEed - item.Begin;
                    DataPerOneMeterItem meterItem = new DataPerOneMeterItem(item.Color, endColorLength);
                    listMeterItem.Add(meterItem);
                }
                // 中间的颇色 
                else
                {
                    DataPerOneMeterItem meterltem = new DataPerOneMeterItem(item.Color, item.Length);
                    listMeterItem.Add(meterltem);
                }
            }
            DataPerOneMeter dataPer = new DataPerOneMeter(tempBegin, tempEed, listMeterItem);
            rel.Add(dataPer);
        }
    }
    return rel;
}

List<TapeColor> tapes = new List<TapeColor>()
{
    new TapeColor(0,BackgroundColor.Red,64),
    new TapeColor(64 ,BackgroundColor.Blue,29),
    new TapeColor(93 ,BackgroundColor.Red,57),
    new TapeColor(150,BackgroundColor.Yellow,143),
    new TapeColor(293,BackgroundColor.Purple,129),
    new TapeColor(422,BackgroundColor.Red,18),
    new TapeColor(440,BackgroundColor.Purple,4),
    new TapeColor(444,BackgroundColor.Yellow,56)
};

var res = Calc(tapes, 100, 400);

string st = JsonSerializer.Serialize(res);
Console.WriteLine(st);