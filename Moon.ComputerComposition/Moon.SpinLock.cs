using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace Moon.ComputerComposition2
{
    public class Moon_SpinLock
    {
        // Demonstrates:
        //      Default SpinLock construction ()
        //      SpinLock.Enter(ref bool)
        //      SpinLock.Exit()
        static void Main()
        {
            ISyncPolicy policy = Policy.Handle<Exception>(exception => exception.Message == "").Retry(1000);

            
            SpinLock sl = new SpinLock();

            StringBuilder sb = new StringBuilder();

            // Action taken by each parallel job.
            // Append to the StringBuilder 10000 times, protecting
            // access to sb with a SpinLock.
            Action action = () =>
            {
                bool gotLock = false;
                for (int i = 0; i < 10000; i++)
                {
                    gotLock = false;
                    try
                    {
                        sl.Enter(ref gotLock);
                        sb.Append((i % 10).ToString());
                    }
                    finally
                    {
                        // Only give up the lock if you actually acquired it
                        if (gotLock) sl.Exit();
                    }
                }
            };

            // Invoke 3 concurrent instances of the action above
            Parallel.Invoke(action, action, action);

            // Check/Show the results
            Console.WriteLine("sb.Length = {0} (should be 30000)", sb.Length);
            Console.WriteLine("number of occurrences of '5' in sb: {0} (should be 3000)",
                sb.ToString().Where(c => (c == '5')).Count());

            Console.ReadLine();
        }
    }
}