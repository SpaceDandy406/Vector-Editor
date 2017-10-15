using System.Collections.Generic;
using VectorEditorCore.Interfaces;

namespace VectorEditorController.States
{
    public class StateMachine : IStateMachine
    {
        private readonly List<StateBase> _stateList = new List<StateBase>();
        private StateBase _currentState;

        public StateMachine(IFigureManager figureManager)
        {
            _stateList.Add(new CreateFigure(this, figureManager));
            _stateList.Add(new InitialState(this, figureManager));
            _stateList.Add(new MovingFigure(this, figureManager));
            _stateList.Add(new SelectFigure(this, figureManager));
            _stateList.Add(new StretchFigure(this, figureManager));

            _currentState = _stateList[(int)StateType.InitialState];
        }

        public StateBase GetCurrentState()
        {
            return _currentState;
        }

        public void SetState(StateType stateType)
        {            
            _currentState = _stateList[(int)stateType];
        }
    }
}
