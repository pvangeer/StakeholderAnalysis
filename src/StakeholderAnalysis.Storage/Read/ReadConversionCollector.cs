using System;
using System.Collections.Generic;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal class ReadConversionCollector
    {
        private readonly Dictionary<AttitudeImpactDiagramXmlEntity, AttitudeImpactDiagram> attitudeImpactDiagrams =
            CreateDictionary<AttitudeImpactDiagramXmlEntity, AttitudeImpactDiagram>();

        private readonly Dictionary<AttitudeImpactDiagramStakeholderXmlEntity, Data.PositionedStakeholder>
            attitudeImpactDiagramStakeholders =
                CreateDictionary<AttitudeImpactDiagramStakeholderXmlEntity, Data.PositionedStakeholder>();

        private readonly Dictionary<ForceFieldDiagramXmlEntity, ForceFieldDiagram> forceFieldDiagrams =
            CreateDictionary<ForceFieldDiagramXmlEntity, ForceFieldDiagram>();

        private readonly Dictionary<ForceFieldDiagramStakeholderXmlEntity, Data.PositionedStakeholder>
            forceFieldDiagramStakeholders =
                CreateDictionary<ForceFieldDiagramStakeholderXmlEntity, Data.PositionedStakeholder>();

        private readonly Dictionary<OnionDiagramXmlEntity, OnionDiagram> onionDiagrams =
            CreateDictionary<OnionDiagramXmlEntity, OnionDiagram>();

        private readonly Dictionary<OnionDiagramStakeholderXmlEntity, PositionedStakeholder>
            onionDiagramStakeholders =
                CreateDictionary<OnionDiagramStakeholderXmlEntity, PositionedStakeholder>();

        private readonly Dictionary<OnionRingXmlEntity, OnionRing> onionRings =
            CreateDictionary<OnionRingXmlEntity, OnionRing>();

        private readonly Dictionary<StakeholderConnectionGroupXmlEntity, StakeholderConnectionGroup>
            stakeholderConnectionGroups =
                CreateDictionary<StakeholderConnectionGroupXmlEntity, StakeholderConnectionGroup>();

        private readonly Dictionary<StakeholderConnectionXmlEntity, StakeholderConnection> stakeholderConnections =
            CreateDictionary<StakeholderConnectionXmlEntity, StakeholderConnection>();

        private readonly Dictionary<StakeholderXmlEntity, Stakeholder> stakeholders =
            CreateDictionary<StakeholderXmlEntity, Stakeholder>();

        private readonly Dictionary<StakeholderTypeXmlEntity, StakeholderType> stakeholderTypes =
            CreateDictionary<StakeholderTypeXmlEntity, StakeholderType>();

        internal void Collect(StakeholderXmlEntity entity, Stakeholder model)
        {
            Collect(stakeholders, entity, model);
        }

        internal void Collect(StakeholderTypeXmlEntity entity, StakeholderType model)
        {
            Collect(stakeholderTypes, entity, model);
        }

        internal void Collect(OnionDiagramXmlEntity entity, OnionDiagram model)
        {
            Collect(onionDiagrams, entity, model);
        }

        internal void Collect(OnionRingXmlEntity entity, OnionRing model)
        {
            Collect(onionRings, entity, model);
        }

        internal void Collect(OnionDiagramStakeholderXmlEntity entity, PositionedStakeholder model)
        {
            Collect(onionDiagramStakeholders, entity, model);
        }

        internal void Collect(StakeholderConnectionGroupXmlEntity entity, StakeholderConnectionGroup model)
        {
            Collect(stakeholderConnectionGroups, entity, model);
        }

        internal void Collect(StakeholderConnectionXmlEntity entity, StakeholderConnection model)
        {
            Collect(stakeholderConnections, entity, model);
        }

        internal void Collect(ForceFieldDiagramXmlEntity entity, ForceFieldDiagram model)
        {
            Collect(forceFieldDiagrams, entity, model);
        }

        internal void Collect(ForceFieldDiagramStakeholderXmlEntity entity, Data.PositionedStakeholder model)
        {
            Collect(forceFieldDiagramStakeholders, entity, model);
        }

        internal void Collect(AttitudeImpactDiagramXmlEntity entity, AttitudeImpactDiagram model)
        {
            Collect(attitudeImpactDiagrams, entity, model);
        }

        internal void Collect(AttitudeImpactDiagramStakeholderXmlEntity entity, Data.PositionedStakeholder model)
        {
            Collect(attitudeImpactDiagramStakeholders, entity, model);
        }

        internal bool Contains(StakeholderXmlEntity entity)
        {
            return Contains(stakeholders, entity);
        }

        internal bool Contains(StakeholderTypeXmlEntity entity)
        {
            return Contains(stakeholderTypes, entity);
        }

        internal bool Contains(OnionDiagramXmlEntity entity)
        {
            return Contains(onionDiagrams, entity);
        }

        internal bool Contains(OnionRingXmlEntity entity)
        {
            return Contains(onionRings, entity);
        }

        internal bool Contains(OnionDiagramStakeholderXmlEntity entity)
        {
            return Contains(onionDiagramStakeholders, entity);
        }

        internal bool Contains(StakeholderConnectionXmlEntity entity)
        {
            return Contains(stakeholderConnections, entity);
        }

        internal bool Contains(StakeholderConnectionGroupXmlEntity entity)
        {
            return Contains(stakeholderConnectionGroups, entity);
        }

        internal bool Contains(ForceFieldDiagramXmlEntity entity)
        {
            return Contains(forceFieldDiagrams, entity);
        }

        internal bool Contains(ForceFieldDiagramStakeholderXmlEntity entity)
        {
            return Contains(forceFieldDiagramStakeholders, entity);
        }

        internal bool Contains(AttitudeImpactDiagramXmlEntity entity)
        {
            return Contains(attitudeImpactDiagrams, entity);
        }

        internal bool Contains(AttitudeImpactDiagramStakeholderXmlEntity entity)
        {
            return Contains(attitudeImpactDiagramStakeholders, entity);
        }

        internal Stakeholder Get(StakeholderXmlEntity entity)
        {
            return Get(stakeholders, entity);
        }

        internal StakeholderType Get(StakeholderTypeXmlEntity entity)
        {
            return Get(stakeholderTypes, entity);
        }

        internal OnionDiagram Get(OnionDiagramXmlEntity entity)
        {
            return Get(onionDiagrams, entity);
        }

        internal OnionRing Get(OnionRingXmlEntity entity)
        {
            return Get(onionRings, entity);
        }

        internal PositionedStakeholder Get(OnionDiagramStakeholderXmlEntity entity)
        {
            return Get(onionDiagramStakeholders, entity);
        }

        internal StakeholderConnection Get(StakeholderConnectionXmlEntity entity)
        {
            return Get(stakeholderConnections, entity);
        }

        internal StakeholderConnectionGroup Get(StakeholderConnectionGroupXmlEntity entity)
        {
            return Get(stakeholderConnectionGroups, entity);
        }

        internal ForceFieldDiagram Get(ForceFieldDiagramXmlEntity entity)
        {
            return Get(forceFieldDiagrams, entity);
        }

        internal Data.PositionedStakeholder Get(ForceFieldDiagramStakeholderXmlEntity entity)
        {
            return Get(forceFieldDiagramStakeholders, entity);
        }

        internal AttitudeImpactDiagram Get(AttitudeImpactDiagramXmlEntity entity)
        {
            return Get(attitudeImpactDiagrams, entity);
        }

        internal Data.PositionedStakeholder Get(AttitudeImpactDiagramStakeholderXmlEntity entity)
        {
            return Get(attitudeImpactDiagramStakeholders, entity);
        }

        public StakeholderType GetReferencedStakeholderType(long id)
        {
            var key = stakeholderTypes.Keys.FirstOrDefault(k => k.Id == id);
            return key == null
                ? throw new ReadReferencedObjectsFirstException(nameof(StakeholderType))
                : Get(key);
        }

        public StakeholderConnectionGroup GetReferencedStakeholderConnectionGroup(long id)
        {
            var key = stakeholderConnectionGroups.Keys.FirstOrDefault(k => k.Id == id);
            return key == null
                ? throw new ReadReferencedObjectsFirstException(nameof(StakeholderConnectionGroup))
                : Get(key);
        }

        public PositionedStakeholder GetReferencedOnionDiagramStakeholder(long id)
        {
            var key = onionDiagramStakeholders.Keys.FirstOrDefault(k => k.Id == id);
            return key == null
                ? throw new ReadReferencedObjectsFirstException(nameof(PositionedStakeholder))
                : Get(key);
        }

        public Stakeholder GetReferencedStakeholder(long id)
        {
            var key = stakeholders.Keys.FirstOrDefault(k => k.Id == id);
            return key == null
                ? throw new ReadReferencedObjectsFirstException(nameof(Stakeholder))
                : Get(key);
        }

        #region helpers

        private TModel Get<TEntity, TModel>(Dictionary<TEntity, TModel> collection, TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                return collection[entity];
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidOperationException(e.Message, e);
            }
        }

        private bool Contains<TEntity, TModel>(Dictionary<TEntity, TModel> collection, TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return collection.ContainsKey(entity);
        }

        private void Collect<TEntity, TModel>(Dictionary<TEntity, TModel> collection, TEntity entity, TModel model)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (model == null) throw new ArgumentNullException(nameof(model));

            collection[entity] = model;
        }

        private static Dictionary<TEntity, TModel> CreateDictionary<TEntity, TModel>()
        {
            return new Dictionary<TEntity, TModel>(new ReferenceEqualityComparer<TEntity>());
        }

        #endregion
    }
}