using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditorCore;
using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    public class InitialState : State
    {
        bool isCatch;

        public InitialState(StateList stateList, StateContainer stateContainer, IFigureManager figureManager)
            : base(stateList, stateContainer, figureManager)
        {
            isCatch = false;
        }

        public override void MouseUp(int x, int y)
        {
            if (figureManager.CatchFigure(x, y) && isCatch)
            {
                stateContainer.SetState(stateList[(int)StateType.SelectFigure]);
            }
            figureManager.ReDraw();
        }

        public override void MouseDown(int x, int y)
        {
            if (figureManager.CatchFigure(x, y))
            {
                isCatch = true;
                figureManager.ReDraw();
            }
        }

        public override void MouseMove(int x, int y)
        {
            
        }

        public override void Delete()
        {
            
        }

        public override void Escape()
        {
            
        }
    }
}
