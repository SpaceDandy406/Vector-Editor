using System.Windows.Media;
using VectorEditorController.Interfaces;
using VectorEditorController.States;
using VectorEditorCore;
using VectorEditorCore.Interfaces;
using VectorsEditorCore.Interfaces;

namespace VectorEditorController
{
    public class Command1 : ICommand1
    {
        private readonly StateContainer _insideStateContainer;
        private readonly StateList _insideStateList;
        private readonly IFigureManager _figureManager;
        private readonly ISavable _saver;
        private readonly ILoadable _loader;

        public Command1(StateList stateList, StateContainer stateContainer, IFigureManager figureManager, ISavable saver, ILoadable loader)
        {
            _insideStateContainer = stateContainer;
            _insideStateList = stateList;
            _figureManager = figureManager;
            _saver = saver;
            _loader = loader;
        }

        public void Save(string filePath)
        {
            _saver.Save(filePath);
            _insideStateContainer.SetState(_insideStateList[(int)StateType.InitialState]);
        }

        public void Load(string filePath)
        {
            _figureManager.Clear();

            _loader.Load(filePath);
            _insideStateContainer.SetState(_insideStateList[(int)StateType.InitialState]);
            ReDraw();
        }

        public void SetFigure(FigureType figureType)
        {
            _figureManager.SetFigureType(figureType);
        }

        public void SetFigureFillColor(Color color)
        {
            _figureManager.SetFigureFillColor(color);
            _figureManager.SetFigureFactoryFillColor(color);
            ReDraw();
        }

        public void SetFigureLineColor(Color color)
        {
            _figureManager.SetFigureLineColor(color);
            _figureManager.SetFigureFactoryLineColor(color);
            ReDraw();
        }

        public void SetFigureLineSize(int lineSize)
        {
            _figureManager.SetFigureLineSize(lineSize);
            ReDraw();
        }

        public void ReDraw()
        {
            _figureManager.ReDraw();
        }

        public void CreateFigure()
        {
            _insideStateContainer.SetState(_insideStateList[(int) StateType.CreateFigure]);
            ReDraw();
        }

        public void SetMultiSelect(bool shiftIsDown)
        {
            _figureManager.SetMultiSelect(shiftIsDown);
            ReDraw();
        }
    }
}
