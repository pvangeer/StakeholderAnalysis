using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    [XmlRoot(ElementName = "analysis", IsNullable = false)]
    public class AnalysisXmlEntity
    {
        public AnalysisXmlEntity()
        {
            StakeholderTypeXmlEntities = new Collection<StakeholderTypeXmlEntity>();
            StakeholderXmlEntities = new Collection<StakeholderXmlEntity>();
            AttitudeImpactDiagramXmlEntities = new Collection<AttitudeImpactDiagramXmlEntity>();
            ForceFieldDiagramXmlEntities = new Collection<ForceFieldDiagramXmlEntity>();
            OnionDiagramXmlEntities = new Collection<OnionDiagramXmlEntity>();
        }

        [XmlArray(ElementName = "stakeholdertypes")]
        [XmlArrayItem(ElementName = "stakeholdertype")]
        public Collection<StakeholderTypeXmlEntity> StakeholderTypeXmlEntities { get; set; }

        [XmlArray(ElementName = "stakeholders")]
        [XmlArrayItem(ElementName = "stakeholder")]
        public Collection<StakeholderXmlEntity> StakeholderXmlEntities { get; set; }

        [XmlArray(ElementName = "attitudeimpactdiagrams")]
        [XmlArrayItem(ElementName = "attitudeimpactdiagram")]
        public Collection<AttitudeImpactDiagramXmlEntity> AttitudeImpactDiagramXmlEntities { get; set; }

        [XmlArray(ElementName = "forcefielddiagrams")]
        [XmlArrayItem(ElementName = "forcefielddiagram")]
        public Collection<ForceFieldDiagramXmlEntity> ForceFieldDiagramXmlEntities { get; set; }

        [XmlArray(ElementName = "oniondiagrams")]
        [XmlArrayItem(ElementName = "oniondiagram")]
        public Collection<OnionDiagramXmlEntity> OnionDiagramXmlEntities { get; set; }
    }
}