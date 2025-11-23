# C# Simple Math Expression Evaluate Library

A robust .NET C# library designed to parse and evaluate mathematical expressions. This solution separates the **Parsing Logic** from the **UI**, allowing you to easily integrate calculation features into Console, WinForms, or WPF applications.

## 🚀 Features

* **Single Operation Parsing:** Evaluates expressions with two operands and one operator (e.g., `10 + 5`, `5 * -2`).
* **Trigonometry Support:** Supports unary operations for `sin`, `cos`, and `tan` (e.g., `cos 0`).
* **Flexible Syntax:** Supports both symbols and keywords (e.g., `%` or `mod`, `^` or `pow`).
* **Safe Evaluation:** Includes a `TryEvaluate` pattern to handle errors gracefully without crashing.
* **Custom Parser:** Handles whitespace trimming, negative numbers, and decimal points automatically.

## 📦 Project Structure

* **Calculator.Logic:** The core DLL containing the parsing algorithms and math logic.
* **Calculator.ConsoleUI:** A console application that runs the calculator loop and includes a built-in **Test Suite**.

## 🔧 Supported Operations

The parser (`clsParser.cs`) supports the following operations:

| Operation Type | Symbols / Keywords | Example Input |
| :--- | :--- | :--- |
| **Basic Math** | `+`, `-`, `*`, `/` | `10.5 + 20`, `5 * -5` |
| **Modulus** | `%`, `mod` | `10 % 3`, `10 mod 3` |
| **Power** | `^`, `pow` | `2 ^ 3`, `4 pow 2` |
| **Trigonometry** | `sin`, `cos`, `tan` | `sin 0`, `cos 3.14` |

> **Note:** The parsed expressions must contain **only one operation** at a time. Inputting `5 + 5 * 2` will throw an "Error Format" exception.

## 💻 Usage Guide

### 1. Installation
Reference the `Calculator.Logic.dll` in your project.

### 2. Code Example

You should use the `IExpressionCalculator` interface for cleaner code.

```csharp
using Calculator.Logic;

// Initialize the calculator
IExpressionCalculator calc = new clsExpressionCalculator();

string input = "10 * 5.5";
double result;

// Use TryEvaluate for safe execution
if (calc.TryEvaluate(input, out result))
{
    Console.WriteLine($"Success: {result}"); // Output: 55
}
else
{
    Console.WriteLine("Invalid Expression Format");
}
