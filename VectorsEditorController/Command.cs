using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using VectorEditorController.Interfaces;
using VectorEditorController.States;
using VectorEditorCore;
using VectorEditorCore.Interfaces;

namespace VectorEditorController
{
    public class Command : ICommand
    {
        StateContainer insideStateContainer;
        StateList insideStateList;
        IFigureManager figureManager;

        public Command(StateList stateList, StateContainer stateContainer, IFigureManager figureManager)
        {
            this.insideStateContainer = stateContainer;
            this.insideStateList = stateList;
            this.figureManager = figureManager;
        }

        public void Save(string filePath)
        {
            figureManager.Save(filePath);
            insideStateContainer.SetState(insideStateList[(int)StateType.InitialState]);
        }

        public void Load(string filePath)
        {
            figureManager.Clear();

            figureManager.Load(filePath);
            insideStateContainer.SetState(insideStateList[(int)StateType.InitialState]);
            ReDraw();
        }

        public void SetFigure(FigureType figureType)
        {
            figureManager.SetFigureType(figureType);
        }

        public void SetFigureFillColor(Color color)
        {
            figureManager.SetFigureFillColor(color);
            figureManager.SetFigureFactoryFillColor(color);
            ReDraw();
        }

        public void SetFigureLineColor(Color color)
        {
            figureManager.SetFigureLineColor(color);
            figureManager.SetFigureFactoryLineColor(color);
            ReDraw();
        }

        public void SetFigureLineSize(int lineSize)
        {
            figureManager.SetFigureLineSize(lineSize);
            ReDraw();
        }

        public void ReDraw()
        {
            figureManager.ReDraw();
        }

        public void CreateFigure()
        {
            insideStateContainer.SetState(insideStateList[(int) StateType.CreateFigure]);
            ReDraw();
        }

        public void SetMultiSelect(bool shiftIsDown)
        {
            figureManager.SetMultiSelect(shiftIsDown);
            ReDraw();
        }
    }
}
