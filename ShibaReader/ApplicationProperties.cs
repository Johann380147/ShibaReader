using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibaReader
{
    internal static class ApplicationProperties
    {
        private static string appPropertiesFileName = "app.config";

        public static string JilFileProperty { get; set; } = "DefaultJILFile";
        public static string CalFileProperty { get; set; } = "DefaultCalendarFile";

        internal static void InitializeDefaultProperties(App app)
        {
            // Initialize application-scope property
            app.Properties["DefaultJILFile"] = @"C:\Users\timothy.ding\Downloads\PSEA Guide\Reference Materials\autosys_20210323.jil";
            app.Properties["DefaultCalendarFile"] = @"C:\Users\timothy.ding\Downloads\PSEA Guide\Reference Materials\autosys_calendar_20210323.txt";
        }

        internal static void RetrieveProperties(App app)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
            try
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(appPropertiesFileName, FileMode.Open, storage))
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Restore each application-scope property individually
                    while (!reader.EndOfStream)
                    {
                        string[] keyValue = reader.ReadLine().Split(new char[] { ',' });
                        app.Properties[keyValue[0]] = keyValue[1];
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                // Handle when file is not found in isolated storage:
                // * When the first application session
                // * When file has been deleted
            }
        }

        internal static void SaveProperties(App app)
        {
            // Persist application-scope property to isolated storage
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(appPropertiesFileName, FileMode.Create, storage))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                // Persist each application-scope property individually
                foreach (string key in app.Properties.Keys)
                {
                    writer.WriteLine("{0},{1}", key, app.Properties[key]);
                }
            }
        }
    }
}
