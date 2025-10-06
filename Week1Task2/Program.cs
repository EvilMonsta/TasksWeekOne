using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace task12
{
    internal class SyncAsyncTask
    {
        static async Task Main()
        {
            Console.WriteLine("СИНХР:");
            var swSync = Stopwatch.StartNew();
            RunSync();
            swSync.Stop();
            Console.WriteLine($"Синхр: {swSync.Elapsed.TotalSeconds:F1} c\n");

            Console.WriteLine("АСИНХР:");
            var swAsync = Stopwatch.StartNew();
            await RunAsync();
            swAsync.Stop();
            Console.WriteLine($"Асинхр: {swAsync.Elapsed.TotalSeconds:F1} c\n");
        }

        static string ProcessData(string dataName)
        {
            const int ms = 3000;
            Thread.Sleep(ms);
            return $"Обработка '{dataName}' завершена за {ms / 1000} секунды";
        }

        static void RunSync()
        {
            var names = new[] { "Файл 1", "Файл 2", "Файл 3" };
            foreach (var name in names)
            {
                var result = ProcessData(name);
                Console.WriteLine(result);
            }
        }

        static async Task<string> ProcessDataAsync(string dataName)
        {
            const int ms = 3000;
            await Task.Delay(ms);
            return $"Обработка '{dataName}' завершена за {ms / 1000} секунды";
        }

        static async Task RunAsync()
        {
            var names = new[] { "Файл 1", "Файл 2", "Файл 3" };

            var tasks = names.Select(n => ProcessDataAsync(n)).ToList();

            while (tasks.Count > 0)
            {
                var finished = await Task.WhenAny(tasks);
                tasks.Remove(finished);
                Console.WriteLine(await finished);
            }
        }
    }

}
