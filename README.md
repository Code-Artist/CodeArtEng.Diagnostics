# CodeArtEng.Diagnostics
## Diagnostics Toolbox

Diagnostics text box is a .NET WinForm Control written in C# derived from <code>System.Windows.Forms.TextBox</code> 
with capability to capture all DEBUG and TRACE log from System.Diagnostics. 
The TraceLogger implementation is derived from <code>System.Diagnostics.TraceListener.</code>

In version 3.3.0, we introduced Diagnostics rich text box to add color to log windows with customizable formatting rules.
Visit http://www.codearteng.com/2011/08/diagnostics-textbox.html for more info.

## Process Executor

ProcessExecutor is a wrapper class for System.Diagnostic.Process which created with intention to manage external process within .NET application without showing up the console window and capture all the console output logs at the same time.

Visit https://www.codearteng.com/2015/09/process-executor.html#more for more info.
