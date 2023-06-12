using System;
using System.Collections.Generic;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Read
{
    internal class ReadConversionCollector
    {
        private readonly Dictionary<OnionDiagramXmlEntity, OnionDiagram> onionDiagrams =
            CreateDictionary<OnionDiagramXmlEntity, OnionDiagram>();

        private readonly Dictionary<OnionRingXmlEntity, OnionRing> onionRings =
            CreateDictionary<OnionRingXmlEntity, OnionRing>();

        private readonly Dictionary<PositionedStakeholderXmlEntity, PositionedStakeholder>
            positionedDiagramStakeholders =
                CreateDictionary<PositionedStakeholderXmlEntity, PositionedStakeholder>();

        private readonly Dictionary<StakeholderConnectionGroupXmlEntity, StakeholderConnectionGroup>
            stakeholderConnectionGroups =
                CreateDictionary<StakeholderConnectionGroupXmlEntity, StakeholderConnectionGroup>();

        private readonly Dictionary<StakeholderConnectionXmlEntity, StakeholderConnection> stakeholderConnections =
            CreateDictionary<StakeholderConnectionXmlEntity, StakeholderConnection>();

        private readonly Dictionary<StakeholderXmlEntity, Stakeholder> stakeholders =
            CreateDictionary<StakeholderXmlEntity, Stakeholder>();

        private readonly Dictionary<StakeholderTypeXmlEntity, StakeholderType> stakeholderTypes =
            CreateDictionary<StakeholderTypeXmlEntity, StakeholderType>();

        private readonly Dictionary<TwoAxisDiagramXmlEntity, TwoAxisDiagram> twoAxisDiagrams =
            CreateDictionary<TwoAxisDiagramXmlEntity, TwoAxisDiagram>();

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

        internal void Collect(StakeholderConnectionGroupXmlEntity entity, StakeholderConnectionGroup model)
        {
            Collect(stakeholderConnectionGroups, entity, model);
        }

        internal void Collect(StakeholderConnectionXmlEntity entity, StakeholderConnection model)
        {
            Collect(stakeholderConnections, entity, model);
        }

        internal void Collect(TwoAxisDiagramXmlEntity entity, TwoAxisDiagram model)
        {
            Collect(twoAxisDiagrams, entity, model);
        }

        internal void Collect(PositionedStakeholderXmlEntity entity, PositionedStakeholder model)
        {
            Collect(positionedDiagramStakeholders, entity, model);
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

        internal bool Contains(StakeholderConnectionXmlEntity entity)
        {
            return Contains(stakeholderConnections, entity);
        }

        internal bool Contains(StakeholderConnectionGroupXmlEntity entity)
        {
            return Contains(stakeholderConnectionGroups, entity);
        }

        internal bool Contains(TwoAxisDiagramXmlEntity entity)
        {
            return Contains(twoAxisDiagrams, entity);
        }

        internal bool Contains(PositionedStakeholderXmlEntity entity)
        {
            return Contains(positionedDiagramStakeholders, entity);
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

        internal StakeholderConnection Get(StakeholderConnectionXmlEntity entity)
        {
            return Get(stakeholderConnections, entity);
        }

        internal StakeholderConnectionGroup Get(StakeholderConnectionGroupXmlEntity entity)
        {
            return Get(stakeholderConnectionGroups, entity);
        }

        internal TwoAxisDiagram Get(TwoAxisDiagramXmlEntity entity)
        {
            return Get(twoAxisDiagrams, entity);
        }

        internal PositionedStakeholder Get(PositionedStakeholderXmlEntity entity)
        {
            return Get(positionedDiagramStakeholders, entity);
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

        public PositionedStakeholder GetReferencedPositionedStakeholder(long id)
        {
            var key = positionedDiagramStakeholders.Keys.FirstOrDefault(k => k.Id == id);
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