﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Drawing;
using Xunit;

namespace System.Windows.Forms.Tests
{
    public class ToolStripContentPanelRenderEventArgsTests : IClassFixture<ThreadExceptionFixture>
    {
        [WinFormsFact]
        public void ToolStripContentPanelRenderEventArgs_NullGraphics_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ToolStripContentPanelRenderEventArgs(null, null));
        }

        public static IEnumerable<object[]> Ctor_Graphics_ToolStripContentPanel_TestData()
        {
            var image = new Bitmap(10, 10);
            Graphics graphics = Graphics.FromImage(image);

            yield return new object[] { graphics, null };
            yield return new object[] { graphics, new ToolStripContentPanel() };
        }

        [WinFormsTheory]
        [MemberData(nameof(Ctor_Graphics_ToolStripContentPanel_TestData))]
        public void Ctor_Graphics_ToolStripContentPanel(Graphics g, ToolStripContentPanel contentPanel)
        {
            var e = new ToolStripContentPanelRenderEventArgs(g, contentPanel);
            Assert.Equal(g, e.Graphics);
            Assert.Equal(contentPanel, e.ToolStripContentPanel);
            Assert.False(e.Handled);
        }

        [WinFormsTheory]
        [InlineData(true)]
        [InlineData(false)]
        public void Handled_Set_GetReturnsExpected(bool value)
        {
            using (var image = new Bitmap(10, 10))
            using (Graphics graphics = Graphics.FromImage(image))
            {
                using var panel = new ToolStripContentPanel();
                var e = new ToolStripContentPanelRenderEventArgs(graphics, panel)
                {
                    Handled = value
                };
                Assert.Equal(value, e.Handled);
            }
        }
    }
}
