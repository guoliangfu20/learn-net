using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace BCTests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private List<int> Students = new List<int>();


        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
            Students.AddRange(Enumerable.Range(1, 1000));
        }

        /// <summary>
        /// 1.随机产生考生列表，考生数量在20以上。例如：L0, L1, …, Ln-1, Ln
        /// </summary>
        /// <param name="count">需要生成列表的长度</param>
        /// <returns></returns>
        [HttpGet("getList")]
        public IEnumerable<int> GetList(int count)
        {
            count = count <= 1 ? 1 : count;
            _logger.LogInformation($"请求获取随机考生列表; -- start  --; 请求参数：count:{count}");
            var list = GetRandom(Students, count)
                .ToArray();
            _logger.LogInformation($"请求获取随机考生列表; -- end  --; 返回结果:{JsonConvert.SerializeObject(list)}");
            return list;
        }

        /// <summary>
        /// 2. 将考生重新排列，顺序改为：L0 , Ln → L1 → Ln-1 → L2 → Ln-2 ……
        /// </summary>
        /// <returns></returns>
        [HttpGet("getNewList")]
        public IEnumerable<int> GetNewList()
        {
            _logger.LogInformation($"请求获取重新排列考生列表; -- start  --");

            var list = GetNew(Students)
                .ToArray();

            _logger.LogInformation($"请求获取重新排列考生列表; -- end  --; 返回结果: {JsonConvert.SerializeObject(list)}");
            return list;
        }

        /// <summary>
        /// 随机取
        /// </summary>
        /// <param name="list">目标数据源</param>
        /// <param name="count">列表总数</param>
        /// <returns></returns>
        private List<int> GetRandom(List<int> list, int count = 1)
        {
            if (count <= 0
                || list == null
                || list.Count < count)
            {
                return list;
            }

            Random random = new Random();
            List<int> result = new();
            for (int i = 0; i < count; i++)
            {
                result.Add(GetOne(result, list, random));
            }
            return result;
        }

        /// <summary>
        /// 使用递归(防止重复)获取数据.
        /// </summary>
        /// <param name="result">结果数据</param>
        /// <param name="list">目标数据源</param>
        /// <param name="random">随机值</param>
        /// <returns></returns>
        private int GetOne(List<int> result, List<int> list, Random random)
        {
            int o = list[random.Next(0, list.Count)];
            if (result.Contains(o)) o = GetOne(result, list, random);
            return o;
        }


        /// <summary>
        /// 获取新的列表
        /// </summary>
        /// <param name="list">目标数据源</param>
        /// <returns></returns>
        private List<int> GetNew(List<int> list)
        {
            if (list == null
                || list.Count == 1)
            {
                return list;
            }

            /* 
             * L0 , Ln , 
             * L1 , Ln-1 , 
             * L2 , Ln-2 , 
             * L3 , Ln-3,
             * L4 , Ln-4,
             * L5 , Ln-5,
             * L6 , Ln-6, ...
             * 
             * 6=list.Count-1
            */

            // 考虑当列表总数为奇数，则中间值会重复计算，此处做处理
            int countIndex = list.Count % 2 == 1 ? list.Count - 1 : list.Count - 1;

            List<int> result = new();
            for (int i = 0; i < countIndex; i++)
            {
                if (result.Contains(list[i])) continue;

                result.Add(list[i]);  // 从前索引
                if (i != countIndex - i)
                {
                    // 从后索引
                    result.Add(list[countIndex - i]);
                }
            }
            return result;
        }
    }
}
