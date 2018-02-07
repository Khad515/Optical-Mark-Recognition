using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;
using System.IO;
using System.Diagnostics;

namespace OMR_WinForm_1
{
    public partial class Form1 : Form
    {
        Image original_image;
        Image grayscale_image;
        Image cliped_image;
        Image cv_image;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(ImageOpen.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ImageOpen.FileName;
                pictureBox1.Load(ImageOpen.FileName);
                original_image = pictureBox1.Image;
                label1.ForeColor = Color.Green;
            }

        }

        private void autoSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(original_image);
            Bitmap32 bm32 = new Bitmap32(bm);
            bm32.Grayscale();
            pictureBox2.Image = bm;
            grayscale_image = bm;
            label2.ForeColor = Color.Green;

            ImageProcessing img_p = new ImageProcessing();
            Point loc = img_p.GetLocation_Top_Left(grayscale_image);
            Point loc1 = img_p.GetLocation_Width_Height(grayscale_image);
            label3.ForeColor = Color.Green;
            label5.Text = String.Format("X = {0}; Y = {1}", loc.X, loc.Y); // +10 + 10
            label6.Text = String.Format("XW = {0}; YH = {1}", loc1.X, loc1.Y);


            Bitmap bmp_gr = new Bitmap(grayscale_image);
            Image<Bgr, Byte> sourceImage1 = new Image<Bgr, Byte>(bmp_gr);
            sourceImage1.ROI = new Rectangle(loc.X + 10, loc.Y + 10, (loc1.X - loc.X)-20, (loc1.Y - loc.Y)-20);
            pictureBox2.Image = sourceImage1.Clone().ToBitmap();
            cliped_image = sourceImage1.Clone().ToBitmap();
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            label4.ForeColor = Color.Green;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image triangleRectangleImageBox1;
            Image circleImageBox1;
            Image lineImageBox1;
            StringBuilder msgBuilder = new StringBuilder("Performance: ");
            Bitmap clip_bmp = new Bitmap(cliped_image);
            //Load the image from file and resize it for display
            Image<Bgr, Byte> img = new Image<Bgr, byte>(clip_bmp).Resize(400, 400, Emgu.CV.CvEnum.Inter.Linear, true);

            //Convert the image to grayscale and filter out the noise
            UMat uimage = new UMat();
            CvInvoke.CvtColor(img, uimage, ColorConversion.Bgr2Gray);

            //use image pyr to remove noise
            UMat pyrDown = new UMat();
            CvInvoke.PyrDown(uimage, pyrDown);
            CvInvoke.PyrUp(pyrDown, uimage);

            #region circle detection
            Stopwatch watch = Stopwatch.StartNew();
            double cannyThreshold = 180.0; // 180
            double circleAccumulatorThreshold = 120.0; // 120
            CircleF[] circles = CvInvoke.HoughCircles(uimage, HoughType.Gradient, 2.0, 20.0, cannyThreshold, circleAccumulatorThreshold, 5);

            watch.Stop();
            msgBuilder.Append(String.Format("Hough circles - {0} ms; ", watch.ElapsedMilliseconds));
            #endregion

            #region Canny and edge detection
            watch.Reset(); watch.Start();
            double cannyThresholdLinking = 120.0; // 120
            UMat cannyEdges = new UMat();
            CvInvoke.Canny(uimage, cannyEdges, cannyThreshold, cannyThresholdLinking);

            LineSegment2D[] lines = CvInvoke.HoughLinesP(
               cannyEdges,
               1, //Distance resolution in pixel-related units - 1
               Math.PI / 45.0, //Angle resolution measured in radians.
               0, //threshold - 20
               0, //min Line width - 30
               10); //gap between lines - 10

            watch.Stop();
            msgBuilder.Append(String.Format("Canny & Hough lines - {0} ms; ", watch.ElapsedMilliseconds));
            #endregion

            #region Find triangles and rectangles
            watch.Reset(); watch.Start();
            List<Triangle2DF> triangleList = new List<Triangle2DF>();
            List<RotatedRect> boxList = new List<RotatedRect>(); //a box is a rotated rectangle

            using (Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (Emgu.CV.Util.VectorOfPoint contour = contours[i])
                    using (Emgu.CV.Util.VectorOfPoint approxContour = new Emgu.CV.Util.VectorOfPoint())
                    {
                        CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                        if (CvInvoke.ContourArea(approxContour, false) > 250) //only consider contours with area greater than 250
                        {
                            if (approxContour.Size == 3) //The contour has 3 vertices, it is a triangle
                            {
                                Point[] pts = approxContour.ToArray();
                                triangleList.Add(new Triangle2DF(
                                   pts[0],
                                   pts[1],
                                   pts[2]
                                   ));
                            }
                            else if (approxContour.Size == 4) //The contour has 4 vertices.
                            {
                                #region determine if all the angles in the contour are within [80, 100] degree
                                bool isRectangle = true;
                                Point[] pts = approxContour.ToArray();
                                LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                for (int j = 0; j < edges.Length; j++)
                                {
                                    double angle = Math.Abs(
                                       edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                    if (angle < 80 || angle > 100)
                                    {
                                        isRectangle = false;
                                        break;
                                    }
                                }
                                #endregion

                                if (isRectangle) boxList.Add(CvInvoke.MinAreaRect(approxContour));
                            }
                        }
                    }
                }
            }

            watch.Stop();
            msgBuilder.Append(String.Format("Triangles & Rectangles - {0} ms; ", watch.ElapsedMilliseconds));
            #endregion

            //originalImageBox.Image = img.ToBitmap();
            this.Text = msgBuilder.ToString();

            #region draw triangles and rectangles
            Image<Bgr, Byte> triangleRectangleImage = img.CopyBlank();
            foreach (Triangle2DF triangle in triangleList)
                triangleRectangleImage.Draw(triangle, new Bgr(Color.DarkBlue), 2);
            foreach (RotatedRect box in boxList)
                triangleRectangleImage.Draw(box, new Bgr(Color.DarkOrange), 2);
            triangleRectangleImageBox1 = triangleRectangleImage.ToBitmap();
            #endregion

            #region draw circles
            Image<Bgr, Byte> circleImage = img.CopyBlank();
            foreach (CircleF circle in circles)
                circleImage.Draw(circle, new Bgr(Color.Brown), 2);
            circleImageBox1 = circleImage.ToBitmap();
            #endregion

            #region draw lines
            Image<Bgr, Byte> lineImage = img.CopyBlank();
            foreach (LineSegment2D line in lines)
                lineImage.Draw(line, new Bgr(Color.Green), 2);
            lineImageBox1 = lineImage.ToBitmap();
            #endregion

            cv_image = lineImageBox1;
            pictureBox2.Image = lineImageBox1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox2.Image.Save(@"C:\Users\sivanuk\Desktop\Game_Dev\Template\omr_image2.png");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ImageProcessing pr = new ImageProcessing();
            List<char> results = pr.ProcessResults(pictureBox2.Image);
           
            
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(cv_image);
            Bitmap32 bm32 = new Bitmap32(bm);
            bm32.Grayscale();
            pictureBox2.Image = bm;

            ImageProcessing pr = new ImageProcessing();

            List<char> res = pr.ProcessResults(pictureBox2.Image);

            for(int i = 0; i < 10; i++)
            {
                textBox2.Text += string.Format("{0}) {1} \n", i + 1, res[i]);
            }
        }
    }
}
