using System;
using System.Collections.Generic;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal class ReadConversionCollector
    {
        private readonly Dictionary<StakeholderEntity, Stakeholder> stakeholders = CreateDictionary<StakeholderEntity, Stakeholder>();
        private readonly Dictionary<OnionDiagramEntity, OnionDiagram> onionDiagrams = CreateDictionary<OnionDiagramEntity, OnionDiagram>();

        internal void Collect(StakeholderEntity entity, Stakeholder model)
        {
            Collect(stakeholders,entity,model);
        }
        internal void Collect(OnionDiagramEntity entity, OnionDiagram model)
        {
            Collect(onionDiagrams, entity, model);
        }

        internal bool Contains(StakeholderEntity entity)
        {
            return Contains(stakeholders, entity);
        }
        internal bool Contains(OnionDiagramEntity entity)
        {
            return Contains(onionDiagrams, entity);
        }

        internal Stakeholder Get(StakeholderEntity entity)
        {
            return Get(stakeholders, entity);
        }
        internal OnionDiagram Get(OnionDiagramEntity entity)
        {
            return Get(onionDiagrams, entity);
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