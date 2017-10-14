namespace VectorEditorController.Interfaces
{
    interface IEvent
    {
        void MouseUp(int x, int y);

        void MouseDown(int x, int y);

        void MouseMove(int x, int y);

        void Delete();

        void Escape();
    }
}
