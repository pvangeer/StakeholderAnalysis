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
    
    public partial class StakeholderEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StakeholderEntity()
        {
            this.AttitudeImpactDiagramStakeholderEntities = new HashSet<AttitudeImpactDiagramStakeholderEntity>();
            this.ForceFieldDiagramStakeholderEntities = new HashSet<ForceFieldDiagramStakeholderEntity>();
            this.OnionDiagramStakeholderEntities = new HashSet<OnionDiagramStakeholderEntity>();
        }
    
        public long StakeholderEntityId { get; set; }
        public long StakeholderTypeId { get; set; }
        public long AnalysisEntityId { get; set; }
        public string Name { get; set; }
        public long Order { get; set; }
    
        public virtual AnalysisEntity AnalysisEntity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttitudeImpactDiagramStakeholderEntity> AttitudeImpactDiagramStakeholderEntities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForceFieldDiagramStakeholderEntity> ForceFieldDiagramStakeholderEntities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnionDiagramStakeholderEntity> OnionDiagramStakeholderEntities { get; set; }
        public virtual StakeholderTypeEntity StakeholderTypeEntity { get; set; }
    }
}
