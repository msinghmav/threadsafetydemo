using System.Threading;

namespace ThreadsafeFileAccess
{
    internal sealed class DemoDriver
    {
        public DemoDriver() { }

        public void Run()
        {
            var outputPath = "log/out.txt";

            FileWriter fileWriter = new (outputPath);
            var result = fileWriter.Initialize();

            if (!result.IsSuccess)
            {
                Console.WriteLine($"{result.FailureDetail}");
                return;
            }

            // Create and start 10 tasks
            var tasks = Enumerable.Range(0, 10)
                .Select(i => Task.Run(() => fileWriter.WriteToFile(Thread.CurrentThread.ManagedThreadId)))
                .ToArray();

            // Wait for all tasks to complete
            Task.WhenAll(tasks).Wait();

            if(tasks.Any(task => !task.Result.IsSuccess))
            {
                foreach (var task in tasks.Where(task => !task.Result.IsSuccess))
                {
                    Console.WriteLine(task.Result);
                }

                Console.WriteLine("Failed file write operation, press any key to close");
            }
            else
            {
                Console.WriteLine("Completed file write operations for all threads, press any key to close");
            }

            
            Console.ReadLine();
        }
    }
}
