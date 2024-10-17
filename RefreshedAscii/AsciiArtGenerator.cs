using System.Drawing;
using System.Text;

namespace RefreshedAscii;
internal static class AsciiArtGenerator
{
    /// <summary>
    /// Converts the image to AsciiArt applying the Atkinson dithering algorithm
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    internal static string ConvertImageToAscii(string filePath)
    {
        var image = new Bitmap(filePath);
        var asciiArt = new StringBuilder();

        var newWidth = 150;
        var aspectRatioCorrection = 0.5;
        var newHeight = (int)(image.Height / (double)image.Width * newWidth * aspectRatioCorrection);

        var resizedImage = ResizeImage(image, newWidth, newHeight);

        char[] asciiChars = { '@', '#', '8', '&', 'o', ':', '*', '=', '-', '.', ' ' };
        double[] asciiWeights = { 0.0, 0.1, 0.25, 0.35, 0.45, 0.55, 0.65, 0.75, 0.85, 0.95, 1.0 };

        char GetAsciiChar(double grayValue)
        {
            for (int i = 0; i < asciiWeights.Length; i++)
            {
                if (grayValue <= asciiWeights[i])
                    return asciiChars[i];
            }
            return asciiChars[asciiChars.Length - 1];
        }

        // Atkinson dithering algo
        for (int y = 0; y < resizedImage.Height; y++)
        {
            for (int x = 0; x < resizedImage.Width; x++)
            {
                var pixelColor = resizedImage.GetPixel(x, y);
                var grayValue = 0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B;
                var asciiChar = GetAsciiChar(grayValue / 255.0);

                asciiArt.Append(asciiChar);

                var oldGray = grayValue;
                var newGray = asciiWeights[Array.IndexOf(asciiChars, asciiChar)] * 255.0;
                var quantError = oldGray - newGray;

                if (x + 1 < resizedImage.Width)
                {
                    AdjustPixelBrightness(resizedImage, x + 1, y, quantError * 1 / 8);
                }
                if (x - 1 >= 0 && y + 1 < resizedImage.Height)
                {
                    AdjustPixelBrightness(resizedImage, x - 1, y + 1, quantError * 1 / 8);
                }
                if (y + 1 < resizedImage.Height)
                {
                    AdjustPixelBrightness(resizedImage, x, y + 1, quantError * 1 / 8);
                    if (x + 1 < resizedImage.Width)
                        AdjustPixelBrightness(resizedImage, x + 1, y + 1, quantError * 1 / 8);
                }
            }
            asciiArt.AppendLine();
        }
        return asciiArt.ToString();
    }

    /// <summary>
    /// Set the pixels lightness (dithering)
    /// </summary>
    /// <param name="image"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="error"></param>
    private static void AdjustPixelBrightness(Bitmap image, int x, int y, double error)
    {
        var pixelColor = image.GetPixel(x, y);
        var r = Math.Min(Math.Max(0, (int)(pixelColor.R + error)), 255);
        var g = Math.Min(Math.Max(0, (int)(pixelColor.G + error)), 255);
        var b = Math.Min(Math.Max(0, (int)(pixelColor.B + error)), 255);
        image.SetPixel(x, y, Color.FromArgb(r, g, b));
    }

    /// <summary>
    /// Resize the image with Bicubic interpolation
    /// </summary>
    /// <param name="img"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    private static Bitmap ResizeImage(Bitmap img, int width, int height)
    {
        var resized = new Bitmap(width, height);
        using (var g = Graphics.FromImage(resized))
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic; 
            g.DrawImage(img, 0, 0, width, height);
        }
        return resized;
    }
}
