using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeSpace.Pages;

namespace SafeSpace.ViewModel
{
    public class MeetupViewModel
    {
        public ObservableCollection<Meetup> Meetups { get; set; }

        public MeetupViewModel()
        {

            Meetups = new ObservableCollection<Meetup>
        {
            new Meetup
            {
                Name = "John Doe",
                Title = "Walk around the Retiro Park",
                Time = DateTime.Now.AddHours(48),
                Image = "descarga.jpg",
                Place = "Puerta de Madrid",
                Description = "Nice walk around the Retiro park to chill out and disconnect for some time",
                isJoined = false

            },
            new Meetup
{
    Name = "Carlos Reyes",
    Title = "Bowling & Chill",
    Time = DateTime.Now.AddDays(3).AddHours(2),
    Place = "Chamartin Bowling Alley",
    Image = "bolera.png",
    Description = "An easygoing evening of bowling, snacks, and casual chats. Great for unwinding and meeting people in a fun, no-pressure environment.",
    isJoined = false
},
            new Meetup
{
    Name = "Aisha Patel",
    Title = "Cozy Gaming Meetup",
    Time = DateTime.Now.AddDays(2).AddHours(5),
    Place = "Meltdown Gaming Bar",
    Image = "gaming.jpg",
    Description = "Bring your Nintendo Switch, laptop, or just yourself! We’ll be playing chill games like Stardew Valley, Mario Kart, and Journey. No competition, just fun.",
    isJoined = false
},
        };
        }
    }
}
