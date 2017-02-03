using System.IO;

namespace VectorEditorCore.IOData
{
	public class TxtSaveAlgorythm : DataOutConverter
	{
        public override void Convert(Stream inStream, Stream outStream)
        {
            inStream.Position = 0;
            using (StreamWriter sw = new StreamWriter(outStream))
            {
                using (StreamReader sr = new StreamReader(inStream))
                {
                    string str;
                    while ((str = sr.ReadLine()) != null)
                    {
                        sw.WriteLine(str);
                    }
                }
            }
        }
	}
}

