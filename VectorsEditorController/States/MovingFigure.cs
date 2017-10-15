using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    public class MovingFigure : StateBase
    {
        public MovingFigure(IStateMachine stateMachine, IFigureManager figureManager)
            : base(stateMachine, figureManager)
        {

        }

        public override void MouseUp(int x, int y)
        {
            _stateMachine.SetState(StateType.SelectFigure);            
            _figureManager.ReDraw();
        }

        public override void MouseDown(int x, int y)
        {
        }

        public override void MouseMove(int x, int y)
        {
            _figureManager.MoveCatched(x, y);
            _figureManager.ReDraw();
        }

        public override void Delete()
        {
            
        }

        public override void Escape()
        {
            
        }
    }
}
