using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditorCore;
using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    public class StateList : List<State>
    {
        public StateList(StateContainer stateContainer, IFigureManager figureManager)
        {
            this.Add(new CreateFigure(this, stateContainer, figureManager));
            this.Add(new InitialState(this, stateContainer, figureManager));
            this.Add(new MovingFigure(this, stateContainer, figureManager));
            this.Add(new SelectFigure(this, stateContainer, figureManager));
            this.Add(new StretchFigure(this, stateContainer, figureManager));
            stateContainer.SetState(this[(int)StateType.InitialState]);
        }
    }
}
