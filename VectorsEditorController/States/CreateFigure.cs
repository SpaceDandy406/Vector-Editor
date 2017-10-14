using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    public class CreateFigure : StateBase
    {
        public CreateFigure(StateMachine stateMachine, IFigureManager figureManager)
            : base(stateMachine, figureManager)
        {
        }

        public override void MouseUp(int x, int y)
        {
        }

        public override void MouseDown(int x, int y)
        {
            _figureManager.CreateFigure(x, y);

            _figureManager.ReDraw();

            _stateMachine.SetState(StateType.StretchFigure);
        }

        public override void MouseMove(int x, int y)
        {
            
        }

        public override void Delete()
        {
            
        }

        public override void Escape()
        {
            _stateMachine.SetState(StateType.InitialState);            
        }
    }
}
