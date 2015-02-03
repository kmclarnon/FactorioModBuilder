using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.Base
{
    public enum DisplayState
    {
        Normal,
        Renaming
    }

    public interface IDisplayState
    {
        DisplayState DisplayState { get; }

        void DoRename();
    }
}
