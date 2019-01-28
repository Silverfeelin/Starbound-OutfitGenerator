namespace OutfitGenerator.Util
{
    public static class CommandGenerator
    {
        /// <summary>
        /// Generates a /spawnitem command from an item descriptor.
        /// </summary>
        /// <param name="descriptor">Item descriptor</param>
        /// <returns></returns>
        public static string SpawnItem(ItemDescriptor descriptor)
        {
            if (descriptor == null) return "/spawnitem perfectlygenericitem 1";

            string parameters = descriptor.Parameters.ToString(Newtonsoft.Json.Formatting.None);
            string escapedParameters = parameters.Replace("'", "\\'");
            string command = $"/spawnitem {descriptor.Name} {descriptor.Count} '{escapedParameters}'";
            return command;
        }
    }
}
