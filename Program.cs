using System;
using System.Drawing;
using ImageMagick;

namespace ImageAnimationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load images
            using (var playgroundImage = Image.FromFile(@"C:\Users\gulam.waris\Desktop\ConsoleApp2\ConsoleApp2\Images\Playground.png"))
            using (var footballImage = Image.FromFile(@"C:\Users\gulam.waris\Desktop\ConsoleApp2\ConsoleApp2\Images\Football.png"))
            {
                int totalFrames = 30;
                int yMovementPerFrame = 15;
                int delayBetweenFrames = 20; // in milliseconds
                int initialX = 400; // Customize as needed
                int initialY = 300; // Customize as needed (slightly below the top)

                // Initialize Magick.NET
                using (var gifImages = new MagickImageCollection())
                {
                    for (int frameIndex = 0; frameIndex < totalFrames; frameIndex++)
                    {
                        // Create a new frame
                        using (var frameImage = new Bitmap(playgroundImage))
                        {
                            // Calculate the new football position
                            int newY = initialY + (frameIndex * yMovementPerFrame);

                            // Draw the football onto the frame
                            using (var graphics = Graphics.FromImage(frameImage))
                            {
                                graphics.DrawImage(footballImage, new Point(initialX, newY));
                            }

                            // Convert the Bitmap to byte array
                            var frameBytes = ImageToByteArray(frameImage);

                            // Add the frame bytes to the GIF collection
                            gifImages.Add(new MagickImage(frameBytes));
                        }
                    }

                    // Set GIF properties
                    gifImages[0].AnimationDelay = delayBetweenFrames;
                    gifImages[0].AnimationIterations = 0; // Repeat indefinitely

                    // Save the GIF
                    gifImages.Write(@"C:\Users\gulam.waris\Desktop\ConsoleApp2\ConsoleApp2\Images\Output.gif");


                }
            }
        }
        

        static byte[] ImageToByteArray(Image image)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
