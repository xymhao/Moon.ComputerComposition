using System;
using System.Threading;

namespace Moon.ComputerComposition2
{
    public class Worker
    {
        // run release
        // Keyword volatile is used as a hint to the compiler that this data
        // member is accessed by multiple threads.
        private bool _shouldStop;

        // This method is called when the thread is started.
        public void DoWork()
        {
            var work = false;
            while (!_shouldStop) work = !work; // simulate some work
            Console.WriteLine("Worker thread: terminating gracefully.");
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }
    }

    public class WorkerThreadExample
    {
        public static void Main2()
        {
            // Create the worker thread object. This does not start the thread.
            var workerObject = new Worker();
            var workerThread = new Thread(workerObject.DoWork);

            // Start the worker thread.
            workerThread.Start();
            Console.WriteLine("Main thread: starting worker thread...");

            // Loop until the worker thread activates.
            while (!workerThread.IsAlive)

                // Put the main thread to sleep for 500 milliseconds to
                // allow the worker thread to do some work.
                Thread.Sleep(500);

            // Request that the worker thread stop itself.
            workerObject.RequestStop();

            // Use the Thread.Join method to block the current thread
            // until the object's thread terminates.
            workerThread.Join();
            Console.WriteLine("Main thread: worker thread has terminated.");
            Console.ReadLine();
        }

        // Sample output:
        // Main thread: starting worker thread...
        // Worker thread: terminating gracefully.
        // Main thread: worker thread has terminated.
    }
}