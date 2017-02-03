using System.IO;

namespace VectorEditorCore.IOData
{
	abstract public class DataOutConverter
	{
		abstract public void Convert(Stream inStream, Stream outStream);
	}
}

