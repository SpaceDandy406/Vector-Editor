using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using VectorEditorCore.Drawing;
using VectorEditorCore.FigureProperties;
using VectorEditorCore.Selections;

namespace VectorEditorCore.FigureSet
{
	public class Figure
	{
		public int x1, x2, y1, y2;
		protected PropSet propSet;
        public int catchedX, catchedY;
	    public List<Marker> listMarker;

		public Figure (PropSet propSet)
		{
			this.propSet = propSet;

            listMarker = new List<Marker>()
		    {
		        new Marker() {x = x1 - 5, y = y1 - 5},
		        new Marker() {x = x2 - 5, y = y1 - 5},
		        new Marker() {x = x2 - 5, y = y2 - 5},
		        new Marker() {x = x1 - 5, y = y2 - 5}
		    };
		}

        public virtual bool IsHitToFigure(int x, int y)
        {
            return (y > y1 - 15 && y < y2 + 15 && x > x1 - 15 && x < x2 + 15);
        }
        
		virtual public void SetFrame(int _x1, int _x2, int _y1, int _y2)
		{
			this.x1 = _x1;
			this.x2 = _x2;
			this.y1 = _y1;
			this.y2 = _y2;

            listMarker[0].x = x1 - 5;
            listMarker[0].y = y1 - 5;

            listMarker[1].x = x2 - 5;
            listMarker[1].y = y1 - 5;

            listMarker[2].x = x2 - 5;
            listMarker[2].y = y2 - 5;

            listMarker[3].x = x1 - 5;
            listMarker[3].y = y2 - 5;
		}

        public void SetFigureFillColor(Color color)
        {
            propSet.SetFigureFill(color);
        }

        public void SetFigureLineSize(int lineSize) 
        {
            propSet.SetFigureLineSize(lineSize);
        }

		virtual public void WriteToStream(Stream stream)
		{
		}

		virtual public void LoadFromStream(Stream stream)
		{
            byte buffByte = (byte)stream.ReadByte();
            StringBuilder strBuilder = new StringBuilder();

            while (buffByte != '_')
            {
                strBuilder.Append((char)buffByte);
                buffByte = (byte)stream.ReadByte();
            }

            string tempStr = strBuilder.ToString();
            string pattern = @"\D(\d+)";

            Regex regex = new Regex(pattern);

            MatchCollection matchColl = regex.Matches(tempStr);

            x1 = Convert.ToInt32(matchColl[0].Groups[1].Value);
            x2 = Convert.ToInt32(matchColl[1].Groups[1].Value);
            y1 = Convert.ToInt32(matchColl[2].Groups[1].Value);
            y2 = Convert.ToInt32(matchColl[3].Groups[1].Value);

            propSet.LoadFromStream(stream);

            listMarker[0].x = x1 - 5;
            listMarker[0].y = y1 - 5;

            listMarker[1].x = x2 - 5;
            listMarker[1].y = y1 - 5;

            listMarker[2].x = x2 - 5;
            listMarker[2].y = y2 - 5;

            listMarker[3].x = x1 - 5;
            listMarker[3].y = y2 - 5;
		}

        virtual public void DrawSelf(Plotter _plotter)
        {

        }

        virtual public void MoveMarker(int x, int y, int numberOfMarker)
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

            x1 = Math.Min(listMarker[0].x, listMarker[1].x) + 5;
            x2 = Math.Max(listMarker[0].x, listMarker[1].x) + 5;
            y1 = Math.Min(listMarker[1].y, listMarker[2].y) + 5;
            y2 = Math.Max(listMarker[1].y, listMarker[2].y) + 5;
        }

        virtual public void MoveFigure(int x, int y)
        {
            x1 = x1 + x;
            x2 = x2 + x;
            y1 = y1 + y;
            y2 = y2 + y;

            listMarker[0].x = x1 - 5;
            listMarker[0].y = y1 - 5;

            listMarker[1].x = x2 - 5;
            listMarker[1].y = y1 - 5;

            listMarker[2].x = x2 - 5;
            listMarker[2].y = y2 - 5;

            listMarker[3].x = x1 - 5;
            listMarker[3].y = y2 - 5;
        }

        public void SetFigureLineColor(Color color)
        {
            propSet.SetFigureLineColor(color);
        }
    }
}

