using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public interface IGraphicsSource : INotifyPropertyChanged
    {
        string GraphicPath { get; }
        string Name { get; }
    }
}
