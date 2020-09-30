using System;
using System.Collections.Generic;
using Windows.Storage;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

[assembly: Dependency(typeof(CrossApp.UWP.FileWorker))]

namespace CrossApp.UWP
{
    class FileWorker : IFileWorker
    {
        public async Task<string> SaveTextAsync(string filename,string text)
        {
            StorageFolder localFolder = ApplicationData.Current.RoamingFolder;
            StorageFile helloFile = await localFolder.CreateFileAsync(filename,  CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(helloFile, text, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            return localFolder.Path+@"\"+filename;
        }

        public async Task<bool> ExistsAsync(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.RoamingFolder;
            try
            {
                await localFolder.GetFileAsync(filename);
            }
            catch { return false; }
            return true;
        }
    }
}
