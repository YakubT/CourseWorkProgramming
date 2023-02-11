using CourseWork.src.main.cs.utility;
using CourseWork.src.main.cs.ViewModels.utils;
using CourseWork.src.main.cs.ViewModels.utils.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWork.src.main.cs.ViewModels
{
    public class ChooseLevelViewModel : BaseViewModel, ICloseableWindow
    {
        private Dictionary<string, ILanguageChooseLevelState> dictionary = new Dictionary<string, ILanguageChooseLevelState>();

        private ILevelState[] levelStates = new ILevelState[4];

        private string titleText;

        private string backButtonContent;

        private string trainingText;

        private bool isEnabledLevel2;

        private bool isEnabledLevel3;

        private string level1PassedVisibility;

        private string level2PassedVisibility;

        private string level3PassedVisibility;

        public Window Window { get; }

        public string BackButtonContent
        {
            get=>backButtonContent;
            set
            {
                backButtonContent = value;
                OnPropertyChanged(nameof(BackButtonContent));
            }
        }

        public string TrainingText
        {
            get => trainingText;
            set
            {
                trainingText = value;
                OnPropertyChanged(nameof(TrainingText));
            }
        }

        public string TitleText 
        { 
            get=>titleText;
            set
            {
                titleText = value;
                OnPropertyChanged(nameof(TitleText));
            }
        }

        public bool IsEnabledLevel2
        {
            get => isEnabledLevel2;
            set
            {
                isEnabledLevel2 = value;
                OnPropertyChanged(nameof(IsEnabledLevel2));
            }
        }

        public bool IsEnabledLevel3
        {
            get => isEnabledLevel3;
            set
            {
                isEnabledLevel3 = value;
                OnPropertyChanged(nameof(IsEnabledLevel3));
            }
        }

        public string Level1PassedVisibility
        {
            get => level1PassedVisibility;
            set
            {
                level1PassedVisibility = value;
                OnPropertyChanged(nameof(level1PassedVisibility));
            }
        }

        public string Level2PassedVisibility
        {
            get => level2PassedVisibility;
            set
            {
                level2PassedVisibility = value;
                OnPropertyChanged(nameof(level2PassedVisibility));
            }
        }

        public string Level3PassedVisibility
        {
            get => level3PassedVisibility;
            set
            {
                level3PassedVisibility = value;
                OnPropertyChanged(nameof(level3PassedVisibility));
            }
        }

        public BackButtonClickCommand BackButtonClickCommand { get; }

        public StartTrainingCommand StartTrainingCommand { get; }

        public Level1Click Level1Click { get; }

        public Level2Click Level2Click { get; }

        public ChooseLevelViewModel(Window window)
        {
            StartTrainingCommand = new StartTrainingCommand(this);
            Level1Click = new Level1Click(this);
            Level2Click = new Level2Click(this);
            dictionary["EN"] = new ChooseLevelEnglishLanguageImplementor();
            dictionary["UA"] = new ChooseLevelUkrainianLanguageImplementor();
            levelStates[0] = new LevelState0();
            levelStates[1] = new LevelState1();
            levelStates[2] = new LevelState2();
            levelStates[3] = new LevelState3();
            PropertiesUtil properties = new PropertiesUtil(GlobalConstants.file);
            string s = properties.getValue("language");
            dictionary[s].UpdateLanguage(this);
            BackButtonClickCommand = new BackButtonClickCommand(this);
            Window = window;
            string levelHighest = properties.getValue("level");
            if (levelHighest == null || levelHighest=="")
            {
                levelHighest = "0";
                properties.setValue("level", levelHighest);
            }
            int levelPassed = int.Parse(levelHighest);
            levelStates[levelPassed].setStateOfButtons(this);
          
        }
    }
}
