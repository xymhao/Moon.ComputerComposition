using System;
using System.Diagnostics;

namespace Moon.ComputerComposition2
{
    public class Cache
    {
        public static void main()
        {
            /*
             * 在这一讲一开始的程序里，运行程序的时间主要花在了将对应的数据从内存中读取出来，加载到 CPU Cache 里。CPU 从内存中读取数据到 CPU Cache 的过程中，是一小块一小块来读取数据的，而不是按照单个数组元素来读取数据的。
             * 这样一小块一小块的数据，在 CPU Cache 里面，我们把它叫作 Cache Line（缓存块）。在我们日常使用的 Intel 服务器或者 PC 里，Cache Line 的大小通常是 64 字节。
             * 而在上面的循环 2 里面，我们每隔 16 个整型数计算一次，16 个整型数正好是 64 个字节。于是，循环 1 和循环 2，需要把同样数量的 Cache Line 数据从内存中读取到 CPU Cache 中，最终两个程序花费的时间就差别不大了。
             */
            var arr = new int[64 * 1024 * 1024];
            var arr2 = new int[64 * 1024 * 1024];

            var watch1 = new Stopwatch();
            var watch2 = new Stopwatch();
            watch1.Start();
            // 循环1 92ms
            for (var i = 0; i < arr.Length; i++) arr[i] *= 3;
            watch1.Stop();
            Console.WriteLine(watch1.ElapsedMilliseconds);

            watch2.Start();
            // 循环2 62ms
            for (var i = 0; i < arr2.Length; i += 8) arr2[i] *= 3;
            watch2.Stop();

            Console.WriteLine(watch2.ElapsedMilliseconds);
        }
    }
}