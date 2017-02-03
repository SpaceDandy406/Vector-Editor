using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VectorEditorCore.IOData
{
    class TxtLoadAlgorythm:DataInConverter
    {

        public override void Convert(Stream inStream, Stream outStream)
        {
            StreamWriter sw = new StreamWriter(inStream);

            StreamReader sr = new StreamReader(outStream);

            string str;
            while ((str = sr.ReadLine()) != null)
            {
                sw.WriteLine(str);
            }
            sw.Flush();
        }
    }
}
