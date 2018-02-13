/*
 * Created by David Cagnacci
 * Latest Revision 8/26/2016
 * Contact: david.cagnacci@outlook.com
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharacterCreator
{
    public class Character
    {
        public int race;
        public int characterClass;
        public int background;
        public int alignment;

        public string name;
        public string playerName;
        public int strength, dexterity, constitution, intelligence, wisdom, charisma;
        public bool strSave, dexSave, conSave, intSave, wisSave, chaSave;
        public bool acrobatics, animalHandling, arcana, athletics, deception, history, insight, intimidation, investigation, medicine, nature, perception, performance, persuasion, religion, sleightOfHand, stealth, survival;
        public int armorClass;
        public int initiative;
        public string speed;
        public string hitDice;
        public int hitPointMaximum;
        public string proficiencies;
        public string attacks;
        public string equipment;
        public string traits;
        public string ideals;
        public string bonds;
        public string flaws;
        public string features;

        public Character()
        {

        }
    }
}
