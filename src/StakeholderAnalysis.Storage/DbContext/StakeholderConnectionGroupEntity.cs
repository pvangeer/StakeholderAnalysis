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
    
    public partial class StakeholderConnectionGroupEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StakeholderConnectionGroupEntity()
        {
            this.StakeholderConnectionEntity = new HashSet<StakeholderConnectionEntity>();
        }
    
        public long StakeholderConnectionGroupEntityId { get; set; }
        public Nullable<long> OnionDiagramId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Visible { get; set; }
        public Nullable<double> StrokeThickness { get; set; }
        public long Order { get; set; }
    
        public virtual OnionDiagramEntity OnionDiagramEntity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StakeholderConnectionEntity> StakeholderConnectionEntity { get; set; }
    }
}