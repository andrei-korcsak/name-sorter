# Name Sorter

Small console application that sorts a list of personal names and writes the sorted list to
sorted-names-list.txt. Names are ordered by last name, then by given names (closest to the last
name takes precedence). The project is implemented with small, testable services following
SOLID principles.

## Build
From the repository root:

```powershell
dotnet build
```

## Run
Run the built executable (Debug) from the name-sorter directory:

```powershell
.\bin\Debug\net10.0\name-sorter.exe .\unsorted-names-list.txt
```
## Output
- The program prints the sorted names to stdout and writes/overwrites a file called
  sorted-names-list.txt in the working directory.

## Sorting rules
- Primary: Last name (case-insensitive, ordinal)
- Secondary: Given names compared right-to-left (the given name nearest the last name is
  compared first). If all compared tokens are equal the shorter sequence (fewer given names)
  sorts earlier.

## Tests
- The repository includes an xUnit test project at name-sorter.tests.
- Run tests:

```powershell
dotnet test
```

