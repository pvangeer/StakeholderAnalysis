﻿using System;
using System.Collections.Generic;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal class ReadConversionCollector
    {
        private readonly Dictionary<StakeholderEntity, Stakeholder> stakeholders = CreateDictionary<StakeholderEntity, Stakeholder>();
        private readonly Dictionary<OnionDiagramEntity, OnionDiagram> onionDiagrams = CreateDictionary<OnionDiagramEntity, OnionDiagram>();
        private readonly Dictionary<ForceFieldDiagramEntity, ForceFieldDiagram> forceFieldDiagrams = CreateDictionary<ForceFieldDiagramEntity, ForceFieldDiagram>();
        private readonly Dictionary<ForceFieldDiagramStakeholderEntity, ForceFieldDiagramStakeholder> forceFieldDiagramStakeholders = CreateDictionary<ForceFieldDiagramStakeholderEntity, ForceFieldDiagramStakeholder>();
        private readonly Dictionary<AttitudeImpactDiagramEntity, AttitudeImpactDiagram> attitudeImpactDiagrams = CreateDictionary<AttitudeImpactDiagramEntity, AttitudeImpactDiagram>();
        private readonly Dictionary<AttitudeImpactDiagramStakeholderEntity, AttitudeImpactDiagramStakeholder> attitudeImpactDiagramStakeholders = CreateDictionary<AttitudeImpactDiagramStakeholderEntity, AttitudeImpactDiagramStakeholder>(); 

        internal void Collect(StakeholderEntity entity, Stakeholder model)
        {
            Collect(stakeholders,entity,model);
        }
        internal void Collect(OnionDiagramEntity entity, OnionDiagram model)
        {
            Collect(onionDiagrams, entity, model);
        }
        internal void Collect(ForceFieldDiagramEntity entity, ForceFieldDiagram model)
        {
            Collect(forceFieldDiagrams, entity, model);
        }
        internal void Collect(ForceFieldDiagramStakeholderEntity entity, ForceFieldDiagramStakeholder model)
        {
            Collect(forceFieldDiagramStakeholders, entity, model);
        }
        internal void Collect(AttitudeImpactDiagramEntity entity, AttitudeImpactDiagram model)
        {
            Collect(attitudeImpactDiagrams, entity, model);
        }
        internal void Collect(AttitudeImpactDiagramStakeholderEntity entity, AttitudeImpactDiagramStakeholder model)
        {
            Collect(attitudeImpactDiagramStakeholders, entity, model);
        }

        internal bool Contains(StakeholderEntity entity)
        {
            return Contains(stakeholders, entity);
        }
        internal bool Contains(OnionDiagramEntity entity)
        {
            return Contains(onionDiagrams, entity);
        }
        internal bool Contains(ForceFieldDiagramEntity entity)
        {
            return Contains(forceFieldDiagrams, entity);
        }
        internal bool Contains(ForceFieldDiagramStakeholderEntity entity)
        {
            return Contains(forceFieldDiagramStakeholders, entity);
        }
        internal bool Contains(AttitudeImpactDiagramEntity entity)
        {
            return Contains(attitudeImpactDiagrams, entity);
        }
        internal bool Contains(AttitudeImpactDiagramStakeholderEntity entity)
        {
            return Contains(attitudeImpactDiagramStakeholders, entity);
        }

        internal Stakeholder Get(StakeholderEntity entity)
        {
            return Get(stakeholders, entity);
        }
        internal OnionDiagram Get(OnionDiagramEntity entity)
        {
            return Get(onionDiagrams, entity);
        }
        internal ForceFieldDiagram Get(ForceFieldDiagramEntity entity)
        {
            return Get(forceFieldDiagrams, entity);
        }
        internal ForceFieldDiagramStakeholder Get(ForceFieldDiagramStakeholderEntity entity)
        {
            return Get(forceFieldDiagramStakeholders, entity);
        }
        internal AttitudeImpactDiagram Get(AttitudeImpactDiagramEntity entity)
        {
            return Get(attitudeImpactDiagrams, entity);
        }
        internal AttitudeImpactDiagramStakeholder Get(AttitudeImpactDiagramStakeholderEntity entity)
        {
            return Get(attitudeImpactDiagramStakeholders, entity);
        }

        #region helpers

        private TModel Get<TEntity, TModel>(Dictionary<TEntity, TModel> collection, TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
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
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return collection.ContainsKey(entity);
        }

        private void Collect<TEntity, TModel>(Dictionary<TEntity, TModel> collection, TEntity entity, TModel model)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            collection[entity] = model;
        }

        private static Dictionary<TEntity, TModel> CreateDictionary<TEntity, TModel>()
        {
            return new Dictionary<TEntity, TModel>(new ReferenceEqualityComparer<TEntity>());
        }
        #endregion
    }
}