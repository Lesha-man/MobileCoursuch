using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileCoursuch.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(FileReader))]
namespace MobileCoursuch.Droid
{
    class FileReader : IFileReader
    {
        public byte[] ReadAllByteS(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}