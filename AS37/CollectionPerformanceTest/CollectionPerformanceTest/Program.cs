using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;


namespace CollectionPerformanceTest
{
    class Program
    {
        static int TestScale = 10000000;
        static int TestInterval = 100000;
        static int TestNumbers = TestScale / TestInterval;

        static void Main(string[] args)
        {
            int absolutenr = 0;
            Stopwatch stopWatch = Stopwatch.StartNew();

            #region List Tests

            List<int> list = new List<int>();
            long[] listtimes = new long[TestNumbers];
            while (absolutenr < TestScale)
            {
                stopWatch.Restart();
                for (int i = 0; i < TestInterval; i++)
                {
                    list.Add(absolutenr);
                    absolutenr++;
                }
                long listtime = stopWatch.ElapsedMilliseconds;
                listtimes[absolutenr/TestInterval-1] = listtime;
                Console.WriteLine("List time(" + absolutenr + "): " + listtime);
            }



            //Free up some memory
            list = null;
            #endregion

            #region SortedDictionary Tests
            SortedDictionary<int, int> sortedDictionary = new SortedDictionary<int, int>();
            absolutenr = 0;
            long[] sortedtimes = new long[TestNumbers];
            while(absolutenr < TestScale)
            {
                stopWatch.Restart();
                for (int i = 0; i < TestInterval; i++)
                {
                    sortedDictionary.Add(absolutenr, i);
                    absolutenr++;
                }
                long sortedTime = stopWatch.ElapsedMilliseconds;
                sortedtimes[absolutenr / TestInterval - 1] = sortedTime;
                Console.WriteLine("SortedDictionary time(" + sortedDictionary.Count + "): " + sortedTime);
            }

            #endregion

            #region Dictionary Tests
            long[] dictiontimes = new long[TestNumbers];
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            absolutenr = 0;
            long[] diciontimes = new long[TestNumbers];
            while (absolutenr < TestScale)
            {
                stopWatch.Restart();
                for (int i = 0; i < TestInterval; i++)
                {
                    dictionary.Add(absolutenr, i);
                    absolutenr++;
                }
                long dictiontime = stopWatch.ElapsedMilliseconds;
                dictiontimes[absolutenr / TestInterval - 1] = dictiontime;
                Console.WriteLine("Dictionary time(" + dictionary.Count + "): " + dictiontime);
            }

            #endregion

            SaveAsExcel(listtimes,sortedtimes,dictiontimes);


            Console.WriteLine("Press any key to close");
            Console.Read();
        }

        /// <summary>
        /// Some code come from http://csharp.net-informations.com/excel/csharp-excel-chart.htm
        /// </summary>
        static void SaveAsExcel(long[] ListTimes, long[] SortedDictionaryTimes, long[] DictionaryTimes)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Worksheet xlWorkSheet2;
            Excel.Worksheet xlWorkSheet3;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            //WORKSHEET Add Test
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = "Add Test";

            xlWorkSheet.Cells[1, 1] = "Test Count";
            xlWorkSheet.Cells[1, 2] = "List";
            xlWorkSheet.Cells[1, 3] = "Sorted Dictionary";
            xlWorkSheet.Cells[1, 4] = "Dictionary";

            for (int i = 0; i < ListTimes.Length; i++)
            {
                xlWorkSheet.Cells[i + 2, 1] = (i+1) * TestInterval;
            }

            for (int i = 0; i < ListTimes.Length; i++)
            {
                xlWorkSheet.Cells[i + 2, 2] = ListTimes[i];
            }

            for (int i = 0; i < SortedDictionaryTimes.Length; i++)
            {
                xlWorkSheet.Cells[i + 2, 3] = SortedDictionaryTimes[i];
            }

            for (int i = 0; i < DictionaryTimes.Length; i++)
            {
                xlWorkSheet.Cells[i + 2, 4] = DictionaryTimes[i];
            }


            //WORKSHEET2 Contain Test
            xlWorkSheet2 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
            xlWorkSheet2.Name = "Contain Test";

            xlWorkSheet2.Cells[1, 1] = "Test Count";
            xlWorkSheet2.Cells[1, 2] = "List";
            xlWorkSheet2.Cells[1, 3] = "Sorted Dictionary";
            xlWorkSheet2.Cells[1, 4] = "Dictionary";


            //WORKSHEET3 Binary Contain Test
            xlWorkSheet3 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);
            xlWorkSheet3.Name = "Binary Contain Test";

            xlWorkSheet3.Cells[1, 1] = "Test Count";
            xlWorkSheet3.Cells[1, 2] = "List";
            xlWorkSheet3.Cells[1, 3] = "Dictionary";
            

            //Save and cleanup
            xlWorkBook.SaveAs(Directory.GetCurrentDirectory() + "\\csharp.net-informations.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);


        }

        /// <summary>
        /// The code is from http://csharp.net-informations.com/excel/csharp-excel-chart.htm
        /// </summary>
        /// <param name="obj"></param>
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
