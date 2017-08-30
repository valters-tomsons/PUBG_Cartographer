using System;
using System.Linq;
using System.Windows;
using PUBG_Cartographer.Domain;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static PUBG_Cartographer.Interface.PUBG_External;

namespace PUBG_Cartographer
{
    public partial class MainWindow : Window
    {
        Overlay overlay = new Overlay();
        KeyHandler handler = new KeyHandler();
        GlobalKeyboardHook KBhook = new GlobalKeyboardHook();
        ObservableCollection<int> Buttons = new ObservableCollection<int>();

        Process PUBG_game = null;
        //bool wasGameRunning = false;

        private int[] KeyCodes;
        private string[] KeyNames;

        bool HandleKeys = false;

        public MainWindow()
        {
            InitializeComponent();
            KeyNames = Configuration.CreateKeyArrays().Item1;
            KeyCodes = Configuration.CreateKeyArrays().Item2;

            KBhook.KeyboardPressed += On_KeyPressed;
            Buttons.CollectionChanged += Buttons_Changed;

            overlay.Show();
        }

        //Gets called when actual key is pressed on keyboard
        [STAThread]
        private void On_KeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if(isGameInFocus(PUBG_game))
            {
                if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
                {
                    if (!Buttons.Contains(e.KeyboardData.VirtualCode))
                    {
                        Buttons.Add(e.KeyboardData.VirtualCode);

                        if (HandleKeys)
                        {
                            e.Handled = true;
                        }
                    }
                }
                else
                {
                    Buttons.Remove(e.KeyboardData.VirtualCode);

                    if (HandleKeys)
                    {
                        e.Handled = true;
                    }
                }
            }

        }

        private void Buttons_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            //= on press
            if (e.NewItems != null)
            {
                foreach (int key in e.NewItems)
                {
                    //listen only to supported keys
                    if (KeyCodes.Contains(key))
                    {
                        int loc = Array.IndexOf(KeyCodes, key);
                        string keyname = KeyNames[loc];
                        string[] _binds = XMLParser.Binds(keyname);
                        string[] _types = XMLParser.Types(keyname);

                        if (_binds.Length >= 0)
                        {
                            var i = 0;
                            foreach (string bind in _binds)
                            {
                                handler.HandleKey(_binds[i], _types[i], true, overlay);
                                i++;
                            }
                        }
                    }
                }
            }
            
            //= on depress
            if (e.OldItems != null)
            {
                foreach (int key in e.OldItems)
                {
                    //listen only to supported keys
                    if (KeyCodes.Contains(key))
                    {
                        int loc = Array.IndexOf(KeyCodes, key);
                        string keyname = KeyNames[loc];
                        string[] _binds = XMLParser.Binds(keyname);
                        string[] _types = XMLParser.Types(keyname);

                        if (_binds.Length >= 0)
                        {
                            var i = 0;
                            foreach (string bind in _binds)
                            {
                                handler.HandleKey(_binds[i], _types[i], false, overlay);
                                i++;
                            }
                        }
                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            overlay.Close();
        }
    }
}
