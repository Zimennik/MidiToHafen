using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Midi;

namespace MidiToHafen
{



    public partial class Form1 : Form
    {
        private InputDevice currentInputDevice;
        public Dictionary<string, NoteModel> NotesDictionary { get; private set; }
        public Form1()
        {
            InitializeComponent();
            NotesDictionary = new Dictionary<string, NoteModel>
            {
                {"C3", new NoteModel(KeyCode.KEY_Z,Modifier.Ctrl)} ,
                {"CSharp3", new NoteModel(KeyCode.KEY_S,Modifier.Ctrl)} ,
                {"D3", new NoteModel(KeyCode.KEY_X,Modifier.Ctrl)},
                {"DSharp3", new NoteModel(KeyCode.KEY_D,Modifier.Ctrl)},
                {"E3", new NoteModel(KeyCode.KEY_C,Modifier.Ctrl)},
                {"F3", new NoteModel(KeyCode.KEY_V,Modifier.Ctrl)},
                {"FSharp3", new NoteModel(KeyCode.KEY_G,Modifier.Ctrl)},
                {"G3", new NoteModel(KeyCode.KEY_B,Modifier.Ctrl)},
                {"GSharp3", new NoteModel(KeyCode.KEY_H,Modifier.Ctrl)},
                {"A3", new NoteModel(KeyCode.KEY_N,Modifier.Ctrl)},
                {"ASharp3", new NoteModel(KeyCode.KEY_J,Modifier.Ctrl)},
                {"B3", new NoteModel(KeyCode.KEY_M,Modifier.Ctrl)},

                {"C4", new NoteModel(KeyCode.KEY_Z,Modifier.None)} ,
                {"CSharp4", new NoteModel(KeyCode.KEY_S,Modifier.None)} ,
                {"D4", new NoteModel(KeyCode.KEY_X,Modifier.None)},
                {"DSharp4", new NoteModel(KeyCode.KEY_D,Modifier.None)},
                {"E4", new NoteModel(KeyCode.KEY_C,Modifier.None)},
                {"F4", new NoteModel(KeyCode.KEY_V,Modifier.None)},
                {"FSharp4", new NoteModel(KeyCode.KEY_G,Modifier.None)},
                {"G4", new NoteModel(KeyCode.KEY_B,Modifier.None)},
                {"GSharp4", new NoteModel(KeyCode.KEY_H,Modifier.None)},
                {"A4", new NoteModel(KeyCode.KEY_N,Modifier.None)},
                {"ASharp4", new NoteModel(KeyCode.KEY_J,Modifier.None)},
                {"B4", new NoteModel(KeyCode.KEY_M,Modifier.None)},

                {"C5", new NoteModel(KeyCode.KEY_Z,Modifier.Shift)} ,
                {"CSharp5", new NoteModel(KeyCode.KEY_S,Modifier.Shift)} ,
                {"D5", new NoteModel(KeyCode.KEY_X,Modifier.Shift)},
                {"DSharp5", new NoteModel(KeyCode.KEY_D,Modifier.Shift)},
                {"E5", new NoteModel(KeyCode.KEY_C,Modifier.Shift)},
                {"F5", new NoteModel(KeyCode.KEY_V,Modifier.Shift)},
                {"FSharp5", new NoteModel(KeyCode.KEY_G,Modifier.Shift)},
                {"G5", new NoteModel(KeyCode.KEY_B,Modifier.Shift)},
                {"GSharp5", new NoteModel(KeyCode.KEY_H,Modifier.Shift)},
                {"A5", new NoteModel(KeyCode.KEY_N,Modifier.Shift)},
                {"ASharp5", new NoteModel(KeyCode.KEY_J,Modifier.Shift)},
                {"B5", new NoteModel(KeyCode.KEY_M,Modifier.Shift)},
            };
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (InputDevice.InstalledDevices.Count > 0)
            {
                if (currentInputDevice != null && currentInputDevice.IsOpen)
                {
                    currentInputDevice.StopReceiving();
                    currentInputDevice.NoteOn -= new InputDevice.NoteOnHandler(NoteOn);
                    currentInputDevice.NoteOff -= new InputDevice.NoteOffHandler(NoteOff);
                    currentInputDevice.Close();
                }

                if (comboBox1.SelectedIndex >=0)
                {
                    label1.Text = "";
                    currentInputDevice = InputDevice.InstalledDevices[comboBox1.SelectedIndex];
                }

                if (currentInputDevice != null)
                {
                    currentInputDevice.Open();
                    label1.Text = "Connected to" +currentInputDevice.Name;

                    currentInputDevice.NoteOn += new InputDevice.NoteOnHandler(NoteOn);
                    currentInputDevice.NoteOff += new InputDevice.NoteOffHandler(NoteOff);
                    currentInputDevice.StartReceiving(null);  // Note events will be received in another thread
                }
            }
        }

        public enum Modifier
        {
            Shift,
            Ctrl,
            None
        }

        public void NoteOn(NoteOnMessage msg)
        {
            try
            {
                NotesDictionary[msg.Pitch.ToString()].PressKey();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void NoteOff(NoteOffMessage msg)
        {
             try
            {
                NotesDictionary[msg.Pitch.ToString()]. ReleaseKey();
            }
             catch (Exception)
             {
                 // ignored
             }
        }      

        private void Form1_Load(object sender, EventArgs e)
        {
            // Делаем обычный стиль.
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            // Убираем кнопки свернуть, развернуть, закрыть.
            //this.ControlBox = false;
            // Убираем заголовок.
            //this.Text = "";

            foreach (var t in InputDevice.InstalledDevices)
            {
                comboBox1.Items.Add(t.Name);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }



}
