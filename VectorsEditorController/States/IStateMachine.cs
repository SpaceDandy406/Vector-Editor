namespace VectorEditorController.States
{
    public interface IStateMachine
    {
        StateBase GetCurrentState();

        void SetState(StateType stateType);
    }
}