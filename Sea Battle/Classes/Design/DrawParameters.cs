using System.Drawing;

namespace Sea_Battle.Design
{
    static class DrawParameters
    {
        public static readonly Pen boldPen = new Pen(Brushes.Black, 2f);
        public static readonly Pen cellPen = new Pen(Brushes.Gray, 1f);
        public static readonly Pen activeCellPen = new Pen(Brushes.BlueViolet, 2.5f);
        public static readonly Pen second_activeCellPen = new Pen(Brushes.CornflowerBlue, 2.1f);
        public static readonly Pen crossPen = new Pen(Brushes.Red, 1.5f);
        public static readonly Pen shipPen = new Pen(Brushes.DarkSlateGray, 3f);
        public static readonly Pen activePen = new Pen(Brushes.BlueViolet, 2.5f);
        public static readonly Pen elipsePen = new Pen(Brushes.LightGray, 1f);
        public static readonly Font markersFont = new Font(FontFamily.GenericSerif, 16f, FontStyle.Italic);
        public static readonly Font textFont = new Font(FontFamily.GenericSansSerif, 22f, FontStyle.Regular);
        public static readonly Brush textColor = Brushes.Black;
        public static readonly Brush destrShipColor = Brushes.DarkBlue;
        public static readonly int markersShift = 15;
    }
}
