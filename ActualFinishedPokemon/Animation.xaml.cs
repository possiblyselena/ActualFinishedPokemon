using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

// TODO: set this to your solution name
namespace ActualFinishedPokemon
{
    public partial class Animation : UserControl
    {
        // stores the possible animations of the pokemon
        public enum Modes
        {
            Idle,
            Attack1,
            Attack2,
            Attack3,
            Faint
        }

        private int index = 0;              // the current frame number
        private Modes mode = Modes.Idle;    // the current animation being displayed
        private DispatcherTimer? timer;     // timer used to change frames

        // Lists of images for the various animations
        List<BitmapImage>? imgIdle;
        List<BitmapImage>? imgAttack1;
        List<BitmapImage>? imgAttack2;
        List<BitmapImage>? imgAttack3;
        List<BitmapImage>? imgFaint;
        // TODO: add more animation types here to match possible Modes

        // constructor - should not need to add anything in here
        public Animation()
        {
            InitializeComponent();
        }

        // setup all the images for each of the animations in the correct list
        // filesnames must be: resources, png, have a name "filename-#.png"
        // TODO: pass more info for other animation modes
        public void Initiate(int idleFrames, string idleFileName, int attack1Frames, string attack1FileName, int attack2Frames, string attack2FileName, int attack3Frames, string attack3FileName, int faintFrames, string faintFileName, double speed)
        {
            imgIdle = new List<BitmapImage>(idleFrames);
            for (int i = 1; i <= idleFrames; i++)
            {
                imgIdle.Add(new BitmapImage(new Uri("pack://application:,,,/images/" + idleFileName + "-" + i + ".png")));
            }
            imgAttack1 = new List<BitmapImage>(attack1Frames);
            for (int i = 1; i <= attack1Frames; i++)
            {
                imgAttack1.Add(new BitmapImage(new Uri("pack://application:,,,/images/" + attack1FileName + "-" + i + ".png")));
            }
            imgAttack2 = new List<BitmapImage>(attack2Frames);
            for (int i = 1; i <= attack2Frames; i++)
            {
                imgAttack2.Add(new BitmapImage(new Uri("pack://application:,,,/images/" + attack2FileName + "-" + i + ".png")));
            }
            imgAttack3 = new List<BitmapImage>(attack3Frames);
            for (int i = 1; i <= attack3Frames; i++)
            {
                imgAttack3.Add(new BitmapImage(new Uri("pack://application:,,,/images/" + attack3FileName + "-" + i + ".png")));
            }
            imgFaint = new List<BitmapImage>(faintFrames);
            for (int i = 1; i <= faintFrames; i++)
            {
                imgFaint.Add(new BitmapImage(new Uri("pack://application:,,,/images/" + faintFileName + "-" + i + ".png")));
            }   

            // TODO: add more lists for each of the animation types.

            // set the starting image
            image.Source = imgIdle.ElementAt(0);

            // set up the timer that will change the images
            this.timer = new DispatcherTimer(DispatcherPriority.Render);
            this.timer.Interval = TimeSpan.FromMilliseconds(speed);
            this.timer.Tick += new EventHandler(this.updateImage);
            this.timer.Start();
        }

        // called every time the timer goes off (ie: every frame)
        private void updateImage(object? sender, EventArgs e)
        {
            if (imgIdle != null && imgAttack1 != null && imgAttack2 != null && imgAttack3 != null && imgFaint != null)  // must ensure the lists are instantiated
            {
                List<BitmapImage> currImages = imgIdle;

                // select the images from the correct list
                switch (mode)
                {
                    case Modes.Idle:
                        currImages = imgIdle;
                        break;
                    case Modes.Attack1:
                        currImages = imgAttack1;
                        break;
                    case Modes.Attack2:
                        currImages = imgAttack2;
                        break;
                    case Modes.Attack3:
                        currImages = imgAttack3;
                        break;
                    case Modes.Faint:
                        currImages = imgFaint;
                        break;
                        // TODO: add more list choices
                }
                // set the current image
                image.Source = currImages.ElementAt(index);

                // move the index forward
                index++;

                // check if we've seen all the images
                if (index >= currImages.Count)
                {
                    switch (mode)
                    {
                        case Modes.Idle:
                            index = 0; // just start again
                            break;
                        case Modes.Attack1:
                        case Modes.Attack2:
                        case Modes.Attack3:
                            setMode(Modes.Idle); // go back to idle animation
                            break;
                        case Modes.Faint:
                            index = currImages.Count - 1; // stay dead
                            break;
                    }
                }
            }
        }

        // change the current animation
        public void setMode(Modes mode)
        {
            this.mode = mode;
            index = 0;
        }
    }
}
