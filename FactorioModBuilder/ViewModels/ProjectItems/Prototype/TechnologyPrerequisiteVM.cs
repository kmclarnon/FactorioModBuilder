using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils.Extensions;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    /// <summary>
    /// A view model for the technology prerequisite model
    /// </summary>
    public class TechnologyPrerequisiteVM 
        : ProjectItem<TechnologyPrerequisite, TechnologyPrerequisiteVM>
    {
        /// <summary>
        /// Information to provide to the compiler at build time
        /// </summary>
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// The technology required by this prerequisite
        /// </summary>
        public TechnologyVM Technology
        {
            get { return this.GetProperty<TechnologyVM>(); }
            set { this.SetProperty(value, false, this.HandleTechnologyBinding, (x => this.Name = value.Name)); }
        }

        /// <summary>
        /// Technologies that could be selected as a prerequsite
        /// </summary>
        public ObservableCollection<TechnologyVM> Technologies
        {
            get
            {
                PrototypesVM pvm;
                if (!this.TryFindElementUp(out pvm))
                    throw new Exception("Could not find prototypes parent");
                return pvm.Technologies;
            }
        }

        public TechnologyPrerequisiteVM(TechnologyPrerequisite item)
            : this(null, item)
        {
        }

        public TechnologyPrerequisiteVM(TreeItemVMBase parent, 
            TechnologyPrerequisite item)
            : base(parent, item)
        {
        }

        /// <summary>
        /// Handles hooking and unhooking the TechnologyVM's property changed notifications
        /// in order to catch renaming
        /// </summary>
        /// <param name="tech">The potentially new Technology value</param>
        private void HandleTechnologyBinding(TechnologyVM tech)
        {
            if(this.Technology != null)
                this.Technology.PropertyChanged -= this.TechnologyPropertyChanged;
            if (tech != null)
                tech.PropertyChanged += this.TechnologyPropertyChanged;
        }

        /// <summary>
        /// Rereads the name property from the TechnologyVM whenenver there is
        /// a property change
        /// </summary>
        void TechnologyPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.Technology == null)
                this.Name = String.Empty;
            else
                this.Name = this.Technology.Name;
        }
    }
}
