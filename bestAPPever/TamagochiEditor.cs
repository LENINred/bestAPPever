using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;

namespace bestAPPever
{
    class TamagochiEditor
    {
        private int curHead;
        private int curBody;
        private int curLegs;
        private PictureBox tamagoci;

        public TamagochiEditor(int head, int body, int legs)
        {
            curHead = head;
            curBody = body;
            curLegs = legs;
            getImages();
        }

        List<Image> Heads = new List<Image>();
        List<Image> Bodys = new List<Image>();
        List<Image> Legs = new List<Image>();
        private void getImages()
        {
            ResourceSet rsrcSet = bestAPPever.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, false, true);
            foreach (DictionaryEntry entry in rsrcSet)
            {
                String name = entry.Key.ToString();
                if (name.Contains("head")) Heads.Add((Image)entry.Value);
                if (name.Contains("body")) Bodys.Add((Image)entry.Value);
                if (name.Contains("legs")) Legs.Add((Image)entry.Value);
            }
        }

        public PictureBox createTamagoci()
        {
            tamagoci = new PictureBox();
            tamagoci.Size = new Size(125, 328);
            tamagoci.Location = new Point(12, 70);
            tamagoci.BorderStyle = BorderStyle.FixedSingle;
            tamagoci.Image = new Bitmap(125, 328);
            using (Graphics graphics = Graphics.FromImage(tamagoci.Image))
            {
                graphics.DrawImage(Heads[0], 0, 0);
                graphics.DrawImage(Bodys[0], 0, 135);
                graphics.DrawImage(Legs[0], 0, 242);
                graphics.Save();
            }
            tamagoci.Image = tamagoci.Image;
            return tamagoci;
        }

        public void nextHead()
        {
            using (Graphics graphics = Graphics.FromImage(tamagoci.Image))
            {
                if (curHead < Heads.Count - 1) curHead++;
                    else curHead = 0;
                graphics.DrawImage(Heads[curHead], 0, 0);
                tamagoci.Image = tamagoci.Image;
            }
        }

        public void nextBody()
        {
            using (Graphics graphics = Graphics.FromImage(tamagoci.Image))
            {
                if (curBody < Bodys.Count - 1) curBody++;
                    else curBody = 0;
                graphics.DrawImage(Bodys[curBody], 0, 135);
                tamagoci.Image = tamagoci.Image;
            }
        }

        public void nextLegs()
        {
            using (Graphics graphics = Graphics.FromImage(tamagoci.Image))
            {
                if (curLegs < Legs.Count - 1) curLegs++;
                    else curLegs = 0;
                graphics.DrawImage(Legs[curLegs], 0, 242);
                tamagoci.Image = tamagoci.Image;
            }
        }

        public void prevHead()
        {
            using (Graphics graphics = Graphics.FromImage(tamagoci.Image))
            {
                if (curHead > 0) curHead--;
                else curHead = Heads.Count - 1;
                graphics.DrawImage(Heads[curHead], 0, 0);
                tamagoci.Image = tamagoci.Image;
            }
        }
        
        public void prevBody()
        {
            using (Graphics graphics = Graphics.FromImage(tamagoci.Image))
            {
                if (curBody > 0) curBody--;
                else curBody = Bodys.Count - 1;
                graphics.DrawImage(Bodys[curBody], 0, 135);
                tamagoci.Image = tamagoci.Image;
            }
        }

        public void prevLegs()
        {
            using (Graphics graphics = Graphics.FromImage(tamagoci.Image))
            {
                if (curLegs > 0) curLegs--;
                else curLegs = Legs.Count - 1;
                graphics.DrawImage(Legs[curLegs], 0, 242);
                tamagoci.Image = tamagoci.Image;
            }
        }
    }
}
