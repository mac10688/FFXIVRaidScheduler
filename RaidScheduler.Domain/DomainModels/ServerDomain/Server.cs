using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.ServerDomain
{
    public class Server
    {

        public IList<string> GetAllServers()
        {
            var result = new List<string>()
            {
                            "Aegis",
                            "Atomos",
                            "Carbuncle",
                            "Garuda",
                            "Gungnir",
                            "Kujata",
                            "Ramuh",
                            "Tonberry",
                            "Typhon",
                            "Unicorn",
                            "Alexander",
                            "Bahamut",
                            "Durandel",
                            "Fenrir",
                            "Ifrit",
                            "Ridill",
                            "Tiamat",
                            "Ultima",
                            "Valefor",
                            "Yojimbo",
                            "Zeromus",
                            "Anima",
                            "Asura",
                            "Belias",
                            "Chocobo",
                            "Hades",
                            "Ixion",
                            "Mandragora",
                            "Masamune",
                            "Pandaemonium",
                            "Shinryu",
                            "Titan",
                            "Adamantoise",
                            "Balmug",
                            "Cactuar",
                            "Coeurl",
                            "Faerie",
                            "Gilgamesh",
                            "Goblin",
                            "Jenova",
                            "Mateus",
                            "Midgardsormr",
                            "Sargatanas",
                            "Siren",
                            "Zalera",
                            "Behemoth",
                            "Brynhildr",
                            "Diabolos",
                            "Excalibur",
                            "Exodus",
                            "Famfrit",
                            "Hyperion",
                            "Lamia",
                            "Leviathan",
                            "Malboro",
                            "Ultros",
                            "Cerberus",
                            "Lich",
                            "Moogle",
                            "Odin",
                            "Phoenix",
                            "Ragnarok",
                            "Shiva",
                            "Zodiark"
            };

            return result;

        }

    }
}
