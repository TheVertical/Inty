using System.Xml.Serialization;

namespace Inty.RussianBank
{
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