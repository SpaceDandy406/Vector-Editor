using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditorCore;
using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    public class SelectFigure : State
    {
        public SelectFigure(StateList stateList, StateContainer stateContainer, IFigureManager figureManager)
            : base(stateList, stateContainer, figureManager)
        {

        }

        public override void MouseUp(int x, int y)
        {
            
        }

        public override void MouseDown(int x, int y)
        {
            if (figureManager.CatchMarker(x, y))
                stateContainer.SetState(stateList[(int)StateType.StretchFigure]);
            else if (figureManager.CatchFigure(x, y))
                stateContainer.SetState(stateList[(int)StateType.MovingFigure]);
            else
                stateContainer.SetState(stateList[(int)StateType.InitialState]);

            figureManager.ReDraw();
        }

        public override void MouseMove(int x, int y)
        {
            
        }

        public override void Delete()
        {
            figureManager.DeleteSelectedFigure();
            figureManager.ReDraw();
        }

        public override void Escape()
        {
            stateContainer.SetState(stateList[(int)StateType.InitialState]);
            figureManager.DropCatched();
            figureManager.ReDraw();
        }
    }
}
