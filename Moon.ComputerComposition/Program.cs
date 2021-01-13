using System;
using System.Runtime.Serialization;
using System.Threading;

namespace Moon.ComputerComposition
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            Interlocked.Add(ref i, 1);
            
            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine("123");

            });
        }

        public void Write()
        {
            Console.WriteLine("123");
        }
    }
}