using System;

namespace CodeArtEng.Diagnostics.Controls
{
    /// <summary>
    /// Diagnostics Text Box event arguments
    /// </summary>
    public class TextEventArgs : EventArgs
    {
        /// <summary>
        /// Incoming Debug / Trace message.
        /// </summary>
        public string Message { get; set; }
    }

}
