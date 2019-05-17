using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    internal static class AttitudeImpactDiagramCreateExtensions
    {
        internal static AttitudeImpactDiagramEntity Create(this AttitudeImpactDiagram diagram, PersistenceRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            var entity = new AttitudeImpactDiagramEntity
            {
                Name = diagram.Name.DeepClone(),
            };

            // TODO: Add stakeholders

            return entity;
        }
    }
}
