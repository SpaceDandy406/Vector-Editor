using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using VectorEditorCore.Drawing;

namespace VectorEditorCore.FigureProperties
{
	public class PropSet
	{
		List<BaseProps> baseProps;

		public PropSet ()
		{
			this.baseProps = new List<BaseProps> ();
		}

		public void Add(BaseProps baseProps)
		{
			this.baseProps.Add (baseProps);
		}

		public void WriteToStream(Stream stream)
		{
            foreach (var baseProp in baseProps)
                baseProp.WriteToStream(stream);
		}

		public void LoadFromStream(Stream stream)
		{
            foreach (var baseProp in baseProps)
                baseProp.LoadFromStream(stream);
		}

        public void DrawBaseProps(Drawing.Plotter _plotter)
        {
            foreach (var baseProp in baseProps)
            {
                baseProp.DrawSelf(_plotter);
            }
        }

        public void SetFigureLineSize(int lineSize)
        {
            LineProps temp = null;

            foreach (var item in baseProps)
            {
                if (item is LineProps)
                {
                    temp = item as LineProps;
                    temp.LineSize = lineSize;
                    break;
                }
            }
        }

        public void SetFigureFill(Color color)
        {
            FillProps temp = null;

            foreach (var item in baseProps)
            {
                if (item is FillProps)
                {
                    temp = item as FillProps;
                    temp.Color = color;
                    break;
                }
            }
        }

        internal void SetFigureLineColor(Color color)
        {
            LineProps temp = null;

            foreach (var item in baseProps)
            {
                if (item is LineProps)
                {
                    temp = item as LineProps;
                    temp.LineColor = color;
                    break;
                }
            }
        }
    }
}

