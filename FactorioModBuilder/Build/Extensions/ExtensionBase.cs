using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        FactorioGraphics,
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

        protected string ProjectVersion
        {
            get { return _compiler.ProjectVersion; }
            set { _compiler.ProjectVersion = value; }
        }

        protected string ProjectDirectoryName
        {
            get { return _compiler.ProjectDirectory; }
            set { _compiler.ProjectDirectory = value; }
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

        protected string PrototypeDirectory
        {
            get { return _compiler.PrototypeDirectory; }
            set { _compiler.PrototypeDirectory = value; }
        }

        protected string DependencyString
        {
            get { return _compiler.DependencyString; }
            set { _compiler.DependencyString = value; }
        }

        protected HashSet<string> GroupNames { get { return _compiler.GroupNames; } }

        protected HashSet<string> SubGroupNames { get { return _compiler.SubGroupNames; } }

        protected HashSet<string> ItemNames { get { return _compiler.ItemNames; } }

        protected HashSet<string> EntityNames { get { return _compiler.EntityNames; } }

        protected Dictionary<string, string> GraphicsPathLookup { get { return _compiler.GraphicsPathLookup; } }

        protected IEnumerable<string> GeneratedFiles { get { return _compiler.GeneratedFiles; } }

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
                if (_compiler.EnableBuildValidation && !this.ValidateData(units.Cast<T>()))
                {
                    this.Error("{0} data failed validation check", units.First().Type);
                    return false;
                }

                StringBuilder sb = new StringBuilder();
                if (!this.BuildUnit(units.Cast<T>(), sb))
                {
                    this.Error("Extension failed to execute build task: {0}", this.Extension);
                    return false;
                }

                string path;
                if (this.GetOutputPath(out path))
                {
                    if (sb.Length == 0)
                    {
                        this.Info("Extension generated no information for requested file {0}, skipping...", path);
                        return true;
                    }

                    string fullPath;
                    using (var fs = File.Open(path, FileMode.Create))
                    using (var sw = new StreamWriter(fs))
                    {
                        fullPath = fs.Name;
                        sw.Write(sb.ToString());
                    }

                    if (!_compiler.GeneratedFiles.Add(fullPath))
                        this.Warning(WarningLevel.W1, "Compiler generation of {0} has happened more than once", path);
                    
                    return true;
                }

                return true;
            }
            catch(InvalidCastException)
            {
                this.Error("Expected {0}, recieved {1}", typeof(T).Name, 
                    units.Any() ? units.First().GetType().Name : "null");
                return false;
            }
            catch(Exception e)
            {
                this.Fatal("Encountered an exception in {0} building data unit: {1}",
                    this.GetType().Name, e.Message);
                return false;
            }
        }

        protected abstract bool BuildUnit(IEnumerable<T> units, StringBuilder sb);

        protected abstract bool ValidateData(IEnumerable<T> units);

        protected virtual bool GetOutputPath(out string path)
        {
            path = null;
            return false;
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
    }
}
