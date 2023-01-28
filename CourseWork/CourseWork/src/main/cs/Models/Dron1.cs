using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.Models
{
    public class Dron1 : AbstractDron
    {
        protected override void SetDisplayProperty()
        {
            width = 0.75;
            height = 0.5;
            sprite1  = viewModel.flyWeightSprites[7].GetBitmap;
            spriteMirror = viewModel.flyWeightSprites[8].GetBitmap;
            maxHealth = health = 1;
        }

        protected override void SetSpeed()
        {
            speed = 4;
        }

        protected override void reduceEnemy()
        {
            GameStateSingleton.GetInstance().cntKilledPlains[0]++;
            viewModel.Plain1Cnt = " " + (GameStateSingleton.GetInstance().cntKilledPlains[0]).ToString();
        }

    }
}
