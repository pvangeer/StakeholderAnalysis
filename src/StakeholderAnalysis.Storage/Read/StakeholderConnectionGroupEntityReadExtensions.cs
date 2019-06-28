﻿using System;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Storage.DbContext;

namespace StakeholderAnalysis.Storage.Read
{
    internal static class StakeholderConnectionGroupEntityReadExtensions
    {
        internal static StakeholderConnectionGroup Read(this StakeholderConnectionGroupEntity entity, ReadConversionCollector collector)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (collector == null)
            {
                throw new ArgumentNullException(nameof(collector));
            }

            if (collector.Contains(entity))
            {
                return collector.Get(entity);
            }

            var stakeholderConnectionGroup = new StakeholderConnectionGroup
            {
                Name = entity.Name,
                StrokeThickness = entity.StrokeThickness.ToNullAsNaN(),
                StrokeColor = entity.Color.ToColor(),
                Visible = entity.Visible == 1
            };

            collector.Collect(entity, stakeholderConnectionGroup);

            return stakeholderConnectionGroup;
        }
    }
}
