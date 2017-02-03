using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VectorEditorCore.Interfaces
{
    public interface IFigureManager
    {
        void Save(string filePath);

        void Load(string filePath);

        void SetFigureType(FigureType figureType);

        void SetFigureFillColor(Color color);

        void SetFigureFactoryFillColor(Color color);

        void SetFigureLineSize(int lineSize);

        void CreateFigure(int x, int y);

        bool CatchFigure(int x, int y);

        bool CatchMarker(int x, int y);

        void MoveCatched(int x, int y);

        void ReDraw();

        void DropCatched();

        void DeleteSelectedFigure();

        void SetMultiSelect(bool shiftIsDown);

        void Clear();

        void SetFigureFactoryLineColor(Color color);

        void SetFigureLineColor(Color color);
    }
}
