using System.Drawing;

namespace RefreshedAscii;
internal static class FileManager
{
    /// <summary>
    /// Save the file in the Documents folder as jpg
    /// </summary>
    /// <param name="asciiArt">The asciiArt string</param>
    /// <param name="fileName">The fileName to concat with a string constant to create the ascii art file name</param>
    internal static void SaveAsciiArt(string asciiArt, string fileName)
    {
        var font = new Font("Consolas", 12);
        var lines = asciiArt.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        var width = 0;
        var height = lines.Length * (int)font.GetHeight(); 

        using (var bitmap = new Bitmap(1, 1))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                foreach (var line in lines)
                {
                    var lineWidth = (int)graphics.MeasureString(line, font).Width;

                    if (lineWidth > width)
                        width = lineWidth;
                }
            }
        }

        using (var bitmap = new Bitmap(width, height))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Black);
                var brush = new SolidBrush(Color.White);

                for (int i = 0; i < lines.Length; i++)
                    graphics.DrawString(lines[i], font, brush, new PointF(0, i * font.GetHeight()));
            }

            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{fileName}ascii_art.jpg");
            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            Console.WriteLine($"\u001b[32m ASCII Art successfully saved as {filePath}\n Press a key to continue... \u001b[0m");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Let the user choose the directory and load a jpg/jpeg/png file
    /// </summary>
    internal static void SelectJpgFile()
    {
        var exit = false;
        var asciiArt = string.Empty;
        var fileName = string.Empty;

        do
        {
            Console.WriteLine("\u001b[33m Please, insert the path to the file (accepted formats: jpg/jpeg/png): \u001b[0m");
            var filePath = Console.ReadLine().Replace("\"", "");
            var validExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

            if (File.Exists(filePath) && validExtensions.Contains(Path.GetExtension(filePath), StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("\u001b[32m File correctly selected!\n \u001b[0m");
                asciiArt = AsciiArtGenerator.ConvertImageToAscii(filePath);
                fileName = Path.GetFileNameWithoutExtension(filePath);
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("\u001b[31m File not found or invalid!\n \u001b[0m");
                Thread.Sleep(1000);
            }
        } while (exit);

        SaveAsciiArt(asciiArt, fileName);
    }
}
