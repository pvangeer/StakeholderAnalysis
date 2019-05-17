using System;
using System.Collections.Generic;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    public class PersistenceRegistry
    {
        private readonly Dictionary<Stakeholder, StakeholderEntity> stakeholders = CreateDictionary<Stakeholder, StakeholderEntity>();

        private readonly Dictionary<OnionDiagram, OnionDiagramEntity> onionDiagrams = CreateDictionary<OnionDiagram, OnionDiagramEntity>();
        private readonly Dictionary<OnionDiagramStakeholder, OnionDiagramStakeholderEntity> onionDiagramStakeholders = CreateDictionary<OnionDiagramStakeholder, OnionDiagramStakeholderEntity>();
        private readonly Dictionary<OnionRing, OnionRingEntity> onionRings = CreateDictionary<OnionRing, OnionRingEntity>();
        private readonly Dictionary<StakeholderConnection, StakeholderConnectionEntity> onionDiagramStakeholderConnections = CreateDictionary<StakeholderConnection, StakeholderConnectionEntity>();
        private readonly Dictionary<StakeholderConnectionGroup, StakeholderConnectionGroupEntity> onionDiagramStakeholderConnectionGroups = CreateDictionary<StakeholderConnectionGroup, StakeholderConnectionGroupEntity>();

        private readonly Dictionary<ForceFieldDiagram, ForceFieldDiagramEntity> forceFieldDiagrams = CreateDictionary<ForceFieldDiagram, ForceFieldDiagramEntity>();
        private readonly Dictionary<ForceFieldDiagramStakeholder, ForceFieldDiagramStakeholderEntity> forceFieldDiagramStakeholders = CreateDictionary<ForceFieldDiagramStakeholder, ForceFieldDiagramStakeholderEntity>();
        private readonly Dictionary<AttitudeImpactDiagram, AttitudeImpactDiagramEntity> attitudeImpactDiagrams = CreateDictionary<AttitudeImpactDiagram, AttitudeImpactDiagramEntity>();
        private readonly Dictionary<AttitudeImpactDiagramStakeholder, AttitudeImpactDiagramStakeholderEntity> attitudeImpactDiagramStakeholders = CreateDictionary<AttitudeImpactDiagramStakeholder, AttitudeImpactDiagramStakeholderEntity>();

        #region Register Methods

        internal void Register(Stakeholder model, StakeholderEntity entity)
        {
            Register(stakeholders, model, entity);
        }
        internal void Register(OnionDiagram model, OnionDiagramEntity entity)
        {
            Register(onionDiagrams, model, entity);
        }
        internal void Register(OnionRing model, OnionRingEntity entity)
        {
            Register(onionRings, model, entity);
        }
        internal void Register(OnionDiagramStakeholder model, OnionDiagramStakeholderEntity entity)
        {
            Register(onionDiagramStakeholders, model, entity);
        }
        internal void Register(StakeholderConnection model, StakeholderConnectionEntity entity)
        {
            Register(onionDiagramStakeholderConnections, model, entity);
        }
        internal void Register(StakeholderConnectionGroup model, StakeholderConnectionGroupEntity entity)
        {
            Register(onionDiagramStakeholderConnectionGroups, model, entity);
        }
        internal void Register(ForceFieldDiagram model, ForceFieldDiagramEntity entity)
        {
            Register(forceFieldDiagrams, model, entity);
        }
        internal void Register(ForceFieldDiagramStakeholder model, ForceFieldDiagramStakeholderEntity entity)
        {
            Register(forceFieldDiagramStakeholders, model, entity);
        }
        internal void Register(AttitudeImpactDiagram model, AttitudeImpactDiagramEntity entity)
        {
            Register(attitudeImpactDiagrams, model, entity);
        }
        internal void Register(AttitudeImpactDiagramStakeholder model, AttitudeImpactDiagramStakeholderEntity entity)
        {
            Register(attitudeImpactDiagramStakeholders, model, entity);
        }

        #endregion

        #region Contains Methods

        internal bool Contains(Stakeholder model)
        {
            return ContainsValue(stakeholders, model);
        }
        internal bool Contains(OnionDiagram model)
        {
            return ContainsValue(onionDiagrams, model);
        }
        internal bool Contains(OnionRing model)
        {
            return ContainsValue(onionRings, model);
        }
        internal bool Contains(OnionDiagramStakeholder model)
        {
            return ContainsValue(onionDiagramStakeholders, model);
        }
        internal bool Contains(StakeholderConnection model)
        {
            return ContainsValue(onionDiagramStakeholderConnections, model);
        }
        internal bool Contains(StakeholderConnectionGroup model)
        {
            return ContainsValue(onionDiagramStakeholderConnectionGroups, model);
        }
        internal bool Contains(ForceFieldDiagram model)
        {
            return ContainsValue(forceFieldDiagrams, model);
        }
        internal bool Contains(ForceFieldDiagramStakeholder model)
        {
            return ContainsValue(forceFieldDiagramStakeholders, model);
        }
        internal bool Contains(AttitudeImpactDiagram model)
        {
            return ContainsValue(attitudeImpactDiagrams, model);
        }
        internal bool Contains(AttitudeImpactDiagramStakeholder model)
        {
            return ContainsValue(attitudeImpactDiagramStakeholders, model);
        }
        #endregion

        #region Get Methods
        public StakeholderEntity Get(Stakeholder model)
        {
            return Get(stakeholders, model);
        }
        public OnionDiagramEntity Get(OnionDiagram model)
        {
            return Get(onionDiagrams, model);
        }
        public OnionRingEntity Get(OnionRing model)
        {
            return Get(onionRings, model);
        }
        public OnionDiagramStakeholderEntity Get(OnionDiagramStakeholder model)
        {
            return Get(onionDiagramStakeholders, model);
        }
        public StakeholderConnectionEntity Get(StakeholderConnection model)
        {
            return Get(onionDiagramStakeholderConnections, model);
        }
        public StakeholderConnectionGroupEntity Get(StakeholderConnectionGroup model)
        {
            return Get(onionDiagramStakeholderConnectionGroups, model);
        }
        public ForceFieldDiagramEntity Get(ForceFieldDiagram model)
        {
            return Get(forceFieldDiagrams, model);
        }
        public ForceFieldDiagramStakeholderEntity Get(ForceFieldDiagramStakeholder model)
        {
            return Get(forceFieldDiagramStakeholders, model);
        }
        public AttitudeImpactDiagramEntity Get(AttitudeImpactDiagram model)
        {
            return Get(attitudeImpactDiagrams, model);
        }
        public AttitudeImpactDiagramStakeholderEntity Get(AttitudeImpactDiagramStakeholder model)
        {
            return Get(attitudeImpactDiagramStakeholders, model);
        }

        #endregion

        #region helpers
        private static Dictionary<TEntity, TModel> CreateDictionary<TEntity, TModel>()
        {
            return new Dictionary<TEntity, TModel>(new ReferenceEqualityComparer<TEntity>());
        }

        private bool ContainsValue<TModel, TEntity>(Dictionary<TModel, TEntity> collection, TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return collection.Keys.Contains(model, new ReferenceEqualityComparer<TModel>());
        }

        private void Register<TModel, TEntity>(Dictionary<TModel, TEntity> collection, TModel model, TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            collection[model] = entity;
        }

        private TEntity Get<TModel, TEntity>(Dictionary<TModel, TEntity> collection, TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return collection[model];
        }
        #endregion
    }
}