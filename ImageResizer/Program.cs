/*
花費時間: 3645 ms
花費時間: 2808 ms
*/
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            //imageProcess.Clean(destinationPath);
            await imageProcess.CleanAsync(destinationPath);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            //imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
            sw.Stop();

            Console.WriteLine($"花費時間: {sw.ElapsedMilliseconds} ms");
        }
    }
}
