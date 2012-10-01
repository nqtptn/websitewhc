using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessWebSite
{
    public class BackupFile
    {
        /// <summary>
        /// Gets the file size in bytes;
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// Gets the full file name;
        /// </summary>
        public string FullFileName { get; set; }
        /// <summary>
        /// Gets the file name and extension without path string.
        /// </summary>
        public string FileName { get; set; }
    }
}
