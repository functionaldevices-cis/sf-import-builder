﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_Import_Builder.Helpers; 

public class FileOutputUtility {

    public string RootFolder { get; set; }

    public StreamWriter SW { get; set; } = new("");

    private bool IsOpen => this.SW != null && this.SW.BaseStream != null;

    public FileOutputUtility(string rootFolder) {

        this.RootFolder = rootFolder;

    }

    public void CreateFile(string filePathWithinRoot, string fileContents) {

        if (filePathWithinRoot.Contains('\\')) {

            string nestedFolderPath;
            string[] fileNameParts;

            fileNameParts = filePathWithinRoot.Split('\\');
            fileNameParts = fileNameParts.Take(fileNameParts.Length - 1).ToArray();
            nestedFolderPath = string.Join('\\', fileNameParts);

            Directory.CreateDirectory(Path.Combine(this.RootFolder, nestedFolderPath));

        }

        this.CreateEmptyFile(filePathWithinRoot);
        this.WriteToFile(fileContents);
        this.CloseFile();


    }

    public void CreateEmptyFile(string filePathWithinRoot) {

        if (this.IsOpen) {
            this.SW.Close();
        }
        this.SW = new(Path.Combine(this.RootFolder, filePathWithinRoot), false);

    }

    public void WriteToFile(string fileContents) {

        if (this.IsOpen) {
            this.SW.Write(fileContents);
        }

    }

    public void WriteLineBreakToFile() {

        this.WriteToFile(this.SW.NewLine);

    }

    public void CloseFile() {

        this.SW.Close();

    }

}
