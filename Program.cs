using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganizer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var photoDirectory = args[0];
            var destinationDirectory = args[1];
            if (!Directory.Exists(photoDirectory))
            {
                throw new DirectoryNotFoundException(photoDirectory);
            }
            Console.WriteLine(String.Format("Found the photo directory {0}", photoDirectory));

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
                Console.WriteLine(String.Format("Creating the destination directory {0}", destinationDirectory));
            }
            Console.WriteLine(String.Format("Using the destination directory {0}", destinationDirectory));

            var files = Directory.GetFiles(photoDirectory, "*", SearchOption.AllDirectories);
            Console.WriteLine(String.Format("Found {0} files to move", files.Length));

            foreach (var file in files)
            {
                Console.WriteLine(String.Format("Working {0}", file));
                var fileDate = File.GetLastWriteTime(file);
                var yearPath = Path.Combine(destinationDirectory, fileDate.Year.ToString());
                if (!Directory.Exists(yearPath))
                {
                    Directory.CreateDirectory(yearPath);
                    Console.WriteLine(String.Format("Created {0}", yearPath));
                }

                var monthPath = Path.Combine(yearPath, fileDate.ToString("MMMM"));
                if (!Directory.Exists(monthPath))
                {
                    Directory.CreateDirectory(monthPath);
                    Console.WriteLine(String.Format("Created {0}", monthPath));
                }

                var filename = Path.GetFileName(file);
                Console.WriteLine(String.Format("Moving {0}, to {1}", filename, monthPath));
                File.Copy(file, Path.Combine(monthPath, filename));
            }

            Console.ReadLine();
        }
    }
}
