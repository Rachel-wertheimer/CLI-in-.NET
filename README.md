# File Bundling and Response File Creation Tool

## Overview

A command-line tool that bundles files by language, allows sorting, removes empty lines, adds file metadata, 
and generates a `.rsp` response file for easier future execution.

## Requirements

- .NET 5.0 or higher
- C# compatible development environment

## Installation

```bash
git clone https://github.com/yourusername/yourrepository.git
cd yourrepository
dotnet build
```

## Usage

### `bundle` Command

Bundles files based on specified criteria.

#### Options:

- `-o`, `--output`: Output file path and name. (Required)
- `-l`, `--language`: File language (e.g., `cs`, `txt`, or `all`). (Required)
- `-n`, `--note`: Add file name and relative path as comments.
- `-s`, `--sort`: Sort files by name before bundling.
- `-a`, `--author`: Add author's name in the merged file.
- `-r`, `--remove_empty_line`: Remove empty lines from the merged content.

#### Example:

```bash
fib bundle --output "result.txt" --language "cs" --note --sort --author "Rachel" --remove_empty_line
```

### `creat-rsp` Command

Generates a `.rsp` file based on user input for quicker future runs.

#### Example:

```bash
fib creat-rsp
```

You will be prompted to enter:
- Output file path and name
- Target file language
- Whether to remove empty lines
- Whether to sort files
- Whether to add notes
- Author's name

## Using RSP Files

After creating a `.rsp` file, you can run:

```bash
dotnet run @MyRespons.rsp
```

## Error Handling

- Invalid file paths are detected and reported.
- Missing required options are handled gracefully.
- If no files match the specified language, a warning message is displayed.

## Contribution

Feel free to open Pull Requests with improvements, bug fixes, or new features.

