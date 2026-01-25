using System;
using System.Collections.Generic;
using System.Text;
using Labrune.Properties;

namespace Labrune
{
    /// <summary>
    /// Utility class for normalizing special characters (e.g., Turkish -> ASCII).
    /// </summary>
    public static class CharacterNormalizer
    {
        /// <summary>
        /// Parse rules from Settings format: "ü:u,Ü:U,ğ:g,..."
        /// </summary>
        public static Dictionary<string, string> ParseRules(string rulesString)
        {
            var rules = new Dictionary<string, string>();
            
            if (string.IsNullOrEmpty(rulesString))
                return rules;

            string[] pairs = rulesString.Split(',');
            foreach (string pair in pairs)
            {
                string[] parts = pair.Split(':');
                if (parts.Length == 2)
                {
                    string from = parts[0].Trim();
                    string to = parts[1].Trim();
                    if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
                    {
                        rules[from] = to;
                    }
                }
            }

            return rules;
        }

        /// <summary>
        /// Apply normalization rules to a single string.
        /// </summary>
        public static string Normalize(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Check if normalization is enabled
            if (!Settings.Default.CharNormalizationEnabled)
                return text;

            // Parse rules from settings
            var rules = ParseRules(Settings.Default.CharNormalizationRules);
            
            if (rules.Count == 0)
                return text;

            // Apply each rule
            StringBuilder result = new StringBuilder(text);
            foreach (var rule in rules)
            {
                result.Replace(rule.Key, rule.Value);
            }

            return result.ToString();
        }

        /// <summary>
        /// Batch apply normalization to all records in a list.
        /// </summary>
        public static int NormalizeAllRecords(List<LanguageStringRecord> records)
        {
            if (records == null || records.Count == 0)
                return 0;

            // Check if normalization is enabled
            if (!Settings.Default.CharNormalizationEnabled)
                return 0;

            int changedCount = 0;

            foreach (var record in records)
            {
                if (!string.IsNullOrEmpty(record.Text))
                {
                    string normalized = Normalize(record.Text);
                    if (normalized != record.Text)
                    {
                        record.Text = normalized;
                        record.IsModified = true;
                        changedCount++;
                    }
                }

                if (!string.IsNullOrEmpty(record.Label))
                {
                    string normalized = Normalize(record.Label);
                    if (normalized != record.Label)
                    {
                        record.Label = normalized;
                        record.IsModified = true;
                        changedCount++;
                    }
                }
            }

            return changedCount;
        }
    }
}
