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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DnDCharacterCreator
{
    /// <summary>
    /// Interaction logic for CharacterSheet.xaml
    /// </summary>
    public partial class CharacterSheet : Window
    {

        public CharacterSheet()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
        }

        private void getModifier(ref ComboBox scoreBox, ref TextBox modBox)
        {
            modBox.Text = "";

            int score = 10;

            ComboBoxItem cbi = (ComboBoxItem)scoreBox.SelectedItem;

            if (cbi != null)
            {
                int.TryParse(cbi.Content.ToString(), out score);

                int modifier = (int)Math.Floor(((float)score - 10.0) / 2.0);

                if (modifier > 0)
                {
                    modBox.Text += "+";
                }

                modBox.Text += modifier;
            }
        }

        private void updateHP()
        {
            ComboBoxItem cbi = (ComboBoxItem)cmbClass.SelectedItem;

            int conMod = 0;

            int.TryParse(txtConMod.Text, out conMod);

            if (cbi != null)
            {
                switch (cbi.Content.ToString())
                {
                    case "Cleric":
                        txtHitDice.Text = "d8";
                        txtHPMax.Text = (8 + conMod).ToString();
                        break;
                    case "Fighter":
                        txtHitDice.Text = "d10";
                        txtHPMax.Text = (10 + conMod).ToString();
                        break;
                    case "Rogue":
                        txtHitDice.Text = "d8";
                        txtHPMax.Text = (8 + conMod).ToString();
                        break;
                    case "Wizard":
                        txtHitDice.Text = "d6";
                        txtHPMax.Text = (6 + conMod).ToString();
                        break;
                }
            }            
        }

        private void cmbStr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getModifier(ref cmbStr, ref txtStrMod);
        }

        private void cmbDex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getModifier(ref cmbDex, ref txtDexMod);

            txtInitiative.Text = txtDexMod.Text;
        }

        private void cmbCon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getModifier(ref cmbCon, ref txtConMod);

            updateHP();
        }

        private void cmbInt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getModifier(ref cmbInt, ref txtIntMod);
        }

        private void cmbWis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getModifier(ref cmbWis, ref txtWisMod);
        }

        private void cmbCha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getModifier(ref cmbCha, ref txtChaMod);
        }

        private void cmbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateHP();
        }

        private void cmbRace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cmbRace.SelectedItem;

            int conMod = 0;

            int.TryParse(txtConMod.Text, out conMod);

            if (cbi != null)
            {
                switch (cbi.Content.ToString())
                {
                    case "Hill Dwarf":
                    case "Mountain Dwarf":
                        txtSpeed.Text = "25ft";
                        break;
                    case "High Elf":
                    case "Wood Elf":
                        txtSpeed.Text = "30ft";
                        break;
                    case "Lightfoot Halfling":
                    case "Stout Halfling":
                        txtSpeed.Text = "25ft";
                        break;
                    case "Human":
                        txtSpeed.Text = "30ft";
                        break;
                }
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
            txtPlayerName.Text = "";
            cmbClass.SelectedIndex = -1;
            cmbRace.SelectedIndex = -1;
            cmbBackground.SelectedIndex = -1;
            cmbAlignment.SelectedIndex = -1;

            cmbStr.SelectedIndex = -1;
            cmbDex.SelectedIndex = -1;
            cmbCon.SelectedIndex = -1;
            cmbInt.SelectedIndex = -1;
            cmbWis.SelectedIndex = -1;
            cmbCha.SelectedIndex = -1;

            txtStrMod.Text = "";
            txtDexMod.Text = "";
            txtConMod.Text = "";
            txtIntMod.Text = "";
            txtWisMod.Text = "";
            txtChaMod.Text = "";

            txtProficiencies.Text = "";
            txtAttacks.Text = "";
            txtEquipment.Text = "";
            txtTraits.Text = "";
            txtIdeals.Text = "";
            txtBonds.Text = "";
            txtFlaws.Text = "";
            txtFeatures.Text = "";

            txtAC.Text = "";
            txtInitiative.Text = "";
            txtSpeed.Text = "";
            txtHitDice.Text = "";
            txtHPMax.Text = "";

            chkStrSav.IsChecked = false;
            chkDexSav.IsChecked = false;
            chkConSav.IsChecked = false;
            chkIntSav.IsChecked = false;
            chkWisSav.IsChecked = false;
            chkChaSav.IsChecked = false;

            chkAcrobatics.IsChecked = false;
            chkAnimalHandling.IsChecked = false;
            chkArcana.IsChecked = false;
            chkAthletics.IsChecked = false;
            chkDeception.IsChecked = false;
            chkHistory.IsChecked = false;
            chkInsight.IsChecked = false;
            chkIntimidation.IsChecked = false;
            chkInvestigation.IsChecked = false;
            chkMedicine.IsChecked = false;
            chkNature.IsChecked = false;
            chkPerception.IsChecked = false;
            chkPerformance.IsChecked = false;
            chkPersuasion.IsChecked = false;
            chkReligion.IsChecked = false;
            chkSleightOfHand.IsChecked = false;
            chkStealth.IsChecked = false;
            chkSurvival.IsChecked = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Character myCharacter = new Character();

            myCharacter.name = txtName.Text;
            myCharacter.playerName = txtPlayerName.Text;

            myCharacter.characterClass = cmbClass.SelectedIndex;
            myCharacter.race = cmbRace.SelectedIndex;
            myCharacter.background = cmbBackground.SelectedIndex;
            myCharacter.alignment = cmbAlignment.SelectedIndex;

            myCharacter.strength = cmbStr.SelectedIndex;
            myCharacter.dexterity = cmbDex.SelectedIndex;
            myCharacter.constitution = cmbCon.SelectedIndex;
            myCharacter.intelligence = cmbInt.SelectedIndex;
            myCharacter.wisdom = cmbWis.SelectedIndex;
            myCharacter.charisma = cmbCha.SelectedIndex;

            myCharacter.proficiencies = txtProficiencies.Text;
            myCharacter.attacks = txtAttacks.Text;
            myCharacter.equipment = txtEquipment.Text;
            myCharacter.traits = txtTraits.Text;
            myCharacter.ideals = txtIdeals.Text;
            myCharacter.bonds = txtBonds.Text;
            myCharacter.flaws = txtFlaws.Text;
            myCharacter.features = txtFeatures.Text;

            int.TryParse(txtAC.Text, out myCharacter.armorClass);
            
            int.TryParse(txtInitiative.Text, out myCharacter.initiative);
            myCharacter.speed = txtSpeed.Text;
            myCharacter.hitDice = txtHitDice.Text;
            int.TryParse(txtHPMax.Text, out myCharacter.hitPointMaximum);

            myCharacter.strSave = (bool)chkStrSav.IsChecked;
            myCharacter.dexSave = (bool)chkDexSav.IsChecked;
            myCharacter.conSave = (bool)chkConSav.IsChecked;
            myCharacter.intSave = (bool)chkIntSav.IsChecked;
            myCharacter.wisSave = (bool)chkWisSav.IsChecked;
            myCharacter.chaSave = (bool)chkChaSav.IsChecked;

            myCharacter.acrobatics = (bool)chkAcrobatics.IsChecked;
            myCharacter.animalHandling = (bool)chkAnimalHandling.IsChecked;
            myCharacter.arcana = (bool)chkArcana.IsChecked;
            myCharacter.athletics = (bool)chkAthletics.IsChecked;
            myCharacter.deception = (bool)chkDeception.IsChecked;
            myCharacter.history = (bool)chkHistory.IsChecked;
            myCharacter.insight = (bool)chkInsight.IsChecked;
            myCharacter.intimidation = (bool)chkIntimidation.IsChecked;
            myCharacter.investigation = (bool)chkInvestigation.IsChecked;
            myCharacter.medicine = (bool)chkMedicine.IsChecked;
            myCharacter.nature = (bool)chkNature.IsChecked;
            myCharacter.perception = (bool)chkPerception.IsChecked;
            myCharacter.performance = (bool)chkPerformance.IsChecked;
            myCharacter.persuasion = (bool)chkPersuasion.IsChecked;
            myCharacter.religion = (bool)chkReligion.IsChecked;
            myCharacter.sleightOfHand = (bool)chkSleightOfHand.IsChecked;
            myCharacter.stealth = (bool)chkStealth.IsChecked;
            myCharacter.survival = (bool)chkSurvival.IsChecked;

            XmlSerializer writer =
            new XmlSerializer(typeof(Character));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//myDnDCharacter.xml";
            FileStream file = File.Create(path);

            writer.Serialize(file, myCharacter);
            file.Close();

            MessageBox.Show("Character saved to \"myDnDCharacter.xml\" in Documents folder.", "Save Complete");
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new
            XmlSerializer(typeof(Character));

            // A FileStream is needed to read the XML document.
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//myDnDCharacter.xml";
            FileStream fs = new FileStream(path, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            // Declare an object variable of the type to be deserialized.
            Character myCharacter;

            // Use the Deserialize method to restore the object's state.
            myCharacter = (Character)serializer.Deserialize(reader);
            fs.Close();

            txtName.Text = myCharacter.name;
            txtPlayerName.Text = myCharacter.playerName;
            cmbClass.SelectedIndex = myCharacter.characterClass;
            cmbRace.SelectedIndex = myCharacter.race;
            cmbBackground.SelectedIndex = myCharacter.background;
            cmbAlignment.SelectedIndex = myCharacter.alignment;

            cmbStr.SelectedIndex = myCharacter.strength;
            cmbDex.SelectedIndex = myCharacter.dexterity;
            cmbCon.SelectedIndex = myCharacter.constitution;
            cmbInt.SelectedIndex = myCharacter.intelligence;
            cmbWis.SelectedIndex = myCharacter.wisdom;
            cmbCha.SelectedIndex = myCharacter.charisma;

            getModifier(ref cmbStr, ref txtStrMod);
            getModifier(ref cmbDex, ref txtDexMod);
            getModifier(ref cmbCon, ref txtConMod);
            getModifier(ref cmbInt, ref txtIntMod);
            getModifier(ref cmbWis, ref txtWisMod);
            getModifier(ref cmbCha, ref txtChaMod);

            txtProficiencies.Text = myCharacter.proficiencies;
            txtAttacks.Text = myCharacter.attacks;
            txtEquipment.Text = myCharacter.equipment;
            txtTraits.Text = myCharacter.traits;
            txtIdeals.Text = myCharacter.ideals;
            txtBonds.Text = myCharacter.bonds;
            txtFlaws.Text = myCharacter.flaws;
            txtFeatures.Text = myCharacter.features;

            txtAC.Text = myCharacter.armorClass.ToString();
            txtInitiative.Text = myCharacter.initiative.ToString();
            txtSpeed.Text = myCharacter.speed;
            txtHitDice.Text = myCharacter.hitDice;
            txtHPMax.Text = myCharacter.hitPointMaximum.ToString();

            chkStrSav.IsChecked = myCharacter.strSave;
            chkDexSav.IsChecked = myCharacter.dexSave;
            chkConSav.IsChecked = myCharacter.conSave;
            chkIntSav.IsChecked = myCharacter.intSave;
            chkWisSav.IsChecked = myCharacter.wisSave;
            chkChaSav.IsChecked = myCharacter.chaSave;

            chkAcrobatics.IsChecked = myCharacter.acrobatics;
            chkAnimalHandling.IsChecked = myCharacter.animalHandling;
            chkArcana.IsChecked = myCharacter.arcana;
            chkAthletics.IsChecked = myCharacter.athletics;
            chkDeception.IsChecked = myCharacter.deception;
            chkHistory.IsChecked = myCharacter.history;
            chkInsight.IsChecked = myCharacter.insight;
            chkIntimidation.IsChecked = myCharacter.intimidation;
            chkInvestigation.IsChecked = myCharacter.investigation;
            chkMedicine.IsChecked = myCharacter.medicine;
            chkNature.IsChecked = myCharacter.nature;
            chkPerception.IsChecked = myCharacter.perception;
            chkPerformance.IsChecked = myCharacter.performance;
            chkPersuasion.IsChecked = myCharacter.persuasion;
            chkReligion.IsChecked = myCharacter.religion;
            chkSleightOfHand.IsChecked = myCharacter.sleightOfHand;
            chkStealth.IsChecked = myCharacter.stealth;
            chkSurvival.IsChecked = myCharacter.survival;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Created by David Cagnacci (david.cagnacci@outlook.com).\n\nCharacter sheet and rules adapted from Wizards of the Coast's \"Dungeons & Dragons Basic Rules\". A copy of the rules (available for free download at dnd.wizards.com) is required for use of this program.", "About");
        }
    }
}
