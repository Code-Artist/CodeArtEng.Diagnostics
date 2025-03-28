﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;


namespace CodeArtEng.Diagnostics
{
    /// <summary>
    /// Select pre-defined theme for Diagnostic Text box. 
    /// Settings affect font type, back color and text color.
    /// </summary>
    public enum TextBoxTheme
    {
        /// <summary>
        /// User Custom Style
        /// </summary>
        UserDefined,
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
        PowerShell,
        /// <summary>
        /// Visual studio dark theme with green text
        /// </summary>
        VisualStudio_Green,
        /// <summary>
        /// Visual studio dark theme with blue text
        /// </summary>
        VisualStudio_Blue
    }

    internal static class TextBoxThemeManager
    {
        public static TextBoxTheme SetTheme(this TextBoxBase sender, TextBoxTheme theme)
        {
            switch (theme)
            {
                case TextBoxTheme.MSDos:
                    sender.Foreground = new SolidColorBrush(Colors.White);
                    sender.Background = new SolidColorBrush(Colors.Black);
                    sender.FontFamily = new FontFamily("Consolas");
                    break;

                case TextBoxTheme.NightVision:
                    sender.Foreground = new SolidColorBrush(Color.FromRgb(22, 198, 22));
                    sender.Background = new SolidColorBrush(Color.FromRgb(0, 28, 0));
                    sender.FontFamily = new FontFamily("Consolas");
                    break;

                case TextBoxTheme.PowerShell:
                    sender.Foreground = new SolidColorBrush(Colors.White);
                    sender.Background = new SolidColorBrush(Color.FromRgb(1, 36, 86));
                    sender.FontFamily = new FontFamily("Consolas");
                    break;

                case TextBoxTheme.VisualStudio_Blue:
                    sender.Foreground = new SolidColorBrush(Color.FromRgb(86, 156, 214));
                    sender.Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
                    sender.FontFamily = new FontFamily("Consolas");
                    break;

                case TextBoxTheme.VisualStudio_Green:
                    sender.Foreground = new SolidColorBrush(Color.FromRgb(134, 198, 145));
                    sender.Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
                    sender.FontFamily = new FontFamily("Consolas");
                    break;

                case TextBoxTheme.Windows:
                default:
                    sender.Foreground = SystemColors.WindowTextBrush;
                    sender.Background = SystemColors.WindowBrush;
                    sender.FontFamily = new FontFamily("Microsoft Sans Serif");
                    return TextBoxTheme.Windows;


            }
            return theme;
        }
    }
}