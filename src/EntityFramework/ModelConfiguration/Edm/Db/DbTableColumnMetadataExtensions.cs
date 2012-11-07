// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.ModelConfiguration.Edm.Db
{
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.ModelConfiguration.Edm.Common;
    using System.Data.Entity.Utilities;
    using System.Diagnostics.Contracts;

    internal static class DbTableColumnMetadataExtensions
    {
        private const string OrderAnnotation = "Order";
        private const string PreferredNameAnnotation = "PreferredName";
        private const string UnpreferredUniqueNameAnnotation = "UnpreferredUniqueName";
        private const string AllowOverrideAnnotation = "AllowOverride";

        public static void CopyFrom(this EdmProperty column, EdmProperty other)
        {
            Contract.Requires(column != null);
            Contract.Requires(other != null);

            column.IsFixedLength = other.IsFixedLength;
            column.IsMaxLength = other.IsMaxLength;
            column.IsUnicode = other.IsUnicode;
            column.MaxLength = other.MaxLength;
            column.Precision = other.Precision;
            column.Scale = other.Scale;
        }

        public static EdmProperty Clone(this EdmProperty tableColumn)
        {
            Contract.Requires(tableColumn != null);

            var columnMetadata
                = new EdmProperty(tableColumn.Name, tableColumn.TypeUsage)
                      {
                          Nullable = tableColumn.Nullable,
                          StoreGeneratedPattern = tableColumn.StoreGeneratedPattern,
                          IsFixedLength = tableColumn.IsFixedLength,
                          IsMaxLength = tableColumn.IsMaxLength,
                          IsUnicode = tableColumn.IsUnicode,
                          MaxLength = tableColumn.MaxLength,
                          Precision = tableColumn.Precision,
                          Scale = tableColumn.Scale
                      };

            tableColumn.Annotations.Each(a => columnMetadata.Annotations.Add(a));

            return columnMetadata;
        }

        public static int? GetOrder(this EdmProperty tableColumn)
        {
            Contract.Requires(tableColumn != null);

            return (int?)tableColumn.Annotations.GetAnnotation(OrderAnnotation);
        }

        public static void SetOrder(this EdmProperty tableColumn, int order)
        {
            Contract.Requires(tableColumn != null);

            tableColumn.Annotations.SetAnnotation(OrderAnnotation, order);
        }

        public static string GetPreferredName(this EdmProperty tableColumn)
        {
            Contract.Requires(tableColumn != null);

            return (string)tableColumn.Annotations.GetAnnotation(PreferredNameAnnotation);
        }

        public static void SetPreferredName(this EdmProperty tableColumn, string name)
        {
            Contract.Requires(tableColumn != null);

            tableColumn.Annotations.SetAnnotation(PreferredNameAnnotation, name);
        }

        public static string GetUnpreferredUniqueName(this EdmProperty tableColumn)
        {
            Contract.Requires(tableColumn != null);

            return (string)tableColumn.Annotations.GetAnnotation(UnpreferredUniqueNameAnnotation);
        }

        public static void SetUnpreferredUniqueName(this EdmProperty tableColumn, string name)
        {
            Contract.Requires(tableColumn != null);

            tableColumn.Annotations.SetAnnotation(UnpreferredUniqueNameAnnotation, name);
        }

        public static void RemoveStoreGeneratedIdentityPattern(this EdmProperty tableColumn)
        {
            Contract.Requires(tableColumn != null);

            if (tableColumn.StoreGeneratedPattern
                == StoreGeneratedPattern.Identity)
            {
                tableColumn.StoreGeneratedPattern = StoreGeneratedPattern.None;
            }
        }

        public static bool GetAllowOverride(this EdmProperty column)
        {
            Contract.Requires(column != null);

            return (bool)column.Annotations.GetAnnotation(AllowOverrideAnnotation);
        }

        public static void SetAllowOverride(this EdmProperty column, bool allowOverride)
        {
            Contract.Requires(column != null);

            column.Annotations.SetAnnotation(AllowOverrideAnnotation, allowOverride);
        }
    }
}
