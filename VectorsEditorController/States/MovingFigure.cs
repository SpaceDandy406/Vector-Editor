using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditorCore;
using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    public class MovingFigure : State
    {
        public MovingFigure(StateList stateList, StateContainer stateContainer, IFigureManager figureManager)
            : base(stateList, stateContainer, figureManager)
        {

        }

        public override void MouseUp(int x, int y)
        {
            stateContainer.SetState(stateList[(int)StateType.SelectFigure]);
            figureManager.ReDraw();
        }

        public override void MouseDown(int x, int y)
        {
        }

        public override void MouseMove(int x, int y)
        {
            figureManager.MoveCatched(x, y);
            figureManager.ReDraw();
        }

        public override void Delete()
        {
            
        }

        public override void Escape()
        {
            
        }
    }
}
