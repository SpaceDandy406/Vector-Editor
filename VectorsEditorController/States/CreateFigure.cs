using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditorCore;
using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    public class CreateFigure : State
    {
        public CreateFigure(StateList stateList, StateContainer stateContainer, IFigureManager figureManager)
            : base(stateList, stateContainer, figureManager)
        {
        }

        public override void MouseUp(int x, int y)
        {
        }

        public override void MouseDown(int x, int y)
        {
            figureManager.CreateFigure(x, y);

            figureManager.ReDraw();

            stateContainer.SetState(stateList[(int)StateType.StretchFigure]);
        }

        public override void MouseMove(int x, int y)
        {
            
        }

        public override void Delete()
        {
            
        }

        public override void Escape()
        {
            stateContainer.SetState(stateList[(int)StateType.InitialState]);
        }
    }
}
