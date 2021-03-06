namespace COMMO.OTB.Tests {
	using COMMO.OTB.Tests.OTBMTestWorlds;
	using NUnit.Framework;

	public sealed class OTBDeserializerTests {

		[Test]
		public void BuildTreeFromEmptyWorld() {
			var rawData = OTBMTestWorldsHelper.GetSerializedWorldData(OTBMTestWorldsHelper.Worlds.Empty);
			
			var tree = OTBDeserializer.DeserializeOTBData(
				serializedOTBData: rawData);

			Assert.IsTrue(tree.Children.Count == 1);
		}

		[Test]
		public void BuildTreeFromWorldWithDirtAndMexcalibur() {
			var rawData = OTBMTestWorldsHelper.GetSerializedWorldData(OTBMTestWorldsHelper.Worlds.DirtAndMexcalibur);
			
			var tree = OTBDeserializer.DeserializeOTBData(
				serializedOTBData: rawData);

			Assert.IsTrue(tree.Children.Count == 1);
		}

		[Test]
		public void BuildTreeFromWorldWithAutoGeneratedGrass() {
			var rawData = OTBMTestWorldsHelper.GetSerializedWorldData(OTBMTestWorldsHelper.Worlds.AutoPlacedGrass);
			
			var tree = OTBDeserializer.DeserializeOTBData(
				serializedOTBData: rawData);

			Assert.IsTrue(tree.Children.Count == 1);
		}
	}
}
