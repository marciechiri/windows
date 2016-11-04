using NoteBook.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NoteBook.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace NoteBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddNotexaml : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private Note notes= new Note();
       
        public AddNotexaml()
        {
            this.InitializeComponent();
            DataContext = notes;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
           
            notes.Title = txtAddTitle.Text;
            notes.Content = txtAddDesc.Text;
            notes.User = "091674-091573-092499";
            //notes._latitude = "0.00";
            //notes._longitude = "0.00";


            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5),
                       timeout: TimeSpan.FromSeconds(10));
                notes.Latitude = geoposition.Coordinate.Latitude.ToString("0.00");
                notes.Longitude = geoposition.Coordinate.Longitude.ToString("0.00");
            }
            catch (UnauthorizedAccessException)
            {
                // the app does not have the right capability or the location master switch is off 
                new MessageDialog("Could not get Location").ShowAsync();
            }
            Task<Note> task = Task.Run<Note>(async () => await Proxy.PostNote(notes));


            Note notesRes = task.Result;

            if (notesRes != null)
            {
                Frame.GoBack();
            }
            else
            {
                new MessageDialog("It has failed to save").ShowAsync();
            }

            // Frame.Navigate(typeof(AddNotexaml));

            Frame.Navigate(typeof(MainPage));



        }
    }

    
}
