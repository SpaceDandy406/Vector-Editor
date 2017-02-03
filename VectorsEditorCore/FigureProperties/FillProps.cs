using System.IO;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace VectorEditorCore.FigureProperties
{
	public class FillProps : BaseProps
	{
        public Color Color { get; set; }

        public void SetColorFromRGB(byte _r, byte _g, byte _b)
        {
            this.Color = Color.FromArgb(255, _r, _g, _b);
           
        }

        public FillProps(Color _color)
		{
            this.Color = _color;
		}

		public override void WriteToStream(Stream stream)
		{
            stream = stream as MemoryStream;

            string str = "<FillProps> A = "+ Color.A + " R = " + Color.R + " G = " + Color.G + " B = " + Color.B + " </FillProps>_";
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

            a = Convert.ToByte(matchColl[0].Groups[1].Value);
            r = Convert.ToByte(matchColl[1].Groups[1].Value);
            g = Convert.ToByte(matchColl[2].Groups[1].Value);
            b = Convert.ToByte(matchColl[3].Groups[1].Value);

            this.Color = Color.FromArgb(a, r, g, b);
		}

        public override void DrawSelf(Drawing.Plotter _plotter)
        {
            _plotter.SetBrush(Color);
        }
	}
}

