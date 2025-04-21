using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Diagnostics;


namespace SafeSpace.ViewModel
{
    public class ShowProfilePageViewModel : INotifyPropertyChanged
    {
        private string _fullname;
        private int _age;
        private string _bio;

        private bool _isEditingFullName;
        private bool _isEditingAge;
        private bool _isEditingBio;
        public string FullNameEditButtonText => IsEditingFullName ? "Save" : "Edit";
        public string AgeEditButtonText => IsEditingAge ? "Save" : "Edit";
        public string BioEditButtonText => IsEditingBio ? "Save" : "Edit";

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ToggleEditFullNameCommand { get; }
        public ICommand ToggleEditAgeCommand { get; }
        public ICommand ToggleEditBioCommand { get; }

        public ICommand SaveChanges {  get; }
        public ShowProfilePageViewModel()
        {
            
            var fullName = Preferences.Get("FullName", defaultValue: "N/A");

            var age = Preferences.Get("Age", defaultValue: -2);

            var bio = Preferences.Get("Bio", defaultValue: "No biography yet");
            _fullname = $"{fullName}";
            _age = age;
            
            ToggleEditFullNameCommand = new Command(OnToggleEditFullName);
            ToggleEditAgeCommand = new Command(OnToggleEditAge);
            ToggleEditBioCommand = new Command(OnToggleEditBio);

            SaveChanges = new Command(async () => await UpdateProfile());

            IsEditingFullName = false;
        }

        public string FullName
        {
            get => _fullname;
            set => SetProperty(ref _fullname, value);
        }

        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        public string Bio
        {
            get => _bio;
            set => SetProperty(ref _bio, value);
        }

        public bool IsEditingFullName
        {
            get => _isEditingFullName;
            set
            {
                if (_isEditingFullName != value)
                {
                    _isEditingFullName = value;
                    OnPropertyChanged(nameof(IsEditingFullName));
                    OnPropertyChanged(nameof(FullNameEditButtonText));
                }
            }
        }
        public bool IsEditingBio
        {
            get => _isEditingBio;
            set
            {
                if (_isEditingBio != value)
                {
                    _isEditingBio = value;
                    OnPropertyChanged(nameof(IsEditingBio));
                    OnPropertyChanged(nameof(BioEditButtonText));
                }
            }
        }
        public bool IsEditingAge
        {
            get => _isEditingAge;
            set
            {
                if (_isEditingAge != value)
                {
                    _isEditingAge = value;
                    OnPropertyChanged(nameof(IsEditingAge));
                    OnPropertyChanged(nameof(AgeEditButtonText));
                }
            }
        }
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        private void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(backingField, value))
            {
                backingField = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
       public void OnToggleEditFullName()
        {
            if (IsEditingFullName)
            {
               
                Preferences.Set("FullName", FullName);
                
            }

            IsEditingFullName = !IsEditingFullName;
        }
        public void OnToggleEditAge()
        {
            if (IsEditingAge)
                Preferences.Set("Age", Age);

            IsEditingAge = !IsEditingAge;
        }
        public void OnToggleEditBio()
        {
            if (IsEditingBio)
                Preferences.Set("Bio", Bio);

            IsEditingBio = !IsEditingBio;
        }
        public async Task UpdateProfile()
        {
            int UserId = Preferences.Get("UserId", 0);
            string Name = Preferences.Get("UserName", "");
            string Email = Preferences.Get("Email", "");
            string password = Preferences.Get("Password", ""); // only if needed
            var httpclient = new HttpClient();

            var user = new UserUpdateDto
            {
                Id = UserId,
                Name = Name,
                Email = Email,
                Password = password,
                profile = new ProfileDto
                {
                    UserId = UserId,
                    FullName = this.FullName,
                    Bio = this.Bio,
                    Age = this.Age,
                }
            };
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Replace with your actual API endpoint
                var url = "http://10.0.2.2:5053/api/Auth/update";
                var response = await httpclient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    // maybe show a message or toast
                    Debug.WriteLine("Profile updated successfully.");
                }
                else
                {
                    Debug.WriteLine($"Failed to update profile: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception during profile update: {ex.Message}");
            }
        }

    }
}
