using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using VectorEditorCore.FigureProperties;
using VectorEditorCore.Selections;

namespace VectorEditorCore.FigureSet
{
	public class Line : Figure
	{
        public Line(PropSet propSet)
            : base(propSet)
        {
            
        }

	    public override void WriteToStream(Stream stream)
		{
            stream = stream as MemoryStream;

            string str = "<Line> xa = " + x1 + " xb = " + x2 + " ya = " + y1 + " yb = " + y2 + " </Line>_";
            for (int i = 0; i < str.Length; i++)
                stream.WriteByte((byte)str[i]);

            propSet.WriteToStream(stream);
            stream.WriteByte((byte)'\n');
		}

	    public override void Draw(Drawing.Plotter _plotter)
        {
            propSet.DrawBaseProps(_plotter);
            _plotter.DrawLine(x1, x2, y1, y2);
        }

	    public override bool IsHitToFigure(int x, int y)
	    {
            if (x < Math.Min(x1-5, x2-5) || x > Math.Max(x2+5, x1+5) ||
                y < Math.Min(y1-5, y2-5) || y > Math.Max(y2+5, y1+5)) return false;

	        var tempX = (int)FuncOfX(y);
	        var tempY = (int)FuncOfY(x);

            return (y > tempY - 20 && y < tempY + 20) || (x > tempX - 20 && x < tempX + 20);
	    }

        private double FuncOfX(int y)
        {
            return ((y - (double)y1)/(y2 - (double)y1))*(x2 - x1) + x1;
        }

        private double FuncOfY(int x)
        {
            return ((x - (double)x1) / (x2 - (double)x1)) * (y2 - y1) + y1;
        }

        public override void MoveMarker(int x, int y, int numberOfMarker)
        {
            listMarker[numberOfMarker].Move(x, y);

            switch (numberOfMarker)
            {
                case 0:
                    listMarker[1].y = listMarker[0].y;
                    listMarker[3].x = listMarker[0].x;
                    break;
                case 1:
                    listMarker[0].y = listMarker[1].y;
                    listMarker[2].x = listMarker[1].x;
                    break;
                case 2:
                    listMarker[3].y = listMarker[2].y;
                    listMarker[1].x = listMarker[2].x;
                    break;
                case 3:
                    listMarker[2].y = listMarker[3].y;
                    listMarker[0].x = listMarker[3].x;
                    break;
            }

            x1 = listMarker[0].x + 5;
            x2 = listMarker[2].x + 5;
            y1 = listMarker[0].y + 5;
            y2 = listMarker[2].y + 5;
        }
	}
}

