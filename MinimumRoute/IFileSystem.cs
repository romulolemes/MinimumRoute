﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumRoute
{
    public interface IFileSystem
    {
        string ReadAllLines(string path);
    }
}
