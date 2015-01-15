using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Messages;
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
        FactorioLocale,
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
        public ExtensionType Extension { get; private set; }
        public IEnumerable<ExtensionType> Dependencies { get; private set; }

        private Compiler _compiler;

        public ExtensionBase(ExtensionType extension, 
            params ExtensionType[] dependencies)
        {
            this.Extension = extension;
            if (dependencies != null)
                this.Dependencies = dependencies.ToList();
            else
                this.Dependencies = new List<ExtensionType>();
        }

        public void AttachToCompiler(Compiler c)
        {
            this._compiler = c;
        }

        public virtual bool BuildUnit(IEnumerable<DataUnit> units)
        {
            throw new NotImplementedException();
        }

        public virtual bool BuildUnit(IEnumerable<DataUnit> units, DirectoryInfo outDir)
        {
            throw new NotImplementedException();
        }

        public virtual bool BuildUnit(IEnumerable<DataUnit> units, out string result)
        {
            throw new NotImplementedException();
        }

        protected void Info(string format, params object[] args)
        {
            if (_compiler != null)
                _compiler.BuildMessages.Add(
                    new InfoMessage(format, args));
        }

        protected void Warning(WarningLevel level, string format, params object[] args)
        {
            if (_compiler != null)
                _compiler.BuildMessages.Add(
                    new WarningMessage(level, format, args));
        }

        protected void Error(string format, params object[] args)
        {
            if (_compiler != null)
                _compiler.BuildMessages.Add(
                    new ErrorMessage(format, args));
        }

        protected void Fatal(string format, params object[] args)
        {
            if (_compiler != null)
                _compiler.BuildMessages.Add(
                    new FatalMessage(format, args));
        }

        protected bool TryGetCompilerExtension(ExtensionType type, out ICompilerExtension ext)
        {
            ext = null;
            if (_compiler == null)
                return false;
            return _compiler.TryGetExtension(type, out ext);
        }

        protected DirectoryInfo CreateCleanDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if(!di.Exists)
            {
                di.Create();
            }
            else
            {
                this.CleanDirectory(di);
            }
            return di;
        }

        protected void CleanDirectory(DirectoryInfo di)
        {
            foreach (var file in di.GetFiles())
                file.Delete();
            foreach (var dir in di.GetDirectories())
                dir.Delete(true);
        }

        protected bool CanContinue()
        {
            if (_compiler != null)
                return _compiler.CanContinue();
            return false;
        }
    }
}
