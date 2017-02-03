using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditorController.Interfaces;
using VectorEditorController.States;

namespace VectorEditorController
{
    public class StateContainer : IEvent
    {
        private State insideState { get; set; }

        public void SetState(State state)
        {
            this.insideState = state;
        }

        public void MouseUp(int x, int y)
        {
            this.insideState.MouseUp(x, y);
        }

        public void MouseDown(int x, int y)
        {
            this.insideState.MouseDown(x, y);
        }

        public void MouseMove(int x, int y)
        {
            this.insideState.MouseMove(x, y);
        }

        public  void Delete()
        {
            insideState.Delete();
        }

        public void Escape()
        {
            this.insideState.Escape();
        }
    }
}
