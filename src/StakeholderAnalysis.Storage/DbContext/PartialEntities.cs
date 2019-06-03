using System.Data.Entity;

namespace StakeholderAnalysis.Storage.DbContext
{
    public partial class Entities
    {
        public Entities(string connString) : base(connString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public void LoadVersionTableIntoContext()
        {
            VersionEntities.Load();
        }

        /// <summary>
        /// Loads all tables into the context.
        /// </summary>
        public void LoadTablesIntoContext()
        {
            StakeholderTypeEntities.Load();
            StakeholderEntities.Load();
            OnionDiagramEntities.Load();
            OnionRingEntities.Load();
            OnionDiagramStakeholderEntities.Load();
            StakeholderConnectionGroupEntities.Load();
            StakeholderConnectionEntities.Load();
            ForceFieldDiagramEntities.Load();
            ForceFieldDiagramStakeholderEntities.Load();
            AttitudeImpactDiagramEntities.Load();
            AttitudeImpactDiagramStakeholderEntities.Load();
            AnalysisEntities.Load();
        }
    }
}
