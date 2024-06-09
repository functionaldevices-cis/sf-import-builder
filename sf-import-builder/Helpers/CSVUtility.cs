﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_Import_Builder.Helpers;

public static class CSVUtility {

    public static string EscapeString(string value) {

        return "\"" + value.Replace("\"", "\"\"") + "\"";

    }

    public static string UnEscapeString(string value) {

        if (value.StartsWith("\"")) {
            value = value.Remove(0, 1);
        }

        value = value.Replace("\"\"", "\"");

        if (value.EndsWith("\"")) {
            value = value.Remove(value.Length - 1);
        }

        return value;

    }

    public static List<Dictionary<string, string>> UnSerialize(string[] csvFileContents) {

        List<List<string>> lines = csvFileContents.Select(
            line => line.Split(',').Select(value => CSVUtility.UnEscapeString(value)).ToList()
        ).ToList();

        List<Dictionary<string, string>> linesAsDictionaries = lines.Select(
            values => lines[0].Zip(
                values, (k, v) => new { k, v }
            ).ToDictionary(
                x => x.k,
                x => x.v
            )
        ).ToList();

        linesAsDictionaries.RemoveAt(0);

        return linesAsDictionaries;

    }

}
