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

        static int ContainTest = 100;
        static int ContainStep = TestScale / ContainTest;
        static int ContainPrint = 10;
        static int ContainPrintSum = ContainTest / ContainPrint;

        static void Main(string[] args)
        {
            int absolutenr = 0;
            Stopwatch stopWatch = Stopwatch.StartNew();

            int[] randomnumbers = getRandomArray(TestScale);
            

            #region List Tests

            List<int> list = new List<int>();
            long[] listtimes = new long[TestNumbers];
            while (absolutenr < TestScale)
            {
                stopWatch.Restart();
                for (int i = 0; i < TestInterval; i++)
                {
                    list.Add(randomnumbers[absolutenr]);
                    absolutenr++;
                }
                long listtime = stopWatch.ElapsedMilliseconds;
                listtimes[absolutenr/TestInterval-1] = listtime;
                Console.WriteLine("List time(" + absolutenr + "): " + listtime);
            }

            //Contain not sorted
            long[] containtimes = new long[ContainPrintSum];
            for (int i = 0; i*TestScale/ContainPrintSum < TestScale;i++ )
            {
                stopWatch.Restart();
                for (int step = i; step < i + TestScale/ContainPrint; step += ContainStep)
                {
                    list.Contains(step);
                }
                long listtime = stopWatch.ElapsedMilliseconds;
                containtimes[i] = listtime;
                Console.WriteLine("List Contain time(" + i + "): " + listtime);
            }

            int last = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (last > list.ElementAt(i))
                {
                    Console.WriteLine("ERROR NOT SORTED(not sorted)");
                    break;
                }
                last = list.ElementAt(i);
            }
            
            //Binary Contain
            stopWatch.Reset();
            list.Sort();
            long sorttime = stopWatch.ElapsedMilliseconds;
            last = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (last > list.ElementAt(i))
                {
                    Console.WriteLine("ERROR NOT SORTED(sorted)");
                    break;
                }
                last = list.ElementAt(i);
            }
            Console.WriteLine("List length: " + list.Count.ToString());
            Console.Read();
            long[] bcontaintimes = new long[ContainPrintSum];
            for (int i = 0; i * TestScale / ContainPrint < TestScale; i++)
            {
                stopWatch.Restart();
                for (int step = i; step < i + TestScale / ContainPrint; step += ContainStep)
                {
                    list.BinarySearch(i);
                }
                long listtime = stopWatch.ElapsedMilliseconds;
                bcontaintimes[i] = listtime;
                Console.WriteLine("List Binary Contain time(" + i + "): " + listtime);
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

            //Contain test
            long[] SDcontaintimes = new long[ContainPrintSum];
            for (int i = 0; i * TestScale / ContainPrint < TestScale; i++)
            {
                stopWatch.Restart();
                for (int step = i; step < i + TestScale / ContainPrint; step += ContainStep)
                {
                    sortedDictionary.ContainsKey(step);
                }
                long listtime = stopWatch.ElapsedMilliseconds;
                SDcontaintimes[i] = listtime;
                Console.WriteLine("Sorted Dictionary Contain time(" + i + "): " + listtime);
            }

            sortedDictionary = null;
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

            //Contain test
            long[] Dcontaintimes = new long[ContainPrintSum];
            for (int i = 0; i * TestScale / ContainPrint < TestScale; i++)
            {
                stopWatch.Restart();
                for (int step = i; step < i + TestScale / ContainPrint; step += ContainStep)
                {
                    dictionary.ContainsKey(step);
                }
                long listtime = stopWatch.ElapsedMilliseconds;
                Dcontaintimes[i] = listtime;
                Console.WriteLine("Sorted Dictionary Contain time(" + i + "): " + listtime);
            }

            //Binary Contain test
            

            dictionary = null;
            #endregion

            Console.WriteLine("Sort time: " + sorttime);

            SaveAsExcel(listtimes,sortedtimes,dictiontimes,containtimes,SDcontaintimes,Dcontaintimes,bcontaintimes, new long[0],sorttime);

            Console.WriteLine("Press any key to close");
            Console.Read();
        }

        /// <summary>
        /// Some code come from http://csharp.net-informations.com/excel/csharp-excel-chart.htm
        /// </summary>
        static void SaveAsExcel(long[] ListTimes, long[] SortedDictionaryTimes, long[] DictionaryTimes, long[] ContainList, long[] ContainSortedD, long[] ContainDictionary,long[] binaryListContain, long[] binaryDictionaryContain,long sorttime)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Worksheet xlWorkSheet2;
            Excel.Worksheet xlWorkSheet3;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            #region WorkSheet
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

            #endregion

            #region WorkSheet2
            xlWorkSheet2 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
            xlWorkSheet2.Name = "Contain Test";

            xlWorkSheet2.Cells[1, 1] = "Test Count";
            xlWorkSheet2.Cells[1, 2] = "List";
            xlWorkSheet2.Cells[1, 3] = "Sorted Dictionary";
            xlWorkSheet2.Cells[1, 4] = "Dictionary";

            for (int i = 0; i < ContainList.Length; i++)
            {
                xlWorkSheet2.Cells[i + 2, 1] = (i + 1) * TestScale/ContainPrint;
            }
            for (int i = 0; i < ContainList.Length; i++)
            {
                xlWorkSheet2.Cells[i + 2, 2] = ContainList[i];
            }
            for (int i = 0; i < ContainSortedD.Length; i++)
            {
                xlWorkSheet2.Cells[i + 2, 3] = ContainSortedD[i];
            }
            for (int i = 0; i < ContainDictionary.Length; i++)
            {
                xlWorkSheet2.Cells[i + 2, 4] = ContainDictionary[i];
            }

            #endregion

            #region WorkSheet3
            xlWorkSheet3 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);
            xlWorkSheet3.Name = "Binary Contain Test";

            xlWorkSheet3.Cells[1, 1] = "Test Count";
            xlWorkSheet3.Cells[1, 2] = "List";
            xlWorkSheet3.Cells[1, 3] = "Dictionary";
            xlWorkSheet3.Cells[1, 5] = "Sort Time: " + sorttime.ToString();

            for (int i = 0; i < binaryListContain.Length; i++)
            {
                xlWorkSheet3.Cells[i + 2, 1] = (i + 1) * ContainStep;
            }
            for (int i = 0; i < binaryListContain.Length; i++)
            {
                xlWorkSheet3.Cells[i + 2, 2] = binaryListContain[i];
            }
            for (int i = 0; i < binaryDictionaryContain.Length; i++)
            {
                xlWorkSheet3.Cells[i + 2, 3] = binaryDictionaryContain[i];
            }

            #endregion

            //Save and cleanup
            xlWorkBook.SaveAs(Directory.GetCurrentDirectory() + "\\csharp.net-informations2.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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

        private static int[] getRandomArray(int size)
        {
            int[] tmp = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                tmp[i] = random.Next(0,size);
            }
            
            return tmp;
        }

    }
}
