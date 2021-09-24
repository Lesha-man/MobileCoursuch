using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCoursuch
{
    public interface IFileReader
    {
        byte[] ReadAllByteS(string path);
    }
}
