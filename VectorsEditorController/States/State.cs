using System.Collections.Generic;
using VectorEditorController.Interfaces;
using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    abstract public class StateBase : IEvent
    {
        protected readonly StateMachine _stateMachine;
        protected IFigureManager _figureManager;


        public StateBase(StateMachine stateMachine, IFigureManager figureManager)
        {
            _stateMachine = stateMachine;
            _figureManager = figureManager;
        }

        abstract public void MouseUp(int x, int y);

        abstract public void MouseDown(int x, int y);

        abstract public void MouseMove(int x, int y);

        abstract public void Delete();

        abstract public void Escape();
    }
}
