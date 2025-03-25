using System.Collections.Generic;
using System.Xml.Serialization;

namespace Inty.RussianBank
{
    // Класс для корневого элемента ValuteData
    [XmlRoot("ValuteData")]
    public class ValuteData
    {
        // Атрибут OnDate
        [XmlAttribute("OnDate")]
        public string OnDate { get; set; }

        // Коллекция элементов ValuteCursOnDate
        [XmlElement("ValuteCursOnDate")]
        public List<ValuteCursOnDate> Valutes { get; set; }
    }

// Класс для элемента ValuteCursOnDate
}