using VectorEditorCore.Selections;

namespace VectorEditorCore.Drawing
{
    public class Scene
    {
        private Plotter _plotter;
        private Store _store;
        private SelectionsList _selectList;

        public Scene(Plotter plotter, Store store, SelectionsList selectList)
        {
            _plotter = plotter;
            _store = store;
            _selectList = selectList;
        }

        public void Plot()
        {
            _plotter.BeginDraw();

            foreach (var figure in _store)
                figure.Draw(_plotter);

            foreach (var item in _selectList)
                item.DrawSelf(_plotter);

            _plotter.EndDraw();
        }
    }
}
