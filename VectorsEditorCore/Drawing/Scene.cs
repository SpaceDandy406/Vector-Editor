using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditorCore.Selections;

namespace VectorEditorCore.Drawing
{
    public class Scene
    {
        public Plotter Plotter { get; set; }
        public Store Store { get; set; }
        public SelectionsList selectList { get; set; }

        public Scene(Plotter _plotter, Store _store, SelectionsList _selectList)
        {
            this.Plotter = _plotter;
            this.Store = _store;
            this.selectList = _selectList;
        }

        public void Plot()
        {
            Plotter.BeginDraw();

            foreach (var figure in this.Store)
                figure.DrawSelf(this.Plotter);

            foreach (var item in selectList)
                item.DrawSelf(Plotter);

            Plotter.EndDraw();
        }
    }
}
