using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class AttitudeImpactDiagramEntityReadExtensions
    {
        internal static AttitudeImpactDiagram Read(this AttitudeImpactDiagramEntity entity, ReadConversionCollector collector)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (collector == null)
            {
                throw new ArgumentNullException(nameof(collector));
            }

            if (collector.Contains(entity))
            {
                return collector.Get(entity);
            }

            var stakeholders = entity.AttitudeImpactDiagramStakeholderEntities.OrderBy(e => e.Order).Select(e => e.Read(collector));

            var attitudeImpactDiagram = new AttitudeImpactDiagram
            {
                Name = entity.Name
            };

            foreach (var stakeholder in stakeholders)
            {
                attitudeImpactDiagram.Stakeholders.Add(stakeholder);
            }

            collector.Collect(entity, attitudeImpactDiagram);

            return attitudeImpactDiagram;
        }
    }
}
