using System.IO;
using VectorsEditorCore.Interfaces;

namespace VectorEditorCore.IOData
{
	public class Saver : ISavable
	{
        private readonly Store _store;

        public Saver(Store store)
        {
            _store = store;
        }

		public void Save(string fileName)
		{
            using (var insideStream = new MemoryStream())
            {
                using (var outFileStream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    var dataOutConverter = new TxtSaveAlgorythm();

                    WriteToInsideStream(insideStream);

                    dataOutConverter.Convert(insideStream, outFileStream);
                }
            }
		}

		private void WriteToInsideStream(Stream stream)
		{
			foreach (var figure in _store)
				figure.WriteToStream (stream);
		}
	}
}

