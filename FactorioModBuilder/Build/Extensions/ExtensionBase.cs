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
        PrototypeItems,
        PrototypeRecipes,
        PrototypeTechnologies,
        PrototypeTiles
    }

    public abstract class ExtensionBase<T> : ICompilerExtension where T : DataUnit
    {
        public ExtensionType Extension { get; private set; }
        public IEnumerable<ExtensionType> Dependencies { get; private set; }

        private Compiler _compiler;

        protected string ProjectName 
        { 
            get { return _compiler.ProjectName; }
            set { _compiler.ProjectName = value; }
        }

        protected string TemporaryDirectory
        {
            get { return _compiler.TemporaryDirectory; }
            set { _compiler.TemporaryDirectory = value; }
        }

        protected string OutputDirectory
        {
            get { return _compiler.OutputDirectory; }
            set { _compiler.OutputDirectory = value; }
        }

        protected HashSet<string> GroupNames { get { return _compiler.GroupNames; } }

        protected HashSet<string> SubGroupNames { get { return _compiler.SubGroupNames; } }

        protected HashSet<string> ItemNames { get { return _compiler.ItemNames; } }

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

        public bool BuildUnit(IEnumerable<DataUnit> units)
        {
            if(units == null)
                throw new ArgumentNullException("units");

            try
            {
                return this.BuildUnit(units.Cast<T>());
            }
            catch(InvalidCastException)
            {
                this.Error("Expected {0}, recieved {1}", typeof(T).Name, 
                    units.Any() ? units.First().GetType().Name : "null");
                return false;
            }
        }

        public bool BuildUnit(IEnumerable<DataUnit> units, DirectoryInfo outDir)
        {
            if (units == null)
                throw new ArgumentNullException("units");

            try
            {
                return this.BuildUnit(units.Cast<T>(), outDir);
            }
            catch (InvalidCastException)
            {
                this.Error("Expected {0}, recieved {1}", typeof(T).Name,
                    units.Any() ? units.First().GetType().Name : "null");
                return false;
            }
        }

        public bool BuildUnit(IEnumerable<DataUnit> units, out string result)
        {
            if (units == null)
                throw new ArgumentNullException("units");

            try
            {
                return this.BuildUnit(units.Cast<T>(), out result);
            }
            catch (InvalidCastException)
            {
                this.Error("Expected {0}, recieved {1}", typeof(T).Name,
                    units.Any() ? units.First().GetType().Name : "null");
                result = String.Empty;
                return false;
            }
        }

        protected virtual bool BuildUnit(IEnumerable<T> units)
        {
            throw new NotImplementedException();
        }

        protected virtual bool BuildUnit(IEnumerable<T> units, DirectoryInfo outDir)
        {
            throw new NotImplementedException();
        }

        protected virtual bool BuildUnit(IEnumerable<T> units, out string result)
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

        protected ICompilerExtension GetCompilerExtension(ExtensionType type)
        {
            ICompilerExtension ext;
            if (!_compiler.TryGetExtension(type, out ext))
                throw new Exception("Could not resolve compiler extension for type: " + ext.GetType().Name);
            return ext;
        }
    }
}
