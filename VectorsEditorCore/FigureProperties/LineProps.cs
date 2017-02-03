using System.IO;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace VectorEditorCore.FigureProperties
{
	public class LineProps : BaseProps
	{
        public int LineSize { get; set; }
        public Color LineColor { get; set; }

		public LineProps (int _line, Color color)
		{
            this.LineSize = _line;
		    LineColor = color;
		}

		public override void WriteToStream(Stream stream)
		{
            stream = stream as MemoryStream;

            string str = "<LineProps> line = " + LineSize +
                " lineColorA = " + LineColor.A + " lineColorR = " + LineColor.R +
                " lineColorG = " + LineColor.G + " lineColorB = " + LineColor.B + " </LineProps>_";
            for (int i = 0; i < str.Length; i++)
                stream.WriteByte((byte)str[i]);
		}

		public override void LoadFromStream(Stream stream)
		{
		    byte a, r, g, b;

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

            LineSize = Convert.ToInt32(matchColl[0].Groups[1].Value);

            a = Convert.ToByte(matchColl[1].Groups[1].Value);
            r = Convert.ToByte(matchColl[2].Groups[1].Value);
            g = Convert.ToByte(matchColl[3].Groups[1].Value);
            b = Convert.ToByte(matchColl[4].Groups[1].Value);

            LineColor = Color.FromArgb(a, r, g, b);
        }

        public override void DrawSelf(Drawing.Plotter _plotter)
        {
            _plotter.SetPen(LineColor, LineSize);
        }
    }
}

