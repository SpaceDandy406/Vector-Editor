using System;
using System.Windows.Media;
using VectorEditorCore.FigureProperties;
using VectorEditorCore.FigureSet;


namespace VectorEditorCore
{
	public class FigureFactory
	{
        FigureType figureType;
        PropSet propSet;
        FillProps fillProps;
        LineProps lineProps;

		public FigureFactory ()
		{
            figureType = new FigureType();
            var c = new Color();
            c = Color.FromRgb(255, 255, 255);
            fillProps = new FillProps(c);
            lineProps = new LineProps(1, Colors.Black);
		}

		public void SetFigureType(FigureType _figureType)
		{
            this.figureType = _figureType;
		}

		public Figure CreateFigure()
		{
            Figure figure = null;
            propSet = new PropSet();

            switch (figureType)
            {
                case FigureType.RECT:
                    propSet.Add(fillProps.GetClone());
                    propSet.Add(lineProps.GetClone());
                    figure = new Rect(propSet);
                    break;
                case FigureType.LINE:
                    propSet.Add(lineProps.GetClone());
                    figure = new Line(propSet);
                    break;
                case FigureType.ELLIPSE:
                    propSet.Add(fillProps.GetClone());
                    propSet.Add(lineProps.GetClone());
                    figure = new Ellipse(propSet);
                    break;
                default:
                    break;
            }
			return figure;
		}

        public void SetFillColor(Color _color)
        {
            fillProps.Color = _color;
        }

        public void SetLineSize(int _size)
        {
            lineProps.LineSize = _size;
        }

        public void SetLineColor(Color color)
        {
            lineProps.LineColor = color;
        }
    }
}

