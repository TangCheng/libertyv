﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LibertyV.Rage.Audio.Codecs.MP3;
using LibertyV.Utils;
using LibertyV.Rage.Audio.Codecs.XMA;

namespace LibertyV.Rage.Audio.AWC
{
    class Audio : IAudio
    {
        Stream Data;
        uint Samples;
        int SamplesPerSecond;

        public Audio(Stream data, uint samples, int samplesPerSecond)
        {
            this.Samples = samples;
            this.SamplesPerSecond = samplesPerSecond;
            this.Data = data;
        }

        public Audio(Stream data, Structs.FormatChunk chunkInfo)
        {
            this.Samples = chunkInfo.Samples;
            this.SamplesPerSecond = chunkInfo.SamplesPerSecond;
            this.Data = data;
        }

        public int GetBits()
        {
            return GlobalOptions.AudioBits;
        }

        public uint GetSamplesCount()
        {
            return Samples;
        }

        public int GetSamplesPerSecond()
        {
            return SamplesPerSecond;
        }

        public int GetChannels()
        {
            return 1;
        }

        public Stream GetPCMStream()
        {
            if (GlobalOptions.Platform == Platform.PlatformType.PLAYSTATION3)
            {
                // The playstation3 use mp3 encode
                // The partial stream is because we want that each reader will have its own seeker
                return new MP3DecoderStream(new PartialStream(this.Data, 0, this.Data.Length));
            }
            else if (GlobalOptions.Platform == Platform.PlatformType.XBOX360)
            {
                return new XMA2DecoderStream(new PartialStream(this.Data, 0, this.Data.Length));
            }
            return null;
        }

        public static Stream GetCodecStream(Stream input)
        {
            if (GlobalOptions.Platform == Platform.PlatformType.PLAYSTATION3)
            {
                // The playstation3 use mp3 encode
                // The partial stream is because we want that each reader will have its own seeker
                return new MP3DecoderStream(input);
            }
            else if (GlobalOptions.Platform == Platform.PlatformType.XBOX360)
            {
                return new XMA2DecoderStream(input);
            }
            return null;
        }

        public void Dispose()
        {
            Data.Dispose();
        }
    }
}