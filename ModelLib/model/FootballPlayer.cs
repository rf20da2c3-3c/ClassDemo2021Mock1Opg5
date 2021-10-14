using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.model
{
    public class FootballPlayer
    {
        private int _id;
        private String _name;
        private int _price;
        private int _shirtNumber;

        public FootballPlayer()
        {
        }

        public FootballPlayer(int id, string name, int price, int shirtNumber)
        {
            Id = id;
            Name = name;
            Price = price;
            ShirtNumber = shirtNumber;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    throw new ArgumentException();
                }

                _name = value;
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }

                _price = value;
            }
         
        }

        public int ShirtNumber
        {
            get => _shirtNumber;
            set
            {
                if (value < 1 || 100 < value) 
                {
                    throw new ArgumentException();
                }

                _shirtNumber = value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Price)}: {Price}, {nameof(ShirtNumber)}: {ShirtNumber}";
        }
    }
}
