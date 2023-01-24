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

        private double angle;

        private uint wheelType;

        private double fontsize;

        private int posOfFrame;

        private string labelContent;

        private string pauseMenuVisibility;

        private string goToMenuBtnContent;

        private string goToGameBtnContent;

        private string goToWindowsBtnContent;

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
        }
        
        public void StartTraining()
        {
            FontSize = 0.5 * (window.ActualHeight / GlobalConstants.rowCount);
            const double time = GameStateSingleton.reloadTime;
            LabelContent = new PropertiesUtil(GlobalConstants.file).getValue("language").Equals("UA") ? "Інтервал між пострілами - " +time.ToString() + " с." : "The interval between shots is " + time.ToString()+ " s.";
            dispatcherTimer.Interval = TimeSpan.FromSeconds(4);
            CreatorEmeny[] creators = new CreatorEmeny[3];
            creators[0] = new CreatorPlain1();
            creators[1] = new CreatorPlain2();
            creators[2] = new CreatorDron();
            GameStateSingleton gameStateSingleton = GameStateSingleton.GetInstance();
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
