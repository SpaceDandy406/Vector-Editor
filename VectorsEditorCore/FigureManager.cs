using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using VectorEditorCore.Drawing;
using VectorEditorCore.FigureSet;
using VectorEditorCore.Interfaces;
using VectorEditorCore.IOData;
using VectorEditorCore.Selections;

namespace VectorEditorCore
{
    enum MoveType
    {
        MOVEFIGURE,
        MOVEMARKER,
        MOVENEWFIGURE
    }

    public class FigureManager : IFigureManager
    {
        Plotter plotter { get; set; }
        Scene scene { get; set; }
        Store _store { get; set; }
        SelectionsList selectList { get; set; }
        FigureFactory figureFactory { get; set; }
        Saver saver { get; set; }
        Loader loader { get; set; }
        MoveType moveType;
        IForm form;

        bool multiSelect;
        int catchedX, catchedY;
        int numberOfMoveMarker;

        public FigureManager(IForm form, Image image, Store store)
        {
            this.plotter = new Plotter(image);
            _store = store;
            this.selectList = new SelectionsList();
            this.scene = new Scene(this.plotter, this._store, this.selectList);
            this.figureFactory = new FigureFactory();
            this.saver = new Saver(this._store);
            this.loader = new Loader(this._store);
            this.moveType = new MoveType();
            this.form = form;
            multiSelect = false;
            catchedY = catchedX = 0;
            numberOfMoveMarker = 0;
        }

        public void ReDraw()
        {
            scene.Plot();
        }

        public void SetFigureType(FigureType type)
        {
            this.figureFactory.SetFigureType(type);
        }

        public void CreateFigure(int x, int y)
        {
            moveType = MoveType.MOVENEWFIGURE;

            catchedX = x;
            catchedY = y;
            numberOfMoveMarker = 2;

            selectList.Clear();
            
            Figure temp = this.figureFactory.CreateFigure();
            this._store.Add(temp);
            this.selectList.Add(new Selection(temp));

            Color nullColor = new Color() { A = 0, R = 255, G = 255, B = 255 };

            foreach (var item in selectList)
            {
                item.SetFigureFrame(x, x, y, y);
                item.SetFigureFillColor(nullColor);
            }

            SetChangedPropirties();
        }

        public void SetFigureFillColor(Color color)
        {
            foreach (var item in this.selectList)
            {
                item.SetFigureFillColor(color);
            }
        }

        public void SetFigureFactoryFillColor(Color color)
        {
            figureFactory.SetFillColor(color);
        }

        public void SetFigureLineSize(int lineSize)
        {
            figureFactory.SetLineSize(lineSize);

            foreach (var item in this.selectList)
            {
                item.SetFigureLineSize(lineSize);
            }
        }

        public void DeleteSelectedFigure()
        {
            foreach (var selection in selectList)
            {
                _store.Remove(selection.insideFigure);
            }

            selectList.Clear();
        }

        public bool CatchFigure(int x, int y)
        {
            moveType = MoveType.MOVEFIGURE;

            catchedX = x;
            catchedY = y;

            foreach (var item in _store)
            {
                if (multiSelect && item.IsHitToFigure(x, y))
                {
                    var item1 = item;

                    if (!selectList.Exists(selection => selection.insideFigure == item1))
                        selectList.Add(new Selection(item));

                    SetChangedPropirties();

                    return true;
                } 
                else if (item.IsHitToFigure(x, y))
                {
                    selectList.Clear();
                    selectList.Add(new Selection(item));

                    SetChangedPropirties();

                    return true;
                }
            }

            selectList.Clear();

            SetChangedPropirties();

            return false;
        }

        public bool CatchMarker(int x, int y)
        {
            moveType = MoveType.MOVEMARKER;

            catchedX = x;
            catchedY = y;

            foreach (var item in selectList.Where(item => item.IsHitToMarker(x, y)))
            {
                numberOfMoveMarker = item.ReturnNumberOfCatchedMarker();
                return true;
            }

            return false;
        }

        public void MoveCatched(int x, int y)
        {
            var tempX = x - catchedX;
            var tempY = y - catchedY;

            switch (moveType)
            {
                case MoveType.MOVEFIGURE:
                    foreach (var item in selectList)
                        item.MoveFigure(tempX, tempY);

                    catchedX = x;
                    catchedY = y;
                    break;
                case MoveType.MOVEMARKER:
                    foreach (var item in this.selectList)
                        item.MoveCurrentMarker(tempX, tempY, numberOfMoveMarker);

                    catchedX = x;
                    catchedY = y;
                    break;
                case MoveType.MOVENEWFIGURE:
                    foreach (var item in this.selectList)
                        item.MoveCurrentMarker(tempX, tempY, 2);

                    catchedX = x;
                    catchedY = y;
                    break;
            }
        }

        public void DropCatched()
        {
            this.selectList.Clear();
        }

        public void SetMultiSelect(bool shiftIsDown)
        {
            multiSelect = shiftIsDown;
        }

        public void Clear()
        {
            selectList.Clear();
            _store.Clear();
        }

        public void SetChangedPropirties()
        {
            if (selectList.Capacity != 0)
            {
                form.FillColor = selectList.GetFillColorOpportunity();
                form.LineColor = selectList.GetLineColorOpportunity();
                form.LineSize = selectList.GetLineSizeOpportunity();
            }
            else
            {
                form.FillColor = true;
                form.LineColor = true;
                form.LineSize = true;
            }
        }

        public void SetFigureFactoryLineColor(Color color)
        {
            figureFactory.SetLineColor(color);
        }

        public void SetFigureLineColor(Color color)
        {
            foreach (var item in this.selectList)
            {
                item.SetFigureLineColor(color);
            }
        }
    }
}
