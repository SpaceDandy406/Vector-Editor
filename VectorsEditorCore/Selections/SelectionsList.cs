using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditorCore.Selections
{
    public class SelectionsList : List<Selection>
    {
        public bool GetFillColorOpportunity()
        {
            return this.All(selection => selection.FillColorOpportunity);
        }

        public bool GetLineColorOpportunity()
        {
            return this.All(selection => selection.LineColorOpportunity);
        }

        public bool GetLineSizeOpportunity()
        {
            return this.All(selection => selection.LineSizeOpportunity);
        }
    }
}
