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

        static int BContainTest = 10000000;
        static int BContainStep = TestScale / BContainTest;
        static int BContainPrint = 1000000;
        static int BContainPrintSum = BContainTest / BContainPrint;

        static void Main(string[] args)
        {
            try
            {
                runtest();
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Press any key to exit");
                Console.Read();
            }
        }

        /// <summary>
        /// Some code come from http://csharp.net-informations.com/excel/csharp-excel-chart.htm
        /// </summary>
        static void SaveAsExcel(long[] ListTimes, long[] SortedDictionaryTimes, long[] DictionaryTimes, long[] ContainList,long[] ContainShortList, long[] ContainSortedD, long[] ContainDictionary,long[] binaryListContain, long[] binaryDictionaryContain,long sorttime)
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

            xlWorkSheet.Cells[1, 1] = "Number of elements inserted";
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

            xlWorkSheet2.Cells[1, 1] = "Number of Contains called";
            xlWorkSheet2.Cells[1, 2] = "List";
            xlWorkSheet2.Cells[1, 3] = "Short List(100000)";
            xlWorkSheet2.Cells[1, 4] = "Sorted Dictionary";
            xlWorkSheet2.Cells[1, 5] = "Dictionary";
            
            for (int i = 0; i < ContainList.Length; i++)
            {
                xlWorkSheet2.Cells[i + 2, 1] = (i + 1) * ContainPrint;
            }
            for (int i = 0; i < ContainList.Length; i++)
            {
                xlWorkSheet2.Cells[i + 2, 2] = ContainList[i];
            }
            for (int i = 0; i < ContainShortList.Length; i++)
            {
                xlWorkSheet2.Cells[i + 2, 3] = ContainShortList[i];
            }
            for (int i = 0; i < ContainSortedD.Length; i++)
            {
                xlWorkSheet2.Cells[i + 2, 4] = ContainSortedD[i];
            }
            for (int i = 0; i < ContainDictionary.Length; i++)
            {
                xlWorkSheet2.Cells[i + 2, 5] = ContainDictionary[i];
            }

            #endregion

            #region WorkSheet3
            xlWorkSheet3 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);
            xlWorkSheet3.Name = "Binary Contain Test";

            xlWorkSheet3.Cells[1, 1] = "Number Binary Searches";
            xlWorkSheet3.Cells[1, 2] = "List";
            xlWorkSheet3.Cells[1, 3] = "Dictionary";
            xlWorkSheet3.Cells[1, 5] = "Sort Time: " + sorttime.ToString();

            for (int i = 0; i < binaryListContain.Length; i++)
            {
                xlWorkSheet3.Cells[i + 2, 1] = (i + 1) * BContainPrint;
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

        private static void runtest()
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
                AddTime(absolutenr / TestInterval - 1,listtime,ref listtimes);
                Console.WriteLine("List time(" + absolutenr + "): " + listtime);
            }

            //Contain not sorted
            long[] containtimes = new long[ContainPrintSum];
            for (int i = 0; i * TestScale / ContainPrintSum < TestScale; i++)
            {
                stopWatch.Restart();
                for (int step = i; step < i + TestScale / ContainPrint; step += ContainStep)
                {
                    list.Contains(step);
                }
                long listtime = stopWatch.ElapsedMilliseconds;
                AddTime(i, listtime,ref containtimes);
                Console.WriteLine("List Contain time(" + i + "): " + listtime);
            }

            //Contain not sorted 100.000 elements
            List<int> shortlist = new List<int>(100000);
            for (int i = 0; i < 100000; i++)
            {
                shortlist.Add(randomnumbers[i]);
            }
            long[] SLcontaintimes = new long[ContainPrintSum];
            for (int i = 0; i * TestScale / ContainPrint < TestScale; i++)
            {
                stopWatch.Restart();
                for (int step = i; step < i + TestScale / ContainPrint; step += ContainStep)
                {
                    shortlist.Contains(step);
                }
                long listtime = stopWatch.ElapsedMilliseconds;
                AddTime(i, listtime, ref SLcontaintimes);
                Console.WriteLine("ShortList Contain time(" + i + "): " + listtime);
            }

            //Binary Contain
            stopWatch.Reset();
            list.Sort();
            long sorttime = stopWatch.ElapsedMilliseconds;

            long[] bcontaintimes = new long[BContainPrintSum];
            for (int i = 0; i * TestScale / BContainPrintSum < TestScale; i++)
            {
                stopWatch.Restart();
                for (int step = i; step < i + TestScale / BContainPrint; step += BContainStep)
                {
                    list.BinarySearch(i);
                }
                long listtime = stopWatch.ElapsedMilliseconds;
                AddTime(i, listtime,ref bcontaintimes);
                Console.WriteLine("List Binary Contain time(" + i + "): " + listtime);
            }

            //Free up some memory
            list = null;
            #endregion

            #region SortedDictionary Tests
            SortedDictionary<int, int> sortedDictionary = new SortedDictionary<int, int>();
            absolutenr = 0;
            long[] sortedtimes = new long[TestNumbers];
            while (absolutenr < TestScale)
            {
                stopWatch.Restart();
                for (int i = 0; i < TestInterval; i++)
                {
                    sortedDictionary.Add(absolutenr, i);
                    absolutenr++;
                }
                long sortedTime = stopWatch.ElapsedMilliseconds;
                AddTime(absolutenr / TestInterval - 1,sortedTime,ref sortedtimes);
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
                AddTime(i, listtime,ref SDcontaintimes);
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
                AddTime(absolutenr / TestInterval - 1,dictiontime,ref dictiontimes);
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
                AddTime(i, listtime, ref Dcontaintimes);
                Console.WriteLine("Sorted Dictionary Contain time(" + i + "): " + listtime);
            }

            //Binary Contain test
            long[] BDcontaintimes = new long[BContainPrintSum];
            for (int i = 0; i * TestScale / BContainPrintSum < TestScale; i++)
            {
                stopWatch.Restart();
                for (int step = i; step < i + TestScale / BContainPrint; step += BContainStep)
                {
                    dictionary.ContainsKey(step);
                }
                long listtime = stopWatch.ElapsedMilliseconds;
                AddTime(i, listtime, ref Dcontaintimes);
                Console.WriteLine("Dictionary ContainKey time(" + i + "): " + listtime);
            }

            dictionary = null;
            #endregion

            Console.WriteLine("Sort time: " + sorttime);

            SaveAsExcel(listtimes, sortedtimes, dictiontimes, containtimes,SLcontaintimes, SDcontaintimes, Dcontaintimes, bcontaintimes, BDcontaintimes, sorttime);

            Console.WriteLine("Press any key to close");
            Console.Read();
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

        private static void AddTime(int index, long time, ref long[] times)
        {
            if (index > 0)
            {
                times[index] = time + times[index - 1];
            }
            else
            {
                times[index] = time;
            }
        }
    }
}
