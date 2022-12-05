using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Assessment
{
    public enum Position { Goalkeeper, Defender, Midfielder, Forward }

    internal class Player : IComparable
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

        public int CompareTo(object obj)
        {
            Player that = obj as Player;

            if (this.PreferedPositon > that.PreferedPositon)
                return 1;
            else if (this.PreferedPositon < that.PreferedPositon)
                return -1;
            else
                return this.PreferedPositon.CompareTo(that.PreferedPositon);
        }
    }
}
