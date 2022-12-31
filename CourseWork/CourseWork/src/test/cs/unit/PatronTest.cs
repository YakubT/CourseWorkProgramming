using CourseWork.src.main.cs.Models;
using CourseWork.src.main.cs.ViewModels;
using CourseWork.src.main.cs.Views;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWork.src.test.cs.unit
{
    [TestFixture]
    public class PatronTest 
    {
        [Test]
        public void Patron1IsCorrectImage()
        {
            Patron patron = new Patron1();
            patron.SetDisplayProperites();
        } 
    }
}
