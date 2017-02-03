using System.Collections.Generic;
using System.IO;
using VectorEditorCore.FigureProperties;
using VectorEditorCore.Drawing;
using VectorEditorCore.Selections;

namespace VectorEditorCore.FigureSet
{
	public class Rect : Figure
	{
		public Rect (PropSet propSet)
			: base (propSet)
		{
		}

	    public override void WriteToStream(Stream stream)
		{
            stream = stream as MemoryStream;

            string str = "<Rect> xa = " + x1 + " xb = " + x2 + " ya = " + y1 + " yb = " + y2 + " </Rect>_";

            for (int i = 0; i < str.Length; i++)
                stream.WriteByte((byte)str[i]);

            propSet.WriteToStream(stream);
            stream.WriteByte((byte)'\n');
		}

	    public override void DrawSelf(Drawing.Plotter _plotter)
        {
            propSet.DrawBaseProps(_plotter);
            _plotter.DrawRect(this.x1, this.x2, this.y1, this.y2);
        }
	}
}

