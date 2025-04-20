using System;
using System.Linq;
using System.Collections.Generic;
using SkillIssue.SaveSystem.Core;
using SkillIssue.SaveSystem.Models;
using UnityEngine;

namespace SkillIssue.SaveSystem.Extensions
{
    public static class SaveSystemExtensions
    {
        /*
        public static void PrintAttributes<T>(this T obj, AttributeQuerySettings settings = default)
        {
            List<IAttribute> attributes = typeof(T).GetCustomAttributes(false).OfType<IAttribute>().ToList();
            
            attributes = settings.UniquenessConstraints switch
            {
                UniquenessConstraints.None => attributes,

                UniquenessConstraints.AttributeType => attributes
                    .GroupBy(attribute => attribute.GetType())
                    .Select(group => group.First())
                    .ToList(),

                UniquenessConstraints.AttributeValue => attributes
                    .GroupBy(attribute => attribute.GetData(DataFormat.Json))
                    .Select(group => group.First())
                    .ToList(),

                UniquenessConstraints.All => attributes
                    .GroupBy(attribute => (attribute.GetType(), attribute.GetData(DataFormat.Json)))
                    .Select(group => group.First())
                    .ToList(),

                _ => throw new ArgumentOutOfRangeException(nameof(settings.UniquenessConstraints),
                    $"Unexpected value of AttributeQuerySettings.UniquenessConstraints: {settings.UniquenessConstraints}.")
            };

            if (settings.FilterType != null)
                attributes = attributes
                    .Where(a => a.GetType() == settings.FilterType)
                    .ToList();

            List<string> names = attributes.Any()
                ? attributes.Select(a => a.GetType().Name).ToList()
                : null;

            if (names == null)
            {
                Debug.Log("No attributes found.");
                return;
            }

            Debug.Log($"Names: {string.Join(", ", names)}");
        }

        public static void PrintAttributesData<T>(this T obj, AttributeQuerySettings settings = default)
        {
            List<IAttribute> attributes = typeof(T).GetCustomAttributes(false).OfType<IAttribute>().ToList();

            attributes = settings.UniquenessConstraints switch
            {
                UniquenessConstraints.None => attributes,

                UniquenessConstraints.AttributeType => attributes
                    .GroupBy(attribute => attribute.GetType())
                    .Select(group => group.First())
                    .ToList(),

                UniquenessConstraints.AttributeValue => attributes
                    .GroupBy(attribute => attribute.GetData(DataFormat.Json))
                    .Select(group => group.First())
                    .ToList(),

                UniquenessConstraints.All => attributes
                    .GroupBy(attribute => (attribute.GetType(), attribute.GetData(DataFormat.Json)))
                    .Select(group => group.First())
                    .ToList(),

                _ => throw new ArgumentOutOfRangeException(nameof(settings.UniquenessConstraints),
                    $"Unexpected value of AttributeQuerySettings.UniquenessConstraints: {settings.UniquenessConstraints}.")
            };

            if (settings.FilterType != null)
                attributes = attributes
                    .Where(a => a.GetType() == settings.FilterType)
                    .ToList();

            List<string> values = attributes.Any()
                ? attributes.Select(a => a.GetData(DataFormat.Json)).ToList()
                : null;
            
            if (values == null)
            {
                Debug.Log("No attribute data found.");
                return;
            }
            
            Debug.Log($"Values: {string.Join(", ", values)}");
        }
        */
    }
}
