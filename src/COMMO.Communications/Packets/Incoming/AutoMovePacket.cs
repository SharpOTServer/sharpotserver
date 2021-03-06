// <copyright file="AutoMovePacket.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.IO;
using COMMO.Data.Contracts;
using COMMO.Server.Data;
using COMMO.Server.Data.Interfaces;

namespace COMMO.Communications.Packets.Incoming
{
    /// <summary>
    /// Class that represents an auto movement packet.
    /// </summary>
    public class AutoMovePacket : IPacketIncoming, IAutoMoveInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMovePacket"/> class.
        /// </summary>
        /// <param name="message">The message to parse the packet from.</param>
        public AutoMovePacket(NetworkMessage message)
        {
            try
            {
                var numberOfMovements = message.GetByte();

				Directions = new Direction[numberOfMovements];

                for (var i = 0; i < numberOfMovements; i++)
                {
                    var dir = message.GetByte();
                    switch (dir)
                    {
                        case 1:
							Directions[i] = Direction.East;
                            break;
                        case 2:
							Directions[i] = Direction.NorthEast;
                            break;
                        case 3:
                            Directions[i] = Direction.North;
                            break;
                        case 4:
                            Directions[i] = Direction.NorthWest;
                            break;
                        case 5:
                            Directions[i] = Direction.West;
                            break;
                        case 6:
                            Directions[i] = Direction.SouthWest;
                            break;
                        case 7:
                            Directions[i] = Direction.South;
                            break;
                        case 8:
                            Directions[i] = Direction.SouthEast;
                            break;
                        default:
                            throw new InvalidDataException($"Invalid direction value {dir} on {nameof(AutoMovePacket)}.");
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: proper logging
                Console.WriteLine(ex.ToString());
            }
        }

        /// <inheritdoc/>
        public Direction[] Directions { get; }
    }
}
