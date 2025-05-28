using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeSpace.ViewModel;

namespace SafeSpace.Pages
{
    public partial class MeetupPage : ContentPage
    {
        public MeetupPage()
        {
            InitializeComponent();

            BindingContext = new MeetupViewModel();
        }

        private void OnJoinMeetupClicked(object sender, EventArgs e)
        {
            
            var button = (Button)sender;

            
            var meetup = (Meetup)button.CommandParameter;

            meetup.isJoined = !meetup.isJoined;


            DisplayAlert("Join Meetup", $"You have joined the meetup: {meetup.Title}", "OK");

            
        }
    }
}
