namespace ColorCount
{
    /// <summary>
    /// 背景颜色
    /// </summary>
    enum BackgroundColor
    {
        Red,
        Yellow,
        Blue,
        Purple
    }

    /// <summary>
    /// 卷尺的记录的单条数据
    /// </summary>
    /// <param name="begin">染色初始位置对应的卷尺刻度（单位：cm）</param>
    /// <param name="color">染的背景色</param>
    /// <param name="length">染色的长度</param>
    record TapeColor(int begin, BackgroundColor color, int length);


    /// <summary>
    /// 每 1 米计算的结果
    /// </summary>
    /// <param name="begin">计算范围内起始的刻度</param>
    /// <param name="end">计算范围内起始的刻度+100cm</param>
    /// <param name="items">在 100cm内每个颜色的长度数据</param>
    record DataPerOneMeter(int begin, int end, List<DataPerOneMeterItem> items);


    /// <summary>
    /// 每 1 米计算结果中的子项
    /// </summary>
    /// <param name="color">颜色</param>
    /// <param name="length">染色长度（单位: cm）</param>
    record DataPerOneMeterItem(BackgroundColor color, int length);
}
