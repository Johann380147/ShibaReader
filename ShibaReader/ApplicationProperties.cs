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
        private static App app = null;
        private static string appPropertiesFileName = "app.config";
        internal static string DefaultFolderProperty { get; private set; } = "DefaultFolder";
        internal static string JilFileProperty { get; private set; } = "DefaultJILFile";
        internal static string CalFileProperty { get; private set; } = "DefaultCalendarFile";

        internal static void InitializeDefaultProperties(App app)
        {
            ApplicationProperties.app = app;
            // Initialize application-scope property
            app.Properties["DefaultFolder"] = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            app.Properties["DefaultJILFile"] = @"";
            app.Properties["DefaultCalendarFile"] = @"";
        }

        internal static void RetrieveProperties(App app)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
            try
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(appPropertiesFileName, FileMode.Open, FileAccess.Read, FileShare.Read, storage))
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
            catch (IsolatedStorageException ex)
            {
                if (ex.InnerException.GetType() == typeof(FileNotFoundException))
                {
                    // Handle when file is not found in isolated storage:
                    // * When the first application session
                    // * When file has been deleted
                }
            }
        }

        internal static void SaveProperties(App app)
        {
            // Persist application-scope property to isolated storage
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(appPropertiesFileName, FileMode.Create, FileAccess.Write, FileShare.Write, storage))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                // Persist each application-scope property individually
                foreach (string key in app.Properties.Keys)
                {
                    writer.WriteLine("{0},{1}", key, app.Properties[key]);
                }
            }
        }

        internal static void SetProperty(object property, object? value)
        {
            if (app.Properties.Contains(property))
            {
                app.Properties[property] = value;
            }
            else
            {
                app.Properties.Add(property, value);
            }
        }

        internal static object? GetPropertyValue(object property)
        {
            if (app.Properties.Contains(property))
            {
                return app.Properties[property];
            }
            return null;
        }
    }
}
