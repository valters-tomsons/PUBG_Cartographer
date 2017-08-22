using PUBG_Cartographer.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PUBG_Cartographer
{
    public partial class Overlay : Window
    {
        private readonly Uri MapPath = new Uri($"{AppDomain.CurrentDomain.BaseDirectory}//data//map.jpg", UriKind.Absolute);

        public Overlay()
        {
            InitializeComponent();

            //Resize overlay to cover the whole screen
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
            Opacity = 0.8;
            LoadMap();
            MapVisibility = false;
        }

        //Make overlay click-through-able
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }

        //Load map image and add it to overlay viewport
        private void LoadMap()
        {
            mapOverlay.Source = new BitmapImage(MapPath);
        }

        private bool _mapVisibility = false;
        public bool MapVisibility
        {
            get
            {
                return _mapVisibility;
            }
            set
            {
                _mapVisibility = value;
                if(value == true)
                {
                    mapOverlay.Visibility = Visibility.Visible;
                }
                else
                {
                    mapOverlay.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
