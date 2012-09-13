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
        patterns.Add(groupName, new RegExPattern(groupName, convertToRegEx(pattern)));
      }
      string days = @"(mon|tue|wed|thu|fri|sat|sun)";
      string months = @"(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)";
      patterns.Add("URL", new RegExPattern("URL", @"\b(?:\S+)://(\S+)\b", ConsoleColor.Blue, ConsoleColor.White));
      patterns.Add("Date", new RegExPattern("Date", days + @"(,)?\s" + "(([0-2][0-9])|(30))" + @"\s" + months + @"\s" + @"[0-9]{4}\s((0?[0-9])|(([0-1][0-9])|(2[0-3]))):[0-5][0-9]:[0-5][0-9]\s[\+|-][0-9]{4}", ConsoleColor.Red, ConsoleColor.White));
      highlightRegExMatches(input);
    }

    private string convertToRegEx(string input) {
      input = input.Replace("+", @"\W?");
      if (input.Substring(0, 1).Equals("*")) input = input.Replace("*", @"\w*?");
      else if (input.Substring(input.Length - 1, 1).Equals("*")) input = input.Replace("*", @"\w*?");
      else input = input.Replace("*", @"\w*");
      return @"\b" + input + @"\b";
    }

    private void highlightRegExMatches(string haystack) {
      StringBuilder entirePattern = new StringBuilder();
      foreach (RegExPattern pattern in patterns.Values) {
        entirePattern.Append(pattern.RegEx + "|");
      }
      string needle = entirePattern.ToString().Substring(0, entirePattern.Length - 1);
      Regex regex = new Regex(needle, RegexOptions.IgnoreCase);
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

      public RegExPattern(string GroupName, string RegEx, ConsoleColor BgColor, ConsoleColor FgColor)
        : this() {
        this.GroupName = GroupName;
        this.RegEx = "(?<" + GroupName + ">" + RegEx + ")";
        this.BgColor = BgColor;
        this.FgColor = FgColor;
      }

      public RegExPattern(string GroupName, string RegEx)
        : this() {
        this.GroupName = GroupName;
        this.RegEx = "(?<" + GroupName + ">" + RegEx + ")";
        this.BgColor = ConsoleColor.Yellow;
        this.FgColor = ConsoleColor.Black;
      }
    }
  }
}
