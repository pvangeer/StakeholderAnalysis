using System;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class StakeholderConnectionGroupEntityReadExtensions
    {
        internal static StakeholderConnectionGroup Read(this StakeholderConnectionGroupXmlEntity entity,
            ReadConversionCollector collector)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            if (collector.Contains(entity)) return collector.Get(entity);

            var stakeholderConnectionGroup = new StakeholderConnectionGroup
            {
                Name = entity.Name,
                StrokeThickness = entity.StrokeThickness,
                StrokeColor = entity.Color.ToColor(),
                LineStyle = (LineStyle)entity.LineStyle,
                Visible = entity.Visible == 1
            };

            collector.Collect(entity, stakeholderConnectionGroup);

            return stakeholderConnectionGroup;
        }
    }
}