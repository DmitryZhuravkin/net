using System;

using DZzzz.Net.Core.Interfaces;
using DZzzz.Net.Serialization.Json;

namespace DZzzz.Net.Http
{
    internal class Serializers
    {
        private static readonly Lazy<ISerializer<string>> jsonLazy =
            new Lazy<ISerializer<string>>(() => new NewtonJsonSerializer());

        public static ISerializer<string> Json => jsonLazy.Value;
    }
}