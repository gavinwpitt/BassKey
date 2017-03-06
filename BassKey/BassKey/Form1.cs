using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BassKey
{
    public enum Note
    {
        [Description("A")]
        A,
        [Description("A#")]
        Asharp,
        [Description("B")]
        B,
        [Description("C")]
        C,
        [Description("C#")]
        Csharp,
        [Description("D")]
        D,
        [Description("D#")]
        Dsharp,
        [Description("E")]
        E,
        [Description("F")]
        F,
        [Description("F#")]
        Fsharp,
        [Description("G")]
        G,
        [Description("G#")]
        Gsharp
    }

    public partial class Form1 : Form
    {
        Button[] buttons;
        Color defaultColor;
        Boolean[] displayOptions;
        int currentRoot;
        ToolStripMenuItem[] toolStripOptions;

        // Display Options
        Boolean displayMajorScale;
        Boolean displayMinorScale;
        Boolean displayMajorPentatonic;
        Boolean displayMinorPentatonic;
        Boolean displayMajorBlues;
        Boolean displayMinorBlues;
        Boolean displayMajorMixo;
        Boolean displayDorian;
        Boolean displayPhrygian;
        // Mode type boolean
        Boolean majorMode;
        Boolean minorMode;
        Boolean currDisplay;

        public Form1()
        {
            InitializeComponent();

            initButtons();

            initDisplayOptions();

            resetDisplayOptions();

            initMenuItems();

            defaultColor = button1.BackColor;

            
        }
        
        private void initButtons()
        {
            buttons = new Button[]{
                button1, button2, button3, button4, button5, button6, button7, button8,button9, button10,
                button11, button12, button13, button14, button15, button16, button17, button18, button19, button20,
                button21, button22, button23, button24, button25, button26, button27, button28, button29, button30,
                button31, button32, button33, button34, button35, button36, button37, button38, button39, button40,
                button41, button42, button43, button44, button45, button46, button47, button48
            };

            for (int i = 0; i < 48; i++)
            {
                // E STRING
                if (i < 12 && i >= 0)
                {
                    buttons[i].Text = returnLetter((i + 7) % 12);
                }
                // A STRING
                if (i < 24 && i >= 12)
                {
                    buttons[i].Text = returnLetter((i + 0) % 12);
                }
                // D STRING
                if (i < 36 && i >= 24)
                {
                    buttons[i].Text = returnLetter((i + 5) % 12);
                }
                // G STRING
                if (i < 48 && i >= 36)
                {
                    buttons[i].Text = returnLetter((i + 10) % 12);
                }

                buttons[i].Name = i.ToString();
                buttons[i].Click += myButtonClick;
            }
        }

        private void initMenuItems()
        {
            toolStripOptions = new ToolStripMenuItem[]
            {
                naturalMajor,
                lydian,
                mixolydian,
                naturalMinor,
                phrygian,
                dorian,
                pentaMajor,
                pentaMinor,
                bluesMajor,
                bluesMinor
            };

            for (int i = 0; i < toolStripOptions.Length; i++)
            {
                //toolStripOptions[i].Checked = true;
                toolStripOptions[i].Click += menuItemClick;
            }
        }

        private void resetDisplayOptions()
        {
            for(int i = 0; i < displayOptions.Length; i++)
            {
                displayOptions[i] = false;
            }
            // For some reason displayMajorScale is always true, so i have to reset it here
            displayDorian = false;
            displayPhrygian = false;
            displayMajorScale = false;
            displayMinorScale = false;
            displayMajorPentatonic = false;
            displayMinorPentatonic = false;
            displayMajorBlues = false;
            displayMinorBlues = false;
            displayMajorMixo = false;
        }

        private void initDisplayOptions()
        {
            displayOptions = new Boolean[]
            {
                displayMajorScale,
                displayMinorScale,
                displayMajorPentatonic,
                displayMinorPentatonic,
                displayMajorBlues,
                displayMinorBlues,
                displayMajorMixo
            };

            for (int i = 0; i < displayOptions.Length; i++)
            {
                displayOptions[i] = false;
            }

        }

        private void resetMenuOptions()
        {
            Console.Write(buttons.Length);
            for (int j = 0; j < toolStripOptions.Length; j++)
            {
                toolStripOptions[j].Checked = false;
            }
        }

        private void changeDisplayBoolean(string name)
        {
            resetDisplayOptions();
            
            switch (name)
            {
                case "naturalMajor":displayMajorScale = true; return;
                case "naturalMinor":displayMinorScale = true; return;
                case "dorian": displayDorian = true; return;
                case "phrygian": displayPhrygian = true; return;
                case "mixolydian":displayMajorMixo = true; return;
                case "pentaMajor":displayMajorPentatonic = true; return;
                case "pentaMinor":displayMinorPentatonic = true; return;
                case "bluesMajor":displayMajorBlues = true; return;
                case "bluesMinor":displayMinorBlues = true; return;
            }
        }

        private String returnLetter(int note)
        {
            switch (note % 12)
            {
                case 0: return "A";
                case 1: return "A#";
                case 2: return "B";
                case 3: return "C";
                case 4: return "C#";
                case 5: return "D";
                case 6: return "D#";
                case 7: return "E";
                case 8: return "F";
                case 9: return "F#";
                case 10: return "G";
                case 11: return "G#";

            }
            return "ERROR IN RETURN LETTER";
        }

        private int returnInt(string note)
        {
            switch (note)
            {
                case "A": return 0;
                case "A#": return 1;
                case "B": return 2;
                case "C": return 3;
                case "C#": return 4;
                case "D": return 5;
                case "D#": return 6;
                case "E": return 7;
                case "F": return 8;
                case "F#": return 9;
                case "G": return 10;
                case "G#": return 11;
            }
            return -1;
        }

        private void displayScale(int note)
        {
            string[] scale = getScale(note);
            for (int i = 0; i < 48; i++)
            {
                try
                {
                    if (scale.Contains((buttons[i].Text)))
                    {
                        // Highlight all notes
                        if (buttons[i].Text.Equals(returnLetter(note)))
                        {
                            buttons[i].BackColor = Color.Red;
                        }else { 
                            buttons[i].BackColor = Color.Green;
                        }

                        // Handle Major Chord Tones
                        if (majorMode)
                        {
                            if (buttons[i].Text.Equals(scale[2].ToString()))       // THIRD
                            {
                                buttons[i].BackColor = Color.GreenYellow;
                            }
                            else if (buttons[i].Text.Equals(scale[4].ToString()))       // Fifth
                            {
                                buttons[i].BackColor = Color.GreenYellow;
                            }
                            else if (buttons[i].Text.Equals(scale[6].ToString()))          // Seventh
                            {
                                buttons[i].BackColor = Color.LawnGreen;
                            }
                            else
                            {

                            }
                        }
                        else if (minorMode)
                        {

                        }
                        else { }
                    }
                }
                catch (ArgumentNullException oops)
                {
                    //button1.Text = "no2";
                }
            }
        }

        private void clearScale()
        {
            foreach(Button button in buttons)
            {
                button.BackColor = defaultColor;
            }
        }

        private string[] getScale(int root)
        {
            string[] scale = new string[7];
            if (displayMajorScale == true)
            {
                // R W W H W W W H
                scaleIndicator.Text = "Displaying " + Note.GetName(typeof(Note), root) + " Major";
                scale[0] = returnLetter(root);
                scale[1] = returnLetter((root + 2) % 12);
                scale[2] = returnLetter((root + 4) % 12);
                scale[3] = returnLetter((root + 5) % 12);
                scale[4] = returnLetter((root + 7) % 12);
                scale[5] = returnLetter((root + 9) % 12);
                scale[6] = returnLetter((root + 11) % 12);
            } else if (displayDorian) {
                // R W H W W W H W
                scaleIndicator.Text = "Displaying " + Note.GetName(typeof(Note), root) + " Dorian";
                scale[0] = returnLetter(root);
                scale[1] = returnLetter((root + 2) % 12);
                scale[2] = returnLetter((root + 3) % 12);
                scale[3] = returnLetter((root + 5) % 12);
                scale[4] = returnLetter((root + 7) % 12);
                scale[5] = returnLetter((root + 9) % 12);
                scale[6] = returnLetter((root + 10) % 12);
            } else if (displayPhrygian){
                // R H W W W H W W
                scaleIndicator.Text = "Displaying " + Note.GetName(typeof(Note), root) + " Phrygian";
                scale[0] = returnLetter(root);
                scale[1] = returnLetter((root + 1) % 12);
                scale[2] = returnLetter((root + 3) % 12);
                scale[3] = returnLetter((root + 5) % 12);
                scale[4] = returnLetter((root + 7) % 12);
                scale[5] = returnLetter((root + 8) % 12);
                scale[6] = returnLetter((root + 10) % 12);
            } else if (displayMinorScale) {
                // W H W W H W W
                scaleIndicator.Text = "Displaying " + Note.GetName(typeof(Note), root) + " Minor";
                scale[0] = returnLetter(root);
                scale[1] = returnLetter((root + 2) % 12);
                scale[2] = returnLetter((root + 3) % 12);
                scale[3] = returnLetter((root + 5) % 12);
                scale[4] = returnLetter((root + 7) % 12);
                scale[5] = returnLetter((root + 8) % 12);
                scale[6] = returnLetter((root + 10) % 12);
            } else if (displayMajorMixo)
            {
                // W W H W W W bH
                scaleIndicator.Text = "Displaying MajorMixo";
                scale[0] = returnLetter(root);
                scale[1] = returnLetter((root + 2) % 12);
                scale[2] = returnLetter((root + 4) % 12);
                scale[3] = returnLetter((root + 5) % 12);
                scale[4] = returnLetter((root + 7) % 12);
                scale[5] = returnLetter((root + 9) % 12);
                scale[6] = returnLetter((root + 10) % 12);
            }
            
            return scale;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            displayScale(7);
        }

        private void myButtonClick(object sender, EventArgs e)
        {
            clearScale();
            Button button = sender as Button;
            currentRoot = returnInt(button.Text);
            displayScale(currentRoot);
        }

        private void menuItemClick(object sender, EventArgs e)
        {
            resetMenuOptions();
            clearScale();

            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            menuItem.Checked = true;
            ToolStripMenuItem parent = (ToolStripMenuItem)menuItem.OwnerItem;
            if( parent.Name.Equals(majorModesToolStripMenuItem.Name) )
            {
                majorMode = true;
                minorMode = false;
            }else if( parent.Name.Equals(minorModesToolStripMenuItem.Name) )
            {
                majorMode = false;
                minorMode = true;
            }else { }
            changeDisplayBoolean(menuItem.Name);
            displayScale(currentRoot);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void scaleViewSwitch_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void majorScalesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void majorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void majorToolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void majorToolStripMenuItem4_Click(object sender, EventArgs e)
        {
           // standardMajor.Checked = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void standardMinor_Click(object sender, EventArgs e)
        {

        }

        private void dorianToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
