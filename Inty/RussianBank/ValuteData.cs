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
    public class ValuteCursOnDate
    {
        // Элемент Vname
        [XmlElement("Vname")]
        public string Vname { get; set; }

        // Элемент Vnom
        [XmlElement("Vnom")]
        public int Vnom { get; set; }

        // Элемент Vcurs
        [XmlElement("Vcurs")]
        public decimal Vcurs { get; set; }

        // Элемент Vcode
        [XmlElement("Vcode")]
        public int Vcode { get; set; }

        // Элемент VchCode
        [XmlElement("VchCode")]
        public string VchCode { get; set; }

        // Элемент VunitRate
        [XmlElement("VunitRate")]
        public decimal VunitRate { get; set; }
    }
}