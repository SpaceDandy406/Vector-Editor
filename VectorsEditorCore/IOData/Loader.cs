using System;
using System.IO;
using System.Text;
using VectorEditorCore.FigureSet;
using VectorsEditorCore.Interfaces;

namespace VectorEditorCore.IOData
{
	public class Loader : ILoadable
	{
        private readonly FigureFactory _figureFactory = new FigureFactory();
        private readonly Store _store;

        public Loader(Store store)
        {
            _store = store;
        }

        public void Load(string fileName)
        {
            using (var insideStream = new MemoryStream())
            {
                using (var outStream = new FileStream(fileName, FileMode.Open))
                {
                    var dataInConverter = new TxtLoadAlgorythm();

                    dataInConverter.Convert(insideStream, outStream);
                }

                insideStream.Position = 0;

                string str;

                while ((insideStream.Length) != (insideStream.Position))
                {
                    str = ReadTagFromStream(insideStream);

                    if (str.StartsWith("<Rect>"))
                    {
                        _figureFactory.SetFigureType(FigureType.RECT);
                        Figure figure = _figureFactory.CreateFigure();
                        figure.LoadFromStream(insideStream);                        
                        _store.Add(figure);
                    }
                    else if (str.StartsWith("<Line>"))
                    {
                        _figureFactory.SetFigureType(FigureType.LINE);
                        Figure figure = _figureFactory.CreateFigure();
                        figure.LoadFromStream(insideStream);
                        _store.Add(figure);
                    }
                    else if (str.StartsWith("<Ellipse>"))
                    {
                        _figureFactory.SetFigureType(FigureType.ELLIPSE);
                        Figure figure = _figureFactory.CreateFigure();
                        figure.LoadFromStream(insideStream);
                        _store.Add(figure);
                    }
                    insideStream.ReadByte();
                    insideStream.ReadByte();
                }
            }
        }

        private string ReadTagFromStream(Stream stream)
        {
            byte buff;
            StringBuilder strBuilder = new StringBuilder();

            buff = (byte)stream.ReadByte();

            while ((char)buff != '>')
            {
                strBuilder.Append(Convert.ToChar(buff));
                buff = (byte)stream.ReadByte();
            }

            strBuilder.Append(Convert.ToChar(buff));

            return strBuilder.ToString();
        }
	}
}

