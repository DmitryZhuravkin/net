using System;

using DZzzz.Net.Serialization.Interfaces;
using DZzzz.Net.Serialization.Json;

namespace DZzzz.Net.Http
{
    internal class Serializers
    {
        private static readonly Lazy<ISerializer<string>> jsonLazy =
            new Lazy<ISerializer<string>>(() => new NewtonJsonSerializer());

        private static readonly Lazy<ISerializer<string>> xmlLazy =
            new Lazy<ISerializer<string>>(() => new Serialization.Xml.XmlSerializer());

        public static ISerializer<string> Json => jsonLazy.Value;

        public static ISerializer<string> Xml => xmlLazy.Value;
    }
}