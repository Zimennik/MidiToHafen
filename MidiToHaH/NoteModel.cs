using System;

namespace MidiToHafen
{
    public class NoteModel
    {

        public KeyCode Key { get; private set; }
       // public Action PlayNote { get; private set; }
        public Form1.Modifier Modifier { get; private set; }



        public NoteModel(KeyCode keyCode,  Form1.Modifier modifier)
        {
            Key = keyCode;
            Modifier = modifier;
        }


        public void PressKey()
        {

            switch (Modifier)
            {
                case Form1.Modifier.Shift: KeyboardEmulator.SendKeyDown(KeyCode.SHIFT); KeyboardEmulator.SendKeyDown(Key); KeyboardEmulator.SendKeyUp(KeyCode.SHIFT);
                    break;
                case Form1.Modifier.Ctrl: KeyboardEmulator.SendKeyDown(KeyCode.CONTROL); KeyboardEmulator.SendKeyDown(Key); KeyboardEmulator.SendKeyUp(KeyCode.CONTROL);
                    break;
                case Form1.Modifier.None: KeyboardEmulator.SendKeyDown(Key);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("modifier");
            }
        }

        public void ReleaseKey()
        {
            KeyboardEmulator.SendKeyUp(Key);
        }
    }
}
