using System;
using System.Collections.Generic;

namespace VideoPay.Models
{
    public class FileListViewModel
    {
        public FileListViewModel()
        {
            Files = new Dictionary<string, string>();
        }
        public Dictionary<string, string> Files { get; set; }
    }
}