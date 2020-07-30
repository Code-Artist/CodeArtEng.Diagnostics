using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CodeArtEng.Diagnostics
{
    /// <summary>
    /// Select pre-defined theme for Diagnostic Text box. 
    /// Settings affect font type, back color and text color.
    /// </summary>
    public enum TextBoxTheme
    {
        /// <summary>
        /// Default Winform Style
        /// </summary>
        Windows,
        /// <summary>
        /// MS Dos command prompt sytle, black background, white text.
        /// </summary>
        MSDos,
        /// <summary>
        /// Night vision, dark green background, light green text.
        /// </summary>
        NightVision,
        /// <summary>
        /// Power shell stype, blue background, white text.
        /// </summary>
        PowerShell
    }

    internal static class TextBoxThemeManager
    {
        public static TextBoxTheme SetTheme(this TextBoxBase sender, TextBoxTheme theme)
        {
            switch (theme)
            {
                case TextBoxTheme.MSDos:
                    sender.ForeColor = Color.White;
                    sender.BackColor = Color.Black;
                    sender.Font = new Font("Consolas", sender.Font.SizeInPoints, FontStyle.Regular);
                    break;

                case TextBoxTheme.NightVision:
                    sender.ForeColor = Color.FromArgb(22, 198, 12);
                    sender.BackColor = Color.FromArgb(0, 28, 0);
                    sender.Font = new Font("Consolas", sender.Font.SizeInPoints, FontStyle.Regular);
                    break;

                case TextBoxTheme.PowerShell:
                    sender.ForeColor = Color.White;
                    sender.BackColor = Color.FromArgb(1,36,86);
                    sender.Font = new Font("Consolas", sender.Font.SizeInPoints, FontStyle.Regular);
                    break;

                case TextBoxTheme.Windows:
                default:
                    sender.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    sender.BackColor = Color.FromKnownColor(KnownColor.Window);
                    sender.Font = new Font("Microsoft Sans Serif", sender.Font.SizeInPoints, FontStyle.Regular);
                    return TextBoxTheme.Windows;

            }
            return theme;
        }
    }
}