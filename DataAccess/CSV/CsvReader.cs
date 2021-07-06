using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CSV {
    public class CsvReader {

        public static List<Dictionary<string, object>> GetCsvData(string csvAbsolutePath) {

            var csvExtenstion = Path.GetExtension(csvAbsolutePath);
            if(!string.Equals(csvExtenstion, "csv", StringComparison.OrdinalIgnoreCase)) {
                throw new Exception($"The specified absolute path [{csvAbsolutePath}] isn't for CSV file.");
            }

            var isFileExists = File.Exists(csvAbsolutePath);
            if (!isFileExists) {
                throw new Exception($"The specified absolute path [{csvAbsolutePath}] for CSV file doesn't exists.");
            }

            var records = new List<Dictionary<string, object>>();
            var columns = new List<string>();
            using(StreamReader reader = new StreamReader(csvAbsolutePath)) {

                var isColLine = true;
                var line = reader.ReadLine();
                while(line != null && line != string.Empty) {

                    if (isColLine) {
                        columns = line.Split(',').ToList();
                        isColLine = false;
                    } else {
                        var data = line.Split(',').ToList();
                        var dataLen = data.Count;
                        var record = new Dictionary<string, object>();
                        
                        for(int index = 0; index < dataLen; index++) {
                            var key = columns[index];
                            var value = data[index];
                            record[key] = value;
                        }

                        records.Add(record);

                    }

                    line = reader.ReadLine();

                }
            }

            return records;

        }
    }
}
