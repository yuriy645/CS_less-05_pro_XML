using System.Windows.Media;

namespace Task4
{
    interface ISetting
    {
        Brush BackColor { get; set; }
        Brush TextColor { get; set; }
        int TextSize { get; set; }
        FontFamily TextFont { get; set; }

        void SaveSettings();
    }
}
