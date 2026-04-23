# WebLogViewer
## Published 4/22/2026

This is a basic web log viewer to load and merge logs with the *.log extension.  It can handle ZIP and GZ files containing *.log files.

The program will parse any space-delimited text file into columns which it then displays in the data grid on the form. It is designed to ignore spaces between double-quotes and brackets and will skip over escaped characters.  It will also adapt the number of rows if it encounters extra columns between files.  Comment lines starting with # will be ignored.

I am planning on adding features to output the grid to a CSV file or other format.

This project does not use A.I. generated code.
