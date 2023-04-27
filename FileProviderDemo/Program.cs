using Microsoft.Extensions.FileProviders;
using System;

namespace FileProviderDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider1 = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);

            // 物理文件
            //var contents1 = provider1.GetDirectoryContents("/");
            //foreach (var file in contents1)
            //{
            //    Console.WriteLine($"name: {file.Name} , length: {file.Length} , IsDirectory: {file.IsDirectory}");
            //}

            // 嵌入式
            var provider2 = new EmbeddedFileProvider(typeof(Program).Assembly);

            var file = provider2.GetFileInfo("emb.html");


            // 组合文件
            IFileProvider provider = new CompositeFileProvider(provider1, provider2);

            var contents = provider.GetDirectoryContents("/");
            foreach (var item in contents)
            {
                Console.WriteLine(item.Name);
            }

        }
    }
}
