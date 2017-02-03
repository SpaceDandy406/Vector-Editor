using System;
using System.IO;
using System.Text;
using VectorEditorCore.FigureSet;

namespace VectorEditorCore.IOData
{
	public class Loader
	{
        Store store;
        DataInConverter dataInConverter;
        MemoryStream insideStream;
        FileStream outStream;
        FigureFactory figureFactory;

		public Loader (Store _store, FigureFactory _figureFactory)
		{
            this.store = _store;
		    figureFactory = _figureFactory;
		}

        public void Load(string fileName)
        {
            using (insideStream = new MemoryStream())
            {
                using (outStream = new FileStream(fileName, FileMode.Open))
                {
                    dataInConverter = new TxtLoadAlgorythm();

                    dataInConverter.Convert(insideStream, outStream);
                }

                insideStream.Position = 0;

                string str;

                while ((insideStream.Length) != (insideStream.Position))
                {
                    str = ReadTagFromStream();

                    if (str.StartsWith("<Rect>"))
                    {
                        figureFactory.SetFigureType(FigureType.RECT);
                        Figure figure = figureFactory.CreateFigure();
                        figure.LoadFromStream(insideStream);                        
                        store.Add(figure);
                    }
                    else if (str.StartsWith("<Line>"))
                    {
                        figureFactory.SetFigureType(FigureType.LINE);
                        Figure figure = figureFactory.CreateFigure();
                        figure.LoadFromStream(insideStream);
                        store.Add(figure);
                    }
                    else if (str.StartsWith("<Ellipse>"))
                    {
                        figureFactory.SetFigureType(FigureType.ELLIPSE);
                        Figure figure = figureFactory.CreateFigure();
                        figure.LoadFromStream(insideStream);
                        store.Add(figure);
                    }
                    insideStream.ReadByte();
                    insideStream.ReadByte();
                }
            }
        }

        private string ReadTagFromStream()
        {
            byte buff;
            StringBuilder strBuilder = new StringBuilder();

            buff = (byte) insideStream.ReadByte();

            while ((char)buff != '>')
            {
                strBuilder.Append(Convert.ToChar(buff));
                buff = (byte)insideStream.ReadByte();
            }

            strBuilder.Append(Convert.ToChar(buff));

            return strBuilder.ToString();
        }
	}
}

