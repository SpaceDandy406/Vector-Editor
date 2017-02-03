using System.IO;
using System.Windows.Controls;

namespace VectorEditorCore.FigureProperties
{
	public class BaseProps
	{
		public BaseProps ()
		{
		}

        public BaseProps GetClone()
        {
            return (BaseProps)this.MemberwiseClone();
        }

		virtual public void WriteToStream(Stream stream)
		{
		}

		virtual public void LoadFromStream(Stream stream)
		{
		}

        virtual public void DrawSelf(Drawing.Plotter _plotter)
        {

        }
	}
}

