using System;
using Microsoft.Win32;
using System.IO;

namespace CaesarCipherWPF
{
    class Loader
    {

        public string Import()
        {
            string text = "";
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (oFD.ShowDialog() == true)
                text = File.ReadAllText(oFD.FileName);
            else text = "";
            return text;
            
        }

        public void Export(string text)
        {
            SaveFileDialog sFD = new SaveFileDialog();
            sFD.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (sFD.ShowDialog() == true)
            {
                File.WriteAllText(sFD.FileName, text);
            }
        }
    }
}
