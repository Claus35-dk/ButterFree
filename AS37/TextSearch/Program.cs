using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextSearch {
  class TextSearch {
    static void Main(string[] args) {
      new TextSearch();
    }


    public TextSearch() {
      String input = TextFileReader.ReadFile("../../testFile.txt");
      Console.WriteLine(input);
    }
  }
}
