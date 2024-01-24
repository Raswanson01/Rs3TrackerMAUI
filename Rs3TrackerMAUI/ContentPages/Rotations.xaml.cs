using IniParser.Model;
using IniParser;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

using static Rs3TrackerMAUI.Classes.DisplayClasses;
using System.Diagnostics;
using System.ComponentModel;

namespace Rs3TrackerMAUI.ContentPages;

public partial class Rotations : ContentPage
{
    
    private ObservableCollection<Ability> _abilities;

    public ObservableCollection<Ability> Abilities
    {
        get { return _abilities; }
        set
        {
            if (_abilities != value)
            {
                _abilities = value;
                OnPropertyChanged(nameof(Abilities));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
        public ObservableCollection<Rotation> RotationList { get; set; }
    

    private string mainDir = "";
    private string cacheDir = "";


    public Rotations()
    {
#if WINDOWS
        cacheDir = Microsoft.Maui.Storage.FileSystem.AppDataDirectory;
#endif
#if MACCATALYST
        cacheDir = Microsoft.Maui.Storage.FileSystem.AppDataDirectory;
#endif
        try
        {
            InitializeComponent();
            ReadConfig();
            ReadRotations();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); //TODO robust error handling
        }
    }

    private void ReadConfig()
    {
        if (File.Exists(Path.Combine(cacheDir, "Configuration.ini")))
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(Path.Combine(cacheDir, "Configuration.ini"));
            mainDir = data["DATA"]["FOLDER"];
        }
    }

    private void ReadRotations()
    {
        if (File.Exists(Path.Combine(mainDir, "Rotations.json")))
        {
            List<Rotation> rots = JsonConvert.DeserializeObject<List<Rotation>>(File.ReadAllText(
                Path.Combine(mainDir, "Rotations.json")));
            RotationList = new ObservableCollection<Rotation>(rots);       
        }
        else
        {
            File.Create(Path.Combine(mainDir, "Rotations.json")).Close();
            RotationList = new ObservableCollection<Rotation>(new List<Rotation>());
        }
        rotationPicker.ItemsSource = RotationList;
    }

    private async void btnImport_Clicked(object sender, EventArgs e)
    {
        FilePickerService fps = new FilePickerService();
        Rotation result = await fps.ImportRotation(mainDir);
        RotationList.Add(result);
    }

    private void btnExport_Clicked(object sender, EventArgs e)
    {

    }

    private void btnClose_Clicked(object sender, EventArgs e)
    {
        MainPage.CloseRotationsWindow();
    }

    private void rotationPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedRotation = ((Picker)sender).SelectedItem as Rotation;
        Abilities = new ObservableCollection<Ability>(selectedRotation.abilities);
        foreach (var ability in Abilities)
        {
            var oldImg = ability.img;
            var path = Path.Combine("Images", oldImg);
            ability.img = Path.Combine(mainDir, path);
        }
        abilityView.ItemsSource = Abilities;
        //this.ForceLayout();
    }
}
