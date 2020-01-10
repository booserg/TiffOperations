using System;
using System.Diagnostics;

using BitMiracle.LibTiff.Classic;

namespace BitMiracle.LibTiff.Samples
{
    public static class WriteBlackWhiteTiff
    {
        public static void Main()
        {
            const int width = 100;
            const int height = 150;
            const string fileName = "WriteBlackWhiteTiff.tif";

            using (Tiff output = Tiff.Open(fileName, "w"))
            {
                output.SetField(TiffTag.IMAGEWIDTH, width);
                output.SetField(TiffTag.IMAGELENGTH, height);
                output.SetField(TiffTag.SAMPLESPERPIXEL, 1);
                output.SetField(TiffTag.BITSPERSAMPLE, 32);
                output.SetField(TiffTag.ORIENTATION, Orientation.TOPLEFT);
                output.SetField(TiffTag.ROWSPERSTRIP, height);
                output.SetField(TiffTag.XRESOLUTION, 88.0);
                output.SetField(TiffTag.YRESOLUTION, 88.0);
                output.SetField(TiffTag.RESOLUTIONUNIT, ResUnit.INCH);
                output.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);
                output.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISBLACK);
                output.SetField(TiffTag.COMPRESSION, Compression.NONE);
                output.SetField(TiffTag.FILLORDER, FillOrder.MSB2LSB);

                Random random = new Random();
                for (int i = 0; i < height; i=i+1)
                {
                    byte[] buf = new byte[width * 4];
                    for (int j = 0; j < width * 4; j = j + 4)
                    {
                        buf[j] = (byte)random.Next(255);
                        buf[j + 1] = (byte)random.Next(255);
                        buf[j + 2] = (byte)random.Next(255);
                        buf[j + 3] = (byte)random.Next(255);
                    }

                    output.WriteScanline(buf, i);
                }
            }

            Process.Start(fileName);
        }
    }
}