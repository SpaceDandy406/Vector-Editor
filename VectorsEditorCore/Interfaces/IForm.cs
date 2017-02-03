using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditorCore.Interfaces
{
    public interface IForm
    {
        bool FillColor { get; set; }

        bool LineColor { get; set; }

        bool LineSize { get; set; }
    }
}
