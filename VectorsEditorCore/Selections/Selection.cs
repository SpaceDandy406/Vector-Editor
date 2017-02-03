using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using VectorEditorCore.Drawing;
using VectorEditorCore.FigureSet;

namespace VectorEditorCore.Selections
{
    public class Marker
    {
        public int x, y;
        public int catchedX, catchedY;
        
        public bool IsHit(int _x, int _y)
        {
            if ((_x > this.x) && (_x < this.x + 11) && (_y > this.y) && (_y < this.y + 11))
                return true;
            return false;
        }

        public void Move(int _x, int _y)
        {
            this.x = x + _x;
            this.y = y + _y;
        }
    }

    public class Selection
    {
        public bool FillColorOpportunity { get; set; }
        public bool LineColorOpportunity { get; set; }
        public bool LineSizeOpportunity { get; set; }

        public Figure insideFigure { get; set; }        
        int numberOfCatchedMarker;

        public Selection(Figure _figure)
        {
            this.insideFigure = _figure;
            numberOfCatchedMarker = 0;

            FillColorOpportunity = true;
            LineColorOpportunity = true;
            LineSizeOpportunity = true;

            if (insideFigure is Line)
                FillColorOpportunity = false;
        }

        public void SetFigureFrame(int x1, int x2, int y1, int y2)
        {
            insideFigure.SetFrame(x1, x2, y1, y2);
        }

        public void SetFigureFillColor(Color color)
        {
            insideFigure.SetFigureFillColor(color);
        }

        public void SetFigureLineSize(int lineSize)
        {
            insideFigure.SetFigureLineSize(lineSize);
        }

        public void SetMarkersCatchPoint(int x, int y)
        {
            foreach (var item in insideFigure.listMarker)
            {
                item.catchedX = x;
                item.catchedY = y;
            }
        }

        public bool IsHitToMarker(int x, int y)
        {
            int i = 0;

            foreach (var item in insideFigure.listMarker)
            {
                if (item.IsHit(x, y))
                {
                    item.catchedX = x;
                    item.catchedY = y;

                    numberOfCatchedMarker = i;

                    return true;
                }

                i++;
            }
            return false;
        }

        public int ReturnNumberOfCatchedMarker()
        {
            return numberOfCatchedMarker;
        }

        public void MoveMarker(int x, int y)
        {
            insideFigure.MoveMarker(x, y, numberOfCatchedMarker);
        }

        public void MoveCurrentMarker(int x, int y, int numberOfMoveMarker)
        {
            insideFigure.MoveMarker(x, y, numberOfMoveMarker);
        }

        public void MoveFigure(int x, int y)
        {
            insideFigure.MoveFigure(x, y);
        }

        public void DrawSelf(Plotter plotter)
        {
            if (insideFigure is Line)
            {
                var newListMarkers = new List<Marker>()
                {
                    insideFigure.listMarker[0],
                    insideFigure.listMarker[2]
                };

                plotter.DrawSelection(newListMarkers);

                return;
            }

            plotter.DrawSelection(insideFigure.listMarker);
        }

        public void SetFigureLineColor(Color color)
        {
            insideFigure.SetFigureLineColor(color);
        }
    }
}
