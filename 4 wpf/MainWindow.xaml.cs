using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task4

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ISetting settings;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextFont.ItemsSource = Fonts.SystemFontFamilies.OrderBy(x => x.Source);// поле ComboBox получает коллекцию системных шрифтов типа System.Windows.Media.FontFamily с сортировкой их по именам (.Source)
            TextSize.ItemsSource = new int[] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }; // поле ComboBox получает коллекцию размеров шрифтов


            settings = new SettingConfig(); // получение настроек из одного из 2-х классов

            //settings = new SettingRegistry();

            FillSettings(); // метод заполнения переменных информацией, сгенерированной конструкторами классов SettingConfig или SettingRegistry  
        }

        private void ClrBack_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e) //изменился фон текста
        {
            var bc = new BrushConverter();
            MainText.Background = BackColor.Background = TextColor.Background = FontSize.Background = FontStyle.Background // присвоение для TextBlock и всех 4-ти лейблов
                = (Brush)bc.ConvertFromString(ClrBack.SelectedColor.ToString());// цвет, выбранный в ColorPicker для фона делаем стрингом ,
                                                                                // из стринга делаем object методом из класса BrushConverter (а точнее базового класса TypeConverter)
                                                                                // наконец приводим к типу Brush 

        }

        private void ClrText_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e) //изменился цвет текста
        {
            var bc = new BrushConverter();
            MainText.Foreground = BackColor.Foreground = TextColor.Foreground = FontSize.Foreground = FontStyle.Foreground 
               = (Brush)bc.ConvertFromString(ClrText.SelectedColor.ToString()); //  ColorPicker для текста 
                                                                                
        }

        private void TextSize_SelectionChanged(object sender, SelectionChangedEventArgs e) //изменился размер текста
        {
            MainText.FontSize = BackColor.FontSize = TextColor.FontSize = FontSize.FontSize = FontStyle.FontSize
                = Convert.ToInt32(TextSize.SelectedItem);
        }

        private void TextFont_SelectionChanged(object sender, SelectionChangedEventArgs e) // изменился шрифт текста
        {
            MainText.FontFamily = BackColor.FontFamily = TextColor.FontFamily = FontSize.FontFamily = FontStyle.FontFamily
                = TextFont.SelectedItem as FontFamily;
        }

        private void Button_Click(object sender, RoutedEventArgs e) // по нажатию кнопки
        {
            settings.BackColor = BackColor.Background;  // передаём заданные поля Label в класс, который запишет их в xml
            settings.TextColor = TextColor.Foreground;
            settings.TextSize = (int)FontSize.FontSize;
            settings.TextFont = FontStyle.FontFamily;

            settings.SaveSettings(); //вызов метода записи в xml

            MainTab.IsSelected = true;
        }

        private void FillSettings() // заполнение переменных
        {
            MainText.Background = BackColor.Background = TextColor.Background = FontSize.Background = FontStyle.Background
                = settings.BackColor;

            MainText.Foreground = BackColor.Foreground = TextColor.Foreground = FontSize.Foreground = FontStyle.Foreground
                = settings.TextColor;

            MainText.FontSize = BackColor.FontSize = TextColor.FontSize = FontSize.FontSize = FontStyle.FontSize
                = settings.TextSize;

            MainText.FontFamily = BackColor.FontFamily = TextColor.FontFamily = FontSize.FontFamily = FontStyle.FontFamily
                = settings.TextFont;
        }
    }
}
