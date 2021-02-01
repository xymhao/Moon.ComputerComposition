using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moon.ComputerComposition2
{
    internal class Program
    {
        private static int i;
        private static bool getLock;

        public static void Main2()
        {
            var list = new List<int> {123};
            var spinLock = new SpinLock();
            for (var j = 0; j < 10; j++)
            {
                int j1 = j;

                Task Function()
                {
                    while (true)
                        try
                        {
                            Console.WriteLine($"线程{j1}： i= {i}");
                            spinLock.Enter(ref getLock);
                            Console.WriteLine($"线程{j1}： getLock= {getLock}");
                        }
                        finally
                        {
                            if (getLock) spinLock.Exit();
                        }
                }

                Task.Run(Function);
            }

            while (true)
            {
                int result = Interlocked.Add(ref i, i++);
                Thread.Sleep(2000);
            }

            // Console.WriteLine(i);
            // Console.WriteLine(result);
            // //Interlocked.CompareExchange();
            // //ReaderWriterLock
            //
            // ThreadPool.QueueUserWorkItem(state =>
            // {
            //     Console.WriteLine("123");
            //
            // });

            Console.ReadLine();
        }
    }
}