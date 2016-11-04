using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace NoteBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoteLocation : Page
    {
        public NoteLocation()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

            base.OnNavigatingFrom(e);
          

        }

        private void noteMap_Loaded(object sender, RoutedEventArgs e)
        {
            Geopoint centerGeopoint = new Geopoint(new BasicGeoposition()

            { Latitude = -1.27981, Longitude = 36.809792 });
            noteMap.Center = centerGeopoint;
            noteMap.ZoomLevel = 12;

            //adding mapElements
            MapIcon mapicon = new MapIcon();
            mapicon.Location = centerGeopoint;
            mapicon.Title = "Center";
            // mapicon.Image= RandomAccessStream
            noteMap.MapElements.Add(mapicon);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewNote),e.OriginalSource);
        }
    }
}
