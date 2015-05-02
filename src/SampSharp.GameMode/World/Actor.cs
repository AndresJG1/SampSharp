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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a SA-MP actor.
    /// </summary>
    public class Actor : IdentifiedPool<Actor>, IIdentifiable, IWorldObject
    {
        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no player matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.InvalidActorId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Actor" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Actor(int id)
        {
            Id = id;
        }

        /// <summary>
        ///     Gets the facing angle of this <see cref="Actor" />.
        /// </summary>
        public float FacingAngle
        {
            get
            {
                float angle;

                AssertNotDisposed();

                Native.GetActorFacingAngle(Id, out angle);
                return angle;
            }
            set
            {
                AssertNotDisposed();
                Native.SetActorFacingAngle(Id, value);
            }
        }

        /// <summary>
        ///     Gets the health of this <see cref="Actor" />.
        /// </summary>
        public float Health
        {
            get
            {
                float health;

                AssertNotDisposed();

                Native.GetActorHealth(Id, out health);
                return health;
            }
            set
            {
                AssertNotDisposed();
                Native.SetActorHealth(Id, value);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="Actor" /> is invulnerable.
        /// </summary>
        public bool IsInvulnerable
        {
            get
            {
                AssertNotDisposed();
                return Native.IsActorInvulnerable(Id);
            }
            set
            {
                AssertNotDisposed();
                Native.SetActorInvulnerable(Id, value);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="Actor" /> is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                AssertNotDisposed();
                return Native.IsValidActor(Id);
            }
        }

        /// <summary>
        ///     Gets or sets the virtual world of this <see cref="Actor" />.
        /// </summary>
        public int VirtualWorld
        {
            get
            {
                AssertNotDisposed();
                return Native.GetActorVirtualWorld(Id);
            }
            set
            {
                AssertNotDisposed();
                Native.SetActorVirtualWorld(Id, value);
            }
        }

        /// <summary>
        ///     Gets the size of the actors pool.
        /// </summary>
        public static int PoolSize
        {
            get { return Native.GetActorPoolSize(); }
        }

        #region Implementation of IIdentifiable

        /// <summary>
        ///     Gets the Identity of this <see cref="Actor" />.
        /// </summary>
        public int Id { get; private set; }

        #endregion

        #region Implementation of IWorldObject

        /// <summary>
        ///     Gets the position of this <see cref="Actor" />.
        /// </summary>
        public Vector Position
        {
            get
            {
                float x, y, z;

                AssertNotDisposed();

                Native.GetActorPos(Id, out x, out y, out z);
                return new Vector(x, y, z);
            }
            set
            {
                AssertNotDisposed();
                Native.SetActorPos(Id, value.X, value.Y, value.Z);
            }
        }

        #endregion

        /// <summary>
        ///     Creates a new <see cref="Actor" />.
        /// </summary>
        /// <param name="modelid">The modelid.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns>The instance of the actor.</returns>
        public static Actor Create(int modelid, Vector position, float rotation)
        {
            var id = Native.CreateActor(modelid, position.X, position.Y, position.Z, rotation);

            return id == Misc.InvalidActorId ? null : new Actor(id);
        }

        /// <summary>
        ///     Determines whether this <see cref="Actor" /> is streamed in for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>True if streamed in; False otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public bool IsStreamedIn(GtaPlayer player)
        {
            if (player == null) throw new ArgumentNullException("player");

            AssertNotDisposed();

            return Native.IsActorStreamedIn(Id, player.Id);
        }

        /// <summary>
        ///     Applies the specified animation to this <see cref="Actor" />.
        /// </summary>
        /// <param name="library">The animation library from which to apply an animation.</param>
        /// <param name="name">The name of the animation to apply, within the specified library.</param>
        /// <param name="fDelta">The speed to play the animation.</param>
        /// <param name="loop">if set to <c>true</c> the animation will loop.</param>
        /// <param name="lockx">if set to <c>true</c> allow this Actor to move it's x-coordinate.</param>
        /// <param name="locky">if set to <c>true</c> allow this Actor to move it's y-coordinate.</param>
        /// <param name="freeze">if set to <c>true</c> freeze this Actor at the end of the animation.</param>
        /// <param name="time">The amount of time (in milliseconds) to play the animation.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     animlib
        ///     or
        ///     animname
        /// </exception>
        public void ApplyAnimation(string library, string name, float fDelta, bool loop, bool lockx, bool locky,
            bool freeze, int time)
        {
            if (library == null) throw new ArgumentNullException("animlib");
            if (name == null) throw new ArgumentNullException("animname");

            AssertNotDisposed();

            Native.ApplyActorAnimation(Id, library, name, fDelta, loop, lockx, locky, freeze, time);
        }

        /// <summary>
        ///     Clear any animations applied to this Actor.
        /// </summary>
        public void ClearAnimations()
        {
            AssertNotDisposed();

            Native.ClearActorAnimations(Id);
        }

        #region Overrides of Pool<GtaPlayer>

        /// <summary>
        ///     Removes this instance from the pool.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            Native.DestroyActor(Id);
            base.Dispose(disposing);
        }

        #endregion
    }
}