using System.IO;

namespace VectorEditorCore.IOData
{
	public class Saver
	{
		Store store;
		DataOutConverter dataOutConverter;
		MemoryStream insideStream;
        FileStream outFileStream;

		public Saver (Store store)
		{
			this.store = store;
		}

		public void Save(string fileName)
		{
            using (insideStream = new MemoryStream())
            {
                using (outFileStream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    dataOutConverter = new TxtSaveAlgorythm();

                    WriteToInsideStream();

                    dataOutConverter.Convert(insideStream, outFileStream);
                }
            }
		}

		private void WriteToInsideStream()
		{
			foreach (var figure in store)
				figure.WriteToStream (insideStream);
		}
	}
}

