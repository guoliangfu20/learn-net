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
    throw new NotImplementedException();
}