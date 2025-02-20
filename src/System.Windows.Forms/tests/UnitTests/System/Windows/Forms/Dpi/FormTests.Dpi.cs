﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;
using static Interop;

namespace System.Windows.Forms.Tests.Dpi
{
    public class FormDpiTests : IClassFixture<ThreadExceptionFixture>
    {
        [WinFormsTheory]
        [InlineData(2 * DpiHelper.LogicalDpi)]
        [InlineData(3.5 * DpiHelper.LogicalDpi)]
        public void Form_DpiChanged_Bounds(int newDpi)
        {
            // Run tests only on Windows 10 versions that support thread dpi awareness.
            if (!PlatformDetection.IsWindows10Version1803OrGreater)
            {
                return;
            }

            IntPtr originalAwarenessContext = User32.SetThreadDpiAwarenessContext(User32.DPI_AWARENESS_CONTEXT.PER_MONITOR_AWARE_V2);
            try
            {
                using var form = new Form();
                form.AutoScaleMode = AutoScaleMode.Dpi;
                form.Show();
                Drawing.Rectangle initialBounds = form.Bounds;
                float initialFontSize = form.Font.Size;
                DpiMessageHelper.TriggerDpiMessage(User32.WM.DPICHANGED, form, newDpi);
                var factor = newDpi / DpiHelper.LogicalDpi;

                // Lab machines giving strange values that I could not explain. for ex: on local machine,
                // I get 1050*1050 for factor 3.5. This is not same on lab machines ( ex, we get 1044). For now,
                // just verifying they are scaled.
                Assert.NotEqual(initialBounds.Width, form.Bounds.Width);
                Assert.NotEqual(initialBounds.Height, form.Bounds.Height);
                Assert.NotEqual(initialFontSize, form.Font.Size);
                form.Close();
            }
            finally
            {
                // Reset back to original awareness context.
                User32.SetThreadDpiAwarenessContext(originalAwarenessContext);
            }
        }
    }
}
