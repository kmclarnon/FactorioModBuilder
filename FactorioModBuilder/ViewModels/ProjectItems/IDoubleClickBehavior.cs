using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public enum DoubleClickBehavior
    {
        OpenContent,
        Ignore
    }

    public interface IDoubleClickBehavior
    {
        DoubleClickBehavior DoubleClickBehavior { get; }
    }
}
