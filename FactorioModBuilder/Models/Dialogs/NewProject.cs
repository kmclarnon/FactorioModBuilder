using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.Dialogs
{
    public enum SolutionType
    {
        CreateNew,
        AddExisting,
        CreateInNewInstance
    }

    public class NewProject
    {
        public string ResultProjectName { get; set; }

        public string ResultLocation { get; set; }

        public SolutionType ResultSolutionType { get; set; }

        public string ResultSolutionName { get; set; }
    }
}
