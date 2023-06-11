using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace StakeholderAnalysis.Storage.XmlEntities
{
    [Serializable]
    public class AnalysisXmlEntity
    {
        public AnalysisXmlEntity()
        {
            StakeholderTypeXmlEntities = new Collection<StakeholderTypeXmlEntity>();
            StakeholderXmlEntities = new Collection<StakeholderXmlEntity>();
            OnionDiagramXmlEntities = new Collection<OnionDiagramXmlEntity>();
            ForceFieldDiagramXmlEntities = new Collection<ForceFieldDiagramXmlEntity>();
            AttitudeImpactDiagramXmlEntities = new Collection<TwoAxisDiagramXmlEntity>();
        }

        [XmlArray(ElementName = "stakeholdertypes")]
        [XmlArrayItem(ElementName = "stakeholdertype")]
        public Collection<StakeholderTypeXmlEntity> StakeholderTypeXmlEntities { get; set; }

        [XmlArray(ElementName = "stakeholders")]
        [XmlArrayItem(ElementName = "stakeholder")]
        public Collection<StakeholderXmlEntity> StakeholderXmlEntities { get; set; }

        [XmlArray(ElementName = "oniondiagrams")]
        [XmlArrayItem(ElementName = "oniondiagram")]
        public Collection<OnionDiagramXmlEntity> OnionDiagramXmlEntities { get; set; }

        [XmlArray(ElementName = "forcefielddiagrams")]
        [XmlArrayItem(ElementName = "forcefielddiagram")]
        public Collection<ForceFieldDiagramXmlEntity> ForceFieldDiagramXmlEntities { get; set; }

        [XmlArray(ElementName = "attitudeimpactdiagrams")]
        [XmlArrayItem(ElementName = "twoaxisdiagram")]
        public Collection<TwoAxisDiagramXmlEntity> AttitudeImpactDiagramXmlEntities { get; set; }
    }
}