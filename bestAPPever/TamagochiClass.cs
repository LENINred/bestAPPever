using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;

namespace bestAPPever
{
    class TamagochiClass
    {
        private string Head = String.Empty;
        private string Body = String.Empty;
        private string Legs = String.Empty;
        private PictureBox tamagoci;
        private int headsCount = 0;
        private int bodysCount = 0;
        private int legsCount = 0;

        public TamagochiClass(int head, int body, int legs)
        {
            Head = head.ToString();
            Body = body.ToString();
            Legs = legs.ToString();
        }

        private void getCounts()
        {
            headsCount = 0;
            bodysCount = 0;
            legsCount = 0;
            ResourceSet rsrcSet = bestAPPever.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, false, true);
            foreach (DictionaryEntry entry in rsrcSet)
            {
                String name = entry.Key.ToString();
                if (name.Contains("head")) headsCount++;
                if (name.Contains("body")) bodysCount++;
                if (name.Contains("legs")) legsCount++;
            }
        }

        private Image getHead()
        {
            Image headPic = (Image)Properties.Resources.ResourceManager.GetObject("head_" + Head);
            return headPic;
        }

        private Image getBody()
        {
            Image bodyPic = (Image)Properties.Resources.ResourceManager.GetObject("body_" + Body);
            return bodyPic;
        }

        private Image getLegs()
        {
            Image legsPic = (Image)Properties.Resources.ResourceManager.GetObject("legs_" + Legs);
            return legsPic;
        }

        public PictureBox createTamagoci()
        {
            tamagoci = new PictureBox();
            tamagoci.Size = new Size(125, 299);
            tamagoci.Location = new Point(12, 70);
            tamagoci.BorderStyle = BorderStyle.FixedSingle;
            tamagoci.Image = new Bitmap(125, 299);
            using (Graphics g = Graphics.FromImage(tamagoci.Image))
            {
                g.DrawImage(getHead(), 0, 0, getBody().Width, getHead().Height);
                g.DrawImage(getBody(), 0, 114, getBody().Width, getBody().Height);
                g.DrawImage(getLegs(), 0, 222, getBody().Width, getLegs().Height);
                g.Save();
            }
            tamagoci.Image = tamagoci.Image;
            return tamagoci;
        }

        public void nextHead()
        {
            getCounts();
            if (short.Parse(Head) <= headsCount)
            {
                using (Graphics g = Graphics.FromImage(tamagoci.Image))
                {
                    g.DrawImage(getHead(), 0, 0, getBody().Width, getHead().Height);
                    g.Save();
                }
                tamagoci.Refresh();
                tamagoci.Update();
                Head = (short.Parse(Head) + 1).ToString();
            }
            else Head = "1";
        }

        public void nextBody()
        {
            getCounts();
            if (short.Parse(Body) <= bodysCount)
            {
                Body = (short.Parse(Body) + 1).ToString();
                using (Graphics g = Graphics.FromImage(tamagoci.Image))
                {
                    g.DrawImage(getBody(), 0, 114, getBody().Width, getBody().Height);
                    g.Save();
                }
                tamagoci.Refresh();
                tamagoci.Update();
            }
            else Body = "1";
        }

        public void nextLegs()
        {
            getCounts();
            if (short.Parse(Legs) <= legsCount)
            {
                Head = (short.Parse(Legs) + 1).ToString();
                using (Graphics g = Graphics.FromImage(tamagoci.Image))
                {
                    g.DrawImage(getLegs(), 0, 222, getBody().Width, getLegs().Height);
                    g.Save();
                }
                tamagoci.Refresh();
                tamagoci.Update();
            }
            else Legs = "1";
        }

    }
}
