//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StakeholderAnalysis.Storage.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnalysisEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AnalysisEntity()
        {
            this.AttitudeImpactDiagramEntities = new HashSet<AttitudeImpactDiagramEntity>();
            this.ForceFieldDiagramEntities = new HashSet<ForceFieldDiagramEntity>();
            this.OnionDiagramEntities = new HashSet<OnionDiagramEntity>();
            this.StakeholderEntities = new HashSet<StakeholderEntity>();
        }
    
        public long AnalysisEntityId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttitudeImpactDiagramEntity> AttitudeImpactDiagramEntities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForceFieldDiagramEntity> ForceFieldDiagramEntities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnionDiagramEntity> OnionDiagramEntities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StakeholderEntity> StakeholderEntities { get; set; }
    }
}
