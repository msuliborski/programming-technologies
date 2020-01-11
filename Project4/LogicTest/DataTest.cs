using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.IO;
using System.Linq;
using System;
using System.Collections;

namespace Tests {
    [TestClass]
    [DeploymentItem(@"Lib.mdf")]
    public class DataTest {

        private static string connectionString;

        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context) {
            string DBRelativePath = @"Lib.mdf";
            string testingWorkingFolder = Environment.CurrentDirectory;
            string DBPath = Path.Combine(testingWorkingFolder, DBRelativePath);
            FileInfo databaseFile = new FileInfo(DBPath);
            Assert.IsTrue(databaseFile.Exists, $"{Environment.CurrentDirectory}");
            connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DBPath};Integrated Security = True; Connect Timeout = 30;";
        }

        [TestMethod]
        public void Test() {

            using (LibDataContext lib = new LibDataContext(connectionString)) {
                Assert.IsNotNull(lib.Connection);
                Assert.AreEqual<int>(2, lib.Readers.Count());
                //IEnumerable filtered = lib.FilterReadersByLastName_ForEach("Reader");           
            }
        }

       
    }
}