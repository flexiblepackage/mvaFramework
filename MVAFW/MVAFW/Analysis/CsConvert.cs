using System;
using System.Collections.Generic;
using System.Text;

 class CsConvert
    {

        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        private CsConvert()
        {
            // Prevent people from trying to instantiate this class
        }

        public unsafe static void Y8ToY8(int Width, int Height, IntPtr Dest, IntPtr Src, RECT Cropping, uint Orientation)
        {
            int DestWidth = Cropping.right - Cropping.left;
            int DestHeight = Cropping.bottom - Cropping.top;

            int SrcLine = Width;
            int DestLine = (DestWidth + 3) & ~3;  // align to 4

            int SrcOffset;

            if (Orientation == 0)
            {
                // The image is a bottom-up one.
                // offset is counting from bottom.
                SrcOffset = (Height - Cropping.top - 1) * SrcLine
                           + Cropping.left;
            }
            else
            {
                // The image is a top-down one.
                // offset is counting from top.
                SrcOffset = Cropping.top * SrcLine
                           + Cropping.left;
                SrcLine = -SrcLine;
            }

            // Convert to pointer type to reduce processing time
            byte* src = (byte*)Src.ToPointer() + SrcOffset;
            byte* dest = (byte*)Dest.ToPointer();
            byte* s;
            byte* d;


            // Each line
            for (int j = 0; j < DestHeight; j++)
            {
                s = src;
                d = dest;

                // Each pixel
                for (int i = 0; i < DestWidth; i++)
                {
                    *d++ = *s++;
                }

                // Next line
                src  -= SrcLine;
                dest += DestLine;
            }
        }

        public unsafe static void Rgb24ToRgb24(int Width, int Height, IntPtr Dest, IntPtr Src, uint Orientation)
        {
            RECT Cropping;

            Cropping.left = 0;
            Cropping.top = 0;
            Cropping.right = Width;
            Cropping.bottom = Height;

            Rgb24ToRgb24(Width, Height, Dest, Src, Cropping, Orientation);
        }

        public unsafe static void Rgb24ToRgb24(int Width, int Height, IntPtr Dest, IntPtr Src, RECT Cropping, uint Orientation)
        {
            int DestWidth  = Cropping.right - Cropping.left;
            int DestHeight = Cropping.bottom - Cropping.top;

            int SrcLine  = Width * 3;
            int DestLine = ((DestWidth * 3) + 3) & ~3;  // align to 4

            int SrcOffset;

            if (Orientation == 0)
            {
                // The image is a bottom-up one.
                // offset is counting from bottom.
                SrcOffset = (Height - Cropping.top - 1) * SrcLine
                           + Cropping.left * 3;
            }
            else
            {
                // The image is a top-down one.
                // offset is counting from top.
                SrcOffset = Cropping.top * SrcLine
                           + Cropping.left * 3;
                SrcLine = -SrcLine;
            }

            // Convert to pointer type to reduce processing time
            byte* src = (byte*)Src.ToPointer() + SrcOffset;
            byte* dest = (byte*)Dest.ToPointer();
            byte* s;
            byte* d;


            // Each line
            for (int j = 0; j < DestHeight; j++)
            {
                s = src;
                d = dest;

                // Each pixel
                for (int i = 0; i < DestWidth; i++)
                {
                    *d++ = *s++;    // B
                    *d++ = *s++;    // G
                    *d++ = *s++;    // R
                }

                // Next line
                src  -= SrcLine;
                dest += DestLine;
            }
        }

        public static void Bgr30ToRgb24(int Width, int Height, IntPtr Dest, IntPtr Src, uint Orientation)
        {
            RECT Cropping;

            Cropping.left = 0;
            Cropping.top = 0;
            Cropping.right = Width;
            Cropping.bottom = Height;

            Bgr30ToRgb24(Width, Height, Dest, Src, Cropping, Orientation);
        }

        public unsafe static void Bgr30ToRgb24(int Width, int Height, IntPtr Dest, IntPtr Src, RECT Cropping, uint Orientation)
        {
            int DestWidth = Cropping.right - Cropping.left;
            int DestHeight = Cropping.bottom - Cropping.top;

            int SrcLine  = Width * 4;
            int DestLine = ((DestWidth * 3) + 3) & ~3;   // align to 4

            int SrcOffset;

            if (Orientation == 0)
            {
                // The image is a bottom-up one.
                // offset is counting from bottom.
                SrcOffset = (Height - Cropping.top - 1) * SrcLine
                           + Cropping.left * 4;
            }
            else
            {
                // The image is a top-down one.
                // offset is counting from top.
                SrcOffset = Cropping.top * SrcLine
                           + Cropping.left * 4;
                SrcLine = -SrcLine;
            }

            // Convert to pointer type to reduce processing time
            byte* src = (byte*)Src.ToPointer() + SrcOffset;
            byte* dest = (byte*)Dest.ToPointer();
            uint* s;
            byte* d;


            // Each line
            for(int j = 0; j < DestHeight ; j++)
            {
                s = (uint*)src;
                d = dest;

                // Each pixel
                for(int i = 0; i < DestWidth; i++)
                {
                    // Get 8bit R,G,B from MSB of 10bit R,G,B
                    *d++ = (byte)((*s >> 22) & 0xFF);    // B
                    *d++ = (byte)((*s >> 12) & 0xFF);    // G
                    *d++ = (byte)((*s >> 2)  & 0xFF);    // R
                    s++;
                }

                // Next line
                src  -= SrcLine;
                dest += DestLine;
            }
        }

        public unsafe static void Rgb32ToRgb24(int Width, int Height, IntPtr Dest, IntPtr Src, uint Orientation)
        {
            RECT Cropping;

            Cropping.left = 0;
            Cropping.top = 0;
            Cropping.right = Width;
            Cropping.bottom = Height;

            Rgb32ToRgb24(Width, Height, Dest, Src, Cropping, Orientation);
        }

        public unsafe static void Rgb32ToRgb24(int Width, int Height, IntPtr Dest, IntPtr Src, RECT Cropping, uint Orientation)
        {
            int DestWidth = Cropping.right - Cropping.left;
            int DestHeight = Cropping.bottom - Cropping.top;

            int SrcLine = Width * 4;
            int DestLine = ((DestWidth * 3) + 3) & ~3;  // align to 4

            int SrcOffset;

            if (Orientation == 0)
            {
                // The image is a bottom-up one.
                // offset is counting from bottom.
                SrcOffset = (Height - Cropping.top - 1) * SrcLine
                           + Cropping.left * 4;
            }
            else
            {
                // The image is a top-down one.
                // offset is counting from top.
                SrcOffset = Cropping.top * SrcLine
                           + Cropping.left * 4;
                SrcLine = -SrcLine;
            }

            byte* src = (byte*)Src.ToPointer() + SrcOffset;
            byte* dest = (byte*)Dest.ToPointer();
            byte* s;
            byte* d;


            // Each line
            for (int j = 0; j < DestHeight; j++)
            {
                s = src;
                d = dest;

                // Each pixel
                for (int i = 0; i < DestWidth; i++)
                {
                    *d++ = *s++;    // B
                    *d++ = *s++;    // G
                    *d++ = *s++;    // R
                    s++;            // Alpha, not used here
                }

                // Next line
                src -= SrcLine;
                dest += DestLine;
            }
        }

        private static byte clip(int n)
        {
            if (n < 0) n = 0;
            else if (n > 255) n = 255;

            return (byte)n;
        }

        public static void Yuv8ToRgb24(int Width, int Height, IntPtr Dest, IntPtr Src, uint Orientation)
        {
            RECT Cropping;

            Cropping.left = 0;
            Cropping.top = 0;
            Cropping.right = Width;
            Cropping.bottom = Height;

            Yuv8ToRgb24(Width, Height, Dest, Src, Cropping, Orientation);
        }

        public unsafe static void Yuv8ToRgb24(int Width, int Height, IntPtr Dest, IntPtr Src, RECT Cropping, uint Orientation)
        {
            int DestWidth = Cropping.right - Cropping.left;
            int DestHeight = Cropping.bottom - Cropping.top;

            int SrcLine = Width * 3;
            int DestLine = ((DestWidth * 3) + 3) & ~3;   // align to 4

            int SrcOffset;

            if (Orientation == 0)
            {
                // The image is a bottom-up one.
                // offset is counting from bottom.
                SrcOffset = (Height - Cropping.top - 1) * SrcLine
                           + Cropping.left * 3;
            }
            else
            {
                // The image is a top-down one.
                // offset is counting from top.
                SrcOffset = Cropping.top * SrcLine
                           + Cropping.left * 3;
                SrcLine = -SrcLine;
            }

            // Convert to pointer type to reduce processing time
            byte* src = (byte*)Src.ToPointer() + SrcOffset;
            byte* dest = (byte*)Dest.ToPointer();
            byte* s;
            byte* d;

            byte Y, Cb, Cr;
            int C, D, E;

            // Each line
            for(int j = 0; j < DestHeight ; j++)
            {
                s = src;
                d = dest;

                // Each pixel
                for(int i = 0; i < DestWidth; i++)
                {
                    Y  = *s++;
                    Cb = *s++;
                    Cr = *s++;

                    C = Y;
                    D = Cb - 128;
                    E = Cr - 128;

                    // B
                    *d++ = clip(C + ((443 * D) >> 8));
                    // G
                    *d++ = clip(C - ((86 * D + 179 * E) >> 8));
                    // R
                    *d++ = clip(C + ((351 * E) >> 8));
                }

                // Next line
                src  -= SrcLine;
                dest += DestLine;
            }
        }

        public static void Yuy2ToRgb24(int Width, int Height, IntPtr Dest, IntPtr Src, uint Orientation)
        {
            RECT Cropping;

            Cropping.left = 0;
            Cropping.top = 0;
            Cropping.right = Width;
            Cropping.bottom = Height;

            Yuy2ToRgb24(Width, Height, Dest, Src, Cropping, Orientation);
        }

        public unsafe static void Yuy2ToRgb24(int Width, int Height, IntPtr Dest, IntPtr Src, RECT Cropping, uint Orientation)
        {
            int DestWidth = Cropping.right - Cropping.left;
            int DestHeight = Cropping.bottom - Cropping.top;

            int SrcLine  = Width * 2;
            int DestLine = ((DestWidth * 3) + 3) & ~3;   // align to 4

            int SrcOffset;

            // One YUY2 contains 2 pixels
            int nYuy2 = DestWidth / 2;

            if (Orientation == 0)
            {
                // The image is a bottom-up one.
                // offset is counting from bottom.
                SrcOffset = (Height - Cropping.top - 1) * SrcLine
                          + Cropping.left * 2;
            }
            else
            {
                // The image is a top-down one.
                // offset is counting from top.
                SrcOffset = Cropping.top * SrcLine
                          + Cropping.left * 2;
                SrcLine = -SrcLine;
            }

            if ((Cropping.left & 1) == 0)
            {
                if ((DestWidth & 1) == 1)
                {
                    //
                    // In the case of DestWidth = odd, we extract
                    // the last pixel of each line from the first pixel
                    // of the last YUY2.
                    //
                    nYuy2++;
                }
            }
            else
            {
                //
                // YUY2 always start from even pixel.
                //
                SrcOffset -= 2;
            }

            //
            // Convert to pointer type to reduce processing time
            //
            byte* src = (byte*)Src.ToPointer() + SrcOffset;
            byte* dest = (byte*)Dest.ToPointer();
            byte *s;
            byte *d;

            byte Y0, Y1, Cb, Cr;
            int C, D, E;

            //
            // Each line
            //
            for(int j = 0; j < DestHeight ; j++)
            {
                s = src;
                d = dest;

                int w = 0;

                //
                // First pixel of each line,
                // if start at odd pixel.
                //
                if ((Cropping.left & 1) == 1)
                {
                    // YUY2 always start from even pixel.
                    // In the case, we extract the first pixel of the line
                    // from the second pixel of the first YUY2.
                    //
                    Y0 = *s++;
                    Cb = *s++;
                    Y1 = *s++;
                    Cr = *s++;

                    //
                    // second pixel of the YUY2
                    //
                    C = Y1;
                    D = Cb - 128;
                    E = Cr - 128;

                    // B
                    *d++ = clip(C + ((443 * D) >> 8));
                    // G
                    *d++ = clip(C - ((86 * D + 179 * E) >> 8));
                    // R
                    *d++ = clip(C + ((351 * E) >> 8));

                    w++;
                }

                //
                // Each pixel
                //
                for (int i = 0; i < nYuy2; i++)
                {
                    Y0 = *s++;
                    Cb = *s++;
                    Y1 = *s++;
                    Cr = *s++;

                    //
                    // fist pixel of the YUY2
                    //
                    C = Y0;
                    D = Cb - 128;
                    E = Cr - 128;

                    // B
                    *d++ = clip(C + ((443 * D) >> 8));
                    // G
                    *d++ = clip(C - ((86 * D + 179 * E) >> 8));
                    // R
                    *d++ = clip(C + ((351 * E) >> 8));

                    w++;

                    //
                    // Reach final pixel of the line
                    //
                    if (w >= DestWidth)
                        break;

                    //
                    // second pixel of the YUY2
                    //
                    C = Y1;

                    // B
                    *d++ = clip(C + ((443 * D) >> 8));
                    // G
                    *d++ = clip(C - ((86 * D + 179 * E) >> 8));
                    // R
                    *d++ = clip(C + ((351 * E) >> 8));

                    w++;

                }   //for(int i = 0; i < width; i++)


                //
                // Next line
                //
                src  -= SrcLine;
                dest += DestLine;

            }   //for(int j = 0; j < DestHeight ; j++)
        }

    }
