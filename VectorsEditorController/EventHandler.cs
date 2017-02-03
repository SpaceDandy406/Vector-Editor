using System;
using System.Windows.Controls;
using System.Windows.Media;
using VectorEditorController.Interfaces;
using VectorEditorController.States;
using VectorEditorCore;
using VectorEditorCore.Interfaces;
using ICommand = VectorEditorController.Interfaces.ICommand;

namespace VectorEditorController
{

    public class EventHandler : IEvent, Interfaces.ICommand
    {
        StateList stateList;
        StateContainer stateContainer;
        Interfaces.ICommand command;
        IFigureManager figureManager;

        public EventHandler(IForm form, Image image)
        {
            figureManager = new VectorEditorCore.FigureManager(form, image);
            stateContainer = new StateContainer();
            stateList = new StateList(stateContainer, figureManager);
            command = new Command(stateList, stateContainer, figureManager);
        }

        public void MouseUp(int x, int y)
        {
            stateContainer.MouseUp(x, y);
        }

        public void MouseDown(int x, int y)
        {
            stateContainer.MouseDown(x, y);
        }

        public void MouseMove(int x, int y)
        {
            stateContainer.MouseMove(x, y);
        }

        public void Delete()
        {
            stateContainer.Delete();
        }

        public void Escape()
        {
            stateContainer.Escape();
        }

        public void Save(string filePath)
        {
            command.Save(filePath);
        }

        public void Load(string filePath)
        {
            command.Load(filePath);
        }

        public void SetFigure(FigureType figureType)
        {
            command.SetFigure(figureType);
        }

        public void SetFigureFillColor(Color color)
        {
            command.SetFigureFillColor(color);
        }

        public void CreateFigure()
        {
            command.CreateFigure();
        }

        public void ReDraw()
        {
            command.ReDraw();
        }

        public void SetFigureLineSize(int lineSize)
        {
            command.SetFigureLineSize(lineSize);
        }

        public void SetMultiSelect(bool shiftIsDown)
        {
            command.SetMultiSelect(shiftIsDown);
        }

        public void SetFigureLineColor(Color color)
        {
            command.SetFigureLineColor(color);
        }
    }
}
