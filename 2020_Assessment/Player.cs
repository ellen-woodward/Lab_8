using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Assessment
{
    public enum Position { Goalkeeper, Defender, Midfielder, Forward }

    internal class Player
    {
        // Properties
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Position PreferedPositon { get; set; }
        public DateTime DOB { get; set; }

        private int age;

        public int Age 
        {
            get 
            {
                age = DateTime.Now.Year - DOB.Year;
                if (DOB.DayOfYear >= DateTime.Now.DayOfYear)
                    age--;
                return age;
            }
            set 
            { 
                age = value;
            }
        }

        // Constructors

        // Methods
        public override string ToString()
        {
            return $"{FirstName} {Surname} ({Age}) {PreferedPositon.ToString().ToUpper()}";
        }
    }
}
