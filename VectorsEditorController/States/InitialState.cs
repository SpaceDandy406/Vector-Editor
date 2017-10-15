using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    public class InitialState : StateBase
    {
        bool isCatch;

        public InitialState(IStateMachine stateMachine, IFigureManager figureManager)
            : base(stateMachine, figureManager)
        {
            isCatch = false;
        }

        public override void MouseUp(int x, int y)
        {
            if (_figureManager.CatchFigure(x, y) && isCatch)
            {
                _stateMachine.SetState(StateType.SelectFigure);
            }
            _figureManager.ReDraw();
        }

        public override void MouseDown(int x, int y)
        {
            if (_figureManager.CatchFigure(x, y))
            {
                isCatch = true;
                _figureManager.ReDraw();
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
