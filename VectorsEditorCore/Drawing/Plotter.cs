using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using VectorEditorCore.Selections;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using Image = System.Windows.Controls.Image;
using Pen = System.Windows.Media.Pen;
using Point = System.Windows.Point;

namespace VectorEditorCore.Drawing
{
    public class Plotter
    {
        Image image;
        RenderTargetBitmap bmp;
        DrawingVisual drawingVisual;
        DrawingContext drawingContext;
        public double Width { get; private set; }
        public double Height { get; private set; }
        double dpiX, dpiY;
        Brush brush;
        Pen pen;

        public Plotter(Image image)
        {
            var g = Graphics.FromHwnd(IntPtr.Zero);
            dpiX = g.DpiX;
            dpiY = g.DpiY;

            this.image = image;           
            drawingVisual = new DrawingVisual();
            brush = new SolidColorBrush();
            pen = new Pen();
            Width = image.Width;
            this.Height = image.Height;
            bmp = new RenderTargetBitmap((int) image.Width, (int) image.Height, dpiX, dpiY, PixelFormats.Pbgra32);
        }

        public  void BeginDraw()
        {
            bmp = new RenderTargetBitmap((int)this.image.Width, (int)this.image.Height, dpiX, dpiY, PixelFormats.Pbgra32);
            
            drawingVisual.Children.Clear();

            drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawRectangle(new SolidColorBrush(Colors.White), null, new Rect(0, 0, bmp.PixelWidth, bmp.PixelHeight));
        }

        public void DrawRect(int x1, int x2, int y1, int y2)
        {
            ResizeImage(x2, y2);

            drawingContext.DrawRectangle(brush, pen, new Rect(x1, y1, Math.Abs(x2 - x1), Math.Abs(y2 - y1)));
        }

        public void DrawLine(int x1, int x2, int y1, int y2)
        {
            ResizeImage(x2, y2);
            drawingContext.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));
        }

        public void ResizeImage(int x2, int y2)
        {
            if (this.image.Width <= x2)
                image.Width = x2 + 20;
            if (this.image.Height <= y2)
                image.Height = y2 + 20;
        }

        public void DrawSelection(List<Marker> markerList)
        {
            foreach (var item in markerList)
            {
                ResizeImage(item.x + 11, item.y + 11);

                Brush tempBrush = new SolidColorBrush(Colors.DodgerBlue);
                var tempPen = new Pen(new SolidColorBrush(Colors.RoyalBlue), 1.5);

                drawingContext.DrawEllipse(tempBrush, tempPen, new Point(item.x+5, item.y+5), 5, 5);
            }
        }

        public void EndDraw()
        {
            drawingContext.Close();
            bmp.Render(drawingVisual);
            image.Source = bmp;
        }

        public void SetBrush(Color color)
        {
            brush = new SolidColorBrush(color);
        }

        public void SetPen(Color color, int lineSize)
        {
            pen = new Pen(new SolidColorBrush(color), lineSize);
        }

        internal void DrawEllipse(int x1, int x2, int y1, int y2)
        {
            double centerX = x1 + ((x2 - x1) / 2.0);
            double centerY = y1 + ((y2 - y1) / 2.0);
            double radiusX = x2 - centerX;
            double radiusY = y2 - centerY;

            ResizeImage(x2, y2);

            drawingContext.DrawEllipse(brush, pen, new Point(centerX, centerY), radiusX, radiusY);
        }
    }
}
