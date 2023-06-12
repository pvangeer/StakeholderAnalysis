using System;
using System.Collections.Generic;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Create
{
    public class PersistenceRegistry
    {
        private readonly Dictionary<OnionDiagram, OnionDiagramXmlEntity> onionDiagrams =
            CreateDictionary<OnionDiagram, OnionDiagramXmlEntity>();

        private readonly Dictionary<StakeholderConnectionGroup, StakeholderConnectionGroupXmlEntity>
            onionDiagramStakeholderConnectionGroups =
                CreateDictionary<StakeholderConnectionGroup, StakeholderConnectionGroupXmlEntity>();

        private readonly Dictionary<StakeholderConnection, StakeholderConnectionXmlEntity>
            onionDiagramStakeholderConnections =
                CreateDictionary<StakeholderConnection, StakeholderConnectionXmlEntity>();

        private readonly Dictionary<OnionRing, OnionRingXmlEntity> onionRings =
            CreateDictionary<OnionRing, OnionRingXmlEntity>();

        private readonly Dictionary<PositionedStakeholder, IPositionedStakeholderXmlEntity>
            positionedStakeholders =
                CreateDictionary<PositionedStakeholder, IPositionedStakeholderXmlEntity>();

        private readonly Dictionary<Stakeholder, StakeholderXmlEntity> stakeholders =
            CreateDictionary<Stakeholder, StakeholderXmlEntity>();

        private readonly Dictionary<StakeholderType, StakeholderTypeXmlEntity> stakeholderTypes =
            CreateDictionary<StakeholderType, StakeholderTypeXmlEntity>();

        private readonly Dictionary<TwoAxisDiagram, TwoAxisDiagramXmlEntity> twoAxisDiagrams =
            CreateDictionary<TwoAxisDiagram, TwoAxisDiagramXmlEntity>();

        #region Register Methods

        internal void Register(Stakeholder model, StakeholderXmlEntity entity)
        {
            Register(stakeholders, model, entity);
        }

        internal void Register(StakeholderType model, StakeholderTypeXmlEntity entity)
        {
            Register(stakeholderTypes, model, entity);
        }

        internal void Register(OnionDiagram model, OnionDiagramXmlEntity entity)
        {
            Register(onionDiagrams, model, entity);
        }

        internal void Register(OnionRing model, OnionRingXmlEntity entity)
        {
            Register(onionRings, model, entity);
        }

        internal void Register(StakeholderConnection model, StakeholderConnectionXmlEntity entity)
        {
            Register(onionDiagramStakeholderConnections, model, entity);
        }

        internal void Register(StakeholderConnectionGroup model, StakeholderConnectionGroupXmlEntity entity)
        {
            Register(onionDiagramStakeholderConnectionGroups, model, entity);
        }

        internal void Register(TwoAxisDiagram model, TwoAxisDiagramXmlEntity entity)
        {
            Register(twoAxisDiagrams, model, entity);
        }

        internal void Register(PositionedStakeholder model, PositionedStakeholderXmlEntity entity)
        {
            Register(positionedStakeholders, model, entity);
        }

        #endregion

        #region Contains Methods

        internal bool Contains(Stakeholder model)
        {
            return ContainsValue(stakeholders, model);
        }

        internal bool Contains(StakeholderType model)
        {
            return ContainsValue(stakeholderTypes, model);
        }

        internal bool Contains(OnionDiagram model)
        {
            return ContainsValue(onionDiagrams, model);
        }

        internal bool Contains(OnionRing model)
        {
            return ContainsValue(onionRings, model);
        }

        internal bool Contains(StakeholderConnection model)
        {
            return ContainsValue(onionDiagramStakeholderConnections, model);
        }

        internal bool Contains(StakeholderConnectionGroup model)
        {
            return ContainsValue(onionDiagramStakeholderConnectionGroups, model);
        }

        internal bool Contains(PositionedStakeholder model)
        {
            return ContainsValue(positionedStakeholders, model);
        }

        internal bool Contains(TwoAxisDiagram model)
        {
            return ContainsValue(twoAxisDiagrams, model);
        }

        #endregion

        #region Get Methods

        public StakeholderXmlEntity Get(Stakeholder model)
        {
            return Get(stakeholders, model);
        }

        public StakeholderTypeXmlEntity Get(StakeholderType model)
        {
            return Get(stakeholderTypes, model);
        }

        public OnionDiagramXmlEntity Get(OnionDiagram model)
        {
            return Get(onionDiagrams, model);
        }

        public OnionRingXmlEntity Get(OnionRing model)
        {
            return Get(onionRings, model);
        }

        public StakeholderConnectionXmlEntity Get(StakeholderConnection model)
        {
            return Get(onionDiagramStakeholderConnections, model);
        }

        public StakeholderConnectionGroupXmlEntity Get(StakeholderConnectionGroup model)
        {
            return Get(onionDiagramStakeholderConnectionGroups, model);
        }

        public TwoAxisDiagramXmlEntity Get(TwoAxisDiagram model)
        {
            return Get(twoAxisDiagrams, model);
        }

        public PositionedStakeholderXmlEntity Get(PositionedStakeholder model)
        {
            return Get(positionedStakeholders, model) as PositionedStakeholderXmlEntity;
        }

        #endregion

        #region helpers

        private static Dictionary<TEntity, TModel> CreateDictionary<TEntity, TModel>()
        {
            return new Dictionary<TEntity, TModel>(new ReferenceEqualityComparer<TEntity>());
        }

        private bool ContainsValue<TModel, TEntity>(Dictionary<TModel, TEntity> collection, TModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            return collection.Keys.Contains(model, new ReferenceEqualityComparer<TModel>());
        }

        private void Register<TModel, TEntity>(Dictionary<TModel, TEntity> collection, TModel model, TEntity entity)
            where TEntity : IXmlEntity
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (model == null) throw new ArgumentNullException(nameof(model));

            collection[model] = entity;
            entity.Id = collection.Count - 1;
        }

        private TEntity Get<TModel, TEntity>(Dictionary<TModel, TEntity> collection, TModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            return collection[model];
        }

        #endregion
    }
}