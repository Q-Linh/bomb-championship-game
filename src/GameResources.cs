using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;
using SwinGameSDK;


/// <summary>
/// The GameResources is responsible for loading all the neccessary resources (such as images and fonts) at the start of the game
/// and free those resources when the game ends.
/// </summary> 
namespace MyGame
{
    public static class GameResources
    {

        private static void LoadFonts()
        {
            NewFont("Arial", "arial", 40);
        }

        private static void LoadImages()
        {
            //NewImage("BombField", "BombChampionshipField.png");
            NewImage("Char1Up", "Character1_Up.png");
            NewImage("Char1Down", "Character1_Down.png");
            NewImage("Char1Left", "Character1_Left.png");
            NewImage("Char1Right", "Character1_Right.png");
            NewImage("Char1MovingUp", "Character1_MovingUp.png");
            NewImage("Char1MovingDown", "Character1_MovingDown.png");
            NewImage("Char1MovingLeft", "Character1_MovingLeft.png");
            NewImage("Char1MovingRight", "Character1_MovingRight.png");
            NewImage("Char2Up", "Character2_Up.png");
            NewImage("Char2Down", "Character2_Down.png");
            NewImage("Char2Left", "Character2_Left.png");
            NewImage("Char2Right", "Character2_Right.png");
            NewImage("Char2MovingUp", "Character2_MovingUp.png");
            NewImage("Char2MovingDown", "Character2_MovingDown.png");
            NewImage("Char2MovingLeft", "Character2_MovingLeft.png");
            NewImage("Char2MovingRight", "Character2_MovingRight.png");
            NewImage("UnactivatedBomb", "UnactivatedBomb.png");
            NewImage("ExplosionAnimation", "ExplosionAnimation.png");
            NewImage("Explosion4", "Explosion4.png");
            NewImage("Field", "Field.png");
            NewImage("Vertical", "Vertical.png");
            NewImage("Horizontal", "Horizontal.png");
            NewImage("SlowBomb", "SlowBomb.png");
            NewImage("MediumBomb", "MediumBomb.png");
            NewImage("FastBomb", "FastBomb.png");
            NewImage("Tree", "Tree.png");
            NewImage("Wall", "Wall.png");
            NewImage("Dynamite", "Dynamite.png");
            NewImage("PlayButton", "PlayButton.png");
        }

        private static void LoadSounds()
        {
        } 

        private static void LoadMusic()
        {
        }

        /// <summary>
        /// Gets a Font Loaded in the Resources
        /// </summary>
        /// <param name="font">Name of Font</param>
        /// <returns>The Font Loaded with this Name</returns>
      
        public static Font GameFont(string font)
        {
            return _Fonts[font];
        }



        /// <summary>
        /// Gets an Image loaded in the Resources
        /// </summary>
        /// <param name="image">Name of image</param>
        /// <returns>The image loaded with this name</returns>

        public static Bitmap GameImage(string image)
        {
            return _Images[image];
        }

        /// <summary>
        /// Gets an sound loaded in the Resources
        /// </summary>
        /// <param name="sound">Name of sound</param>
        /// <returns>The sound with this name</returns>

        public static SoundEffect GameSound(string sound)
        {
            return _Sounds[sound];
        }

        /// <summary>
        /// Gets the music loaded in the Resources
        /// </summary>
        /// <param name="music">Name of music</param>
        /// <returns>The music with this name</returns>

        public static Music GameMusic(string music)
        {
            return _Music[music];
        }

        private static Dictionary<string, Bitmap> _Images = new Dictionary<string, Bitmap>();
        private static Dictionary<string, Font> _Fonts = new Dictionary<string, Font>();
        private static Dictionary<string, SoundEffect> _Sounds = new Dictionary<string, SoundEffect>();

        private static Dictionary<string, Music> _Music = new Dictionary<string, Music>();
        /// <summary>
        /// The Resources Class stores all of the Games Media Resources, such as Images, Fonts
        /// Sounds, Music.
        /// </summary>

        public static void LoadResources()
        {
            LoadFonts();
            LoadImages();
            LoadSounds();
            LoadMusic();
        }


        //
        //Add new resources, including: fonts, iamges, sounds, music
        //

        private static void NewFont(string fontName, string filename, int size)
        {
            _Fonts.Add(fontName, SwinGame.LoadFont(filename, size));
        }

        private static void NewImage(string imageName, string filename)
        {
            _Images.Add(imageName, SwinGame.LoadBitmap(filename));
        }

        private static void NewSound(string soundName, string filename)
        {
            _Sounds.Add(soundName, Audio.LoadSoundEffect(SwinGame.PathToResource(filename, ResourceKind.SoundResource)));
        }

        private static void NewMusic(string musicName, string filename)
        {
            _Music.Add(musicName, Audio.LoadMusic(SwinGame.PathToResource(filename, ResourceKind.SoundResource)));
        }

        //
        //Free all resources, including: fonts, images, sounds, music
        //
        private static void FreeFonts()
        {
            foreach (Font obj in _Fonts.Values)
            {
                SwinGame.FreeFont(obj);
            }
        }

        private static void FreeImages()
        {
            foreach (Bitmap obj in _Images.Values)
            {
                SwinGame.FreeBitmap(obj);
            }
        }

        private static void FreeSounds()
        {
            foreach (SoundEffect obj in _Sounds.Values)
            {
                Audio.FreeSoundEffect(obj);
            }
        }

        private static void FreeMusic()
        {

            foreach (Music obj in _Music.Values)
            {
                Audio.FreeMusic(obj);
            }
        }

        public static void FreeResources()
        {
            FreeFonts();
            FreeImages();
            FreeMusic();
            FreeSounds();
            SwinGame.ProcessEvents();
        }
    }
}
