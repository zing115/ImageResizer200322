/*
花費時間: 3645 ms
花費時間: 2808 ms
*/
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            #region 等候使用者輸入 取消 c 按鍵
            ThreadPool.QueueUserWorkItem(x =>
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.C)
                {
                    cts.Cancel();
                }
            });
            #endregion

            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            //imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            try
            {
                await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0, cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"{Environment.NewLine}非同步執行已經取消");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Environment.NewLine}發現例外異常 {ex.Message}");
            }
            sw.Stop();

            Console.WriteLine($"花費時間: {sw.ElapsedMilliseconds} ms");
        }
    }
}
