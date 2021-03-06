// <copyright file="LocationNotObstructedEventCondition.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

using COMMO.Scheduling.Contracts;
using COMMO.Server.Data.Interfaces;
using COMMO.Server.Data.Models.Structs;

namespace COMMO.Server.Movement.EventConditions
{
    /// <summary>
    /// Class that represents an event condition that evaluates whether a location is not obstructed.
    /// </summary>
    internal class LocationNotObstructedEventCondition : IEventCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationNotObstructedEventCondition"/> class.
        /// </summary>
        /// <param name="requestingCreatureId">The id of the creature requesting the event.</param>
        /// <param name="thing">The thing being moved in the event.</param>
        /// <param name="location">The location being checked.</param>
        public LocationNotObstructedEventCondition(uint requestingCreatureId, IThing thing, Location location)
        {
            RequestorId = requestingCreatureId;
            Thing = thing;
            Location = location;
        }

        /// <summary>
        /// Gets the location being checked.
        /// </summary>
        public Location Location { get; }

        /// <summary>
        /// Gets the <see cref="IThing"/> being moved.
        /// </summary>
        public IThing Thing { get; }

        /// <summary>
        /// Gets the id of the creature requesting the event.
        /// </summary>
        public uint RequestorId { get; }

        /// <inheritdoc/>
        public string ErrorMessage => "There is not enough room.";

        /// <inheritdoc/>
        public bool Evaluate()
        {
            var requestor = RequestorId == 0 ? null : Game.Instance.GetCreatureWithId(RequestorId);
            var destTile = Game.Instance.GetTileAt(Location);

            if (requestor == null || Thing == null || destTile == null)
            {
                // requestor being null means this was probably called from a script.
                // Not this policy's job to restrict 
                return true;
            }

            // creature trying to land on a blocking item.
            if (destTile.BlocksPass && Thing is ICreature)
            {
                return false;
            }

            var thingAsItem = Thing as IItem;

            if (thingAsItem != null)
            {
                if (destTile.BlocksLay || (thingAsItem.BlocksPass && destTile.BlocksPass))
                {
                    return false;
                }
            }

            return true;
        }
    }
}