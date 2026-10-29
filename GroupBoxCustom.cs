using System.Drawing;
using System.Windows.Forms;

namespace projetStarGate
{
    public class GroupBoxCustom : GroupBox
    {
        public Color BorderColor { get; set; } = SystemColors.Info;
        public Color TextColor { get; set; } = SystemColors.Info;

        public GroupBoxCustom()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                dessinerSansTexte(e);
            }
            else
            {
                dessinerAvecTexte(e);
            }
        }

        private void dessinerSansTexte(PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            using (Pen pen = new Pen(BorderColor, 2))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private void dessinerAvecTexte(PaintEventArgs e)
        {
            int textX = 10;
            int textY = 0;
            int textPadding = 8;

            Size textSize = TextRenderer.MeasureText(Text, Font);

            int borderY = textSize.Height / 2;
            int borderLeft = 0;
            int borderRight = Width - 1;
            int borderBottom = Height - 1;

            using (Pen pen = new Pen(BorderColor, 2))
            {
                e.Graphics.DrawLine(pen, borderLeft, borderY, borderLeft, borderBottom);
                e.Graphics.DrawLine(pen, borderRight, borderY, borderRight, borderBottom);
                e.Graphics.DrawLine(pen, borderLeft, borderBottom, borderRight, borderBottom);

                e.Graphics.DrawLine(
                    pen,
                    borderLeft,
                    borderY,
                    textX - textPadding,
                    borderY
                );

                e.Graphics.DrawLine(
                    pen,
                    textX + textSize.Width + textPadding,
                    borderY,
                    borderRight,
                    borderY
                );
            }

            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                new Point(textX, textY),
                TextColor,
                TextFormatFlags.NoPadding
            );
        }
    }
}