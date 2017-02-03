using System.IO;

namespace VectorEditorCore.IOData
{
	abstract public class DataInConverter
	{
        abstract public void Convert(Stream inStream, Stream outStream);
	}
}

