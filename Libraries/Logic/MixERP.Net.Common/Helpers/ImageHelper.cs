/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace MixERP.Net.Common.Helpers
{
    public static class ImageHelper
    {
        public static Bitmap CreateThumbnail(Image image, Size thumbnailSize)
        {
            if (image == null)
            {
                return null;
            }

            if (thumbnailSize.Width.Equals(0))
            {
                thumbnailSize.Width = image.Size.Width;
            }

            if (thumbnailSize.Height.Equals(0))
            {
                thumbnailSize.Height = image.Size.Height;
            }

            float scalingRatio = CalculateScalingRatio(image.Size, thumbnailSize);

            int scaledWidth = (int)Math.Round((float)image.Size.Width * scalingRatio);
            int scaledHeight = (int)Math.Round((float)image.Size.Height * scalingRatio);
            int scaledLeft = (thumbnailSize.Width - scaledWidth) / 2;
            int scaledTop = (thumbnailSize.Height - scaledHeight) / 2;

            // For portrait mode, adjust the vertical top of the crop area so that we get more of
            // the top area
            if (scaledWidth < scaledHeight && scaledHeight > thumbnailSize.Height)
            {
                scaledTop = (thumbnailSize.Height - scaledHeight) / 4;
            }

            Rectangle cropArea = new Rectangle(scaledLeft, scaledTop, scaledWidth, scaledHeight);

            using (Bitmap thumbnail = new Bitmap(thumbnailSize.Width, thumbnailSize.Height, PixelFormat.Format32bppPArgb))
            {
                using (Graphics thumbnailGraphics = Graphics.FromImage(thumbnail))
                {
                    thumbnailGraphics.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    thumbnailGraphics.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraphics.Clear(Color.Transparent);
                    thumbnailGraphics.DrawImage(image, cropArea);
                }
                return thumbnail;
            }
        }

        public static string GetContentType(string extension)
        {
            switch (extension)
            {
                case ".bmp":
                    return "Image/bmp";

                case ".gif":
                    return "Image/gif";

                case ".jpg":
                    return "Image/jpeg";

                case ".jpeg":
                    return "Image/jpeg";

                case ".png":
                    return "Image/png";

                default:
                    return "text/plain";
            }
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            if (format == null)
            {
                return null;
            }

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static string GetFileExtension(ImageFormat format)
        {
            return ImageCodecInfo.GetImageEncoders()
                .First(x => x.FormatID == format.Guid)
                .FilenameExtension
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .First()
                .Trim('*')
                .ToUpperInvariant();
        }

        public static ImageFormat GetImageFormat(string extension)
        {
            switch (extension)
            {
                case "bmp":
                    return ImageFormat.Bmp;

                case "gif":
                    return ImageFormat.Gif;

                case "jpg":
                    return ImageFormat.Jpeg;

                case "png":
                    return ImageFormat.Png;
            }

            return ImageFormat.Jpeg;
        }

        public static byte[] GetResizedImage(Image image, int width, int height)
        {
            using (Image resizedImage = CreateThumbnail(image, new Size(width, height)))
            {
                return Conversion.TryCastByteArray(resizedImage);
            }
        }

        private static float CalculateScalingRatio(Size originalSize, Size targetSize)
        {
            float originalAspectRatio = (float)originalSize.Width / (float)originalSize.Height;
            float targetAspectRatio = (float)targetSize.Width / (float)targetSize.Height;

            float scalingRatio;

            if (targetAspectRatio >= originalAspectRatio)
            {
                scalingRatio = (float)targetSize.Width / (float)originalSize.Width;
            }
            else
            {
                scalingRatio = (float)targetSize.Height / (float)originalSize.Height;
            }

            return scalingRatio;
        }
    }
}