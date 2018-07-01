namespace OutfitGenerator.Util
{
    public static class CommandGenerator
    {
        public static string SpawnItem(ItemDescriptor descriptor)
        {
            string parameters = descriptor.Parameters.ToString(Newtonsoft.Json.Formatting.None);
            string command = string.Format("/spawnitem {0} {1} '{2}'", descriptor.Name, descriptor.Count, parameters);
            return command;
        }
    }
}
