using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditorCore.FigureProperties;
using VectorEditorCore.Selections;

namespace VectorEditorCore.FigureSet
{
    public class Ellipse : Figure
    {
        public Ellipse(PropSet propSet) : base(propSet)
        {
            
        }

        public override void WriteToStream(Stream stream)
        {
            stream = stream as MemoryStream;

            string str = "<Ellipse> xa = " + x1 + " xb = " + x2 + " ya = " + y1 + " yb = " + y2 + " </Ellipse>_";

            for (int i = 0; i < str.Length; i++)
                stream.WriteByte((byte)str[i]);

            propSet.WriteToStream(stream);
            stream.WriteByte((byte)'\n');
        }

        public override void Draw(Drawing.Plotter _plotter)
        {
            propSet.DrawBaseProps(_plotter);
            _plotter.DrawEllipse(this.x1, this.x2, this.y1, this.y2);
        }
    }
}
