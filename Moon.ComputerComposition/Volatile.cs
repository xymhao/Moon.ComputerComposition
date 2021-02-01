using System;
using System.Threading;

namespace Moon.ComputerComposition2
{
    public class VolatileTest
    {
        //使用release执行
        //volatile 关键字它会确保我们对于这个变量的读取和写入，都一定会同步到主内存里，而不是从 Cache 里面读取。
        // private static volatile int COUNTER;
        private static int COUNTER;

        /*如果有 volatile 执行结果
         * Incrementing COUNTER to : 1
           Got Change for COUNTER : 1
           Incrementing COUNTER to : 2
           Got Change for COUNTER : 2
           Incrementing COUNTER to : 3
           Got Change for COUNTER : 3
           Incrementing COUNTER to : 4
           Got Change for COUNTER : 4
           Incrementing COUNTER to : 5
           Got Change for COUNTER : 5
         */

        /*没有volatile
         * Incrementing COUNTER to : 1
           Incrementing COUNTER to : 2
           Incrementing COUNTER to : 3
           Incrementing COUNTER to : 4
           Incrementing COUNTER to : 5
         */

        /// <summary>
        ///     1.刚刚第一个使用了 volatile 关键字的例子里，因为所有数据的读和写都来自主内存。那么自然地，我们的 ChangeMaker 和 ChangeListener 之间，看到的 COUNTER 值就是一样的。
        ///     2.到了第二段进行小小修改的时候，我们去掉了 volatile 关键字。这个时候，ChangeListener 又是一个忙等待的循环，它尝试不停地获取 COUNTER 的值，这样就会从当前线程的“Cache”里面获取。
        ///     于是，这个线程就没有时间从主内存里面同步更新后的 COUNTER 值。这样，它就一直卡死在 COUNTER=0 的死循环上了。
        /// </summary>
        public static void Main()
        {
            var changeListen = new Thread(o =>
            {
                int threadValue = COUNTER;
                while (threadValue < 5)
                    if (threadValue != COUNTER)
                    {
                        Console.WriteLine("Got Change for COUNTER : " + COUNTER + "");
                        threadValue = COUNTER;
                    }
            });

            var changeMaker = new Thread(o =>
            {
                int threadValue = COUNTER;
                while (COUNTER < 5)
                {
                    Console.WriteLine("Incrementing COUNTER to : " + (threadValue + 1) + "");
                    COUNTER = ++threadValue;
                    try
                    {
                        Thread.Sleep(500);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            });
            changeListen.Start();
            changeMaker.Start();

            Console.ReadLine();
        }
    }
}