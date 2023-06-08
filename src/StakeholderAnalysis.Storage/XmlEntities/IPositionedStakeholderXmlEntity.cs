namespace StakeholderAnalysis.Storage.XmlEntities
{
    internal interface IPositionedStakeholderXmlEntity : IXmlEntity
    {
        long Rank { get; set; }

        long Order { get; set; }

        long StakeholderReferenceId { get; set; }

        long Id { get; set; }
    }
}