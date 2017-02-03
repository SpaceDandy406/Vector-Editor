using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditorCore;

namespace VectorEditorController.Interfaces
{
    interface ICommand
    {
        void Save(string filePath);

        void Load(string filePath);

        void SetFigure(FigureType figureType);

        void CreateFigure();

        void ReDraw();

        void SetFigureFillColor(System.Windows.Media.Color color);

        void SetFigureLineSize(int lineSize);

        void SetMultiSelect(bool shiftIsDown);

        void SetFigureLineColor(System.Windows.Media.Color color);
    }
}
