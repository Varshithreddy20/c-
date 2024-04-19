using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace structures1
{
    public readonly struct Marvel
    {
        private readonly string _characterName;

        public string CharacterName { get { return _characterName; } }

        public void PrintCharacterName()
        {
            Console.WriteLine(this.CharacterName);
            
        }

        public Marvel(string characterName)
        {
            this._characterName = characterName;
        }
    }
}
