using CourseWork.src.main.cs.Models;
using CourseWork.src.main.cs.Models.utility;
using CourseWork.src.main.cs.utility;
using CourseWork.src.main.cs.ViewModels.utils;
using CourseWork.src.main.cs.ViewModels.utils.interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CourseWork.src.main.cs.ViewModels
{
   
    public class FieldViewModel : BaseViewModel,ICloseableWindow
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public delegate void PauseEventHandler (FieldViewModel field);

        public event PauseEventHandler PauseEvent;

        public event PauseEventHandler ResumeEvent;

        private Dictionary <string,ILanguageField> bridge = new Dictionary<string, ILanguageField>();

        public FlyWeightSprite[] flyWeightSprites = {new FlyWeightSprite(new BitmapImage(new Uri("/src/main/resources/img/rockets/ppo_rocket1.png", UriKind.Relative))),
            new FlyWeightSprite(new BitmapImage(new Uri("/src/main/resources/img/rockets/ppo_rocket2.png", UriKind.Relative))),
            new  FlyWeightSprite (new BitmapImage(new Uri("/src/main/resources/img/rockets/ppo_rocket3.png", UriKind.Relative))),
            new FlyWeightSprite(new BitmapImage(new Uri("/src/main/resources/img/plains/mig_31.png", UriKind.Relative))),
            new FlyWeightSprite(new BitmapImage(new Uri("/src/main/resources/img/plains/mig_31_mirror.png", UriKind.Relative))),
            new FlyWeightSprite(new BitmapImage(new Uri("/src/main/resources/img/plains/plain2.png", UriKind.Relative))),
            new FlyWeightSprite(new BitmapImage(new Uri("/src/main/resources/img/plains/plain2_mirror.png", UriKind.Relative))),
            new FlyWeightSprite(new BitmapImage(new Uri("/src/main/resources/img/dron1.png", UriKind.Relative))),
            new FlyWeightSprite(new BitmapImage(new Uri("/src/main/resources/img/dron1_2.png", UriKind.Relative))),
        };
        public List<AbstractEnemy> enemyList = new List<AbstractEnemy>();

        public List<AbstractPatron> patrons = new List<AbstractPatron>();

        private Window window;
        public GunRotateCommand RotateGunCommand { get;}

        public PatronStartFlyCommand PatronStartFly { get; }

        public ChangeWeaponCommand WheelScroll { get; }

        public PauseCommand PauseCommand { get; }

        public ResumeCommand ResumeCommand { get; }

        public ShuttdownCommand ShuttdownCommand { get; }

        public BackButtonClickCommand BackButtonClickCommand { get; }

        public RestartCommand RestartCommand { get; }

        private double angle;

        private uint wheelType;

        private double fontsize;

        private int posOfFrame;

        private string labelContent;

        private string pauseMenuVisibility;

        private string goToMenuBtnContent;

        private string goToGameBtnContent;

        private string goToWindowsBtnContent;

        private string patronInfoVisibility;

        private string rocket1Cnt;

        private string rocket1MaxCnt;

        private string rocket2Cnt;

        private string rocket2MaxCnt;

        private string rocket3Cnt;

        private string rocket3MaxCnt;

        public double Angle
        {
            get => angle;
            set
            {
                angle = value;
                OnPropertyChanged(nameof(Angle));
            }
        }

        public uint WheelType
        {
            get => wheelType;
            set
            {
                wheelType = value;
                PosOfFrame = 20 + (int)wheelType;
            }
        }

        public double FontSize
        {
            get => fontsize;
            set
            {
                fontsize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }

        public string PatronInfoVisibility
        {
            get => patronInfoVisibility;
            set
            {
                patronInfoVisibility = value;
                OnPropertyChanged(nameof(PatronInfoVisibility));
            }
        }
        public int PosOfFrame
        {
            get => posOfFrame;
            set
            {
                posOfFrame = value;
                OnPropertyChanged(nameof(posOfFrame));
            }
        }

        public string LabelContent
        {
            get => labelContent;
            set
            {
                labelContent = value;
                OnPropertyChanged(nameof(LabelContent));
            }
        }

        public string PauseMenuVisibility
        {
            get => pauseMenuVisibility;
            set
            {
                pauseMenuVisibility = value;
                OnPropertyChanged(nameof(pauseMenuVisibility));
            }
        }

        public string GoToMenuBtnContent
        {
            get =>  goToMenuBtnContent;
            set
            {
                goToMenuBtnContent = value;
                OnPropertyChanged(GoToMenuBtnContent);
            }
        }

        public string GoToGameBtnContent
        {
            get => goToGameBtnContent;
            set
            {
                goToGameBtnContent = value;
                OnPropertyChanged(GoToGameBtnContent);
            }
        }

        public string GoToWindowsBtnContent
        {
            get => goToWindowsBtnContent;
            set
            {
                goToWindowsBtnContent = value;
                OnPropertyChanged(GoToWindowsBtnContent);
            }
        }

        public string Rocket1Cnt
        {
            get => rocket1Cnt;
            set
            {
                rocket1Cnt = value;
                OnPropertyChanged(nameof(Rocket1Cnt));
            }
        }

        public string Rocket2Cnt
        {
            get => rocket2Cnt;
            set
            {
                rocket2Cnt = value;
                OnPropertyChanged(nameof(Rocket2Cnt));
            }
        }

        public string Rocket3Cnt
        {
            get => rocket3Cnt;
            set
            {
                rocket3Cnt = value;
                OnPropertyChanged(nameof(Rocket3Cnt));
            }
        }


        public string Rocket1MaxCnt
        {
            get => rocket1MaxCnt;
            set
            {
                rocket1MaxCnt = value;
                OnPropertyChanged(nameof(Rocket1MaxCnt));
            }
        }

        public string Rocket2MaxCnt
        {
            get => rocket2MaxCnt;
            set
            {
                rocket2MaxCnt = value;
                OnPropertyChanged(nameof(Rocket2MaxCnt));
            }
        }

        public string Rocket3MaxCnt
        {
            get => rocket3MaxCnt;
            set
            {
                rocket3MaxCnt = value;
                OnPropertyChanged(nameof(Rocket3MaxCnt));
            }
        }

        private string plain1MaxCnt;
        public string Plain1MaxCnt
        {
            get => plain1MaxCnt;
            set
            {
                plain1MaxCnt = value;
                OnPropertyChanged(nameof(Plain1MaxCnt));
            }
        }

        private string plain2MaxCnt;
        public string Plain2MaxCnt
        {
            get => plain2MaxCnt;
            set
            {
                plain2MaxCnt = value;
                OnPropertyChanged(nameof(Plain2MaxCnt));
            }
        }

        private string plain3MaxCnt;
        public string Plain3MaxCnt
        {
            get => plain3MaxCnt;
            set
            {
                plain3MaxCnt = value;
                OnPropertyChanged(nameof(Plain3MaxCnt));
            }
        }

        private string plain1Cnt;
        public string Plain1Cnt
        {
            get => plain1Cnt;
            set
            {
                plain1Cnt = value;
                OnPropertyChanged(nameof(Plain1Cnt));
            }
        }

        private string plain2Cnt;
        public string Plain2Cnt
        {
            get => plain2Cnt;
            set
            {
                plain2Cnt = value;
                OnPropertyChanged(nameof(Plain2Cnt));
            }
        }

        private string plain3Cnt;
        public string Plain3Cnt
        {
            get => plain3Cnt;
            set
            {
                plain3Cnt = value;
                OnPropertyChanged(nameof(Plain3Cnt));
            }
        }

        private string restartText;

        public string RestartText
        {
            get => restartText;
            set
            {
                restartText = value;
                OnPropertyChanged(nameof(RestartText));
            }
        }

        public void UpdateLanguge()
        {
            string s = "";
            try
            {
                s = new PropertiesUtil(GlobalConstants.file).getValue("language");
            }
            catch (Exception e)
            {
                s = "UA";
                new PropertiesUtil(GlobalConstants.file).setValue("language", s);
            }
            bridge[s].UpdateLanguage(this);
        }
        public FieldViewModel(Window window)
        {
            this.window = window;
            RotateGunCommand = new GunRotateCommand(this);
            PatronStartFly = new PatronStartFlyCommand(this);
            WheelScroll = new ChangeWeaponCommand(this);
            PauseCommand = new PauseCommand(this);
            ResumeCommand = new ResumeCommand(this);
            ShuttdownCommand = new ShuttdownCommand();
            BackButtonClickCommand = new BackButtonClickCommand(this);
            WheelType = 0;
            PauseMenuVisibility = "Hidden";
            bridge["EN"] = new EnLanguageField();
            bridge["UA"] = new UkrLanguageField();
            GameStateSingleton.GetInstance().Ispause = false;
            UpdateLanguge();
            PatronInfoVisibility = "Visible";
            RestartCommand = new RestartCommand(this);
        }
        
        public void StartTraining()
        {
            PatronInfoVisibility = "Hidden";
            FontSize = 0.5 * (window.ActualHeight / GlobalConstants.rowCount);
            const double time = GameStateSingleton.reloadTime;
            LabelContent = new PropertiesUtil(GlobalConstants.file).getValue("language").Equals("UA") ? "Інтервал між пострілами - " +time.ToString() + " с." : "The interval between shots is " + time.ToString()+ " s.";
            dispatcherTimer.Interval = TimeSpan.FromSeconds(4);
            CreatorEmeny[] creators = new CreatorEmeny[3];
            creators[0] = new CreatorPlain1();
            creators[1] = new CreatorPlain2();
            creators[2] = new CreatorDron();
            GameStateSingleton gameStateSingleton = GameStateSingleton.GetInstance();
            gameStateSingleton.levelLoaded = 0;
            gameStateSingleton.cntRockets[0] = int.MaxValue;
            gameStateSingleton.cntRockets[1] = int.MaxValue;
            gameStateSingleton.cntRockets[2] = int.MaxValue;
            dispatcherTimer.Tick += (s, e) =>
            {
                Random rnd = new Random();
                AbstractEnemy plain = creators[rnd.Next(3)].Create();
                if (plain is AbstractDron)
                {
                    plain.HeightOfFly = rnd.Next(15, 18) + rnd.NextDouble();
                }
                else
                {
                    plain.HeightOfFly = rnd.Next(18, 22) + rnd.NextDouble();
                }
                plain.viewModel = this;
                plain.IsFromRight = Convert.ToBoolean(rnd.Next(2));
                plain.Fly();
            };
            dispatcherTimer.Start();

            Refresh();
        }

        public void StartEnemy (AbstractEnemy plain, int time)
        {
            
            DispatcherTimer dispatcherTimer3 = new DispatcherTimer();
                dispatcherTimer3.Interval = TimeSpan.FromSeconds(time);
                dispatcherTimer3.Tick += (s, e) =>
                {
                    if (!GameStateSingleton.GetInstance().Ispause) {
                        plain.Fly();
                        dispatcherTimer3.Stop();
                    }
                       
                };

                dispatcherTimer3.Start();
        }

        public void Refresh()
        {
            DispatcherTimer dispatcherTimer2 = new DispatcherTimer();
            dispatcherTimer2.Interval = TimeSpan.FromMilliseconds(1);
            dispatcherTimer2.Tick += (s, e) =>
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    for (int j = 0; j < patrons.Count; j++)
                    {
                        if (check(enemyList[i], patrons[j]))
                        {
                            enemyList[i].health -= patrons[j].Demage;
                            patrons[j].Abort(this);
                        }
                    }
                }
            };
            dispatcherTimer2.Start();
        }
        public void StartLevel1()
        {
            GameStateSingleton.GetInstance().levelLoaded = 1;
            FontSize = 0.5 * (window.ActualHeight / GlobalConstants.rowCount);
            GameStateSingleton gameStateSingleton = GameStateSingleton.GetInstance();
            gameStateSingleton.cntMaxRockets[0] = gameStateSingleton.cntRockets[0] = 10;
            gameStateSingleton.cntMaxRockets[1] = gameStateSingleton.cntRockets[1] = 6;
            gameStateSingleton.cntMaxRockets[2] = gameStateSingleton.cntRockets[2] = 2;
            Rocket1MaxCnt = Rocket1Cnt = " "+gameStateSingleton.cntRockets[0].ToString();
            Rocket2MaxCnt = Rocket2Cnt = " "+gameStateSingleton.cntRockets[1].ToString();
            Rocket3MaxCnt = Rocket3Cnt = " "+gameStateSingleton.cntRockets[2].ToString();
            gameStateSingleton.cntMaxPlains[1] = gameStateSingleton.cntPlains[1] = 8;
            Plain2Cnt = Plain2MaxCnt = " " + gameStateSingleton.cntMaxPlains[1].ToString();
            Refresh();
            Plain1 plain1 = new Plain1();
            plain1.viewModel = this;
            plain1.HeightOfFly = 18;
            plain1.IsFromRight=false;
            StartEnemy(plain1,3);
            plain1 = new Plain1();
            plain1.viewModel = this;
            plain1.HeightOfFly = 19;
            plain1.IsFromRight = false;
            StartEnemy(plain1,7);
            plain1 = new Plain1();
            plain1.viewModel = this;
            plain1.HeightOfFly = 18;
            plain1.IsFromRight = true;
            StartEnemy(plain1,14);
            plain1 = new Plain1();
            plain1.viewModel = this;
            plain1.HeightOfFly = 21;
            plain1.IsFromRight = true;
            StartEnemy(plain1, 17);
            plain1 = new Plain1();
            plain1.viewModel = this;
            plain1.HeightOfFly = 22;
            plain1.IsFromRight = true;
            StartEnemy(plain1, 20);
            plain1 = new Plain1();
            plain1.viewModel = this;
            plain1.HeightOfFly = 20;
            plain1.IsFromRight = true;
            StartEnemy(plain1, 24);

            plain1 = new Plain1();
            plain1.viewModel = this;
            plain1.HeightOfFly = 21;
            plain1.IsFromRight = false;
            StartEnemy(plain1, 27);

            plain1 = new Plain1();
            plain1.viewModel = this;
            plain1.HeightOfFly = 22;
            plain1.IsFromRight = true;
            StartEnemy(plain1, 31);
        }
        
        public double Soriented(Models.Vector a,Models.Vector b, Models.Vector c)
        {
            return (a.X - b.X) * (b.Y + a.Y) / 2.0+(c.X - a.X) * (c.Y + a.Y) / 2.0+ (b.X - c.X) * (b.Y + c.Y) / 2.0;
        }

        public bool intersect(Models.Vector v1, Models.Vector v2, Models.Vector v3, Models.Vector v4, Models.Vector v5, Models.Vector v6)
        {
            if ((Soriented(v1, v5, v6)) * (Soriented(v2, v5, v6)) <= 0 && (Soriented(v5, v1, v2)) * (Soriented(v6, v1, v2)) <= 0)
            {

                return true;
            }

            if ((Soriented(v1, v3, v4)) * (Soriented(v2, v3, v4)) <= 0 && (Soriented(v3, v1, v2)) * (Soriented(v4, v1, v2)) <= 0)
            {
                return true;
            }
           
            if ((Soriented(v1, v3, v5)) * (Soriented(v2, v3, v5)) <= 0 && (Soriented(v3, v1, v2)) * (Soriented(v5, v1, v2)) <= 0)
            {

                return true;
            }
            if ((Soriented(v1, v4, v6)) * (Soriented(v2, v4, v6)) <= 0 && (Soriented(v4, v1, v2)) * (Soriented(v6, v1, v2)) <= 0)
            {
                return true;
            }
            return false;
        }
        public bool check(AbstractEnemy abstractPlain, AbstractPatron abstractPatron)
        {
            double cos = abstractPatron.Speed.X / Math.Sqrt(abstractPatron.Speed.X * abstractPatron.Speed.X + abstractPatron.Speed.Y * abstractPatron.Speed.Y);
            double sin = abstractPatron.Speed.Y / Math.Sqrt(abstractPatron.Speed.X * abstractPatron.Speed.X + abstractPatron.Speed.Y * abstractPatron.Speed.Y);

            Models.Vector v1, v2, v3, v4, v5, v6;
            v1 = new Models.Vector();
            v2 = new Models.Vector();
            v3 = new Models.Vector();
            v4 = new Models.Vector();
            v5 = new Models.Vector();
            v6 = new Models.Vector();
            v1.X = abstractPatron.Coordinates.X + abstractPatron.Height * window.ActualHeight / GlobalConstants.rowCount / 2.0 * cos;
            v1.Y = abstractPatron.Coordinates.Y + abstractPatron.Height * window.ActualHeight / GlobalConstants.rowCount / 2.0 * sin;
            v2.X = abstractPatron.Coordinates.X - abstractPatron.Height * window.ActualHeight / GlobalConstants.rowCount / 2.0 * cos;
            v2.Y = abstractPatron.Coordinates.Y - abstractPatron.Height * window.ActualHeight / GlobalConstants.rowCount / 2.0 * sin;
            v3.X = abstractPlain.Coordinates.X * window.ActualWidth / GlobalConstants.rowCount;
            v3.Y = abstractPlain.Coordinates.Y * window.ActualHeight / GlobalConstants.rowCount;
            v4.X = (abstractPlain.Coordinates.X + abstractPlain.Width) * window.ActualWidth / GlobalConstants.columnCount;
            v4.Y = abstractPlain.Coordinates.Y * window.ActualHeight / GlobalConstants.rowCount;
            v5.X = (abstractPlain.Coordinates.X) * window.ActualWidth / GlobalConstants.columnCount;
            v5.Y = (abstractPlain.Coordinates.Y - abstractPlain.Height) * window.ActualHeight / GlobalConstants.rowCount;
            v6.X = (abstractPlain.Coordinates.X+abstractPlain.Width) * window.ActualWidth / GlobalConstants.columnCount;
            v6.Y = (abstractPlain.Coordinates.Y - abstractPlain.Height) * window.ActualHeight / GlobalConstants.rowCount;
            Models.Vector vec = new Models.Vector(v2.X - v1.X, v2.Y - v1.Y);
            Models.Vector perp = new Models.Vector(v2.Y - v1.Y, -v2.X + v1.X) * abstractPatron.Width*0.5* (1.0 / Math.Sqrt((v2.Y - v1.Y) * (v2.Y - v1.Y) + (v2.X - v1.X) * (v2.X - v1.X))) * (window.ActualWidth / 24.0);
            Models.Vector v1copy, v2copy;
            
            for (double i=-2;i<=1;i+=0.05)
            {
                v1copy = v1 + (perp * i);
                v2copy = v2 + (perp * i);
                if (intersect(v1copy, v2copy, v3, v4, v5, v6)) 
                    return true;
            }
            return false;
         
        }

        public Window Window { get => window; }

        public void Pause()
        {
            if (PauseEvent != null)
            {
                PauseEvent.Invoke(this);
            }
            GameStateSingleton gameStateSingleton = GameStateSingleton.GetInstance();
            gameStateSingleton.Ispause = true;
            dispatcherTimer.Stop();
        }

        public void Resume()
        {
            if (ResumeEvent!=null)
            {
                ResumeEvent.Invoke(this);
            }
            GameStateSingleton gameStateSingleton = GameStateSingleton.GetInstance();
            gameStateSingleton.Ispause = false;
            dispatcherTimer.Start();
        }
       
    }

}
