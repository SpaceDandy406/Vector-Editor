using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using System.Windows.Media;

namespace VectorEditorTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WriteToStream()
        {
            VectorEditorCore.FigureProperties.PropSet prop = new VectorEditorCore.FigureProperties.PropSet();
            prop.Add(new VectorEditorCore.FigureProperties.FillProps(new Color()));
            prop.Add(new VectorEditorCore.FigureProperties.LineProps(4, Colors.Black));
            VectorEditorCore.FigureSet.Figure r = new VectorEditorCore.FigureSet.Rect(prop);
            VectorEditorCore.FigureSet.Figure l = new VectorEditorCore.FigureSet.Line(prop);
            MemoryStream ms = new MemoryStream();

            r.WriteToStream(ms);
            l.WriteToStream(ms);

            ms.Position = 0;

            StreamReader sr = new StreamReader(ms);

            Console.WriteLine(sr.ReadToEnd());
        }

        [TestMethod]
        public void LoadToStream()
        {
            string str = "<Rect> xa = 1 xb = 2 ya = 3 yb = 4 </Rect>_" + 
                "<FillProps> A = 255 R = 0 G = 100 B = 200 </FillProps>_<LineProps> line = 3 " + 
                "lineColorA = 255 lineColorR = 0 " +
                "lineColorG = 0 lineColorB = 0 </LineProps>_\r\n" +
                "<Line> xa = 100 xb = 200 ya = 300 yb = 400 </Line>_<LineProps> line = 3 " +
                "lineColorA = 255 lineColorR = 0 " +
                "lineColorG = 0 lineColorB = 0 </LineProps>_\r\n";
            byte[] buffByte = Encoding.ASCII.GetBytes(str);
            MemoryStream msLoad = new MemoryStream(buffByte);
            MemoryStream ms = new MemoryStream();
            VectorEditorCore.FigureProperties.PropSet propSet1 = new VectorEditorCore.FigureProperties.PropSet();
            propSet1.Add(new VectorEditorCore.FigureProperties.FillProps(new Color()));
            propSet1.Add(new VectorEditorCore.FigureProperties.LineProps(4, Colors.Black));
            VectorEditorCore.FigureSet.Figure rect = new VectorEditorCore.FigureSet.Rect(propSet1);
            VectorEditorCore.FigureProperties.PropSet propSet2 = new VectorEditorCore.FigureProperties.PropSet();
            propSet2.Add(new VectorEditorCore.FigureProperties.LineProps(2, Colors.Black));
            VectorEditorCore.FigureSet.Figure line = new VectorEditorCore.FigureSet.Line(propSet2);

            rect.LoadFromStream(msLoad);
            line.LoadFromStream(msLoad);
            rect.WriteToStream(ms);
            line.WriteToStream(ms);

            ms.Position = 0;
            msLoad.Position = 0;

            byte[] buff = new byte[ms.Length];
            ms.Read(buff, (int)ms.Position, (int)ms.Length);
            string s1 = Encoding.ASCII.GetString(buff);

            buff = new byte[msLoad.Length];
            msLoad.Read(buff, (int)msLoad.Position, (int)msLoad.Length);
            string s2 = Encoding.ASCII.GetString(buff);

            StringAssert.Equals(s1, s2);

        }
    }
}
