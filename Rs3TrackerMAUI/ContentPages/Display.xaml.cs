using Newtonsoft.Json;
using System.Diagnostics;
using static Rs3TrackerMAUI.Classes.DisplayClasses;

namespace Rs3TrackerMAUI.ContentPages;

public partial class Display : ContentPage {
    string mainDir = AppDomain.CurrentDomain.BaseDirectory;
    List<KeybindClass> keybindClasses = new List<KeybindClass>();
    List<BarKeybindClass> keybindBarClasses = new List<BarKeybindClass>();
    int imgCounter = 0;
    public string style = "";
    public List<Keypressed> ListKeypressed = new List<Keypressed>();
    public List<Keypressed> ListPreviousKeypressed = new List<Keypressed>();
    public Stopwatch stopwatch = new Stopwatch();
    public bool control = false;
    private Keypressed previousKey = new Keypressed();
    private List<Keypressed> ListPreviousKeys = new List<Keypressed>();
    private bool trackCD;
    private bool pause = false;
    public Display() {
        InitializeComponent();
        Loaded += Display_Loaded;
    }

    private void Display_Loaded(object sender, EventArgs e) {
        keybindClasses = JsonConvert.DeserializeObject<List<KeybindClass>>(File.ReadAllText(mainDir+"\\keybinds.json"));
        keybindBarClasses = JsonConvert.DeserializeObject<List<BarKeybindClass>>(File.ReadAllText(mainDir+"\\barkeybinds.json"));
        HookKeyDown(new ResquestInput() {
            altKey = false,
            shiftKey = false,
            metaKey = false,
            ctrlKey = false,
            keycode = 40
        });
        HookKeyDown(new ResquestInput() {
            altKey = false,
            shiftKey = false,
            metaKey = false,
            ctrlKey = false,
            keycode = 40
        });
        HookKeyDown(new ResquestInput() {
            altKey = false,
            shiftKey = false,
            metaKey = false,
            ctrlKey = false,
            keycode = 40
        });
    }

    private void HookKeyDown(ResquestInput e) {
        #region display
        if (!control) {
            control = true;
            Keypressed keypressed = new Keypressed();
            keypressed.ability = new Ability();
            string modifier = "";
            if (e.keycode.ToString().ToLower().Equals("none")) {
                control = false;
                return;
            }
            if (e.altKey)
                modifier = "ALT";
            else if (e.ctrlKey)
                modifier = "CTRL";
            else if (e.shiftKey)
                modifier = "SHIFT";
            else if (e.metaKey)//Windows Key
                modifier = "WIN";

            List<Ability> abilityList = (from r in keybindClasses
                                         where r.key.ToLower() == e.keycode.ToString().ToLower()
                                         where r.modifier.ToString().ToLower() == modifier.ToLower()
                                         select r.ability).ToList();

            if (abilityList.Count == 0) {
                if (keybindBarClasses != null) {
                    var listBarChange2 = keybindBarClasses.Where(p => p.key.ToLower().Equals(e.keycode.ToString().ToLower()) && p.modifier.ToLower().Equals(modifier.ToLower())).Select(p => p).FirstOrDefault();
                    if (listBarChange2 != null) {
                        if (listBarChange2.name.ToLower().Equals("clear")) {
                            displayImg1.Source = null;
                            displayImg2.Source = null;
                            displayImg3.Source = null;
                            displayImg4.Source = null;
                            displayImg5.Source = null;
                            displayImg6.Source = null;
                            displayImg7.Source = null;
                            displayImg8.Source = null;
                            displayImg9.Source = null;
                            displayImg10.Source = null;
                            control = false;
                            return;
                        } else if (listBarChange2.name.ToLower().Equals("pause")) {
                            pause = !pause;
                        }
                    }
                }
            }

            if (pause) {
                control = false;
                return;
            }

            foreach (var ability in abilityList) {

                if (ability == null)
                    continue;

                keypressed.modifier = modifier;
                keypressed.key = e.keycode.ToString();
                keypressed.ability.name = ability.name;
                keypressed.ability.img = ability.img;
                keypressed.ability.cooldown = ability.cooldown;
                keypressed.timepressed = stopwatch.Elapsed.TotalMilliseconds;

                for (int i = 0; i < ListPreviousKeypressed.Count; i++) {
                    var prevabil = ListPreviousKeypressed[i];
                    if ((keypressed.timepressed - prevabil.timepressed) > 1200) {
                        ListPreviousKeypressed.RemoveAt(i);
                        i--;
                    }
                }

                previousKey = ListPreviousKeypressed.Where(a => a.ability.img.Equals(keypressed.ability.img)).Select(a => a).FirstOrDefault();
                //if (previousKey != null) {
                //    control = false;
                //    return;
                //}
                ListKeypressed.Add(keypressed);
                previousKey = new Keypressed() {
                    timepressed = keypressed.timepressed,
                    ability = new Ability {
                        img = keypressed.ability.img,
                        name = keypressed.ability.name
                    }
                };
                ListPreviousKeypressed.Add(previousKey);


                //Bitmap bitmap = new Bitmap(ability.img);
                //Bitmap Image;
                ImageSource imageSource;
                //if (trackCD) {
                //    bool onCD = abilCoolDown(ListPreviousKeys, keypressed);
                //    if (onCD) {
                //        Image = Tint(bitmap, System.Drawing.Color.Red, 0.5f);
                //        imageSource = ImageSourceFromBitmap(Image);
                //    } else {
                //        imageSource = ImageSourceFromBitmap(bitmap);
                //        ListPreviousKeys.Add(previousKey);
                //    }
                //} else {
                    imageSource = ImageSource.FromFile(mainDir+ability.img);
                    ListPreviousKeys.Add(previousKey);
                //}

                //Display
                switch (imgCounter) {
                    case 0:
                        displayImg10.Source = imageSource;
                        break;
                    case 1:
                        moveImgs(imgCounter);
                        displayImg10.Source = imageSource;
                        break;
                    case 2:
                        moveImgs(imgCounter);
                        displayImg10.Source = imageSource;
                        break;
                    case 3:
                        moveImgs(imgCounter);
                        displayImg10.Source = imageSource;
                        break;
                    case 4:
                        moveImgs(imgCounter);
                        displayImg10.Source = imageSource;
                        break;
                    case 5:
                        moveImgs(imgCounter);
                        displayImg10.Source = imageSource;
                        break;
                    case 6:
                        moveImgs(imgCounter);
                        displayImg10.Source = imageSource;
                        break;
                    case 7:
                        moveImgs(imgCounter);
                        displayImg10.Source = imageSource;
                        break;
                    case 8:
                        moveImgs(imgCounter);
                        displayImg10.Source = imageSource;
                        break;
                    case 9:
                        moveImgs(imgCounter);
                        displayImg10.Source = imageSource;
                        break;
                    default:
                        moveImgs(imgCounter);
                        displayImg10.Source = imageSource;
                        break;
                }
                if (imgCounter < 9)
                    imgCounter++;
            }
            if (keybindBarClasses != null) {
                var listBarChange = keybindBarClasses.Where(p => p.key.ToLower().Equals(e.keycode.ToString().ToLower()) && p.modifier.ToLower().Equals(modifier.ToLower()) && (p.bar.name.ToLower().Equals(style.ToLower()) || p.bar.name.Equals("ALL"))).Select(p => p).FirstOrDefault();
                if (listBarChange != null) {
                    if (!listBarChange.name.ToLower().Equals("pause") && !listBarChange.name.ToLower().Equals("clear")) {
                        style = listBarChange.name;
                   
                        //changeStyle();
                    }
                }
            }
            control = false;

        }
        #endregion
    }


    private void moveImgs(int counter) {
        switch (counter) {
            case 1:
                displayImg9.Source = displayImg10.Source;
                break;
            case 2:
                displayImg8.Source = displayImg9.Source;
                displayImg9.Source = displayImg10.Source;
                break;
            case 3:
                displayImg7.Source = displayImg8.Source;
                displayImg8.Source = displayImg9.Source;
                displayImg9.Source = displayImg10.Source;
                break;
            case 4:
                displayImg6.Source = displayImg7.Source;
                displayImg7.Source = displayImg8.Source;
                displayImg8.Source = displayImg9.Source;
                displayImg9.Source = displayImg10.Source;
                break;
            case 5:
                displayImg5.Source = displayImg6.Source;
                displayImg6.Source = displayImg7.Source;
                displayImg7.Source = displayImg8.Source;
                displayImg8.Source = displayImg9.Source;
                displayImg9.Source = displayImg10.Source;
                break;
            case 6:
                displayImg4.Source = displayImg5.Source;
                displayImg5.Source = displayImg6.Source;
                displayImg6.Source = displayImg7.Source;
                displayImg7.Source = displayImg8.Source;
                displayImg8.Source = displayImg9.Source;
                displayImg9.Source = displayImg10.Source;
                break;
            case 7:
                displayImg3.Source = displayImg4.Source;
                displayImg4.Source = displayImg5.Source;
                displayImg5.Source = displayImg6.Source;
                displayImg6.Source = displayImg7.Source;
                displayImg7.Source = displayImg8.Source;
                displayImg8.Source = displayImg9.Source;
                displayImg9.Source = displayImg10.Source;
                break;
            case 8:
                displayImg2.Source = displayImg3.Source;
                displayImg3.Source = displayImg4.Source;
                displayImg4.Source = displayImg5.Source;
                displayImg5.Source = displayImg6.Source;
                displayImg6.Source = displayImg7.Source;
                displayImg7.Source = displayImg8.Source;
                displayImg8.Source = displayImg9.Source;
                displayImg9.Source = displayImg10.Source;
                break;
            case 9:
                displayImg1.Source = displayImg2.Source;
                displayImg2.Source = displayImg3.Source;
                displayImg3.Source = displayImg4.Source;
                displayImg4.Source = displayImg5.Source;
                displayImg5.Source = displayImg6.Source;
                displayImg6.Source = displayImg7.Source;
                displayImg7.Source = displayImg8.Source;
                displayImg8.Source = displayImg9.Source;
                displayImg9.Source = displayImg10.Source;
                break;
        }
    }
}