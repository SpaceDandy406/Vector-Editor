using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    public class SelectFigure : StateBase
    {
        public SelectFigure(IStateMachine stateMachine, IFigureManager figureManager)
            : base(stateMachine, figureManager)
        {

        }

        public override void MouseUp(int x, int y)
        {
            
        }

        public override void MouseDown(int x, int y)
        {
            if (_figureManager.CatchMarker(x, y))
                _stateMachine.SetState(StateType.StretchFigure);
            else if (_figureManager.CatchFigure(x, y))
                _stateMachine.SetState(StateType.MovingFigure);
            else
                _stateMachine.SetState(StateType.InitialState);

            _figureManager.ReDraw();
        }

        public override void MouseMove(int x, int y)
        {
            
        }

        public override void Delete()
        {
            _figureManager.DeleteSelectedFigure();
            _figureManager.ReDraw();
        }

        public override void Escape()
        {
            _stateMachine.SetState(StateType.InitialState);
            _figureManager.DropCatched();
            _figureManager.ReDraw();
        }
    }
}
