using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace WineScraper.GUI
{

    public class GrowLabel : Label
    {
        private int _maxWidth;
        public int MaxWidth
        {
            get
            {
                return this._maxWidth;
            }
            set
            {
                this._maxWidth = value;
            }
        }

        private bool mGrowing;
        public GrowLabel()
        {
            this.AutoSize = false;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //this.MaxWidth = this.ClientSize.Width;
        }

        private void WrapTextTillWidth()
        {
            var sz = TextRenderer.MeasureText(this.Text, this.Font, new Size(Int32.MaxValue, Int32.MaxValue), TextFormatFlags.WordBreak);
            if (sz.Width <= this.MaxWidth)
            {
                return;
            }

            int iChopIndex = 2;
            var aStrings = this.Text.Split(' ');
            while (aStrings.Length > iChopIndex)
            {
                int iSplitIncrement = aStrings.Length / iChopIndex;
                var oBuilder = new StringBuilder();
                
                for (int i = 0; i < aStrings.Length; i++)
                {
                    oBuilder.Append(aStrings[i]);
                    if (i > 0 && (i % iSplitIncrement) == 0)
                    {
                        oBuilder.Append("\n");
                    }
                    else
                    {
                        oBuilder.Append(" ");
                    }
                }

                var aTextLines = oBuilder.ToString().Split('\n');
                bool bSetText = true;
                int iHeight = 0;
                foreach (var strLine in aTextLines)
                {
                    sz = TextRenderer.MeasureText(strLine, this.Font, new Size(Int32.MaxValue, Int32.MaxValue), TextFormatFlags.WordBreak);
                    iHeight += sz.Height;
                    if (sz.Width > this.MaxWidth)
                    {
                        bSetText = false;
                        break;
                    }
                }
                if (bSetText)
                {
                    this.Text = oBuilder.ToString();
                    this.ClientSize = new Size(this.MaxWidth, iHeight + this.Padding.Vertical);
                    return;
                }

                iChopIndex++;
            }            
        }

        private void resizeLabel()
        {
            if (mGrowing) return;
            try
            {
                mGrowing = true;
                WrapTextTillWidth();
                //Size sz = new Size(this.ClientSize.Width, Int32.MaxValue);
                //sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
                //int iWrapStart = 100;
                //if (this.MaxWidth > 0)
                //{
                //    while (sz.Width > this.MaxWidth && iWrapStart > 0)
                //    {
                //        var oRegex = new Regex("(.{" + iWrapStart.ToString() + "}\\s)");
                //        this.Text = oRegex.Replace(this.Text, "$1\n");
                //        sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
                //        iWrapStart -= 10;
                //    }
                //}
                
                //    //this.ClientSize = new Size(this.MaxWidth, sz.Height * 2 + this.Padding.Vertical);

                //}
                //else
                //{
                //    this.ClientSize = new Size(this.ClientSize.Width, sz.Height + this.Padding.Vertical);
                //}
            }
            finally
            {
                mGrowing = false;
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            resizeLabel();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            resizeLabel();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            resizeLabel();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            resizeLabel();
        }
    }
}