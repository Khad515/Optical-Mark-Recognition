using System;
using System.IO;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMR_WinForm_1
{
    class ImageProcessing
    {
        public Point GetLocation_Top_Left(Image pic)
        {
            bool End = false;
            Point location = new Point();
            Bitmap bmp = new Bitmap(pic);
            int width = bmp.Width;
            int height = bmp.Height;
            Color c;

            for(int x = 1; x <= width - 1 ; x++)
            {
                for(int y = 1; y <= height - 1; y++)
                {
                    c = bmp.GetPixel(x, y);
                    int A = c.A;
                    int R = c.R;
                    int B = c.B;
                    int G = c.G;
                    if(R <=90)
                    {
                        End = true;
                        location.X = x;
                        location.Y = y;
                        break;
                    }
                }
                if(End == true) { End = false; break; }
            }


            return location;
        }

        public Point GetLocation_Width_Height(Image pic)
        {
            bool End = false;
            Point location = new Point();
            Bitmap bmp = new Bitmap(pic);
            int width = bmp.Width;
            int height = bmp.Height;
            Color c;

            for (int x = width - 1; x > 2; x--)
            {
                for (int y = height - 1; y > 2; y--)
                {
                    c = bmp.GetPixel(x, y);
                    int A = c.A;
                    int R = c.R;
                    int B = c.B;
                    int G = c.G;
                    if (R <= 90)
                    {
                        End = true;
                        location.X = x;
                        location.Y = y;
                        break;
                    }
                }
                if (End == true) { End = false; break; }
            }


            return location;
        }

        public List<char> ProcessResults(Image pic)
        {
            List<char> results = new List<char>();
            bool Q1 = false;
            bool Q2 = false;
            bool Q3 = false;
            bool Q4 = false;
            bool Q5 = false;
            bool Q6 = false;
            bool Q7 = false;
            bool Q8 = false;
            bool Q9 = false;
            bool Q10 = false;

            char Q_1 = ' ';
            char Q_2 = ' ';
            char Q_3 = ' ';
            char Q_4 = ' ';
            char Q_5 = ' ';
            char Q_6 = ' ';
            char Q_7 = ' ';
            char Q_8 = ' ';
            char Q_9 = ' ';
            char Q_10 = ' ';

            Bitmap bmp = new Bitmap(pic);
            Color c;
            bool End = false;
            Point frst = new Point();

            int width = bmp.Width;
            int height = bmp.Height;

            for (int y = 1; y <= height - 1; y++)
            {
                for (int x = 1; x <= width - 1; x++)
                {
                    c = bmp.GetPixel(x, y);
                    int A = c.A;
                    int R = c.R;
                    int B = c.B;
                    int G = c.G;
                    if (R > 0)
                    {
                        frst.X = x;
                        frst.Y = y;

                        if(frst.Y <= 38)
                        {
                            if(frst.X <= 63)
                            {
                                if(!Q1) { Q_1 = 'A'; Q1 = true; }
                            }else
                            {
                                if (frst.X > 63 && frst.X <=95)
                                {
                                    if (!Q1) { Q_1 = 'B'; Q1 = true; }
                                }
                                else
                                {
                                    if (frst.X > 95 && frst.X <= 127)
                                    {
                                        if (!Q1) { Q_1 = 'C'; Q1 = true; }
                                    }
                                    else
                                    {
                                        if (frst.X > 127 && frst.X <= 160)
                                        {
                                            if (!Q1) { Q_1 = 'D'; Q1 = true; }
                                        }
                                        else
                                        {
                                            if (frst.X > 160)
                                            {
                                                if (!Q1) { Q_1 = 'E'; Q1 = true; }
                                            }
                                        }
                                    }
                                }
                            }
                            // 1 Q
                        }else
                        {
                            if (frst.Y > 38 && frst.Y <= 70)
                            {
                                if (frst.X <= 63)
                                {
                                    if (!Q2) { Q_2 = 'A'; Q2 = true; }
                                }
                                else
                                {
                                    if (frst.X > 63 && frst.X <= 95)
                                    {
                                        if (!Q2) { Q_2 = 'B'; Q2 = true; }
                                    }
                                    else
                                    {
                                        if (frst.X > 95 && frst.X <= 127)
                                        {
                                            if (!Q2) { Q_2 = 'C'; Q2 = true; }
                                        }
                                        else
                                        {
                                            if (frst.X > 127 && frst.X <= 160)
                                            {
                                                if (!Q2) { Q_2 = 'D'; Q2 = true; }
                                            }
                                            else
                                            {
                                                if (frst.X > 160)
                                                {
                                                    if (!Q2) { Q_2 = 'E'; Q2 = true; }
                                                }
                                            }
                                        }
                                    }
                                }
                                // 2 Q
                            }
                            else
                            {
                                if (frst.Y > 70 && frst.Y <= 110)
                                {
                                    if (frst.X <= 63)
                                    {
                                        if (!Q3) { Q_3 = 'A'; Q3 = true; }
                                    }
                                    else
                                    {
                                        if (frst.X > 63 && frst.X <= 95)
                                        {
                                            if (!Q3) { Q_3 = 'B'; Q3 = true; }
                                        }
                                        else
                                        {
                                            if (frst.X > 95 && frst.X <= 127)
                                            {
                                                if (!Q3) { Q_3 = 'C'; Q3 = true; }
                                            }
                                            else
                                            {
                                                if (frst.X > 127 && frst.X <= 160)
                                                {
                                                    if (!Q3) { Q_3 = 'D'; Q3 = true; }
                                                }
                                                else
                                                {
                                                    if (frst.X > 160)
                                                    {
                                                        if (!Q3) { Q_1 = 'E'; Q3 = true; }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    // 3 Q
                                }
                                else
                                {
                                    if (frst.Y > 110 && frst.Y <= 147)
                                    {
                                        if (frst.X <= 63)
                                        {
                                            if (!Q4) { Q_4 = 'A'; Q4 = true; }
                                        }
                                        else
                                        {
                                            if (frst.X > 63 && frst.X <= 95)
                                            {
                                                if (!Q4) { Q_4 = 'B'; Q4 = true; }
                                            }
                                            else
                                            {
                                                if (frst.X > 95 && frst.X <= 127)
                                                {
                                                    if (!Q4) { Q_4 = 'C'; Q4 = true; }
                                                }
                                                else
                                                {
                                                    if (frst.X > 127 && frst.X <= 160)
                                                    {
                                                        if (!Q4) { Q_4 = 'D'; Q4 = true; }
                                                    }
                                                    else
                                                    {
                                                        if (frst.X > 160)
                                                        {
                                                            if (!Q4) { Q_4 = 'E'; Q4 = true; }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        // 4 Q
                                    }
                                    else
                                    {
                                        if (frst.Y > 147 && frst.Y <= 190)
                                        {
                                            if (frst.X <= 63)
                                            {
                                                if (!Q5) { Q_5 = 'A'; Q5 = true; }
                                            }
                                            else
                                            {
                                                if (frst.X > 63 && frst.X <= 95)
                                                {
                                                    if (!Q5) { Q_5 = 'B'; Q5 = true; }
                                                }
                                                else
                                                {
                                                    if (frst.X > 95 && frst.X <= 127)
                                                    {
                                                        if (!Q5) { Q_5 = 'C'; Q5 = true; }
                                                    }
                                                    else
                                                    {
                                                        if (frst.X > 127 && frst.X <= 160)
                                                        {
                                                            if (!Q5) { Q_5 = 'D'; Q5 = true; }
                                                        }
                                                        else
                                                        {
                                                            if (frst.X > 160)
                                                            {
                                                                if (!Q5) { Q_5 = 'E'; Q5 = true; }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            // 5 Q
                                        }
                                        else
                                        {
                                            if (frst.Y > 190 && frst.Y <= 230)
                                            {
                                                if (frst.X <= 63)
                                                {
                                                    if (!Q6) { Q_6 = 'A'; Q6 = true; }
                                                }
                                                else
                                                {
                                                    if (frst.X > 63 && frst.X <= 95)
                                                    {
                                                        if (!Q6) { Q_6 = 'B'; Q6 = true; }
                                                    }
                                                    else
                                                    {
                                                        if (frst.X > 95 && frst.X <= 127)
                                                        {
                                                            if (!Q6) { Q_6 = 'C'; Q6 = true; }
                                                        }
                                                        else
                                                        {
                                                            if (frst.X > 127 && frst.X <= 160)
                                                            {
                                                                if (!Q6) { Q_6 = 'D'; Q6 = true; }
                                                            }
                                                            else
                                                            {
                                                                if (frst.X > 160)
                                                                {
                                                                    if (!Q6) { Q_6 = 'E'; Q6 = true; }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                // 6 Q
                                            }
                                            else
                                            {
                                                if (frst.Y > 230 && frst.Y <= 272)
                                                {
                                                    if (frst.X <= 63)
                                                    {
                                                        if (!Q7) { Q_7 = 'A'; Q7 = true; }
                                                    }
                                                    else
                                                    {
                                                        if (frst.X > 63 && frst.X <= 95)
                                                        {
                                                            if (!Q7) { Q_7 = 'B'; Q7 = true; }
                                                        }
                                                        else
                                                        {
                                                            if (frst.X > 95 && frst.X <= 127)
                                                            {
                                                                if (!Q7) { Q_7 = 'C'; Q7 = true; }
                                                            }
                                                            else
                                                            {
                                                                if (frst.X > 127 && frst.X <= 160)
                                                                {
                                                                    if (!Q7) { Q_7 = 'D'; Q7 = true; }
                                                                }
                                                                else
                                                                {
                                                                    if (frst.X > 160)
                                                                    {
                                                                        if (!Q7) { Q_7 = 'E'; Q7 = true; }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    // 7 Q
                                                }
                                                else
                                                {
                                                    if (frst.Y > 272 && frst.Y <= 310)
                                                    {
                                                        if (frst.X <= 63)
                                                        {
                                                            if (!Q8) { Q_8 = 'A'; Q8 = true; }
                                                        }
                                                        else
                                                        {
                                                            if (frst.X > 63 && frst.X <= 95)
                                                            {
                                                                if (!Q8) { Q_8 = 'B'; Q8 = true; }
                                                            }
                                                            else
                                                            {
                                                                if (frst.X > 95 && frst.X <= 127)
                                                                {
                                                                    if (!Q8) { Q_8 = 'C'; Q8 = true; }
                                                                }
                                                                else
                                                                {
                                                                    if (frst.X > 127 && frst.X <= 160)
                                                                    {
                                                                        if (!Q8) { Q_8 = 'D'; Q8 = true; }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (frst.X > 160)
                                                                        {
                                                                            if (!Q8) { Q_8 = 'E'; Q8 = true; }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 8 Q
                                                    }
                                                    else
                                                    {
                                                        if (frst.Y > 310 && frst.Y <= 355)
                                                        {
                                                            if (frst.X <= 63)
                                                            {
                                                                if (!Q9) { Q_9 = 'A'; Q9 = true; }
                                                            }
                                                            else
                                                            {
                                                                if (frst.X > 63 && frst.X <= 95)
                                                                {
                                                                    if (!Q9) { Q_9 = 'B'; Q9 = true; }
                                                                }
                                                                else
                                                                {
                                                                    if (frst.X > 95 && frst.X <= 127)
                                                                    {
                                                                        if (!Q9) { Q_9 = 'C'; Q9 = true; }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (frst.X > 127 && frst.X <= 160)
                                                                        {
                                                                            if (!Q9) { Q_9 = 'D'; Q9 = true; }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (frst.X > 160)
                                                                            {
                                                                                if (!Q9) { Q_9 = 'E'; Q9 = true; }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            // 9 Q
                                                        }
                                                        else
                                                        {
                                                            if (frst.Y > 355)
                                                            {
                                                                if (frst.X <= 63)
                                                                {
                                                                    if (!Q10) { Q_10 = 'A'; Q10 = true; }
                                                                }
                                                                else
                                                                {
                                                                    if (frst.X > 63 && frst.X <= 95)
                                                                    {
                                                                        if (!Q10) { Q_10 = 'B'; Q10 = true; }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (frst.X > 95 && frst.X <= 127)
                                                                        {
                                                                            if (!Q10) { Q_10 = 'C'; Q10 = true; }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (frst.X > 127 && frst.X <= 160)
                                                                            {
                                                                                if (!Q10) { Q_10 = 'D'; Q10 = true; }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (frst.X > 160)
                                                                                {
                                                                                    if (!Q10) { Q_10 = 'E'; Q10 = true; }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                // 10 Q
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            results.Add(Q_1);
            results.Add(Q_2);
            results.Add(Q_3);
            results.Add(Q_4);
            results.Add(Q_5);
            results.Add(Q_6);
            results.Add(Q_7);
            results.Add(Q_8);
            results.Add(Q_9);
            results.Add(Q_10);
            return results;
        }


        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}
