﻿// SampSharp
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    public class ClickTextDrawEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ClickTextDrawEventArgs class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="textDraw">The text draw.</param>
        public ClickTextDrawEventArgs(GtaPlayer player, TextDraw textDraw)
        {
            Player = player;
            TextDraw = textDraw;
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        public GtaPlayer Player { get; private set; }

        /// <summary>
        ///     Gets the text draw.
        /// </summary>
        public TextDraw TextDraw { get; private set; }
    }
    public class ClickPlayerTextDrawEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ClickPlayerTextDrawEventArgs class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="playerTextDraw">The player text draw.</param>
        public ClickPlayerTextDrawEventArgs(GtaPlayer player, PlayerTextDraw playerTextDraw)
        {
            Player = player;
            PlayerTextDraw = playerTextDraw;
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        public GtaPlayer Player { get; private set; }

        /// <summary>
        ///     Gets the text draw.
        /// </summary>
        public PlayerTextDraw PlayerTextDraw { get; private set; }
    }
}