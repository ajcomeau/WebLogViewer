using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebLogViewer
{
    static class LogProcess
    {
        public static CultureInfo ci = new CultureInfo("en-US");
        public static event Action<string>? FileOp;

        public static DataTable LoadFile(string FileName, bool Decompress)
        {
            // If file ends with ZIP for GZ, decompress to LOG.
            DataTable dtLog = new DataTable();
            List<String> fileList = new List<String>();

            try
            {
                if (File.Exists(FileName))
                {
                    // Decompress file if needed and add files to list for processing.
                    if (FileName.EndsWith(".GZ", true, ci) || FileName.EndsWith(".ZIP", true, ci)) 
                    {
                        if (Decompress)
                            fileList = DecompressFile(FileName);
                    }
                    else
                        fileList.Add(FileName);

                    // Iterate through list, read entries in each file, add necessary columns to table
                    // and append rows.
                    foreach (String file in fileList)
                    {
                        // Update event notification for form statusbar.
                        FileOp?.Invoke("Processing " + file);

                        if (File.Exists(file) && file.EndsWith(".log", true, ci))
                        {
                            // Read lines from file, skip any that start with # (comments).
                            // Parse lines into fields 
                            var lines = File.ReadLines(file).Where(line => !line.StartsWith("#"))
                                .Select(line => LogParse(line));

                            // Append each line to the table as an array object.
                            // Add columns when necessary.
                            foreach (var line in lines) { 
                                while (dtLog.Columns.Count < line.Count())
                                    dtLog.Columns.Add("Col" + (dtLog.Columns.Count).ToString());

                                dtLog.Rows.Add(line.ToArray());
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }          
                
            return dtLog;


        }

        public static DataTable LoadDirectory(string Directory, bool Decompress)
        {
            DataTable dtLog = new DataTable();

            try
            {
                if (System.IO.Directory.Exists(Directory))
                {
                    // Iterate through directory looking for LOG, ZIP and GZ files.
                    foreach (String file in System.IO.Directory.EnumerateFiles(Directory))
                    {
                        if (file.EndsWith(".ZIP", true, ci) || file.EndsWith(".GZ", true, ci) || file.EndsWith(".log", true, ci))
                        {
                            // If there are no records so far, just load the file. Otherwise merge with current records.
                            if (dtLog.Rows.Count == 0)
                                dtLog = LoadFile(file, Decompress);
                            else
                                dtLog.Merge(LoadFile(file, Decompress));
                        }
                    }
                }
            }
            catch (Exception) 
            {
                throw;
            }

            return dtLog;
        }

        public static List<String> DecompressFile(string FileName) 
        {
            FileInfo fi = new FileInfo(FileName);
            List<String> fileList = new List<String>();
            String expandedFilePath;

            // Decompress or unzip file depending on filetype.

            try
            {
                if (fi.Exists)
                {
                    if (fi.Extension.ToUpper() == ".GZ")
                    {
                        // GZip
                        expandedFilePath = fi.DirectoryName + "\\" + fi.Name.Substring(0, fi.Name.Length - 3);
                        using (FileStream origFS = File.OpenRead(FileName)) 
                        using (FileStream targetFS = File.Create(expandedFilePath)) 
                        using (GZipStream decompStream = new GZipStream(origFS, CompressionMode.Decompress)) 
                        decompStream.CopyTo(targetFS);
                        fileList.Add(expandedFilePath);
                    }
                    else if (fi.Extension.ToUpper() == ".ZIP")
                    {
                        // ZIP
                        ZipArchive zaFile = ZipFile.OpenRead(FileName);
                        foreach (ZipArchiveEntry entry in zaFile.Entries)
                        { 
                            if (entry.Name.EndsWith(".log", true, ci))
                            {
                                expandedFilePath = fi.DirectoryName + "\\" + entry.Name;
                                entry.ExtractToFile(expandedFilePath, true);
                                fileList.Add(expandedFilePath);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }



            return fileList;       
        
        }

        static List<String> LogParse(String logEntry)
        {
           
            List<String> fields = new List<String>();
            string field = "";

            bool quotes = false;
            bool brackets = false;
            bool escape = false;
            bool fieldComplete = false;

            // Parse log into fields using spaces as delimiters.
            try
            {
                foreach (char c in logEntry)
                {
                    if (c == '\\') escape = true;  // Skip escaped characters

                    if (!escape)
                    {
                        // Add character to string if not blank. Allow blanks inside brackets and quotes.
                        if (c != ' ') 
                            field += c;
                        else if (brackets || quotes)
                            field += c;

                        if (c == '"' && !brackets)  // Ignore quotes inside brackets.
                        {
                            quotes = !quotes;
                            if (!quotes) { fieldComplete = true; }  // End field at closing quote.
                        }
                        else if (c == '[' && !quotes) brackets = true;
                        else if (c == ']' && !quotes)  // Ignore brackets inside quotes.
                        {
                            brackets = false;
                            fieldComplete = true;  // End field at closing bracket.
                        }
                        else if (c == ' ' && !brackets && !quotes && field.Length > 0)
                            fieldComplete = true;  // End field on qualified space.

                        if (fieldComplete)
                        {
                            // Add field to list and reset for next field.
                            fields.Add(field);
                            field = "";
                            fieldComplete = false;
                        }
                    }

                    if (escape && c != '\\') escape = false;  // End escape mode.
                }
            }
            catch (Exception)
            {
                throw;
            }

            return fields;
        }

    }
}
