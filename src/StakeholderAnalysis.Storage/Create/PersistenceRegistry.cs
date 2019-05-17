﻿using System;
using System.Collections.Generic;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    public class PersistenceRegistry
    {
        private readonly Dictionary<Stakeholder, StakeholderEntity> stakeholders = CreateDictionary<Stakeholder, StakeholderEntity>();
        private readonly Dictionary<AttitudeImpactDiagram, AttitudeImpactDiagramEntity> attitudeImpactDiagrams = CreateDictionary<AttitudeImpactDiagram, AttitudeImpactDiagramEntity>();
        private readonly Dictionary<AttitudeImpactDiagramStakeholder, AttitudeImpactDiagramStakeholderEntity> attitudeImpactDiagramStakeholders = CreateDictionary<AttitudeImpactDiagramStakeholder, AttitudeImpactDiagramStakeholderEntity>();

        #region Register Methods

        internal void Register(Stakeholder model, StakeholderEntity entity)
        {
            Register(stakeholders, model, entity);
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