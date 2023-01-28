using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class Plain1 : AbstractPlain
    {
        public Plain1():base() {}
        public Plain1(FieldViewModel viewModel) : base(viewModel) { }

        protected override void reduceEnemy()
        {
            GameStateSingleton.GetInstance().cntKilledPlains[1]++;
            viewModel.Plain2Cnt = " "+GameStateSingleton.GetInstance().cntKilledPlains[1].ToString();
        }

        protected override void SetDisplayProperty()
        {
            width = 3;
            height = 1.2;
            maxHealth = health = 2;
            sprite1 = viewModel.flyWeightSprites[3].GetBitmap;
            spriteMirror = viewModel.flyWeightSprites[4].GetBitmap;
        }

        protected override void SetSpeed()
        {
            speed = 6.2;
        }
    }
}
