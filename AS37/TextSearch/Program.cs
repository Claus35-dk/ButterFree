using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace TextSearch {
  class TextSearch {
    Dictionary<string, RegExPattern> patterns = new Dictionary<string, RegExPattern>();
    static int regexCounter = 0;
    static void Main(string[] args) {
      String input = TextFileReader.ReadFile("../../testFile.txt");
      new TextSearch(input, args);
    }
    private TextSearch(string input, string[] userPatterns) {
      foreach (string pattern in userPatterns) {
        string groupName = "G" + regexCounter++;
        patterns.Add(groupName, new RegExPattern(groupName, pattern));
      }
      patterns.Add("URL", new RegExPattern("URL", @"\b(?:\S+)://(\S+)\b", ConsoleColor.Blue, ConsoleColor.White));
      patterns.Add("Date", new RegExPattern("Date", @"\b((0?[0-9])|(1[0 -2])):[0-5][0-9]\b", ConsoleColor.Red, ConsoleColor.White));
      highlightRegExMatches(input);
    }

    private void highlightRegExMatches(string haystack) {
      StringBuilder entirePattern = new StringBuilder();
      foreach (RegExPattern pattern in patterns.Values) {
        entirePattern.Append(pattern.RegEx + "|");
      }
      string needle = entirePattern.ToString().Substring(0, entirePattern.Length - 1);
      Regex regex = new Regex(needle);
      MatchCollection matches = regex.Matches(haystack);
      int stringIndex = 0;
      foreach (Match match in matches) {
        while (stringIndex < match.Index) Console.Write(haystack[stringIndex++]);
        foreach (string group in regex.GetGroupNames()) {
          double tmp = 0;
          if (match.Groups[group].Success && !double.TryParse(group, out tmp)) {
            RegExPattern matchGroup = patterns[group];
            Console.BackgroundColor = matchGroup.BgColor;
            Console.ForegroundColor = matchGroup.FgColor;
            while (stringIndex < match.Index + match.Length) Console.Write(haystack[stringIndex++]);
            Console.ResetColor();
            break;
          }
        }
      }
    }

    /// <summary>
    /// Linking a group name/regex expression with background and foreground color
    /// </summary>
    struct RegExPattern {
      public string GroupName { set; get; }
      public string RegEx { set; get; }
      public ConsoleColor BgColor { set; get; }
      public ConsoleColor FgColor { set; get; }

      public RegExPattern(string GroupName, string RegEx, ConsoleColor BgColor, ConsoleColor FgColor) : this() {
        this.GroupName = GroupName;
        this.RegEx = "(?<" + GroupName + ">" + RegEx + ")";
        this.BgColor = BgColor;
        this.FgColor = FgColor;
      }

      public RegExPattern(string GroupName, string RegEx) : this() {
        this.GroupName = GroupName;
        this.RegEx = "(?<" + GroupName + ">" + RegEx + ")";
        this.BgColor = ConsoleColor.Yellow;
        this.FgColor = ConsoleColor.White;
      }
    }
  }
}
