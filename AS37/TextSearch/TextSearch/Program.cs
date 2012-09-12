using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextSearch {
  class Program {
    static void Main(string[] args) {
      String input = TextFileReader.ReadFile("../../testFile.txt");
      Console.WriteLine(input);
    }
  }
}
