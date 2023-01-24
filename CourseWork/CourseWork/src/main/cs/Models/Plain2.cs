using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class Plain2 : AbstractPlain
    {
        public Plain2() : base() { }

        public Plain2(FieldViewModel viewModel) : base(viewModel) {  }
        protected override void SetDisplayProperty()
        {
            width = 3.5;
            height = 1.4;
            maxHealth = health = 3;
            sprite1 = viewModel.flyWeightSprites[5].GetBitmap;
            spriteMirror = viewModel.flyWeightSprites[6].GetBitmap;
        }

        protected override void SetSpeed()
        {
            speed = 8.5;
        }
        protected override void reduceEnemy()
        {
            GameStateSingleton.GetInstance().cntPlains[2]--;
            viewModel.Plain3Cnt = " " + GameStateSingleton.GetInstance().cntPlains[2].ToString();
        }
    }
}
