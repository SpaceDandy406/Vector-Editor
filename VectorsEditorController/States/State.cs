using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditorController.Interfaces;
using VectorEditorCore;
using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    abstract public class State : IEvent
    {
        protected StateList stateList;
        protected StateContainer stateContainer;
        protected IFigureManager figureManager;


        public State(StateList stateList, StateContainer stateContainer, IFigureManager figureManager)
        {
            this.stateList = stateList;
            this.stateContainer = stateContainer;
            this.figureManager = figureManager;
        }

        abstract public void MouseUp(int x, int y);

        abstract public void MouseDown(int x, int y);

        abstract public void MouseMove(int x, int y);

        abstract public void Delete();

        abstract public void Escape();
    }
}
