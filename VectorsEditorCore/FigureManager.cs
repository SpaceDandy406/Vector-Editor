using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VectorEditorCore.Drawing;
using VectorEditorCore.Interfaces;
using VectorEditorCore.Selections;

namespace VectorEditorCore
{
    public class FigureManager : IFigureManager
    {
        private readonly Plotter _plotter;
        private readonly Scene _scene;
        private readonly Store _store;
        private readonly SelectionsList _selectList;
        private readonly FigureFactory _figureFactory;
        private readonly IForm _form;

        private MoveType _moveType;
        private bool _multiSelect;
        private Point _catchedPoint;
        private int _numberOfMoveMarker;

        public FigureManager(IForm form, Image image, Store store)
        {
            _store = store;
            _form = form;
            _plotter = new Plotter(image);
            _selectList = new SelectionsList();
            _scene = new Scene(_plotter, _store, _selectList);
            _figureFactory = new FigureFactory();
            _moveType = new MoveType();
            _multiSelect = false;
            _catchedPoint = new Point();
            _numberOfMoveMarker = 0;
        }

        public void ReDraw()
        {
            _scene.Plot();
        }

        public void SetFigureType(FigureType type)
        {
            _figureFactory.SetFigureType(type);
        }

        public void CreateFigure(int x, int y)
        {
            _moveType = MoveType.MOVENEWFIGURE;

            _catchedPoint = new Point(x, y);
            _numberOfMoveMarker = 2;

            _selectList.Clear();

            var temp = _figureFactory.CreateFigure();
            _store.Add(temp);
            _selectList.Add(new Selection(temp));

            var nullColor = new Color() { A = 0, R = 255, G = 255, B = 255 };

            foreach (var item in _selectList)
            {
                item.SetFigureFrame(x, x, y, y);
                item.SetFigureFillColor(nullColor);
            }

            SetChangedPropirties();
        }

        public void SetFigureFillColor(Color color)
        {
            foreach (var item in _selectList)
            {
                item.SetFigureFillColor(color);
            }
        }

        public void SetFigureFactoryFillColor(Color color)
        {
            _figureFactory.SetFillColor(color);
        }

        public void SetFigureLineSize(int lineSize)
        {
            _figureFactory.SetLineSize(lineSize);

            foreach (var item in _selectList)
            {
                item.SetFigureLineSize(lineSize);
            }
        }

        public void DeleteSelectedFigure()
        {
            foreach (var selection in _selectList)
            {
                _store.Remove(selection.insideFigure);
            }

            _selectList.Clear();
        }

        public bool CatchFigure(int x, int y)
        {
            _moveType = MoveType.MOVEFIGURE;

            _catchedPoint = new Point(x, y);

            foreach (var item in _store)
            {
                if (_multiSelect && item.IsHitToFigure(x, y))
                {
                    var item1 = item;

                    if (!_selectList.Exists(selection => selection.insideFigure == item1))
                        _selectList.Add(new Selection(item));

                    SetChangedPropirties();

                    return true;
                }
                else if (item.IsHitToFigure(x, y))
                {
                    _selectList.Clear();
                    _selectList.Add(new Selection(item));

                    SetChangedPropirties();

                    return true;
                }
            }

            _selectList.Clear();

            SetChangedPropirties();

            return false;
        }

        public bool CatchMarker(int x, int y)
        {
            _moveType = MoveType.MOVEMARKER;

            _catchedPoint = new Point(x, y);

            foreach (var item in _selectList.Where(item => item.IsHitToMarker(x, y)))
            {
                _numberOfMoveMarker = item.ReturnNumberOfCatchedMarker();
                return true;
            }

            return false;
        }

        public void MoveCatched(int x, int y)
        {
            var tempPoint = new Point(x - _catchedPoint.X, y - _catchedPoint.Y);

            switch (_moveType)
            {
                case MoveType.MOVEFIGURE:
                    foreach (var item in _selectList)
                        item.MoveFigure((int)tempPoint.X, (int)tempPoint.Y);

                    _catchedPoint = new Point(x, y);
                    break;
                case MoveType.MOVEMARKER:
                    foreach (var item in this._selectList)
                        item.MoveCurrentMarker((int)tempPoint.X, (int)tempPoint.Y, _numberOfMoveMarker);

                    _catchedPoint = new Point(x, y);
                    break;
                case MoveType.MOVENEWFIGURE:
                    foreach (var item in this._selectList)
                        item.MoveCurrentMarker((int)tempPoint.X, (int)tempPoint.Y, 2);

                    _catchedPoint = new Point(x, y);
                    break;
            }
        }

        public void DropCatched()
        {
            _selectList.Clear();
        }

        public void SetMultiSelect(bool shiftIsDown)
        {
            _multiSelect = shiftIsDown;
        }

        public void Clear()
        {
            _selectList.Clear();
            _store.Clear();
        }

        public void SetChangedPropirties()
        {
            if (_selectList.Capacity != 0)
            {
                _form.FillColor = _selectList.GetFillColorOpportunity();
                _form.LineColor = _selectList.GetLineColorOpportunity();
                _form.LineSize = _selectList.GetLineSizeOpportunity();
            }
            else
            {
                _form.FillColor = true;
                _form.LineColor = true;
                _form.LineSize = true;
            }
        }

        public void SetFigureFactoryLineColor(Color color)
        {
            _figureFactory.SetLineColor(color);
        }

        public void SetFigureLineColor(Color color)
        {
            foreach (var item in _selectList)
            {
                item.SetFigureLineColor(color);
            }
        }
    }
}
