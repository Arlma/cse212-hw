using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Create a HashSet for O(1) lookups
        var wordSet = new HashSet<string>(words);
        var results = new HashSet<string>();

        foreach (var word in words)
        {
            // Skip words with same letters (e.g., 'aa')
            if (word[0] == word[1])
                continue;

            // Create the reversed word
            var reversed = new string(new[] { word[1], word[0] });

            // If we find the reversed word in our set and haven't already added this pair
            if (wordSet.Contains(reversed))
            {
                // Create the pair string with words in sorted order to avoid duplicates
                var pair = word.CompareTo(reversed) < 0
                    ? $"{word} & {reversed}"
                    : $"{reversed} & {word}";

                results.Add(pair);
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Determines if two strings are anagrams of each other (same letters, different order)
    /// </summary>
    /// <param name="word1">The first word to check</param>
    /// <param name="word2">The second word to check</param>
    /// <returns>True if the words are anagrams of each other, false otherwise</returns>
    public static bool IsAnagram(string word1, string word2)
    {
        // Remove spaces and convert to lowercase for comparison
        var cleaned1 = word1.Replace(" ", "").ToLower();
        var cleaned2 = word2.Replace(" ", "").ToLower();

        // If lengths are different, they can't be anagrams
        if (cleaned1.Length != cleaned2.Length)
            return false;

        // Create dictionaries to store character counts
        var counts1 = new Dictionary<char, int>();
        var counts2 = new Dictionary<char, int>();

        // Count characters in first word
        foreach (char c in cleaned1)
        {
            if (!counts1.ContainsKey(c))
                counts1[c] = 0;
            counts1[c]++;
        }

        // Count characters in second word
        foreach (char c in cleaned2)
        {
            if (!counts2.ContainsKey(c))
                counts2[c] = 0;
            counts2[c]++;
        }

        // Compare character counts
        foreach (var kvp in counts1)
        {
            if (!counts2.ContainsKey(kvp.Key) || counts2[kvp.Key] != kvp.Value)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Gets earthquake data from USGS and returns formatted location and magnitude information
    /// </summary>
    /// <returns>List of strings with earthquake details</returns>
    public static string[] EarthquakeDailySummary()
    {
        var url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();

        try
        {
            var json = client.GetStringAsync(url).Result;
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var fc = JsonSerializer.Deserialize<FeatureCollection>(json, options);

            var results = new List<string>();
            if (fc?.Features == null)
                return results.ToArray();

            foreach (var f in fc.Features)
            {
                var place = f?.Properties?.Place;
                var mag = f?.Properties?.Mag;
                if (!string.IsNullOrEmpty(place) && mag.HasValue)
                {
                    results.Add($"{place} - Mag {mag.Value}");
                }
            }
            return results.ToArray();
        }
        catch (Exception)
        {
            // Return empty array if there's any error
            return new string[0];
        }
    }

    /// <summary>
    /// Summarize the number of degrees (both bachelor and master) that each person has
    /// using a dictionary
    /// </summary>
    /// <param name="degrees">Dictionary of name to list of degrees</param>
    /// <returns>Dictionary of name to count of degrees</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var result = new Dictionary<string, int>();
        var lines = File.ReadAllLines(filename);

        foreach (var line in lines)
        {
            var degree = line.Split(',')[3].Trim(); // Assumes education level is in 4th column
            if (!result.ContainsKey(degree))
                result[degree] = 0;
            result[degree]++;
        }

        return result;
    }
}