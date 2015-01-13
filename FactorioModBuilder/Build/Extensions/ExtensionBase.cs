using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public enum ExtensionType
    {
        None,
        Project,
        FactorioInfo,
        FactorioDependencies,
        FactorioControl,
        FactorioData,
        Prototypes,
        PrototypeEntities,
        PrototypeEquipment,
        PrototypeFluids,
        PrototypeGroups,
        PrototypeSubgroups,
        PrototypeItems,
        PrototypeRecipes,
        PrototypeTechnologies,
        PrototypeTiles
    }

    public abstract class ExtensionBase : ICompilerExtension
    {
        public abstract ExtensionType Extension { get; }

        public Compiler Parent { get; set; }

        public ExtensionBase()
        {
        }

        public abstract bool BuildUnit(DataUnit unit, DirectoryInfo outDir);

        public abstract bool BuildUnit(DataUnit unit, out string value);
    }
}
