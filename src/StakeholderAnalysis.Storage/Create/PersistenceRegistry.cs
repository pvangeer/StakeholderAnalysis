using System;
using System.Collections.Generic;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Create
{
    public class PersistenceRegistry
    {
        private readonly Dictionary<Analysis, AnalysisEntity> projects = CreateDictionary<Analysis, AnalysisEntity>();

        #region Register Methods

        internal void Register(Analysis model, AnalysisEntity entity)
        {
            Register(projects, model, entity);
        }

        #endregion

        #region Contains Methods

        internal bool Contains(Analysis model)
        {
            return ContainsValue(projects, model);
        }
        #endregion

        #region Get Methods

        public AnalysisEntity Get(Analysis model)
        {
            return Get(projects, model);
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