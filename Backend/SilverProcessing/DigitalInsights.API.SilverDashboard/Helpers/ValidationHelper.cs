using DigitalInsights.DB.Silver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.Helpers
{
    internal static class ValidationHelper
    {
        internal static bool ValidateAndSetProperty<T>(PropertyMetadata metadata, Func<T> getter, Action<T> setter, HashSet<int> dictionary = null)
        {
            if (!metadata.IsEditable)
            {
                return true;
            }
            switch (metadata.FieldType)
            {
                
                case DB.Common.Enums.FieldType.Boolean:
                    {
                        if (typeof(T) == typeof(bool?))
                        {
                            ValidateAndSetBoolean(metadata, getter as Func<bool?>, setter as Action<bool?>);
                            return true;
                        }
                        throw new InvalidOperationException("Inconsistent field type");
                    }
                case DB.Common.Enums.FieldType.String:
                    {
                        if (typeof(T) == typeof(string))
                        {
                            ValidateAndSetString(metadata, getter as Func<string>, setter as Action<string>);
                            return true;
                        }
                        throw new InvalidOperationException("Inconsistent field type");
                    }
                case DB.Common.Enums.FieldType.Integer:
                    {
                        if (typeof(T) == typeof(int?))
                        {
                            ValidateAndSetInteger(metadata, getter as Func<int?>, setter as Action<int?>);
                            return true;
                        }
                        throw new InvalidOperationException("Inconsistent field type");
                    }
                case DB.Common.Enums.FieldType.Float:
                case DB.Common.Enums.FieldType.Percentage:
                    {
                        if (typeof(T) == typeof(double?))
                        {
                            ValidateAndSetDouble(metadata, getter as Func<double?>, setter as Action<double?>);
                            return true;
                        }
                        throw new InvalidOperationException("Inconsistent field type");
                    }
                case DB.Common.Enums.FieldType.Date:
                    {
                        if (typeof(T) == typeof(DateTime?))
                        {
                            ValidateAndSetDateTime(metadata, getter as Func<DateTime?>, setter as Action<DateTime?>);
                            return true;
                        }
                        throw new InvalidOperationException("Inconsistent field type");
                    }
                case DB.Common.Enums.FieldType.DropDown:
                    {
                        if(dictionary == null || string.IsNullOrEmpty(metadata.DropDownDictionary))
                        {
                            throw new InvalidOperationException("Inconsistent field type");
                        }
                        ValidateAndSetDictionary(metadata, getter as Func<int?>, setter as Action<int?>, dictionary);
                        return true;

                    }
                case DB.Common.Enums.FieldType.NestedEntity:
                case DB.Common.Enums.FieldType.Array:
                    {
                        if (getter() == null && !metadata.AllowsNull)
                        {
                            throw new ArgumentException($"{metadata.EntityName} {metadata.PropertyName}");
                        }
                        return true;
                    }
                default:
                    throw new InvalidOperationException("Unknown field type");
            }
        }

        private static void ValidateAndSetDictionary(PropertyMetadata metadata, Func<int?> getter, Action<int?> setter, HashSet<int> dictionary)
        {
            if (!metadata.AllowsNull && getter() == null || getter() != null && !dictionary.Contains(getter().Value))
            {
                throw new ArgumentException($"{metadata.EntityName} {metadata.PropertyName}");
            }
            setter(getter());
        }

        static void ValidateAndSetBoolean(PropertyMetadata metadata, Func<bool?> getter, Action<bool?> setter)
        {
            if (!metadata.AllowsNull && getter() == null)
            {
                throw new ArgumentException($"{metadata.EntityName} {metadata.PropertyName}");
            }
            setter(getter());
        }
        static void ValidateAndSetString(PropertyMetadata metadata, Func<string> getter, Action<string> setter)
        {
            if (!metadata.AllowsNull && getter() == null)
            {
                throw new ArgumentException($"{metadata.EntityName} {metadata.PropertyName}");
            }
            setter(getter());
        }

        static void ValidateAndSetInteger(PropertyMetadata metadata, Func<int?> getter, Action<int?> setter)
        {
            if (!metadata.AllowsNull && getter() == null || getter() != null &&
                    (!string.IsNullOrEmpty(metadata.RangeLow) && getter().Value < int.Parse(metadata.RangeLow)
                    || !string.IsNullOrEmpty(metadata.RangeHigh) && getter().Value > int.Parse(metadata.RangeHigh)))
            {
                throw new ArgumentException($"{metadata.EntityName} {metadata.PropertyName}");
            }
            setter(getter());
        }

        static void ValidateAndSetDouble(PropertyMetadata metadata, Func<double?> getter, Action<double?> setter)
        {
            if (!metadata.AllowsNull && getter() == null || getter() != null &&
                    (!string.IsNullOrEmpty(metadata.RangeLow) && getter().Value < double.Parse(metadata.RangeLow)
                    || !string.IsNullOrEmpty(metadata.RangeHigh) && getter().Value > double.Parse(metadata.RangeHigh)))
            {
                throw new ArgumentException($"{metadata.EntityName} {metadata.PropertyName}");
            }
            setter(getter());
        }

        static void ValidateAndSetDateTime(PropertyMetadata metadata, Func<DateTime?> getter, Action<DateTime?> setter)
        {
            if (!metadata.AllowsNull && getter() == null || getter() != null &&
                    (!string.IsNullOrEmpty(metadata.RangeLow) && getter().Value < DateTime.Parse(metadata.RangeLow)
                    || !string.IsNullOrEmpty(metadata.RangeHigh) && getter().Value > DateTime.Parse(metadata.RangeHigh)))
            {
                throw new ArgumentException($"{metadata.EntityName} {metadata.PropertyName}");
            }
            setter(getter());
        }
    }
}
