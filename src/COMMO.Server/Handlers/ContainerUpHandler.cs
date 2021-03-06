// <copyright file="ContainerUpHandler.cs" company="2Dudes">
// Copyright (c) 2018 2Dudes. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

using COMMO.Communications;
using COMMO.Communications.Packets.Incoming;
using COMMO.Communications.Packets.Outgoing;
using COMMO.Server.Data;

namespace COMMO.Server.Handlers
{
    internal class ContainerUpHandler : IncomingPacketHandler
    {
        public override void HandleMessageContents(NetworkMessage message, Connection connection)
        {
            var packet = new ContainerUpPacket(message);
            var player = Game.Instance.GetCreatureWithId(connection.PlayerId) as Player;

            var currentContainer = player?.GetContainer(packet.ContainerId);

            if (currentContainer?.Parent == null)
            {
                return;
            }

            player?.OpenContainerAt(currentContainer.Parent, packet.ContainerId);

            ResponsePackets.Add(new ContainerOpenPacket
            {
                ContainerId = (byte)currentContainer.Parent.GetIdFor(connection.PlayerId),
                ClientItemId = currentContainer.Parent.ThingId,
                HasParent = currentContainer.Parent.Parent != null,
                Name = currentContainer.Parent.Type.Name,
                Volume = currentContainer.Parent.Volume,
                Contents = currentContainer.Parent.Content
            });
        }
    }
}